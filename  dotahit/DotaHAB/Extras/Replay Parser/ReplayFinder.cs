using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.Core;
using DotaHIT.Core.Resources;
using DotaHIT.Jass.Native.Types;
using System.IO;
using System.Text.RegularExpressions;
using Deerchao.War3Share.W3gParser;

namespace DotaHIT.Extras.Replay_Parser
{
    public partial class ReplayFinder : Form
    {
        ReplayBrowserForm parser;
        List<unit> heroes = new List<unit>();
        Dictionary<string, string> heroNicksCache = new Dictionary<string, string>();

        public ReplayFinder(ReplayBrowserForm parser, string initialPath)
        {
            InitializeComponent();

            if (Current.map == null)
            {
                heroesGridView.Enabled = false;
                heroesGridView.BackgroundColor = Color.Gainsboro;
            }

            this.parser = parser;
            pathTextBox.Text = initialPath;
            columnHeader1.Width = resultsLV.View == View.Details ? resultsLV.Width - 24 : -1;
            
            unitsCheckTimer.Start();
        }

        private void unitsCheckTimer_Tick(object sender, EventArgs e)
        {
            if (Current.map != null && !heroesGridView.Enabled)
            {
                heroesGridView.Enabled = true;
                heroesGridView.BackgroundColor = Color.GhostWhite;
            }
            
            if (Current.player == null) return;

            List<unit> list = new List<unit>(Current.player.units.Count);

            foreach (unit u in Current.player.units.Values)
                if (u.IsHero)
                {
                    list.Add(u);

                    if (!heroNicksCache.ContainsKey(u.codeID))
                        heroNicksCache.Add(u.codeID, "");
                }

            foreach (unit u in heroes)
                if (u.codeID == "AnyHero")
                    list.Add(u);
                else
                    if (!list.Contains(u))
                        heroNicksCache.Remove(u.codeID); // clear unused hero-nicks from the cache

            heroes = list;
            heroesGridView.RowCount = heroes.Count;
        }

        private void heroesGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= heroes.Count) return;

            unit hero = heroes[e.RowIndex];
            if (hero.codeID == "AnyHero")
            {
                switch (e.ColumnIndex)
                {
                    case 0: // image
                        e.Value = null;
                        break;

                    case 1: // hero name
                        e.Value = "Any Hero (Nickname-only search)";
                        break;

                    case 2: // player
                        e.Value = hero.name;
                        break;
                }
            }
            else
            {
                HabProperties hpsHero = DHLOOKUP.hpcUnitProfiles[hero.codeID];
                switch (e.ColumnIndex)
                {
                    case 0: // image
                        e.Value = DHRC.GetImage(hpsHero.GetStringValue("Art"));
                        break;

                    case 1: // hero name
                        e.Value = hero.ID;
                        break;

                    case 2: // player
                        string nick;
                        heroNicksCache.TryGetValue(hero.codeID, out nick);
                        e.Value = nick + "";
                        break;
                }
            }
        }

        private void heroesGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                unit hero = heroes[e.RowIndex];
                if (hero.codeID == "AnyHero")
                    hero.name = e.Value + "";
                else
                    heroNicksCache[hero.codeID] = e.Value + "";
            }
        }

        private void heroesGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                switch (e.KeyCode)
                {
                    case Keys.A:
                        heroes.Add(new unit("AnyHero"));
                        heroesGridView.RowCount = heroes.Count;
                        break;

                    case Keys.D:
                        if (heroesGridView.SelectedRows.Count > 0)
                        {
                            unit hero = heroes[heroesGridView.SelectedRows[0].Index];
                            heroes.Remove(hero);

                            if (hero.codeID != "AnyHero")
                                heroNicksCache.Remove(hero.codeID);
                            
                            hero.destroy();
                            heroesGridView.RowCount = heroes.Count;
                        }
                        break;
                }
        }

        private void startB_Click(object sender, EventArgs e)
        {
            List<string> nicknames = new List<string>();

            foreach (unit hero in heroes)
                if (hero.codeID == "AnyHero" && !string.IsNullOrEmpty(hero.name))
                    nicknames.Add(hero.name);
            
            resultsLV.Items.Clear();

            if (!Directory.Exists(pathTextBox.Text))
            {
                MessageBox.Show("Specified path does not exists");
                return;
            }
            string[] files = Directory.GetFiles(pathTextBox.Text, "*.w3g", subfoldersCB.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            startB.Enabled = false;
            resultsLV.BackColor = Color.WhiteSmoke;
            resultsLV.BeginUpdate();

            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            splashScreen.ShowText("Searching...");            

            int index = 0;
            Replay replay;
            foreach (string file in files)
            {
                index++;
                try
                {
                    replay = new Replay(file);
                    if (IsReplayMatchesCriteria(replay, nicknames))
                    {
                        ListViewItem lvi = new ListViewItem(file);
                        lvi.Tag = replay;

                        resultsLV.Items.Add(lvi);
                    }
                }
                catch { }

                splashScreen.ShowProgress((double)index, (double)files.Length);
            }

            splashScreen.Close();

            resultsLV.EndUpdate();
            resultsLV.BackColor = Color.AliceBlue;
            startB.Enabled = true;
        }

        bool IsReplayMatchesCriteria(Replay replay, List<string> nicknames)
        {
            // all specified nicknames must be present in the replay
            bool Ok;
            foreach(string nickname in nicknames)
            {
                Ok = false;
                string regexNickname = string.IsNullOrEmpty(nickname) ? ".*" : nickname.Replace("*", ".*");

                foreach (Player p in replay.Players)
                    if (Regex.IsMatch(p.Name, regexNickname, RegexOptions.IgnoreCase))
                    {
                        Ok = true;
                        break;
                    }
                
                if (!Ok) return false;
            }
            
            // all specified hero-nickname pairs must be present in the replay
            foreach (KeyValuePair<string,string> kvp in heroNicksCache)
            {
                Ok = false;
                string regexNickname = string.IsNullOrEmpty(kvp.Value) ? ".*" : kvp.Value.Replace("*", ".*");

                foreach (Player p in replay.Players)
                {
                    Hero hero = p.GetMostUsedHero();
                    if (hero != null && kvp.Key == hero.Name && Regex.IsMatch(p.Name, regexNickname, RegexOptions.IgnoreCase))
                    {
                        Ok = true;
                        break;
                    }
                }

                if (!Ok) return false;
            }

            return true;
        }

        private void resultsLV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && resultsLV.SelectedItems.Count > 0)
                if (parser.ParseReplay(resultsLV.SelectedItems[0].Text, true))
                    parser.DisplayReplay(parser.CurrentReplay);
        }

        private void extractDataB_Click(object sender, EventArgs e)
        {
            if (!chatlogCB.Checked && !killLogCB.Checked && !statisticsCB.Checked)
                return;

            string filename;
            string directory = pathTextBox.Text;
            string commonName = "Found Replays";
            string output = "The following files:" + Environment.NewLine + Environment.NewLine;

            if (chatlogCB.Checked)
            {
                filename = commonName + " Chatlog.txt";

                List<string> lines = new List<string>();
                foreach (ListViewItem lvi in resultsLV.Items)
                {
                    Replay replay = lvi.Tag as Replay;
                    lines.Add(@"\\");
                    lines.Add(@"\\ " + lvi.Text);
                    lines.Add(@"\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");                    
                    lines.AddRange(Replay_Parser.ReplayDataExtractForm.ChatsToLines(replay.Chats));
                    lines.Add("");
                }

                File.WriteAllLines(directory + "\\" + filename, lines.ToArray(), Encoding.UTF8);

                output += filename + Environment.NewLine;
            }

            if (killLogCB.Checked)
            {
                filename = commonName + " KillLog.txt";

                List<string> lines = new List<string>();
                foreach (ListViewItem lvi in resultsLV.Items)
                {
                    Replay replay = lvi.Tag as Replay;
                    lines.Add(@"\\");
                    lines.Add(@"\\ " + lvi.Text);
                    lines.Add(@"\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
                    lines.AddRange(Replay_Parser.ReplayDataExtractForm.KillsToLines(replay.Kills));
                    lines.Add("");
                }                

                File.WriteAllLines(directory + "\\" + filename, lines.ToArray(), Encoding.UTF8);

                output += filename + Environment.NewLine;
            }

            if (statisticsCB.Checked)
            {
                filename = commonName + " Stats.txt";

                List<string> lines = new List<string>();
                foreach (ListViewItem lvi in resultsLV.Items)
                {
                    Replay replay = lvi.Tag as Replay;
                    lines.Add(@"\\");
                    lines.Add(@"\\ " + lvi.Text);
                    lines.Add(@"\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
                    lines.AddRange(Replay_Parser.ReplayDataExtractForm.PlayerStatsToLines(replay.Players));
                    lines.Add("");
                }                

                File.WriteAllLines(directory + "\\" + filename, lines.ToArray(), Encoding.UTF8);

                output += filename + Environment.NewLine;
            }

            output += Environment.NewLine + "are saved to '" + directory + "'";
            MessageBox.Show(output, "Replay Data Extraction Complete");
        }
    }
}