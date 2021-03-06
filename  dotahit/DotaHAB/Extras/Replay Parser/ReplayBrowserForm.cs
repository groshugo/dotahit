using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExpTreeLib;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Deerchao.War3Share.W3gParser;
using DotaHIT;
using DotaHIT.Core.Resources;
using DotaHIT.Core;
using DotaHIT.DatabaseModel.Format;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.Jass.Native.Types;
using BitmapUtils;

namespace DotaHIT.Extras
{
    public partial class ReplayBrowserForm : Form
    {
        private ManualResetEvent Event1 = new ManualResetEvent(true);
        private bool showPlayerColors = false;
        private Replay currentReplay = null;
        private RichTextBox buffer = new RichTextBox();
        private Replay_Parser.ReplayFinder replayFinder = null;

        Dictionary<string, Replay> dcReplayCache = new Dictionary<string, Replay>();       

        public ReplayBrowserForm()
        {
            InitializeComponent();
            sentinelBottomLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            sentinelMiddleLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            sentinelTopLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            scourgeBottomLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            scourgeMiddleLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            scourgeTopLineUpTS.Renderer = UIRenderers.NoBorderRenderer;
            

            this.Icon = Properties.Resources.Icon;

            buffer.Font = sentinelRTB.Font;
            buffer.SelectionColor = sentinelRTB.SelectionColor;            
            
            PreviewReplay(null);

            Control.CheckForIllegalCrossThreadCalls = false;

            AttachPlugins();
        }

        public ReplayBrowserForm(string path)
            : this()
        {            
            browser.SelectedPath = path;

            this.Show();
        }

        public ReplayBrowserForm(string file, bool open)
            : this()
        {
            browser.SelectedPath = Path.GetDirectoryName(file);

            if (open)
            {
                if (ParseReplay(file, true))
                    DisplayReplay(currentReplay);
            }

            this.Show();
        }

        private void ReplayParserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveReplayExportConfig();            

            Current.mainForm.ReplayParserForms.Remove(this);

            // remove plugin controls from tabpages
            // to avoid them being disposed
            foreach (TabPage page in replayTabControl.TabPages)
                page.Controls.Clear();
        }

        void AttachPlugins()
        {            
            HabProperties hpsPlugins;
            if (Current.plugins.TryGetValue("ReplayParser", out hpsPlugins))            
                foreach(Plugins.IDotaHITPlugin plugin in hpsPlugins.Values)
                {
                    TabPage tbPlugin = new TabPage(plugin.Name);
                    replayTabControl.TabPages.Add(tbPlugin);

                    tbPlugin.Controls.Add(plugin.Panel);
                }
        }

        public Replay CurrentReplay
        {
            get
            {
                return currentReplay;
            }
        }

        private void browser_FileOk(object sender, CancelEventArgs e)
        {
            string file = browser.SelectedFile;

            if (string.IsNullOrEmpty(file)) return;

            if (Path.GetExtension(file).ToLower() == ".w3g")
            {
                e.Cancel = true; // suppress file execution

                if (ParseReplay(file, true))                
                    DisplayReplay(currentReplay);                
            }
        }

        private void browser_SelectedItemsChanged(object sender, FileBrowser.SelectedItemsChangedEventArgs e)
        {
            if (e.SelectedItems.Count > 0)
            {
                CShItem item = e.SelectedItems[0];

                if (item.IsFileSystem && !item.IsFolder)
                {
                    string ext = Path.GetExtension(item.Path);
                    if (ext == ".w3g")
                    {
                        //lv1.Cursor = Cursors.AppStarting;
                        PreviewReplay(item.Path);
                        //lv1.Cursor = Cursors.Default;
                        return;
                    }
                }
            }

            PreviewReplay(null);
        }

        private void browser_SelectedPathChanged(object sender, EventArgs e)
        {
            PreviewReplay(null);
        }

        private Color playerColorToColor(PlayerColor pc)
        {
            switch (pc)
            {
                case PlayerColor.Blue: return Color.Blue;
                case PlayerColor.Brown: return Color.FromArgb(153, 102, 102);
                case PlayerColor.Cyan: return Color.FromArgb(0, 200, 200);//Color.Cyan;
                case PlayerColor.DarkGreen: return Color.MediumSeaGreen;
                case PlayerColor.Gray: return Color.FromArgb(180, 180, 180);
                case PlayerColor.Green: return Color.Green;
                case PlayerColor.LightBlue: return Color.FromArgb(87, 188, 249);//Color.LightSkyBlue;
                case PlayerColor.Observer: return Color.White;
                case PlayerColor.Orange: return Color.FromArgb(255, 165, 0);
                case PlayerColor.Pink: return Color.FromArgb(255, 153, 204);//return Color.HotPink;
                case PlayerColor.Purple: return Color.FromArgb(153, 102, 204);
                case PlayerColor.Red: return Color.Red;
                case PlayerColor.Yellow: return Color.FromArgb(200, 200, 70);//Color.Yellow;
                default: return Color.Black;
            }
        }        

        private void FillPlayerList(RichTextBox rtb, List<Player> players)
        {            
            buffer.Clear();
            for (int i = 0; i < players.Count; i++)
            {                
                if (showPlayerColors)                
                    buffer.SelectionColor = playerColorToColor(players[i].Color);

                buffer.AppendText(players[i].Name);

                if (i + 1 < players.Count)
                {
                    buffer.SelectionColor = Color.Black;
                    buffer.AppendText(", ");
                }
            }

            rtb.Rtf = buffer.Rtf;
        }

        internal void DisplayReplayInfo(Replay replay)
        {
            currentReplay = replay;

            if (replay != null)
            {
                mapTextBox.Text = replay.Map.Path;
                hostTextBox.Text = replay.Host.Name;

                foreach (Team t in replay.Teams)
                    switch (t.Type)
                    {
                        case TeamType.Sentinel:
                            FillPlayerList(sentinelRTB, t.Players);
                            break;

                        case TeamType.Scourge:
                            FillPlayerList(scourgeRTB, t.Players);
                            break;
                    }

                string path = DHCFG.Items["Path"].GetStringValue("War3") + "\\" + mapTextBox.Text;
                if (File.Exists(path))
                {
                    DisplayUserInfo(Color.Green, "Ready", "for parsing",true);
                }
                else
                    DisplayUserInfo(Color.Red, "Map file", "not found", false, true);
            }
            else
            {
                mapTextBox.Text = "";
                hostTextBox.Text = "";
                sentinelRTB.Clear();
                scourgeRTB.Clear();
            }
        }

        internal void DisplayUserInfo(Color color, string strA, string strB, bool canParse)
        {
            DisplayUserInfo(color, strA, strB, canParse, canParse);
        }
        internal void DisplayUserInfo(Color color, string strA, string strB, bool canParse, bool canExtract)
        {
            userInfoPanel.BackColor = color;
            userInfoALabel.Text = strA;
            userInfoBLabel.Text = strB;
            parseB.Enabled = canParse;
            parseToolStripMenuItem.Enabled = canParse;
            extractToolStripMenuItem.Enabled = canExtract;
        }

        private void PreviewReplay(string filename)
        {
            if (filename != null)
            {
                if (!dcReplayCache.TryGetValue(filename, out currentReplay))
                {
                    try
                    {
                        currentReplay = new Replay(filename, true);
                    }
                    catch
                    {
                        currentReplay = null;
                        DisplayUserInfo(Color.Red, "The file", "is corrupt", false);
                    }
                    dcReplayCache[filename] = currentReplay;
                }

                DisplayReplayInfo(currentReplay);                            
            }
            else
            {
                currentReplay = null;
                DisplayReplayInfo(currentReplay);
                DisplayUserInfo(Color.FromArgb(33,136,235), "Select", "Replay File", false);
            }
        }      

        private void playerColorsLL_MouseDown(object sender, MouseEventArgs e)
        {
            showPlayerColors = !showPlayerColors;
            playerColorsLL.Text = "Player Colors: " + (showPlayerColors ? "On" : "Off");
            DisplayReplayInfo(currentReplay);
        }

        private void parseB_Click(object sender, EventArgs e)
        {
            if (ParseReplay(browser.SelectedFile, true))
                DisplayReplay(currentReplay);
        }

        internal bool ParseReplay(string filename, bool TryReparseOnError)
        {
            if (filename == null) return false;

            if (Current.player != null)
                Current.player.selection.Clear();

            DHCFG.Items["Path"]["ReplayLoad"] = Path.GetDirectoryName(filename);

            if (!dcReplayCache.TryGetValue(filename, out currentReplay) || currentReplay.IsPreview)
            {
                try
                {
                    currentReplay = new Replay(filename, MapRequired);
                    dcReplayCache[filename] = currentReplay;
                    if (Current.mainForm.cbForm.unitsForm != null)
                        Current.mainForm.cbForm.unitsForm.ReloadUnits();
                }
                catch (Exception ex)
                {
                    if (TryReparseOnError)
                        if (ParseReplay(filename, false))
                            return true;
                    // strange thing O_O
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.TargetSite);
                    MessageBox.Show("An error occured while parsing this replay");
                    currentReplay = null;
                    return false;
                }                
            }

            return true;            
        }

        internal void MapRequired(object sender, EventArgs e)
        {
            Replay rp = sender as Replay;
            string filename = DHCFG.Items["Path"].GetStringValue("War3") + "\\" + rp.Map.Path;

            // return if this map is already opened
            if (Current.map != null && string.Compare(Path.GetFileName(Current.map.Name), Path.GetFileName(filename), true) == 0)
                return;

            // turn off all features that increase map loading time
            Current.mainForm.MinimizeMapLoadTime();

            if (!File.Exists(filename))
            {
                DialogResult dr = MessageBox.Show("The map for this replay ('" + filename + "') was not found." +
                    "\nIf this map is located somewhere else, you can open it manually if you press 'Yes'." +
                    "\nNote that if you don't have the required map you can open other DotA map which version is the closest to required (to avoid bugs)." +
                    "\nPressing 'No' will not stop the parsing process, but the information on heroes and items will not be present (only player names and chatlog)." +
                    "\nDo you want to manually specify the map file?", "Map file was not found", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    Current.mainForm.openFileToolStripMenuItem_Click(null, EventArgs.Empty);
            }
            else
                Current.mainForm.LoadMap(filename, false, this);

            this.BringToFront();
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parseB.PerformClick();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replay_Parser.ReplayDataExtractForm extractDataForm = new DotaHIT.Extras.Replay_Parser.ReplayDataExtractForm(browser.SelectedFile, MapRequired);
            extractDataForm.ShowDialog();
        }

        private void replayFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (replayFinder != null && !replayFinder.IsDisposed)
                replayFinder.BringToFront();
            else
            {
                replayFinder = new DotaHIT.Extras.Replay_Parser.ReplayFinder(this, browser.SelectedPath);
                replayFinder.Show();
            }
        }

        private void includeNamesCB_CheckedChanged(object sender, EventArgs e)
        {
            namesCmbB.Enabled = includeNamesCB.Checked;
        }

        private void namesCmbB_SelectedIndexChanged(object sender, EventArgs e)
        {
            layoutCmbB_SelectedIndexChanged(null, EventArgs.Empty);
        }                                           
    }
}