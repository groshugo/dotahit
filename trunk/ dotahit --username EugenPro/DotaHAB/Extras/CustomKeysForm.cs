using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.DatabaseModel.Data;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.Core;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;
using DotaHIT.Jass.Native.Types;
using DotaHIT.Jass;
using DotaHIT.Core.Resources;
using DotaHIT.Jass.Types;
using System.IO;

namespace DotaHIT
{
    public partial class CustomKeysForm : Form
    {
        internal int mbX = -1;
        internal int mbY = -1;

        internal Dictionary<string, string> dcAbilitiesHotkeys = new Dictionary<string, string>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        internal Dictionary<string, List<KeyValuePair<string, AbilityAffinityType>>> dcAffinedAbilites = new Dictionary<string, List<KeyValuePair<string, AbilityAffinityType>>>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        MainForm mainOwner = null;
        DHMpqArchive relevantMap = null;

        internal enum AbilityAffinityType
        {
            None = 0,
            Clone = 1,
            Variation = 2
        }

        public CustomKeysForm()
        {
            InitializeComponent();
            colorDialog.Color = Color.DodgerBlue;
            hotkeyColorB.ForeColor = colorDialog.Color;            
        }

        public void SetParent(MainForm parentForm)
        {         
            this.Owner = parentForm;
            mainOwner = parentForm;
        }

        private void captionB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mbX = MousePosition.X - this.Location.X;
                mbY = MousePosition.Y - this.Location.Y;
            }
            else
                mbX = mbY = -1;
        }

        private void captionB_MouseMove(object sender, MouseEventArgs e)
        {
            if (mbX != -1 && mbY != -1)
                if ((MousePosition.X - mbX) != 0 && (MousePosition.Y - mbY) != 0)
                    this.SetDesktopLocation(MousePosition.X - mbX, MousePosition.Y - mbY);
        }

        private void captionB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mbX = mbY = -1;
        }     

        private void closeB_Click(object sender, EventArgs e)
        {            
            this.Hide();
        }

        private void CustomKeysForm_VisibleChanged(object sender, EventArgs e)
        {
            if (mainOwner != null)
                mainOwner.SetCustomKeysMode(this.Visible, hotkeyColorB.ForeColor);
            
            // make sure the data in the dictionary is correct for the currently loaded map
            if (this.Visible && (relevantMap != Current.map))
            {
                relevantMap = Current.map;
                dcAffinedAbilites = CollectAffinedAbilities();             
            }            
        }

        private void hotkeyColorB_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                hotkeyColorB.ForeColor = colorDialog.Color;
                mainOwner.SetCustomKeysMode(this.Visible, hotkeyColorB.ForeColor);
            }
        }

        internal Dictionary<string, List<KeyValuePair<string, AbilityAffinityType>>> CollectAffinedAbilities()
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            splashScreen.ShowText("Collecting affined abilities...");

            int progressCounter = 0;
            Dictionary<string, List<KeyValuePair<string, AbilityAffinityType>>> result = new Dictionary<string, List<KeyValuePair<string, AbilityAffinityType>>>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
            foreach (string heroID in DHLOOKUP.dcHeroesTaverns.Keys)
            {
                HabProperties hpsHeroAbilities = DHLOOKUP.hpcUnitAbilities[heroID];

                List<string> abilList = hpsHeroAbilities.GetStringListValue("heroAbilList");
                foreach (string abilID in abilList)
                    if (!result.ContainsKey(abilID))
                    {
                        HabProperties hpsAbilityProfile = DHLOOKUP.hpcAbilityProfiles[abilID];

                        string name = hpsAbilityProfile.GetStringValue("Name");
                        string hotkey = hpsAbilityProfile.GetStringValue("Hotkey");
                        if (hotkey == "") hotkey = hpsAbilityProfile.GetStringValue("Researchhotkey").Trim('"');
                        string buttonPos = hpsAbilityProfile.GetStringValue("Buttonpos");                       

                        List<KeyValuePair<string, AbilityAffinityType>> affinedAbilities = new List<KeyValuePair<string, AbilityAffinityType>>();
                        foreach (HabProperties hpsAbility in DHLOOKUP.hpcAbilityProfiles)
                            if (!DHLOOKUP.dcAbilitiesHeroes.ContainsKey(hpsAbility.name))
                            {                                
                                AbilityAffinityType affinityType = CheckAbilityAffinity(hpsAbility, name, hotkey, buttonPos);

                                if (affinityType != AbilityAffinityType.None)
                                    affinedAbilities.Add(new KeyValuePair<string, AbilityAffinityType>(hpsAbility.name, affinityType));
                            }

                        // save affined abilities for current ability
                        result.Add(abilID, affinedAbilities);
                    }

                splashScreen.ShowProgress((double)progressCounter++, (double)DHLOOKUP.dcHeroesTaverns.Keys.Count);
            }

            splashScreen.Close();            

            return result;
        }        
        AbilityAffinityType CheckAbilityAffinity(HabProperties hpsAbility, string name, string hotkey, string buttonPos)
        {
            object abilityName;
            if (hpsAbility.TryGetValue("Name", out abilityName) && string.Equals(abilityName as string, name, StringComparison.OrdinalIgnoreCase))
            {
                // used when one ability can be replaced with another same looking ability
                // which leads to invalid hotkeys for that ability (example: Carrion Swarm)
                if ((hpsAbility.GetStringValue("Hotkey") == hotkey )
                    && (hpsAbility.GetStringValue("Buttonpos") == buttonPos))
                    return AbilityAffinityType.Clone;
                else
                    // used when hero has multiple abilities with the same name,
                    // which are added by triggers (example: Shadowraze) 
                    // NOTE: must be later checked for user-defined hotkey existance to make sure 
                    return AbilityAffinityType.Variation;
            }
            else
                return AbilityAffinityType.None;
        }       

        internal void SaveCustomKeys(string filename)
        {
            FileStream file = File.Create(filename);

            using (StreamWriter sr = new StreamWriter(file))
            {
                foreach (string heroID in DHLOOKUP.dcHeroesTaverns.Keys)
                {
                    HabProperties hpsHeroProfile = DHLOOKUP.hpcUnitProfiles[heroID];
                    HabProperties hpsHeroAbilities = DHLOOKUP.hpcUnitAbilities[heroID];
                    List<string> abilList = hpsHeroAbilities.GetStringListValue("heroAbilList");

                    string output = "";
                    string name;
                    string hotkey;                    
                    foreach (string abilID in abilList)
                    {
                        name = DHLOOKUP.hpcAbilityProfiles.GetStringValue(abilID, "Name").Trim('"');

                        List<KeyValuePair<string, AbilityAffinityType>> affinedAbilities = dcAffinedAbilites[abilID];

                        // get user-defined hotkey for that ability
                        if (!dcAbilitiesHotkeys.TryGetValue(abilID, out hotkey))
                        {
                            // if hotkey was not found, check clones of that ability
                            foreach (KeyValuePair<string, AbilityAffinityType> kvp in affinedAbilities)
                                if (kvp.Value == AbilityAffinityType.Clone && dcAbilitiesHotkeys.TryGetValue(kvp.Key, out hotkey))
                                    break;
                        }

                        // if user-defined hotkey for this ability exists
                        if (hotkey != null)
                        {                            
                            // write hotkey for this ability
                            output += GetCustomKeyStringForAbility(abilID, name, hotkey);

                            // now check ability clones and write same hotkey for them
                            foreach(KeyValuePair<string, AbilityAffinityType> kvp in affinedAbilities)
                                if (kvp.Value == AbilityAffinityType.Clone)
                                    output += GetCustomKeyStringForAbility(kvp.Key, name, hotkey);
                        }
                        
                        // check for ability variations and write their own user-defined hotkey
                        foreach (KeyValuePair<string, AbilityAffinityType> kvp in affinedAbilities)
                            if (kvp.Value == AbilityAffinityType.Variation)
                                if (dcAbilitiesHotkeys.TryGetValue(kvp.Key, out hotkey))
                                    output += GetCustomKeyStringForAbility(kvp.Key, name, hotkey);
                    }
                        /*if (dcAbilitiesHotkeys.TryGetValue(abilID, out hotkey))
                        {
                            HabProperties hpsAbilityProfile = DHLOOKUP.hpcAbilityProfiles[abilID];                            

                            output += "\r\n// " + hpsAbilityProfile.GetStringValue("Name").Trim('"');
                            output += "\r\n[" + abilID + "]";
                            output += "\r\nHotkey=" + hotkey;
                            output += "\r\nResearchhotkey=" + hotkey;
                            output += "\r\nUnhotkey=" + hotkey;
                            output += "\r\n";

                            output += GetAbilityClones(hpsAbilityProfile, hotkey);
                            output += GetAbilityVariations(hpsAbilityProfile);
                        }*/

                    if (output.Length > 0)
                    {
                        sr.WriteLine("////////////");
                        sr.WriteLine("// " + hpsHeroProfile.GetStringValue("Name").Trim('"'));
                        sr.WriteLine("////////////");
                        sr.WriteLine(output);
                    }
                }
            }
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            SaveCustomKeys(saveFileDialog.FileName);
        }

        internal string GetCustomKeyStringForAbility(string codeID, string name, string hotkey)
        {
            string output = "";

            output += "\r\n// " + name;
            output += "\r\n[" + codeID + "]";
            output += "\r\nHotkey=" + hotkey;
            output += "\r\nResearchhotkey=" + hotkey;
            output += "\r\nUnhotkey=" + hotkey;
            output += "\r\n";

            return output;
        }
        
        internal string GetAbilityClones(HabProperties hpsOriginalAbility, string hotkey)
        {
            string name = hpsOriginalAbility.GetStringValue("Name");
            string art = hpsOriginalAbility.GetStringValue("Art");
            string orgHotkey = hpsOriginalAbility.GetStringValue("Hotkey");
            string buttonPos = hpsOriginalAbility.GetStringValue("Buttonpos");

            string output = "";

            foreach (HabProperties hpsAbility in DHLOOKUP.hpcAbilityProfiles)
                if ((hpsAbility.GetStringValue("Name") == name)
                    && (!DHLOOKUP.dcAbilitiesHeroes.ContainsKey(hpsAbility.name))
                    && (hpsAbility.GetStringValue("Hotkey") == orgHotkey)
                    && (hpsAbility.GetStringValue("Buttonpos") == buttonPos))
                {
                    output += "\r\n// " + name;
                    output += "\r\n[" + hpsAbility.name + "]";
                    output += "\r\nHotkey=" + hotkey;
                    output += "\r\nResearchhotkey=" + hotkey;
                    output += "\r\nUnhotkey=" + hotkey;
                    output += "\r\n";
                }

            return output;
        }


        // used when hero has multiple abilities with the same name,
        // which are added by triggers (example: Shadowraze)        
        internal string GetAbilityVariations(HabProperties hpsOriginalAbility)
        {
            string name = hpsOriginalAbility.GetStringValue("Name");
            string art = hpsOriginalAbility.GetStringValue("Art");            
            string hotkey;

            string output = "";

            foreach (HabProperties hpsAbility in DHLOOKUP.hpcAbilityProfiles)
                if ((hpsAbility.GetStringValue("Name") == name)
                    && (!DHLOOKUP.dcAbilitiesHeroes.ContainsKey(hpsAbility.name))
                    && (hpsOriginalAbility.name != hpsAbility.name)
                    && dcAbilitiesHotkeys.TryGetValue(hpsAbility.name, out hotkey))
                {
                    output += "\r\n// " + name;
                    output += "\r\n[" + hpsAbility.name + "]";
                    output += "\r\nHotkey=" + hotkey;
                    output += "\r\nResearchhotkey=" + hotkey;
                    output += "\r\nUnhotkey=" + hotkey;
                    output += "\r\n";
                }

            return output;
        }

        private void saveCustomKeysB_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "CustomKeys.txt";
            saveFileDialog.ShowDialog();
        }

        private void loadCKinfoB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use this to load existing custom keys into memory."
                        +"This can be useful when you want your new custom keys"
                        +" to contain most of the hotkeys that are described in an existing custom-keys file.");
        }

        private void saveCKinfoB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Generates new CustomKeys file,"
                +" based on the data specified by the user."
                +" This file will contain all the hotkeys that you have specified for each hero's skill.");
        }

        private void noteInfoB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Due to some specifics in DotA script, some of the original hero's abilities are changed as the game progresses to another same-looking abilities (clones)."
                + Environment.NewLine + "A good example can be 'Carrion Swarm'(Death Prophet) which is changed to one of it's clones as you level-up Witchcraft (that's how the cooldown/manacost is decreased)."
                + Environment.NewLine + "The only exception is perhaps 'Shadowraze'(Shadow Fiend) which has 3 different instances (not clones) and 4-th instance is used when in ability-research mode (red-plus)."
                + Environment.NewLine + "To handle all these cases correclty, DotaHIT makes sure that if the ability for which a hotkey is entered has clones, that hotkey will be saved for those clones as well."
                + Environment.NewLine + "This results in extra data (hotkeys for clones) written in the ouptut CustomKeys.txt file, which is required for your hotkeys to work properly all game long.");
        }

        private void loadCustomKeysB_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "CustomKeys.txt";
            openFileDialog.ShowDialog();
        }

        internal void LoadCustomKeys(string filename)
        {
            HabPropertiesCollection hpc = new HabPropertiesCollection();
            hpc.ReadFromFile(filename);

            string hotkey;
            foreach (HabProperties hps in hpc)
            {
                hotkey = hps.GetStringValue("Hotkey");
                if (!string.IsNullOrEmpty(hotkey))
                    dcAbilitiesHotkeys[hps.name] = hotkey;
            }

            if (Current.unit != null) Current.unit.Updated = true;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadCustomKeys(openFileDialog.FileName);         
        }

        private void openFromWar3B_Click(object sender, EventArgs e)
        {
            string path = DHCFG.Items["Path"].GetStringValue("War3");
            path = Path.GetFullPath(path) + "\\" + "CustomKeys.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("The specified Warcraft folder does not contain the CustomKeys.txt file");
                return;
            }

            LoadCustomKeys(path);
            MessageBox.Show("The CustomKeys.txt file was loaded successfully");
        }

        private void saveToWar3B_Click(object sender, EventArgs e)
        {
            string path = DHCFG.Items["Path"].GetStringValue("War3");
            path = Path.GetFullPath(path) + "\\" + "CustomKeys.txt";

            SaveCustomKeys(path);
            MessageBox.Show("The CustomKeys.txt file was saved successfully");
        }        
    }
}