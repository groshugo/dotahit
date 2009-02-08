using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExpTreeLib;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Deerchao.War3Share.W3gParser;
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
        public static readonly string ReplayExportCfgFileName = Application.StartupPath + "\\"+ "dhrexport.cfg";

        private bool openBuildsInForms = false;
        private string originalExport = string.Empty;
        Dictionary<string,string> dcUsedHeroTags = null;
        private int currentExportLayout = -1;

        HabPropertiesCollection hpcExportCfg = null;        

        public Color playerColorToColorChat(PlayerColor pc)
        {
            switch (pc)
            {
                case PlayerColor.Blue: return Color.FromArgb(30, 130, 255);
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
                case PlayerColor.Yellow: return Color.FromArgb(255, 255, 102);//Color.Yellow;
                default: return Color.Black;
            }
        }
        public Color playerColorToColorBG(PlayerColor pc)
        {
            switch (pc)
            {
                case PlayerColor.Red: return Color.Red;
                case PlayerColor.Blue: return Color.FromArgb(0, 8, 160);
                case PlayerColor.Cyan: return Color.FromArgb(0, 164, 120);
                case PlayerColor.Purple: return Color.FromArgb(112, 8, 160);
                case PlayerColor.Yellow: return Color.FromArgb(160, 164, 0);//Color.Yellow;
                case PlayerColor.Orange: return Color.FromArgb(160, 112, 0);
                case PlayerColor.Green: return Color.Green;
                case PlayerColor.Pink: return Color.FromArgb(152, 84, 136);
                case PlayerColor.Gray: return Color.FromArgb(112, 120, 112);
                case PlayerColor.LightBlue: return Color.FromArgb(112, 140, 160);//Color.LightSkyBlue;                
                case PlayerColor.DarkGreen: return Color.FromArgb(40, 108, 72);
                case PlayerColor.Brown: return Color.FromArgb(80, 56, 32);
                case PlayerColor.Observer: return Color.White;
                default: return Color.Black;
            }
        }        

        public void DisplayReplay(Replay replay)
        {
            this.Text = "DotA H.I.T. Replay Browser/Parser: " + Path.GetFileName(replay.FileName);
            tabControl.SelectedTab = parseTabPage;

            DHRC.pfResetCount();
            DHRC.pfStartCount();

            ClearPreviousData();

            PrepareMapImage(); DHRC.pfPrintRefreshCount("PrepareMapImage");
            ParseLineups(replay); DHRC.pfPrintRefreshCount("ParseLineups");
            DisplayTeams(replay); DHRC.pfPrintRefreshCount("DisplayTeams");
            PlacePlayersOnTheMap(); DHRC.pfPrintRefreshCount("PlacePlayersOnTheMap");            
            DisplayDescription(replay); DHRC.pfPrintRefreshCount("DisplayDescription");
            DisplayChat(replay); DHRC.pfPrintRefreshCount("DisplayChat");
            DisplayStatistics(replay); DHRC.pfPrintRefreshCount("DisplayStatistics");
            DisplayKillLog(replay); DHRC.pfPrintRefreshCount("DisplayKillLog");

            InitPlugins();

            DHRC.pfEndCount();
            DHRC.pfResetCount();
        }

        void closeReplayB_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = browseTabPage;
            this.Text = "DotA H.I.T. Replay Browser/Parser";
        }
       
        protected void ClearPreviousData()
        {
            for (int i = 0; i < replayTabControl.TabPages.Count; i++)
                if (replayTabControl.TabPages[i].Tag is Player)
                {
                    replayTabControl.TabPages.RemoveAt(i);
                    i--;
                }

            PrepareReplayExport();            
        }

        protected void PrepareMapImage()
        {
            if (Current.map == null)
            {
                mapPanel.Visible = false;
                return;
            }
            else
                mapPanel.Visible = true;

            if (mapPanel.BackgroundImage != null) return;            

            Bitmap bmp = (Bitmap)(Image)DHRC.GetImage("war3mapMap.blp").Clone();            
            BitmapFilter.Gamma(bmp, 2.5, 2.5, 2.5);
            mapPanel.BackgroundImage = bmp;
        }

        protected void ParseLineups(Replay rp)
        {
            if (Current.map == null) return;

            player sentinel = player.players[0];
            player scourge = player.players[6];

            unit sentinelTopTower = null;
            unit sentinelMidTower = null;
            unit sentinelBotTower = null;

            unit scourgeTopTower = null;
            unit scourgeMidTower = null;
            unit scourgeBotTower = null;

            // Sentinel Towers (Treant Protectors):
            // 
            // e00R - level1
            // e011 - level2
            // e00S - level3
            // e019 - level4             

            List<unit> sentinelLevel1Towers = new List<unit>(3);
            List<unit> sentinelLevel3Towers = new List<unit>(3);
            foreach (unit u in sentinel.units.Values)
                switch (u.codeID.Text)
                {
                    case "e00R": sentinelLevel1Towers.Add(u);
                        break;

                    case "e00S": sentinelLevel3Towers.Add(u);
                        break;
                }

            sentinelTopTower = GetTowerForLineUp(sentinelLevel1Towers, LineUp.Top, true);
            sentinelMidTower = GetTowerForLineUp(sentinelLevel1Towers, LineUp.Middle, true);
            sentinelBotTower = GetTowerForLineUp(sentinelLevel1Towers, LineUp.Bottom, true);

            // Scourge Towers (Spirit Towers):
            //
            // u00M - level1
            // u00D - level2
            // u00N - level3
            // u00T - level4

            List<unit> scourgeLevel1Towers = new List<unit>(3);
            List<unit> scourgeLevel3Towers = new List<unit>(3);
            foreach (unit u in scourge.units.Values)
                switch (u.codeID.Text)
                {
                    case "u00M": scourgeLevel1Towers.Add(u);
                        break;

                    case "u00N": scourgeLevel3Towers.Add(u);
                        break;
                }

            scourgeTopTower = GetTowerForLineUp(scourgeLevel1Towers, LineUp.Top, false);
            scourgeMidTower = GetTowerForLineUp(scourgeLevel1Towers, LineUp.Middle, false);
            scourgeBotTower = GetTowerForLineUp(scourgeLevel1Towers, LineUp.Bottom, false);

            // create rectangles that will be used to determine the player's lineup

            rect bottomRect = new rect(
                Math.Min(sentinelBotTower.x, scourgeBotTower.x), sentinelBotTower.y,
                Jass.Native.Constants.map.maxX, scourgeBotTower.y);

            // middle rectange will be calculated 
            // as the rectange with Width = distance between middle towers
            // and Height = 990 (approximate value that should work fine for middle rectangle)
            // 990 ~== square_root(700^2 + 700^2), so X offset = 700 and Y offset = 700
            rect middleRect = new rect(
                sentinelMidTower.x - 700, sentinelMidTower.y,
                scourgeMidTower.x, scourgeMidTower.y + 700);

            rect topRect = new rect(
                Jass.Native.Constants.map.minX, sentinelTopTower.y,
                Math.Max(scourgeTopTower.x, sentinelTopTower.x), scourgeTopTower.y);

            // create rectangles that will be used to determine the base area for each team
            // rectangle's vertices calculation will be based on coordinates of level3 towers

            rect sentinelBase = GetBaseRectFromTowers(sentinelLevel3Towers, true);
            rect scourgeBase = GetBaseRectFromTowers(scourgeLevel3Towers, false);

            setLineUpsForPlayers(rp, topRect, middleRect, bottomRect, sentinelBase, scourgeBase);
        }
        void setLineUpsForPlayers(Replay rp, rect top, rect mid, rect bot, rect sentinelBase, rect scourgeBase)
        {
            // approximate creep spawn time
            int creepSpawnTime = GetFirstCreepAttackTime(rp, top, mid, bot);

            foreach (Team team in rp.Teams)
                switch (team.Type)
                {
                    case TeamType.Sentinel:
                    case TeamType.Scourge:
                        List<int> alliedHeroes = GetListOfAlliedHeroes(team);
                        foreach (Player p in team.Players)
                        {
                            location l = GetAverageLocation(p, (team.Type == TeamType.Sentinel ? sentinelBase : scourgeBase), alliedHeroes, creepSpawnTime);

                            p.LineUpLocation = l;

                            if (top.ContainsXY(l.x, l.y))
                                p.LineUp = LineUp.Top;
                            else
                                if (mid.ContainsXY(l.x, l.y))
                                    p.LineUp = LineUp.Middle;
                                else
                                    if (bot.ContainsXY(l.x, l.y))
                                        p.LineUp = LineUp.Bottom;
                                    else
                                        p.LineUp = LineUp.JungleOrRoaming;
                        }
                        break;
                }
        }
        
        unit GetTowerForLineUp(List<unit> towers, LineUp lineUp, bool isSentinel)
        {
            unit selected = null;

            switch (lineUp)
            {
                case LineUp.Top:
                    if (isSentinel)
                    {
                        // top-most for sentinel
                        foreach (unit tower in towers)
                            if (selected == null || selected.y < tower.y)
                                selected = tower;
                    }
                    else// left-most for scourge
                        foreach (unit tower in towers)
                            if (selected == null || selected.x > tower.x)
                                selected = tower;
                    break;

                case LineUp.Middle:
                    // calculating hypotenuse as the measure 
                    // of the nearest tower to map center
                    foreach (unit tower in towers)
                        if (selected == null ||
                            ((selected.y * selected.y) + (selected.x * selected.x) > (tower.y * tower.y) + (tower.x * tower.x)))
                            selected = tower;
                    break;

                case LineUp.Bottom:
                    if (isSentinel)
                    {
                        // right-most for sentinel
                        foreach (unit tower in towers)
                            if (selected == null || selected.x < tower.x)
                                selected = tower;
                    }
                    else// bottom-most for scourge
                        foreach (unit tower in towers)
                            if (selected == null || selected.y > tower.y)
                                selected = tower;
                    break;
            }

            return selected;
        }
        rect GetBaseRectFromTowers(List<unit> towers, bool isSentinel)
        {
            double maxx;
            double maxy;

            if (isSentinel)
            {
                maxx = Jass.Native.Constants.map.minX;
                maxy = Jass.Native.Constants.map.minY;
                foreach (unit tower in towers)
                {
                    if (maxx < tower.x)
                        maxx = tower.x;

                    if (maxy < tower.y)
                        maxy = tower.y;
                }

                return new rect(
                    Jass.Native.Constants.map.minX, Jass.Native.Constants.map.minY,
                    maxx, maxy);
            }
            else
            {
                maxx = Jass.Native.Constants.map.maxX;
                maxy = Jass.Native.Constants.map.maxY;
                foreach (unit tower in towers)
                {
                    if (maxx > tower.x)
                        maxx = tower.x;

                    if (maxy > tower.y)
                        maxy = tower.y;
                }

                return new rect(
                    maxx, maxy,
                    Jass.Native.Constants.map.maxX, Jass.Native.Constants.map.maxY);
            }
        }
        List<int> GetListOfAlliedHeroes(Team team)
        {
            List<int> heroes = new List<int>(team.Players.Count);

            foreach (Player p in team.Players)
            {
                Hero h = p.GetMostUsedHero();
                if (h != null) heroes.Add(h.ObjectId);
            }

            return heroes;
        }
        int GetFirstCreepAttackTime(Replay rp, rect top, rect mid, rect bot)
        {
            List<string> dcCreeps = new List<string>();

            // Sentinel creeps:
            //
            // esen - Treant
            // edry - Druid of the Talon

            dcCreeps.Add("esen");
            dcCreeps.Add("edry");

            // Scourge creeps:
            // 
            // unec - Necromancer
            // ugho - Ghoul

            dcCreeps.Add("unec");
            dcCreeps.Add("ugho");

            int minCreepAttackTime = int.MaxValue;
            
            foreach (Player p in rp.Players)                    
                foreach (PlayerAction pa in p.Actions)
                    if (pa.IsValidObjects)
                    {
                        string codeID1;                        
                        rp.dcObjectsCodeIDs.TryGetValue(pa.Object1, out codeID1);

                        if (codeID1 == null || dcCreeps.Contains(codeID1))
                        {
                            if (top.ContainsXY(pa.X, pa.Y) || mid.ContainsXY(pa.X, pa.Y) || bot.ContainsXY(pa.X, pa.Y))
                            {
                                minCreepAttackTime = Math.Min(minCreepAttackTime, pa.Time);
                                break; // go to next player
                            }
                        }                       
                    }
            
            return (minCreepAttackTime != int.MaxValue) ? minCreepAttackTime : 180000; // 180000 = 3 minutes (will be used in case of errors)
        }
        location GetAverageLocation(Player player, rect baseArea, List<int> alliedHeroes, int creepSpawnTime)
        {
            double x = 0;
            double y = 0;
            int count = 0;

            foreach (PlayerAction pa in player.Actions)
            {
                // if creeps were not spawned yet, then skip this action
                if (pa.Time < creepSpawnTime) continue;

                // if it's > than 3 minutes since creep spawn then stop
                if (pa.Time > creepSpawnTime + 180000)
                    break;                

                if (pa.IsValidObjects && !alliedHeroes.Contains(pa.Object1))
                {
                    // skip this action if it was performed on the base area
                    if (baseArea.ContainsXY(pa.X, pa.Y, 800))
                        continue;

                    if (x == 0 && y == 0)
                    {
                        x = pa.X;
                        y = pa.Y;
                    }
                    else
                    {
                        x += pa.X;
                        y += pa.Y;
                    }

                    count++;
                    if (count > 50) break;
                }
            }

            x /= count;
            y /= count;

            return new location(x, y);
        }        

        protected void DisplayTeams(Replay replay)
        {
            sentinelTeamToolStrip.Items.Clear();
            scourgeTeamToolStrip.Items.Clear();

            foreach (Team t in replay.Teams)
                switch (t.Type)
                {
                    case TeamType.Sentinel:
                        displayTeam(sentinelTeamToolStrip, t);
                        break;

                    case TeamType.Scourge:
                        displayTeam(scourgeTeamToolStrip, t);
                        break;
                }

            versusLabel.Height = Math.Min(sentinelTeamToolStrip.PreferredSize.Height, scourgeTeamToolStrip.PreferredSize.Height);
        }
        void displayTeam(ToolStrip ts, Team team)
        {
            if (Current.map == null)
            {
                displayPlayersByLineUp(ts, team, LineUp.Unknown);
                return;
            }

            displayPlayersByLineUp(ts, team, LineUp.Top);
            displayPlayersByLineUp(ts, team, LineUp.Middle);
            displayPlayersByLineUp(ts, team, LineUp.Bottom);
            displayPlayersByLineUp(ts, team, LineUp.JungleOrRoaming);            
        }
        void displayPlayersByLineUp(ToolStrip ts, Team team, LineUp lineUp)
        {
            bool separate = (ts.Items.Count != 0 && !(ts.Items[ts.Items.Count - 1] is ToolStripSeparator));

            foreach (Player p in team.Players)
                if (p.LineUp == lineUp)
                {
                    if (separate)
                    {
                        ToolStripSeparator tss = new ToolStripSeparator();
                        tss.Margin = new Padding(0, 5, 0, 5);
                        ts.Items.Add(tss);
                        separate = false;
                    }

                    ToolStripButton tsb = new ToolStripButton();
                    tsb.Text = p.Name;
                    switch (p.LineUp)
                    {
                        case LineUp.Top:
                        case LineUp.Middle:
                        case LineUp.Bottom:
                            tsb.Text += " (" + p.LineUp.ToString() + ")";
                            break;
                        case LineUp.JungleOrRoaming:
                            tsb.Text += " (" + "Jungle/Roaming" + ")";
                            break;
                    }
                    tsb.Font = UIFonts.tahoma9_75Bold;
                    tsb.BackColor = playerColorToColorBG(p.Color);
                    tsb.ForeColor = Color.White;
                    tsb.AutoToolTip = false;
                    tsb.Click += new EventHandler(playerToolStripButton_Click);
                    tsb.MouseDown += new MouseEventHandler(playerToolStripButton_MouseDown);                    
                    tsb.Tag = p;

                    if (p.Heroes.Count != 0)
                    {
                        string imagePath = DHLOOKUP.hpcUnitProfiles[p.GetMostUsedHero().Name, "Art"] as string;
                        tsb.Image = (imagePath != null) ? DHRC.GetImage(imagePath) : Properties.Resources.armor;
                    }
                    else
                        tsb.Image = Properties.Resources.armor;

                    tsb.ImageAlign = ContentAlignment.MiddleLeft;

                    ts.Items.Add(tsb);
                }
        }

        protected void PlacePlayersOnTheMap()
        {
            mapPanel.Visible = false;
            mapPanel.SuspendLayout();            

            sentinelTopLineUpTS.SuspendLayout();
            sentinelMiddleLineUpTS.SuspendLayout();
            sentinelBottomLineUpTS.SuspendLayout();

            scourgeTopLineUpTS.SuspendLayout();
            scourgeMiddleLineUpTS.SuspendLayout();
            scourgeBottomLineUpTS.SuspendLayout();
            
            sentinelTopLineUpTS.Items.Clear();
            sentinelMiddleLineUpTS.Items.Clear();
            sentinelBottomLineUpTS.Items.Clear();

            scourgeTopLineUpTS.Items.Clear();
            scourgeMiddleLineUpTS.Items.Clear();
            scourgeBottomLineUpTS.Items.Clear();            

            for(int i=0; i< mapPanel.Controls.Count; i++)
                if (string.IsNullOrEmpty(mapPanel.Controls[i].Name))
                {
                    mapPanel.Controls.RemoveAt(i);
                    i--;
                }            

            foreach (ToolStripItem tsi in sentinelTeamToolStrip.Items)
                if (tsi.Image != null)
                    placePlayerOnTheMap(tsi.Tag as Player, tsi.Image);

            foreach (ToolStripItem tsi in scourgeTeamToolStrip.Items)
                if (tsi.Image != null)
                    placePlayerOnTheMap(tsi.Tag as Player, tsi.Image);

            sentinelTopLineUpTS.ResumeLayout();
            sentinelMiddleLineUpTS.ResumeLayout();
            sentinelBottomLineUpTS.ResumeLayout();

            scourgeTopLineUpTS.ResumeLayout();
            scourgeMiddleLineUpTS.ResumeLayout();
            scourgeBottomLineUpTS.ResumeLayout();

            mapPanel.ResumeLayout();
            mapPanel.Visible = true;            
        }
        void placePlayerOnTheMap(Player player, Image img)
        {
            if (player.LineUp == LineUp.JungleOrRoaming)
            {
                double realX = player.LineUpLocation.x - Jass.Native.Constants.map.minX;
                double realY = Jass.Native.Constants.map.maxY - player.LineUpLocation.y;
                double mapWidth = Jass.Native.Constants.map.maxX - Jass.Native.Constants.map.minX;
                double mapHeight = Jass.Native.Constants.map.maxY - Jass.Native.Constants.map.minY;

                double scaledX = ((double)mapPanel.Width) * (realX / mapWidth);
                double scaledY = ((double)mapPanel.Height) * (realY / mapHeight);

                ToolStrip ts = new ToolStrip();
                ts.BackColor = Color.Transparent;
                ts.GripStyle = ToolStripGripStyle.Hidden;
                ts.ImageScalingSize = sentinelBottomLineUpTS.ImageScalingSize;
                ts.Renderer = UIRenderers.NoBorderRenderer;
                ts.CanOverflow = false;
                ts.Dock = DockStyle.None;
                ts.LayoutStyle = ToolStripLayoutStyle.Flow;
                ts.Padding = new Padding(0);

                ToolStripButton tsb = new ToolStripButton();
                tsb.Image = img;
                tsb.ToolTipText = player.Name;
                tsb.Margin = new Padding(0, 0, 0, 0);
                tsb.Padding = new Padding(1, 1, 0, 0);
                tsb.BackColor = player.TeamType == TeamType.Sentinel ? Color.Red : Color.Lime;
                tsb.Tag = player;
                tsb.Click += new EventHandler(playerToolStripButton_Click);
                tsb.MouseDown += new MouseEventHandler(playerToolStripButton_MouseDown);

                ts.Items.Add(tsb);

                mapPanel.Controls.Add(ts);

                ts.Left = (int)scaledX - (tsb.Width / 2);
                ts.Top = (int)scaledY - (tsb.Height / 2);
            }
            else
            {
                switch (player.LineUp)
                {
                    case LineUp.Top:
                        addPlayerToMapLineUp(player, img,
                            (player.TeamType == TeamType.Sentinel) ? sentinelTopLineUpTS : scourgeTopLineUpTS,
                            false,
                            (player.TeamType == TeamType.Scourge));
                        break;

                    case LineUp.Middle:
                        addPlayerToMapLineUp(player, img,
                            (player.TeamType == TeamType.Sentinel) ? sentinelMiddleLineUpTS : scourgeMiddleLineUpTS,
                            true,
                            (player.TeamType == TeamType.Scourge));
                        break;

                    case LineUp.Bottom:
                        addPlayerToMapLineUp(player, img,
                            (player.TeamType == TeamType.Sentinel) ? sentinelBottomLineUpTS : scourgeBottomLineUpTS,
                            false,
                            (player.TeamType == TeamType.Scourge));
                        break;
                }
            }            
        }
        void addPlayerToMapLineUp(Player p, Image img, ToolStrip ts, bool useAlternativeLayout, bool downToUpDirection)
        {
            ToolStripButton tsb = new ToolStripButton();
            tsb.Image = img;
            tsb.ToolTipText = p.Name;
            tsb.Margin = new Padding(1, 0, 0, 0);
            tsb.Padding = new Padding(1, 1, 0, 0);
            tsb.BackColor = p.TeamType == TeamType.Sentinel ? Color.Red : Color.Lime;
            tsb.Tag = p;
            tsb.Click += new EventHandler(playerToolStripButton_Click);
            tsb.MouseDown += new MouseEventHandler(playerToolStripButton_MouseDown);

            if (useAlternativeLayout)
            {
                if (downToUpDirection)
                {
                    switch (ts.Items.Count)
                    {
                        case 0:
                            ts.Items.AddRange(new ToolStripItem[]{
                                GetLabel(tsb, true),
                                GetLabel(tsb, true),
                                GetLabel(tsb, false, 4),
                                tsb,
                            });
                            break;

                        case 4:
                            if (ts.Items[0].Image == null)
                            {
                                ts.Items.RemoveAt(0);
                                ts.Items.Insert(0, tsb);
                                break;
                            }
                            else if (ts.Items[1].Image == null)
                            {
                                ts.Items.RemoveAt(1);
                                ts.Items.Insert(1, tsb);
                                break;
                            }
                            else
                                if (ts.Items[2] is ToolStripLabel)
                                {
                                    ts.Items.RemoveAt(2);
                                    ts.Items.Insert(2, tsb);
                                    break;
                                }

                            ts.Items.Add(tsb);
                            break;
                    }
                }
                else
                    switch (ts.Items.Count)
                    {
                        case 0:
                            ts.Items.AddRange(new ToolStripItem[]{                                
                                GetLabel(tsb, true),                                
                                tsb
                            });
                            break;

                        case 2:
                            ts.Items.RemoveAt(0);
                            ts.Items.Insert(0, GetLabel(tsb, false));

                            ts.Items.AddRange(new ToolStripItem[]{                                
                                GetLabel(tsb, true),                                
                                tsb
                            });
                            break;

                        case 4:
                            if (ts.Items[2].Image == null)
                                ts.Items.RemoveAt(2);
                            else if (ts.Items[0].Image == null)
                                ts.Items.RemoveAt(0);
                            ts.Items.Add(tsb);
                            break;
                    }
            }
            else
                if (downToUpDirection)
                {
                    switch (ts.Items.Count)
                    {
                        case 0:
                            ts.Items.AddRange(new ToolStripItem[]{
                                GetLabel(tsb, true),
                                GetLabel(tsb, true),
                                GetLabel(tsb, false, 4),
                                tsb,
                            });
                            break;

                        case 4:
                            if (ts.Items[2].Image == null)
                            {
                                ts.Items.RemoveAt(2);
                                ts.Items.Add(tsb);
                                break;
                            }
                            else if (ts.Items[1].Image == null)
                            {
                                ts.Items.RemoveAt(0);
                                ts.Items.Insert(0, GetLabel(tsb, false, 4));

                                ts.Items.RemoveAt(1);
                                ts.Items.Insert(1, tsb);
                                break;
                            }
                            else
                                if (ts.Items[0].Image == null)
                                {
                                    ts.Items.RemoveAt(0);
                                    ts.Items.Insert(0, tsb);
                                    break;
                                }

                            ts.Items.Add(tsb);
                            break;
                    }
                }
                else
                    switch (ts.Items.Count)
                    {
                        case 0:
                            ts.Items.AddRange(new ToolStripItem[]{
                                GetLabel(tsb, false, 4),                                
                                tsb
                            });
                            break;

                        case 2:
                            if (ts.Items[0] is ToolStripLabel)
                                ts.Items.RemoveAt(0);
                            else
                                ts.Items.Insert(0, GetLabel(tsb, false, 4));

                            ts.Items.Add(tsb);
                            break;

                        case 4:
                            if (ts.Items[0] is ToolStripLabel)
                                ts.Items.RemoveAt(0);

                            ts.Items.Add(tsb);
                            break;
                    }

        }

        ToolStripLabel GetLabel(ToolStripButton tsb, bool buttonWidth)
        {
            return GetLabel(tsb, buttonWidth, 0);
        }
        ToolStripLabel GetLabel(ToolStripButton tsb, bool buttonWidth, int extraSize)
        {
            ToolStripLabel tsl = new ToolStripLabel();
            tsl.AutoSize = false;
            tsl.Height = tsb.Height;
            tsl.Width = (buttonWidth) ? tsb.Width : (10 + extraSize);
            tsl.Margin = new Padding(1, 0, 0, 0);
            return tsl;
        }
        
        void playerToolStripButton_Click(object sender, EventArgs e)
        {          
            Player p = (sender as ToolStripItem).Tag as Player;
            if (p == null) return;

            displayHeroBuildOrder(p);           
        }
        void playerToolStripButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                playerContextMenuStrip.Tag = sender;
                playerContextMenuStrip.Show(MousePosition);
            }            
        }        

        private void bringHeroIconToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player p = (playerContextMenuStrip.Tag as ToolStripItem).Tag as Player;
            if (p == null) return;

            foreach (ToolStrip ts in mapPanel.Controls)
            {
                foreach (ToolStripItem tsi in ts.Items)
                    if (tsi.Tag == p)
                    {
                        ts.BringToFront();
                        return;
                    }
            }
        }

        private void copyPlayerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player p = (playerContextMenuStrip.Tag as ToolStripItem).Tag as Player;
            if (p == null) return;

            Clipboard.SetText(p.Name);
        }

        private void copyHeroNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player p = (playerContextMenuStrip.Tag as ToolStripItem).Tag as Player;
            if (p == null) return;

            Clipboard.SetText(DHFormatter.ToString(DHLOOKUP.hpcUnitProfiles[p.GetMostUsedHero().Name, "Name"]));
        }

        void openBuildsInFormLL_MouseDown(object sender, MouseEventArgs e)
        {
            openBuildsInForms = !openBuildsInForms;            
            openBuildsInFormLL.Text = "Open build orders in new window: " + (openBuildsInForms ? "On" : "Off");
        }
        void displayHeroBuildOrder(Player p)
        {
            if (!openBuildsInForms)
            {
                foreach (TabPage page in replayTabControl.TabPages)
                    if (page.Tag == p)
                    {
                        replayTabControl.SelectedTab = page;
                        return;
                    }

                replayTabControl.SuspendLayout();

                TabPage tp = new TabPage();
                HeroBuildView hbv = new HeroBuildView(p);
                tp.Controls.Add(hbv);
                tp.Text = p.Name;
                tp.Tag = p;
                tp.ToolTipText = p.Name + "'s Hero Build Order (Righ-Click to close)";

                hbv.Dock = DockStyle.Fill;

                replayTabControl.TabPages.Add(tp);
                replayTabControl.SelectedTab = tp;

                replayTabControl.ResumeLayout(false);
            }
            else
            {
                Form buildForm = new Form();
                buildForm.StartPosition = FormStartPosition.CenterScreen;

                buildForm.SuspendLayout();

                HeroBuildView hbv = new HeroBuildView(p);

                buildForm.Controls.Add(hbv);
                buildForm.Text = p.Name + " - " + hbv.heroNameLabel.Text;
                buildForm.Tag = p;

                hbv.Dock = DockStyle.Fill;

                buildForm.Icon = Icon.FromHandle(((Bitmap)hbv.heroImagePanel.BackgroundImage).GetHicon());
                buildForm.ClientSize = hbv.PreferredSize;

                buildForm.ResumeLayout(false);

                buildForm.Show();
            }
        }

        void replayTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            // check if the right mouse button was pressed
            if (e.Button == MouseButtons.Right)
            {
                // iterate through all the tab pages
                for (int i = 0; i < replayTabControl.TabCount; i++)
                {
                    // get their rectangle area and check if it contains the mouse cursor
                    Rectangle r = replayTabControl.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        if (replayTabControl.TabPages[i].Tag is Player)
                            replayTabControl.TabPages.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        protected void DisplayDescription(Replay rp)
        {
            // replay version
            infoLV.Items[0].SubItems[1].Text = "1." + rp.Version;
            // map name
            infoLV.Items[1].SubItems[1].Text = Path.GetFileName(rp.Map.Path);
            // map location
            infoLV.Items[2].SubItems[1].Text = Path.GetDirectoryName(rp.Map.Path);
            // host name
            infoLV.Items[3].SubItems[1].Text = rp.Host.Name;
            // game mode
            infoLV.Items[4].SubItems[1].Text = rp.GameMode;
            // game length
            infoLV.Items[5].SubItems[1].Text = DHFormatter.ToString(rp.GameLength);
            // sentinel players
            infoLV.Items[6].SubItems[1].Text = rp.GetTeamByType(TeamType.Sentinel).Players.Count.ToString();
            // scourge players
            infoLV.Items[7].SubItems[1].Text = rp.GetTeamByType(TeamType.Scourge).Players.Count.ToString();
            // winner
            string winner = "Winner info not found";
            infoLV.Items[8].SubItems[1].ForeColor = Color.Black;
            foreach (Team t in rp.Teams)
                if (t.IsWinner)
                {
                    winner = t.Type.ToString();
                    infoLV.Items[8].SubItems[1].ForeColor = (t.Type == TeamType.Sentinel) ? Color.Red : Color.Green;
                    break;
                }
            infoLV.Items[8].SubItems[1].Text = winner;
        }

        protected void DisplayChat(Replay replay)
        {            
            chatlogRTB.Clear();
            UIRichTextEx.Default.ClearText();

            bool isPaused = false;
            foreach (ChatInfo ci in replay.Chats)
                if (ci.To != TalkTo.System)
                {
                    UIRichTextEx.Default.AddText(DHFormatter.ToString(ci.Time) + " ", UIFonts.boldArial8, isPaused ? Color.Silver : UIColors.toolTipData);

                    switch (ci.To)
                    {
                        case TalkTo.Allies:
                        case TalkTo.All:
                        case TalkTo.Observers:
                            UIRichTextEx.Default.AddText("[" + ci.To.ToString() + "] ", Color.White);
                            break;
                        default:
                            continue;
                    }

                    UIRichTextEx.Default.AddText(ci.From.Name + ": ", playerColorToColorChat(ci.From.Color));
                    UIRichTextEx.Default.AddText(ci.Message, Color.White);
                    UIRichTextEx.Default.AddNewLine();
                }
                else
                {
                    switch (ci.Message)
                    {
                        case "pause":
                            if (!isPaused)
                            {
                                isPaused = true;
                                UIRichTextEx.Default.AddText(DHFormatter.ToString(ci.Time) + " ", UIFonts.boldArial8, Color.White);
                                UIRichTextEx.Default.AddText(ci.From.Name + " ", playerColorToColorChat(ci.From.Color));
                                UIRichTextEx.Default.AddText("paused the game.", Color.White);
                                UIRichTextEx.Default.AddNewLine();
                            }
                            break;

                        case "resume":
                            if (isPaused)
                            {
                                isPaused = false;
                                UIRichTextEx.Default.AddText(DHFormatter.ToString(ci.Time) + " ", UIFonts.boldArial8, Color.White);
                                UIRichTextEx.Default.AddText(ci.From.Name + " ", playerColorToColorChat(ci.From.Color));
                                UIRichTextEx.Default.AddText("has resumed the game.", Color.White);
                                UIRichTextEx.Default.AddNewLine();
                            }
                            break;

                        case "save":
                            UIRichTextEx.Default.AddText(DHFormatter.ToString(ci.Time) + " ", UIFonts.boldArial8, Color.LightGreen);
                            UIRichTextEx.Default.AddText("Game was saved by ", Color.White);
                            UIRichTextEx.Default.AddText(ci.From.Name, playerColorToColorChat(ci.From.Color));                            
                            UIRichTextEx.Default.AddNewLine();
                            break;

                        case "leave":
                            UIRichTextEx.Default.AddText(DHFormatter.ToString(ci.Time) + " ", UIFonts.boldArial8, Color.White);
                            UIRichTextEx.Default.AddText(ci.From.Name + " ", playerColorToColorChat(ci.From.Color));
                            UIRichTextEx.Default.AddText("has left the game.", Color.White);
                            UIRichTextEx.Default.AddNewLine();
                            break;
                    }
                }

            chatlogRTB.Rtf = UIRichTextEx.Default.CloseRtf();
        }

        protected void DisplayStatistics(Replay rp)
        {
            statisticsLV.Items.Clear();
            statisticsLV.BeginUpdate();

            foreach (Player p in rp.Players)
                if (!p.IsComputer && !p.IsObserver)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (p.SlotNo + 1) + "";
                    lvi.BackColor = playerColorToColorBG(p.Color);
                    lvi.UseItemStyleForSubItems = false;
                    lvi.Tag = p;

                    Color bgColor = p.TeamType == TeamType.Sentinel ? Color.FromArgb(255,250,240) : Color.FromArgb(245, 255, 240);

                    ListViewItem.ListViewSubItem lvi_Player = new ListViewItem.ListViewSubItem();
                    lvi_Player.Text = p.Name;
                    lvi_Player.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_Hero = new ListViewItem.ListViewSubItem();
                    if (p.Heroes.Count != 0)
                        lvi_Hero.Text = DHFormatter.ToString(p.GetMostUsedHeroClass());
                    lvi_Hero.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_APM = new ListViewItem.ListViewSubItem();
                    lvi_APM.Text = "" + (int)p.Apm;
                    lvi_APM.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_Kills = new ListViewItem.ListViewSubItem();
                    lvi_Kills.Text = p.gameCacheValues.ContainsKey("kills") ? p.getGCValue("kills").ToString() : p.getGCVStringValue("1", "");
                    lvi_Kills.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_Deaths = new ListViewItem.ListViewSubItem();
                    lvi_Deaths.Text = p.gameCacheValues.ContainsKey("deaths") ? p.getGCValue("deaths").ToString() : p.getGCVStringValue("2", "");
                    lvi_Deaths.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_creepKD = new ListViewItem.ListViewSubItem();
                    lvi_creepKD.Text = p.gameCacheValues.ContainsKey("creeps") ? (p.getGCValue("creeps") + " / " + p.getGCValue("denies")) : "";
                    if (lvi_creepKD.Text.Length == 0) lvi_creepKD.Text = p.gameCacheValues.ContainsKey("3") ? (p.getGCValue("3") + " / " + p.getGCValue("4")) : "";
                    lvi_creepKD.BackColor = bgColor;

                    ListViewItem.ListViewSubItem lvi_wards = new ListViewItem.ListViewSubItem();

                    int[] ward_stats = getWardStatistics(p);

                    if (ward_stats[0]!= 0)
                        lvi_wards.Text = twoChars(ward_stats[0]) + " { "+ ward_stats[1] + " + " + ward_stats[2] + " }";

                    lvi_wards.Tag = ward_stats;
                    lvi_wards.BackColor = bgColor;

                    lvi.SubItems.AddRange(new ListViewItem.ListViewSubItem[]{
                        lvi_Player,
                        lvi_Hero,
                        lvi_APM,
                        lvi_Kills,
                        lvi_Deaths,
                        lvi_creepKD,
                        lvi_wards
                    });

                    statisticsLV.Items.Add(lvi);
                }

            statisticsLV.EndUpdate();
        }
        protected int[] getWardStatistics(Player p)
        {
            if (Current.map == null) return new int[] { 0 };
            string result = string.Empty;

            int total = 0;
            int observer = 0;
            int sentry = 0;

            // get proper observer ward codeID
            string obsCodeID;            
            if (DHLOOKUP.hpcItemProfiles.GetStringValue("sor7", "Name").Contains("Observer"))
                obsCodeID = "sor7";
            else
                obsCodeID = "I05G";

            // get proper sentry ward codeID
            string sentryCodeID;
            if (DHLOOKUP.hpcItemProfiles.GetStringValue("tgrh", "Name").Contains("Sentry"))
                sentryCodeID = "tgrh";
            else
                sentryCodeID = "I05H";

            int observerUses = DHFormatter.ToInt(DHLOOKUP.hpcItemData[obsCodeID, "uses"]);
            int sentryUses = DHFormatter.ToInt(DHLOOKUP.hpcItemData[sentryCodeID, "uses"]);

            foreach (OrderItem item in p.Items.BuildOrders)
            {
                switch (item.Name)
                {
                    // observer ward
                    case "sor7":
                    case "h02C": // new version obs ward
                        total += observerUses;
                        observer += observerUses;
                        break;

                    case "tgrh":
                    case "h02D": // new version sentry ward
                        total += sentryUses;
                        sentry += sentryUses;
                        break;
                }
            }

            return new int[3] { total, observer, sentry };
        }
        protected string twoChars(int value)
        {
            string result = value + "";
            if (result.Length == 1) return " " + result;
            else
                return result;
        }
        void statisticsLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer 
            // object. Setting this property immediately sorts the 
            // ListView using the ListViewItemComparer object.
            this.statisticsLV.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                switch (col)
                {
                    case 0:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)x).Tag as Player).SlotNo, (((ListViewItem)y).Tag as Player).SlotNo);
                    case 1:
                    case 2:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                    case 3:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).Tag as Player).Apm, (((ListViewItem)x).Tag as Player).Apm);
                    case 4:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).Tag as Player).getGCValue("1"), (((ListViewItem)x).Tag as Player).getGCValue("1"));
                    case 5:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).Tag as Player).getGCValue("2"), (((ListViewItem)x).Tag as Player).getGCValue("2"));
                    case 6:
                        int result = CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).Tag as Player).getGCValue("3"), (((ListViewItem)x).Tag as Player).getGCValue("3"));
                        if (result == 0)
                            return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).Tag as Player).getGCValue("4"), (((ListViewItem)x).Tag as Player).getGCValue("4"));
                        else
                            return result;
                    case 7:
                        return CaseInsensitiveComparer.DefaultInvariant.Compare((((ListViewItem)y).SubItems[7].Tag as int[])[0], (((ListViewItem)x).SubItems[7].Tag as int[])[0]);
                    default:
                        return 0;
                }
            }
        }

        protected void DisplayKillLog(Replay rp)
        {
            TimeSpan lastKillTime = TimeSpan.MinValue;

            killLogRTB.Clear();
            UIRichTextEx.Default.ClearText();

            if (rp.Kills.Count == 0)
                UIRichTextEx.Default.AddText("Not available for this replay", Color.White);
            else
                foreach (KillInfo ki in rp.Kills)
                {
                    UIRichTextEx.Default.AddText(DHFormatter.ToString(ki.Time) + "  ", UIFonts.boldArial8, (ki.Time.TotalSeconds - lastKillTime.TotalSeconds < 8) ? Color.Cyan : Color.LightSkyBlue);                    

                    if (ki.Killer != null)
                    {
                        UIRichTextEx.Default.AddText(ki.Killer.Name, playerColorToColorChat(ki.Killer.Color));
                        UIRichTextEx.Default.AddText(" ("+ ki.Killer.GetMostUsedHeroClass() + ")", Color.White);
                    }
                    else
                        UIRichTextEx.Default.AddText("Creeps", Color.White);
                    UIRichTextEx.Default.AddText("  killed  ", Color.White);
                    UIRichTextEx.Default.AddText(ki.Victim.Name, playerColorToColorChat(ki.Victim.Color));
                    UIRichTextEx.Default.AddText(" (" + ki.Victim.GetMostUsedHeroClass() + ")", Color.White);
                    UIRichTextEx.Default.AddNewLine();

                    lastKillTime = ki.Time;
                }

            killLogRTB.Rtf = UIRichTextEx.Default.CloseRtf();            
        }      

        void InitPlugins()
        {
            object[] parameters = new object[2] { currentReplay, hpcExportCfg };

            HabProperties hpsPlugins;
            if (Current.plugins.TryGetValue("ReplayParser", out hpsPlugins))
                foreach (Plugins.IDotaHITPlugin plugin in hpsPlugins.Values)
                    plugin.Tag = parameters;
        }

        private void PrepareReplayExport()
        {
            if (hpcExportCfg == null)
            {
                hpcExportCfg = new HabPropertiesCollection();
                hpcExportCfg.ReadFromFile(ReplayExportCfgFileName);
            }

            HabProperties hpsExportUI;
            if (!hpcExportCfg.TryGetValue("UI", out hpsExportUI))
            {
                hpsExportUI = new HabProperties();
                hpcExportCfg.Add("UI", hpsExportUI);
            }

            originalExport = string.Empty;

            exportPreviewB.Checked = false;

            int layout = hpsExportUI.GetIntValue("Layout", -1);

            if (currentExportLayout == layout) 
                ShowReplayExport(layout);
            else 
                layoutCmbB.SelectedIndex = layout;

            replayExportRTB.Font = new Font(
                hpsExportUI.GetStringValue("FontFamily", "Verdana"),
                (float)hpsExportUI.GetDoubleValue("FontSize", 10));

            fontTextBox.Text = replayExportRTB.Font.FontFamily.Name + " " + replayExportRTB.Font.Size;

            shortLaneNamesCB.Checked = hpsExportUI.GetIntValue("ShortLaneNames", 0) == 1;
            exportIconWidthNumUD.Value = (decimal)hpsExportUI.GetIntValue("ImageWidth", 18);
        }
        private void SaveReplayExportConfig()
        {
            if (hpcExportCfg != null && hpcExportCfg.Count != 0)
            {
                hpcExportCfg["UI"]["FontFamily"] = replayExportRTB.Font.FontFamily.Name;
                hpcExportCfg["UI"]["FontSize"] = (double)replayExportRTB.Font.Size;
                hpcExportCfg["UI"]["ShortLaneNames"] = shortLaneNamesCB.Checked ? 1 : 0;
                hpcExportCfg["UI"]["ImageWidth"] = (int)exportIconWidthNumUD.Value;

                DHFormatter.RefreshFormatForCurrentThread();

                hpcExportCfg.SaveToFile(ReplayExportCfgFileName);
            }
        }

        private void layoutCmbB_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ShowReplayExport(layoutCmbB.SelectedIndex);

            if (exportPreviewB.Checked)
                exportPreviewB_CheckedChanged(null, EventArgs.Empty);
        }

        void ShowReplayExport(int layoutType)
        {
            if (hpcExportCfg["HeroTags"] == null)
                hpcExportCfg["HeroTags"] = new HabProperties();
            currentExportLayout = layoutType;

            Team t1;
            Team t2;

            dcUsedHeroTags = new Dictionary<string, string>();
 
            UIRichText.Default.ClearText();
            UIRichText.Default.buffer.Font = replayExportRTB.Font;
            UIRichText.Default.buffer.SelectionFont = replayExportRTB.Font;

            switch (layoutType)
            {
                case 0:
                    UIRichText.Default.AddText("The Sentinel\n");
                    t1 = currentReplay.GetTeamByType(TeamType.Sentinel);
                    appendLineUpToExport(t1, LineUp.Top, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t1, LineUp.Middle, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t1, LineUp.Bottom, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t1, LineUp.JungleOrRoaming, true); UIRichText.Default.AddText("\n");
                    UIRichText.Default.AddText("The Scourge\n");
                    t2 = currentReplay.GetTeamByType(TeamType.Scourge);
                    appendLineUpToExport(t2, LineUp.Top, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t2, LineUp.Middle, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t2, LineUp.Bottom, true); UIRichText.Default.AddText("\n");
                    appendLineUpToExport(t2, LineUp.JungleOrRoaming, true); UIRichText.Default.AddText("\n");                    
                    break;

                case 1:
                    t1 = currentReplay.GetTeamByType(TeamType.Sentinel);

                    UIRichText.Default.AddText(getLaneName(LineUp.Top) + ":  "); appendLineUpToExport(t1, LineUp.Top, false); UIRichText.Default.AddText("\n");
                    UIRichText.Default.AddText(getLaneName(LineUp.Middle) + ":  "); appendLineUpToExport(t1, LineUp.Middle, false); UIRichText.Default.AddText("\n");
                    UIRichText.Default.AddText(getLaneName(LineUp.Bottom) + ":  "); appendLineUpToExport(t1, LineUp.Bottom, false); UIRichText.Default.AddText("\n");
                    UIRichText.Default.AddText(getLaneName(LineUp.JungleOrRoaming) + ":  "); appendLineUpToExport(t1, LineUp.JungleOrRoaming, false);

                    rightAlignRtfLines(UIRichText.Default.buffer);

                    t2 = currentReplay.GetTeamByType(TeamType.Scourge);

                    string[] lines = UIRichText.Default.buffer.Lines;
                    lines[0] += "   vs   " + getHorzLineUp(t2, LineUp.Top);
                    lines[1] += "   vs   " + getHorzLineUp(t2, LineUp.Middle);
                    lines[2] += "   vs   " + getHorzLineUp(t2, LineUp.Bottom);
                    lines[3] += "        " + getHorzLineUp(t2, LineUp.JungleOrRoaming);

                    UIRichText.Default.buffer.Lines = lines;
                    break;             

                case -1:
                    UIRichText.Default.AddText("Select layout first");
                    break;
            }

            UIRichText.Default.buffer.SelectAll();
            UIRichText.Default.buffer.SelectionFont = replayExportRTB.Font;
            replayExportRTB.Rtf = UIRichText.Default.buffer.Rtf;  

            originalExport = replayExportRTB.Rtf;

            UIRichText.Default.ClearText();
        }

        string getHeroNames(string heroID, int index)
        {
            switch (index)
            {
                case 0:
                    int length = hpcExportCfg["HeroTags"].GetStringValue(heroID).Split(';').Length;
                    if (length < 2)
                        return DHLOOKUP.hpcUnitProfiles[heroID].GetStringValue("Propernames").Split(',')[0];
                    else
                        return hpcExportCfg["HeroTags"].GetStringValue(heroID).Split(';')[1];
                case 1:
                    int len = hpcExportCfg["HeroTags"].GetStringValue(heroID).Split(';').Length;
                    if (len < 3)
                        return DHLOOKUP.hpcUnitProfiles[heroID].GetStringValue("Name").Trim('"');
                    else
                        return hpcExportCfg["HeroTags"].GetStringValue(heroID).Split(';')[2];
                case 2:
                    return getHeroNames(heroID, 0) + " " + getHeroNames(heroID, 1);
                case 3:
                    return getHeroNames(heroID, 0) + " The " + getHeroNames(heroID, 1);

            }
            return string.Empty;
        }

        string getHeroTag(string heroID)
        {
            string tag = null;
//            if (hpcExportCfg["HeroTags"]!=null)
            tag = hpcExportCfg["HeroTags"].GetStringValue(heroID).Split(';')[0];

            return (string.IsNullOrEmpty(tag)) ? ":" + heroID + ":" : tag;
        }
        string getLaneName(LineUp lane)
        {
            switch (lane)
            {
                case LineUp.Top:
                    return lane.ToString();

                case LineUp.Middle:
                    return shortLaneNamesCB.Checked ? "Mid" : lane.ToString();

                case LineUp.Bottom:
                    return shortLaneNamesCB.Checked ? "Bot" : lane.ToString();

                case LineUp.JungleOrRoaming:
                    return shortLaneNamesCB.Checked ? "Jungle" : "Jungle/Roaming";

                default:
                    return string.Empty;
            }
        }
        void rightAlignRtfLines(RichTextBox rtb)
        {
            int maxLine = -1;
            float maxWidth = 0;
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < rtb.Lines.Length; i++)
            {
                float width = (float)UIRichText.MeasureString(g, rtb.Lines[i], rtb.Font).Width;
                width += getExtraWidthForImages(g, rtb.Font, rtb.Lines[i]);

                if (maxWidth < width)
                {
                    maxWidth = width;
                    maxLine = i;
                }
            }
            float wsWidth = (float)UIRichText.MeasureString(g, " ", rtb.Font).Width;
            for (int i = 0; i < rtb.Lines.Length; i++)
            {
                float width = (float)UIRichText.MeasureString(g, rtb.Lines[i], rtb.Font).Width;
                width += getExtraWidthForImages(g, rtb.Font, rtb.Lines[i]);

                //Console.WriteLine("line: "+i+ "="+ width);

                if (width < maxWidth)
                {
                    float countF = (maxWidth - width) / wsWidth;
                    int count = (int)((maxWidth - width) / wsWidth);
                    string newLine = rtb.Lines[i].PadRight(rtb.Lines[i].Length + count, ' ');
                    string[] lines = rtb.Lines;
                    lines[i] = newLine;
                    rtb.Lines = lines;
                }
            }
        }
        float getExtraWidthForImages(Graphics g, Font font, string line)
        {
            float width = 0;
            float imageWidth = (float)exportIconWidthNumUD.Value;

            foreach (string tag in dcUsedHeroTags.Keys)
            {
                int index;
                while ((index = line.IndexOf(tag)) != -1)
                {
                    width += imageWidth - (float)UIRichText.MeasureString(g, tag, font).Width;
                    line = line.Remove(index, tag.Length);
                }
            }

            return width;
        }
        void appendLineUpToExport(Team t, LineUp lineUp, bool vertical)
        {
            int count = 0;
            foreach (Player p in t.Players)
                if (p.LineUp == lineUp)
                {
                    count++;
                    if (count > 1)
                    {
                        if (vertical)
                            UIRichText.Default.AddText("\n");
                        else
                            UIRichText.Default.AddText(" + ");
                    }
                    
                    Hero h = p.GetMostUsedHero();

                    string tag = getHeroTag(h.Name);

                    dcUsedHeroTags[tag] = h.Name;

                    UIRichText.Default.AddText(tag);

                    if (includeNamesCB.Checked && namesCmbB.SelectedIndex != -1)
                        UIRichText.Default.AddText(" " + getHeroNames(h.Name, namesCmbB.SelectedIndex));

                    UIRichText.Default.AddText(" " + p.Name);
                    if (vertical) UIRichText.Default.AddText("(" + getLaneName(lineUp) + ")");                    
                }
        }
        string getHorzLineUp(Team t, LineUp lineUp)
        {
            string result = string.Empty;
            int count = 0;
            foreach (Player p in t.Players)
                if (p.LineUp == lineUp)
                {
                    count++;
                    if (count > 1) result+= " + ";

                    Hero h = p.GetMostUsedHero();

                    string tag = getHeroTag(h.Name);

                    dcUsedHeroTags[tag] = h.Name;

                    result += tag;
                    result+= " " + p.Name;
                }

            return result;
        }

        private void exportPreviewB_CheckedChanged(object sender, EventArgs e)
        {            
            if (exportPreviewB.Checked)
            {
                UIRichText.Default.buffer.Font = replayExportRTB.Font;
                UIRichText.Default.buffer.Rtf = replayExportRTB.Rtf;

                foreach (string tag in dcUsedHeroTags.Keys)
                {
                    int index;
                    while ((index = UIRichText.Default.buffer.Text.IndexOf(tag)) != -1)
                    {
                        UIRichText.Default.buffer.Select(index, tag.Length);
                        Bitmap image = DHRC.GetImage(DHFormatter.ToString(DHLOOKUP.hpcUnitProfiles[dcUsedHeroTags[tag], "Art"]));
                        Bitmap thumbImage = new Bitmap(image, (int)exportIconWidthNumUD.Value, (int)exportIconWidthNumUD.Value);
                        UIRichText.Default.PasteImage(thumbImage);
                    }
                }

                replayExportRTB.Rtf = UIRichText.Default.buffer.Rtf;
                UIRichText.Default.ClearText();
            }
            else
                replayExportRTB.Rtf = originalExport;
        }

        void RefreshExportPreview()
        {
            ShowReplayExport(currentExportLayout);

            if (exportPreviewB.Checked)
                exportPreviewB_CheckedChanged(null, EventArgs.Empty);
        }

        private void shortLaneNamesCB_CheckedChanged(object sender, EventArgs e)
        {
            RefreshExportPreview();
        }

        private void heroTagB_Click(object sender, EventArgs e)
        {
            HeroTagListForm htl = new HeroTagListForm();
            htl.StartPosition = FormStartPosition.CenterScreen;
            htl.ShowDialog(hpcExportCfg);            

            RefreshExportPreview();
        }

        private void chooseFontB_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = replayExportRTB.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                replayExportRTB.Font = fd.Font;
                fontTextBox.Text = fd.Font.FontFamily.Name + " "+ fd.Font.Size;

                RefreshExportPreview();
            }
        }        
    }
}
