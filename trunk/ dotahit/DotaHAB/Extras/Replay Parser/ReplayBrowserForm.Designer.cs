namespace DotaHIT.Extras
{
    partial class ReplayBrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Description", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Replay Version",
            "1.21"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Map Name",
            "DotA Allstars v6.48b.w3x"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Map Location",
            "Maps\\Download\\"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Host Name",
            "MYM^S1rro"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Game Mode",
            "-ap"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Game Length",
            "1:56:43"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sentinel Players",
            "5"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Scourge Players",
            "5"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Winner", System.Drawing.SystemColors.WindowText, System.Drawing.Color.Ivory, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Sentinel", System.Drawing.SystemColors.WindowText, System.Drawing.Color.Ivory, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))))}, -1);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.browseTabPage = new System.Windows.Forms.TabPage();
            this.browser = new DotaHIT.Extras.FileBrowser();
            this.fixedSplitter = new System.Windows.Forms.Splitter();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.playerColorsLL = new System.Windows.Forms.LinkLabel();
            this.scourgeRTB = new System.Windows.Forms.RichTextBox();
            this.sentinelRTB = new System.Windows.Forms.RichTextBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.userInfoPanel = new System.Windows.Forms.Panel();
            this.parseB = new System.Windows.Forms.Button();
            this.userInfoBLabel = new System.Windows.Forms.Label();
            this.userInfoALabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browserMenu = new System.Windows.Forms.MenuStrip();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseTabPage = new System.Windows.Forms.TabPage();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.replayTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chatlogRTB = new System.Windows.Forms.RichTextBox();
            this.killLogTabPage = new System.Windows.Forms.TabPage();
            this.killLogRTB = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statisticsLV = new System.Windows.Forms.ListView();
            this.slotColumn = new System.Windows.Forms.ColumnHeader();
            this.playerColumn = new System.Windows.Forms.ColumnHeader();
            this.heroColumn = new System.Windows.Forms.ColumnHeader();
            this.apmColumn = new System.Windows.Forms.ColumnHeader();
            this.killsColumn = new System.Windows.Forms.ColumnHeader();
            this.deathsColumn = new System.Windows.Forms.ColumnHeader();
            this.creepKDColumn = new System.Windows.Forms.ColumnHeader();
            this.wardsColumn = new System.Windows.Forms.ColumnHeader();
            this.exportTabPage = new System.Windows.Forms.TabPage();
            this.namesCmbB = new System.Windows.Forms.ComboBox();
            this.includeNamesCB = new System.Windows.Forms.CheckBox();
            this.chooseFontB = new System.Windows.Forms.Button();
            this.fontTextBox = new System.Windows.Forms.TextBox();
            this.layoutCmbB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.shortLaneNamesCB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.exportIconWidthNumUD = new System.Windows.Forms.NumericUpDown();
            this.exportPreviewB = new System.Windows.Forms.CheckBox();
            this.heroTagB = new System.Windows.Forms.Button();
            this.replayExportRTB = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.openBuildsInFormLL = new System.Windows.Forms.LinkLabel();
            this.closeReplayB = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.scourgeTeamToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.sentinelTeamToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.versusLabel = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.infoLV = new System.Windows.Forms.ListView();
            this.valueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.sentinelMiddleLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.sentinelTopLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton26 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton27 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton28 = new System.Windows.Forms.ToolStripButton();
            this.scourgeTopLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton23 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton24 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton25 = new System.Windows.Forms.ToolStripButton();
            this.scourgeMiddleLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripButton20 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton22 = new System.Windows.Forms.ToolStripButton();
            this.scourgeBottomLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.sentinelBottomLineUpTS = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.playerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bringHeroIconToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlayerNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHeroNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.browseTabPage.SuspendLayout();
            this.previewPanel.SuspendLayout();
            this.userInfoPanel.SuspendLayout();
            this.browserMenu.SuspendLayout();
            this.parseTabPage.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel15.SuspendLayout();
            this.replayTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.killLogTabPage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.exportTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exportIconWidthNumUD)).BeginInit();
            this.panel12.SuspendLayout();
            this.scourgeTeamToolStrip.SuspendLayout();
            this.sentinelTeamToolStrip.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mapPanel.SuspendLayout();
            this.sentinelMiddleLineUpTS.SuspendLayout();
            this.sentinelTopLineUpTS.SuspendLayout();
            this.scourgeTopLineUpTS.SuspendLayout();
            this.scourgeMiddleLineUpTS.SuspendLayout();
            this.scourgeBottomLineUpTS.SuspendLayout();
            this.sentinelBottomLineUpTS.SuspendLayout();
            this.playerContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.browseTabPage);
            this.tabControl.Controls.Add(this.parseTabPage);
            this.tabControl.Location = new System.Drawing.Point(-4, -22);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(877, 541);
            this.tabControl.TabIndex = 1;
            // 
            // browseTabPage
            // 
            this.browseTabPage.Controls.Add(this.browser);
            this.browseTabPage.Controls.Add(this.fixedSplitter);
            this.browseTabPage.Controls.Add(this.previewPanel);
            this.browseTabPage.Controls.Add(this.browserMenu);
            this.browseTabPage.Location = new System.Drawing.Point(4, 22);
            this.browseTabPage.Name = "browseTabPage";
            this.browseTabPage.Size = new System.Drawing.Size(869, 515);
            this.browseTabPage.TabIndex = 0;
            this.browseTabPage.UseVisualStyleBackColor = true;
            // 
            // browser
            // 
            this.browser.BrowserPanelBackColor = System.Drawing.SystemColors.Control;
            this.browser.BrowserPanelPadding = new System.Windows.Forms.Padding(0);
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.ExplorerBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.browser.FileName = "";
            this.browser.Filter = "Replay files|*.w3g";
            this.browser.Location = new System.Drawing.Point(0, 147);
            this.browser.Name = "browser";
            this.browser.SelectedPath = "";
            this.browser.ShowExplorer = true;
            this.browser.ShowStatusBar = false;
            this.browser.ShowWar3ReplayShortCut = true;
            this.browser.Size = new System.Drawing.Size(869, 368);
            this.browser.TabIndex = 8;
            this.browser.SelectedPathChanged += new System.EventHandler(this.browser_SelectedPathChanged);
            this.browser.SelectedItemsChanged += new DotaHIT.Extras.FileBrowser.SelectedItemsChangedEventHandler(this.browser_SelectedItemsChanged);
            this.browser.FileOk += new System.ComponentModel.CancelEventHandler(this.browser_FileOk);
            // 
            // fixedSplitter
            // 
            this.fixedSplitter.BackColor = System.Drawing.Color.CornflowerBlue;
            this.fixedSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fixedSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.fixedSplitter.Enabled = false;
            this.fixedSplitter.Location = new System.Drawing.Point(0, 143);
            this.fixedSplitter.Name = "fixedSplitter";
            this.fixedSplitter.Size = new System.Drawing.Size(869, 4);
            this.fixedSplitter.TabIndex = 4;
            this.fixedSplitter.TabStop = false;
            // 
            // previewPanel
            // 
            this.previewPanel.BackColor = System.Drawing.Color.AliceBlue;
            this.previewPanel.Controls.Add(this.playerColorsLL);
            this.previewPanel.Controls.Add(this.scourgeRTB);
            this.previewPanel.Controls.Add(this.sentinelRTB);
            this.previewPanel.Controls.Add(this.hostTextBox);
            this.previewPanel.Controls.Add(this.label6);
            this.previewPanel.Controls.Add(this.mapTextBox);
            this.previewPanel.Controls.Add(this.userInfoPanel);
            this.previewPanel.Controls.Add(this.label3);
            this.previewPanel.Controls.Add(this.label2);
            this.previewPanel.Controls.Add(this.label1);
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewPanel.Location = new System.Drawing.Point(0, 24);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(869, 119);
            this.previewPanel.TabIndex = 7;
            // 
            // playerColorsLL
            // 
            this.playerColorsLL.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.playerColorsLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playerColorsLL.LinkColor = System.Drawing.Color.DimGray;
            this.playerColorsLL.Location = new System.Drawing.Point(638, 103);
            this.playerColorsLL.Name = "playerColorsLL";
            this.playerColorsLL.Size = new System.Drawing.Size(92, 13);
            this.playerColorsLL.TabIndex = 12;
            this.playerColorsLL.TabStop = true;
            this.playerColorsLL.Text = "Player Colors: Off";
            this.playerColorsLL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playerColorsLL_MouseDown);
            // 
            // scourgeRTB
            // 
            this.scourgeRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scourgeRTB.BackColor = System.Drawing.Color.AliceBlue;
            this.scourgeRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scourgeRTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scourgeRTB.ForeColor = System.Drawing.Color.Black;
            this.scourgeRTB.Location = new System.Drawing.Point(131, 78);
            this.scourgeRTB.Multiline = false;
            this.scourgeRTB.Name = "scourgeRTB";
            this.scourgeRTB.ReadOnly = true;
            this.scourgeRTB.Size = new System.Drawing.Size(599, 14);
            this.scourgeRTB.TabIndex = 11;
            this.scourgeRTB.Text = "ImbaQ, LightOfHeaven, vigoss, Virtus|Joilie";
            // 
            // sentinelRTB
            // 
            this.sentinelRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sentinelRTB.BackColor = System.Drawing.Color.AliceBlue;
            this.sentinelRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sentinelRTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sentinelRTB.ForeColor = System.Drawing.Color.Black;
            this.sentinelRTB.Location = new System.Drawing.Point(131, 55);
            this.sentinelRTB.Multiline = false;
            this.sentinelRTB.Name = "sentinelRTB";
            this.sentinelRTB.ReadOnly = true;
            this.sentinelRTB.Size = new System.Drawing.Size(599, 14);
            this.sentinelRTB.TabIndex = 10;
            this.sentinelRTB.Text = "ImbaQ, LightOfHeaven, vigoss, Virtus|Joilie";
            // 
            // hostTextBox
            // 
            this.hostTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.hostTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hostTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hostTextBox.Location = new System.Drawing.Point(131, 33);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.ReadOnly = true;
            this.hostTextBox.Size = new System.Drawing.Size(202, 14);
            this.hostTextBox.TabIndex = 8;
            this.hostTextBox.Text = "MYM^S1rro";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(88, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Host:";
            // 
            // mapTextBox
            // 
            this.mapTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.mapTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mapTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mapTextBox.Location = new System.Drawing.Point(131, 14);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.ReadOnly = true;
            this.mapTextBox.Size = new System.Drawing.Size(599, 14);
            this.mapTextBox.TabIndex = 4;
            this.mapTextBox.Text = "Dota Allstars v.6.48b.w3x";
            // 
            // userInfoPanel
            // 
            this.userInfoPanel.BackColor = System.Drawing.Color.DodgerBlue;
            this.userInfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.userInfoPanel.Controls.Add(this.parseB);
            this.userInfoPanel.Controls.Add(this.userInfoBLabel);
            this.userInfoPanel.Controls.Add(this.userInfoALabel);
            this.userInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.userInfoPanel.Location = new System.Drawing.Point(736, 0);
            this.userInfoPanel.Name = "userInfoPanel";
            this.userInfoPanel.Size = new System.Drawing.Size(133, 119);
            this.userInfoPanel.TabIndex = 3;
            // 
            // parseB
            // 
            this.parseB.BackColor = System.Drawing.Color.White;
            this.parseB.Enabled = false;
            this.parseB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.parseB.Location = new System.Drawing.Point(36, 89);
            this.parseB.Name = "parseB";
            this.parseB.Size = new System.Drawing.Size(65, 21);
            this.parseB.TabIndex = 6;
            this.parseB.Text = "parse";
            this.parseB.UseVisualStyleBackColor = false;
            this.parseB.Click += new System.EventHandler(this.parseB_Click);
            // 
            // userInfoBLabel
            // 
            this.userInfoBLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userInfoBLabel.ForeColor = System.Drawing.Color.White;
            this.userInfoBLabel.Location = new System.Drawing.Point(4, 61);
            this.userInfoBLabel.Name = "userInfoBLabel";
            this.userInfoBLabel.Size = new System.Drawing.Size(128, 19);
            this.userInfoBLabel.TabIndex = 5;
            this.userInfoBLabel.Text = "Replay File";
            this.userInfoBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userInfoALabel
            // 
            this.userInfoALabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userInfoALabel.ForeColor = System.Drawing.Color.White;
            this.userInfoALabel.Location = new System.Drawing.Point(3, 35);
            this.userInfoALabel.Name = "userInfoALabel";
            this.userInfoALabel.Size = new System.Drawing.Size(127, 22);
            this.userInfoALabel.TabIndex = 4;
            this.userInfoALabel.Text = "Select";
            this.userInfoALabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(90, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Map:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(22, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Scourge Players:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(23, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sentinel Players:";
            // 
            // browserMenu
            // 
            this.browserMenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.browserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replayToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.browserMenu.Location = new System.Drawing.Point(0, 0);
            this.browserMenu.Name = "browserMenu";
            this.browserMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.browserMenu.Size = new System.Drawing.Size(869, 24);
            this.browserMenu.TabIndex = 13;
            // 
            // replayToolStripMenuItem
            // 
            this.replayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseToolStripMenuItem,
            this.extractToolStripMenuItem});
            this.replayToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            this.replayToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.replayToolStripMenuItem.Text = "Replay";
            // 
            // parseToolStripMenuItem
            // 
            this.parseToolStripMenuItem.Name = "parseToolStripMenuItem";
            this.parseToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.parseToolStripMenuItem.Text = "Parse";
            this.parseToolStripMenuItem.Click += new System.EventHandler(this.parseToolStripMenuItem_Click);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.extractToolStripMenuItem.Text = "Extract data...";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replayFinderToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // replayFinderToolStripMenuItem
            // 
            this.replayFinderToolStripMenuItem.Name = "replayFinderToolStripMenuItem";
            this.replayFinderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.replayFinderToolStripMenuItem.Text = "Replay Finder";
            this.replayFinderToolStripMenuItem.Click += new System.EventHandler(this.replayFinderToolStripMenuItem_Click);
            // 
            // parseTabPage
            // 
            this.parseTabPage.BackColor = System.Drawing.Color.DarkSlateGray;
            this.parseTabPage.Controls.Add(this.panel13);
            this.parseTabPage.Controls.Add(this.panel14);
            this.parseTabPage.Location = new System.Drawing.Point(4, 22);
            this.parseTabPage.Name = "parseTabPage";
            this.parseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.parseTabPage.Size = new System.Drawing.Size(869, 515);
            this.parseTabPage.TabIndex = 1;
            this.parseTabPage.UseVisualStyleBackColor = true;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel15);
            this.panel13.Controls.Add(this.panel12);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(263, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(603, 509);
            this.panel13.TabIndex = 8;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel15.Controls.Add(this.replayTabControl);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 258);
            this.panel15.Name = "panel15";
            this.panel15.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.panel15.Size = new System.Drawing.Size(603, 251);
            this.panel15.TabIndex = 9;
            // 
            // replayTabControl
            // 
            this.replayTabControl.Controls.Add(this.tabPage1);
            this.replayTabControl.Controls.Add(this.killLogTabPage);
            this.replayTabControl.Controls.Add(this.tabPage2);
            this.replayTabControl.Controls.Add(this.exportTabPage);
            this.replayTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replayTabControl.Location = new System.Drawing.Point(6, 6);
            this.replayTabControl.Name = "replayTabControl";
            this.replayTabControl.SelectedIndex = 0;
            this.replayTabControl.ShowToolTips = true;
            this.replayTabControl.Size = new System.Drawing.Size(591, 245);
            this.replayTabControl.TabIndex = 8;
            this.replayTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.replayTabControl_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chatlogRTB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(583, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chatlogRTB
            // 
            this.chatlogRTB.BackColor = System.Drawing.Color.Black;
            this.chatlogRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatlogRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatlogRTB.Location = new System.Drawing.Point(3, 3);
            this.chatlogRTB.Name = "chatlogRTB";
            this.chatlogRTB.ReadOnly = true;
            this.chatlogRTB.Size = new System.Drawing.Size(577, 213);
            this.chatlogRTB.TabIndex = 7;
            this.chatlogRTB.Text = "";
            // 
            // killLogTabPage
            // 
            this.killLogTabPage.Controls.Add(this.killLogRTB);
            this.killLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.killLogTabPage.Name = "killLogTabPage";
            this.killLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.killLogTabPage.Size = new System.Drawing.Size(583, 219);
            this.killLogTabPage.TabIndex = 3;
            this.killLogTabPage.Text = "Kill Log";
            this.killLogTabPage.UseVisualStyleBackColor = true;
            // 
            // killLogRTB
            // 
            this.killLogRTB.BackColor = System.Drawing.Color.Black;
            this.killLogRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.killLogRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.killLogRTB.Location = new System.Drawing.Point(3, 3);
            this.killLogRTB.Name = "killLogRTB";
            this.killLogRTB.ReadOnly = true;
            this.killLogRTB.Size = new System.Drawing.Size(577, 213);
            this.killLogRTB.TabIndex = 8;
            this.killLogRTB.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.statisticsLV);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(583, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Game Statistics";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statisticsLV
            // 
            this.statisticsLV.BackColor = System.Drawing.Color.Ivory;
            this.statisticsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.slotColumn,
            this.playerColumn,
            this.heroColumn,
            this.apmColumn,
            this.killsColumn,
            this.deathsColumn,
            this.creepKDColumn,
            this.wardsColumn});
            this.statisticsLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticsLV.ForeColor = System.Drawing.Color.White;
            this.statisticsLV.FullRowSelect = true;
            this.statisticsLV.GridLines = true;
            this.statisticsLV.Location = new System.Drawing.Point(3, 3);
            this.statisticsLV.MultiSelect = false;
            this.statisticsLV.Name = "statisticsLV";
            this.statisticsLV.Size = new System.Drawing.Size(577, 213);
            this.statisticsLV.TabIndex = 0;
            this.statisticsLV.UseCompatibleStateImageBehavior = false;
            this.statisticsLV.View = System.Windows.Forms.View.Details;
            this.statisticsLV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.statisticsLV_ColumnClick);
            // 
            // slotColumn
            // 
            this.slotColumn.Text = "Slot";
            this.slotColumn.Width = 34;
            // 
            // playerColumn
            // 
            this.playerColumn.Text = "Player";
            this.playerColumn.Width = 134;
            // 
            // heroColumn
            // 
            this.heroColumn.Text = "Hero";
            this.heroColumn.Width = 129;
            // 
            // apmColumn
            // 
            this.apmColumn.Text = "APM";
            this.apmColumn.Width = 39;
            // 
            // killsColumn
            // 
            this.killsColumn.Text = "Kills";
            this.killsColumn.Width = 35;
            // 
            // deathsColumn
            // 
            this.deathsColumn.Text = "Deaths";
            this.deathsColumn.Width = 49;
            // 
            // creepKDColumn
            // 
            this.creepKDColumn.Text = "Creep K/D";
            this.creepKDColumn.Width = 67;
            // 
            // wardsColumn
            // 
            this.wardsColumn.Text = "Wards T{O+S}";
            this.wardsColumn.Width = 86;
            // 
            // exportTabPage
            // 
            this.exportTabPage.BackColor = System.Drawing.Color.White;
            this.exportTabPage.Controls.Add(this.namesCmbB);
            this.exportTabPage.Controls.Add(this.includeNamesCB);
            this.exportTabPage.Controls.Add(this.chooseFontB);
            this.exportTabPage.Controls.Add(this.fontTextBox);
            this.exportTabPage.Controls.Add(this.layoutCmbB);
            this.exportTabPage.Controls.Add(this.label10);
            this.exportTabPage.Controls.Add(this.shortLaneNamesCB);
            this.exportTabPage.Controls.Add(this.label5);
            this.exportTabPage.Controls.Add(this.exportIconWidthNumUD);
            this.exportTabPage.Controls.Add(this.exportPreviewB);
            this.exportTabPage.Controls.Add(this.heroTagB);
            this.exportTabPage.Controls.Add(this.replayExportRTB);
            this.exportTabPage.Controls.Add(this.label9);
            this.exportTabPage.Location = new System.Drawing.Point(4, 22);
            this.exportTabPage.Name = "exportTabPage";
            this.exportTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.exportTabPage.Size = new System.Drawing.Size(583, 219);
            this.exportTabPage.TabIndex = 2;
            this.exportTabPage.Text = "Export";
            this.exportTabPage.UseVisualStyleBackColor = true;
            // 
            // namesCmbB
            // 
            this.namesCmbB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.namesCmbB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.namesCmbB.Enabled = false;
            this.namesCmbB.FormattingEnabled = true;
            this.namesCmbB.Items.AddRange(new object[] {
            "1st Name",
            "2nd Name",
            "1st + 2nd",
            "1st + The + 2nd"});
            this.namesCmbB.Location = new System.Drawing.Point(467, 132);
            this.namesCmbB.Name = "namesCmbB";
            this.namesCmbB.Size = new System.Drawing.Size(110, 21);
            this.namesCmbB.TabIndex = 13;
            this.namesCmbB.SelectedIndexChanged += new System.EventHandler(this.namesCmbB_SelectedIndexChanged);
            // 
            // includeNamesCB
            // 
            this.includeNamesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.includeNamesCB.AutoSize = true;
            this.includeNamesCB.Location = new System.Drawing.Point(467, 109);
            this.includeNamesCB.Name = "includeNamesCB";
            this.includeNamesCB.Size = new System.Drawing.Size(85, 17);
            this.includeNamesCB.TabIndex = 12;
            this.includeNamesCB.Text = "Hero Names";
            this.includeNamesCB.UseVisualStyleBackColor = true;
            this.includeNamesCB.CheckedChanged += new System.EventHandler(this.includeNamesCB_CheckedChanged);
            // 
            // chooseFontB
            // 
            this.chooseFontB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseFontB.Location = new System.Drawing.Point(558, 31);
            this.chooseFontB.Name = "chooseFontB";
            this.chooseFontB.Size = new System.Drawing.Size(19, 23);
            this.chooseFontB.TabIndex = 11;
            this.chooseFontB.Text = "…";
            this.chooseFontB.UseVisualStyleBackColor = true;
            this.chooseFontB.Click += new System.EventHandler(this.chooseFontB_Click);
            // 
            // fontTextBox
            // 
            this.fontTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fontTextBox.BackColor = System.Drawing.Color.White;
            this.fontTextBox.Location = new System.Drawing.Point(499, 33);
            this.fontTextBox.Name = "fontTextBox";
            this.fontTextBox.ReadOnly = true;
            this.fontTextBox.Size = new System.Drawing.Size(59, 20);
            this.fontTextBox.TabIndex = 10;
            // 
            // layoutCmbB
            // 
            this.layoutCmbB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutCmbB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layoutCmbB.FormattingEnabled = true;
            this.layoutCmbB.Items.AddRange(new object[] {
            "Vertical",
            "Horizontal"});
            this.layoutCmbB.Location = new System.Drawing.Point(501, 7);
            this.layoutCmbB.Name = "layoutCmbB";
            this.layoutCmbB.Size = new System.Drawing.Size(76, 21);
            this.layoutCmbB.TabIndex = 7;
            this.layoutCmbB.SelectedIndexChanged += new System.EventHandler(this.layoutCmbB_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(468, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Font";
            // 
            // shortLaneNamesCB
            // 
            this.shortLaneNamesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shortLaneNamesCB.AutoSize = true;
            this.shortLaneNamesCB.Location = new System.Drawing.Point(474, 60);
            this.shortLaneNamesCB.Name = "shortLaneNamesCB";
            this.shortLaneNamesCB.Size = new System.Drawing.Size(106, 17);
            this.shortLaneNamesCB.TabIndex = 6;
            this.shortLaneNamesCB.Text = "short lane names";
            this.shortLaneNamesCB.UseVisualStyleBackColor = true;
            this.shortLaneNamesCB.CheckedChanged += new System.EventHandler(this.shortLaneNamesCB_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(469, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "icon width";
            // 
            // exportIconWidthNumUD
            // 
            this.exportIconWidthNumUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportIconWidthNumUD.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.exportIconWidthNumUD.Location = new System.Drawing.Point(528, 83);
            this.exportIconWidthNumUD.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.exportIconWidthNumUD.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.exportIconWidthNumUD.Name = "exportIconWidthNumUD";
            this.exportIconWidthNumUD.Size = new System.Drawing.Size(49, 20);
            this.exportIconWidthNumUD.TabIndex = 4;
            this.exportIconWidthNumUD.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.exportIconWidthNumUD.ValueChanged += new System.EventHandler(this.shortLaneNamesCB_CheckedChanged);
            // 
            // exportPreviewB
            // 
            this.exportPreviewB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportPreviewB.Appearance = System.Windows.Forms.Appearance.Button;
            this.exportPreviewB.Location = new System.Drawing.Point(467, 160);
            this.exportPreviewB.Name = "exportPreviewB";
            this.exportPreviewB.Size = new System.Drawing.Size(108, 24);
            this.exportPreviewB.TabIndex = 3;
            this.exportPreviewB.Text = "preview";
            this.exportPreviewB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exportPreviewB.UseVisualStyleBackColor = true;
            this.exportPreviewB.CheckedChanged += new System.EventHandler(this.exportPreviewB_CheckedChanged);
            // 
            // heroTagB
            // 
            this.heroTagB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.heroTagB.Location = new System.Drawing.Point(467, 190);
            this.heroTagB.Name = "heroTagB";
            this.heroTagB.Size = new System.Drawing.Size(108, 23);
            this.heroTagB.TabIndex = 2;
            this.heroTagB.Text = "hero<->tag";
            this.heroTagB.UseVisualStyleBackColor = true;
            this.heroTagB.Click += new System.EventHandler(this.heroTagB_Click);
            // 
            // replayExportRTB
            // 
            this.replayExportRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.replayExportRTB.BackColor = System.Drawing.Color.White;
            this.replayExportRTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replayExportRTB.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.replayExportRTB.Location = new System.Drawing.Point(3, 6);
            this.replayExportRTB.Name = "replayExportRTB";
            this.replayExportRTB.Size = new System.Drawing.Size(459, 207);
            this.replayExportRTB.TabIndex = 0;
            this.replayExportRTB.Text = "select layout first";
            this.replayExportRTB.WordWrap = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(460, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Layout";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.ForestGreen;
            this.panel12.Controls.Add(this.openBuildsInFormLL);
            this.panel12.Controls.Add(this.closeReplayB);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Controls.Add(this.label7);
            this.panel12.Controls.Add(this.scourgeTeamToolStrip);
            this.panel12.Controls.Add(this.sentinelTeamToolStrip);
            this.panel12.Controls.Add(this.versusLabel);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(603, 258);
            this.panel12.TabIndex = 6;
            // 
            // openBuildsInFormLL
            // 
            this.openBuildsInFormLL.ActiveLinkColor = System.Drawing.Color.White;
            this.openBuildsInFormLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openBuildsInFormLL.LinkColor = System.Drawing.Color.LightGray;
            this.openBuildsInFormLL.Location = new System.Drawing.Point(402, 242);
            this.openBuildsInFormLL.Name = "openBuildsInFormLL";
            this.openBuildsInFormLL.Size = new System.Drawing.Size(186, 13);
            this.openBuildsInFormLL.TabIndex = 14;
            this.openBuildsInFormLL.TabStop = true;
            this.openBuildsInFormLL.Text = "Open build orders in new window: Off";
            this.openBuildsInFormLL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openBuildsInFormLL_MouseDown);
            // 
            // closeReplayB
            // 
            this.closeReplayB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeReplayB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeReplayB.Location = new System.Drawing.Point(550, -1);
            this.closeReplayB.Name = "closeReplayB";
            this.closeReplayB.Size = new System.Drawing.Size(54, 25);
            this.closeReplayB.TabIndex = 9;
            this.closeReplayB.Text = "close";
            this.closeReplayB.UseVisualStyleBackColor = true;
            this.closeReplayB.Click += new System.EventHandler(this.closeReplayB_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(318, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(271, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "The Scourge";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(15, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(274, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "The Sentinel";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scourgeTeamToolStrip
            // 
            this.scourgeTeamToolStrip.AutoSize = false;
            this.scourgeTeamToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.scourgeTeamToolStrip.CanOverflow = false;
            this.scourgeTeamToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.scourgeTeamToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.scourgeTeamToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.scourgeTeamToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.scourgeTeamToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripSeparator3,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator4,
            this.toolStripButton9,
            this.toolStripButton10});
            this.scourgeTeamToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.scourgeTeamToolStrip.Location = new System.Drawing.Point(318, 36);
            this.scourgeTeamToolStrip.Name = "scourgeTeamToolStrip";
            this.scourgeTeamToolStrip.Padding = new System.Windows.Forms.Padding(0, 1, 1, 0);
            this.scourgeTeamToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.scourgeTeamToolStrip.Size = new System.Drawing.Size(270, 248);
            this.scourgeTeamToolStrip.TabIndex = 4;
            this.scourgeTeamToolStrip.Text = "toolStrip2";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.AutoToolTip = false;
            this.toolStripButton6.BackColor = System.Drawing.Color.Blue;
            this.toolStripButton6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton6.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton6.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton6.Text = "LightOfHeaven12";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.BackColor = System.Drawing.Color.Cyan;
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.Red;
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.BackColor = System.Drawing.Color.Cyan;
            this.toolStripButton7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton7.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton7.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton7.Text = "LightOfHeaven12";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.BackColor = System.Drawing.Color.Orange;
            this.toolStripButton8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton8.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton8.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton8.Text = "LightOfHeaven12";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.BackColor = System.Drawing.Color.HotPink;
            this.toolStripButton9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton9.ForeColor = System.Drawing.Color.White;
            this.toolStripButton9.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton9.Text = "LightOfHeaven12";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.BackColor = System.Drawing.Color.Silver;
            this.toolStripButton10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton10.ForeColor = System.Drawing.Color.White;
            this.toolStripButton10.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton10.Text = "LightOfHeaven12";
            // 
            // sentinelTeamToolStrip
            // 
            this.sentinelTeamToolStrip.AutoSize = false;
            this.sentinelTeamToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.sentinelTeamToolStrip.CanOverflow = false;
            this.sentinelTeamToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.sentinelTeamToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.sentinelTeamToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sentinelTeamToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sentinelTeamToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripSeparator5,
            this.toolStripButton5});
            this.sentinelTeamToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.sentinelTeamToolStrip.Location = new System.Drawing.Point(18, 36);
            this.sentinelTeamToolStrip.Name = "sentinelTeamToolStrip";
            this.sentinelTeamToolStrip.Padding = new System.Windows.Forms.Padding(0, 1, 1, 0);
            this.sentinelTeamToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sentinelTeamToolStrip.Size = new System.Drawing.Size(270, 229);
            this.sentinelTeamToolStrip.TabIndex = 3;
            this.sentinelTeamToolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoToolTip = false;
            this.toolStripButton1.BackColor = System.Drawing.Color.Blue;
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton1.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton1.Text = "LightOfHeaven12 (Top)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.Cyan;
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.Red;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.Color.Cyan;
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton2.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton2.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton2.Text = "LightOfHeaven12 (Mid)";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.BackColor = System.Drawing.Color.Orange;
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton3.ForeColor = System.Drawing.Color.DarkGray;
            this.toolStripButton3.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton3.Text = "LightOfHeaven12";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.BackColor = System.Drawing.Color.HotPink;
            this.toolStripButton4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton4.ForeColor = System.Drawing.Color.White;
            this.toolStripButton4.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton4.Text = "LightOfHeaven12 (Bot)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.BackColor = System.Drawing.Color.Silver;
            this.toolStripButton5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton5.ForeColor = System.Drawing.Color.White;
            this.toolStripButton5.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(268, 28);
            this.toolStripButton5.Text = "LightOfHeaven12";
            // 
            // versusLabel
            // 
            this.versusLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versusLabel.ForeColor = System.Drawing.Color.White;
            this.versusLabel.Location = new System.Drawing.Point(291, 36);
            this.versusLabel.Name = "versusLabel";
            this.versusLabel.Size = new System.Drawing.Size(25, 172);
            this.versusLabel.TabIndex = 5;
            this.versusLabel.Text = "VS";
            this.versusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel14.Controls.Add(this.infoLV);
            this.panel14.Controls.Add(this.panel1);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(260, 509);
            this.panel14.TabIndex = 8;
            // 
            // infoLV
            // 
            this.infoLV.BackColor = System.Drawing.Color.Ivory;
            this.infoLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.valueColumnHeader,
            this.columnHeader1});
            this.infoLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLV.GridLines = true;
            listViewGroup1.Header = "Description";
            listViewGroup1.Name = "listViewGroup1";
            this.infoLV.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.infoLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup1;
            listViewItem4.Group = listViewGroup1;
            listViewItem5.Group = listViewGroup1;
            listViewItem6.Group = listViewGroup1;
            listViewItem7.Group = listViewGroup1;
            listViewItem8.Group = listViewGroup1;
            listViewItem9.Group = listViewGroup1;
            listViewItem9.UseItemStyleForSubItems = false;
            this.infoLV.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.infoLV.Location = new System.Drawing.Point(0, 260);
            this.infoLV.MultiSelect = false;
            this.infoLV.Name = "infoLV";
            this.infoLV.Scrollable = false;
            this.infoLV.ShowItemToolTips = true;
            this.infoLV.Size = new System.Drawing.Size(260, 249);
            this.infoLV.TabIndex = 0;
            this.infoLV.UseCompatibleStateImageBehavior = false;
            this.infoLV.View = System.Windows.Forms.View.Details;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "";
            this.valueColumnHeader.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 142;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mapPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 260);
            this.panel1.TabIndex = 6;
            // 
            // mapPanel
            // 
            this.mapPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mapPanel.Controls.Add(this.sentinelMiddleLineUpTS);
            this.mapPanel.Controls.Add(this.sentinelTopLineUpTS);
            this.mapPanel.Controls.Add(this.scourgeTopLineUpTS);
            this.mapPanel.Controls.Add(this.scourgeMiddleLineUpTS);
            this.mapPanel.Controls.Add(this.scourgeBottomLineUpTS);
            this.mapPanel.Controls.Add(this.sentinelBottomLineUpTS);
            this.mapPanel.Location = new System.Drawing.Point(2, 2);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(256, 256);
            this.mapPanel.TabIndex = 1;
            // 
            // sentinelMiddleLineUpTS
            // 
            this.sentinelMiddleLineUpTS.AutoSize = false;
            this.sentinelMiddleLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.sentinelMiddleLineUpTS.CanOverflow = false;
            this.sentinelMiddleLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.sentinelMiddleLineUpTS.GripMargin = new System.Windows.Forms.Padding(0);
            this.sentinelMiddleLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sentinelMiddleLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sentinelMiddleLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripButton17,
            this.toolStripButton18,
            this.toolStripButton19});
            this.sentinelMiddleLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.sentinelMiddleLineUpTS.Location = new System.Drawing.Point(75, 123);
            this.sentinelMiddleLineUpTS.Name = "sentinelMiddleLineUpTS";
            this.sentinelMiddleLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.sentinelMiddleLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sentinelMiddleLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.sentinelMiddleLineUpTS.TabIndex = 3;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel3.Text = " ";
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton17.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton17.Text = "toolStripButton11";
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 4);
            this.toolStripButton18.Text = "toolStripButton12";
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton19.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton19.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton19.Text = "toolStripButton13";
            // 
            // sentinelTopLineUpTS
            // 
            this.sentinelTopLineUpTS.AutoSize = false;
            this.sentinelTopLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.sentinelTopLineUpTS.CanOverflow = false;
            this.sentinelTopLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.sentinelTopLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sentinelTopLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sentinelTopLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel6,
            this.toolStripButton26,
            this.toolStripButton27,
            this.toolStripButton28});
            this.sentinelTopLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.sentinelTopLineUpTS.Location = new System.Drawing.Point(0, 66);
            this.sentinelTopLineUpTS.Name = "sentinelTopLineUpTS";
            this.sentinelTopLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.sentinelTopLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sentinelTopLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.sentinelTopLineUpTS.TabIndex = 6;
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel6.Text = " ";
            // 
            // toolStripButton26
            // 
            this.toolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton26.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton26.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton26.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton26.Name = "toolStripButton26";
            this.toolStripButton26.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton26.Text = "toolStripButton11";
            // 
            // toolStripButton27
            // 
            this.toolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton27.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton27.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton27.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton27.Name = "toolStripButton27";
            this.toolStripButton27.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton27.Text = "toolStripButton12";
            // 
            // toolStripButton28
            // 
            this.toolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton28.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton28.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton28.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton28.Name = "toolStripButton28";
            this.toolStripButton28.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton28.Text = "toolStripButton13";
            // 
            // scourgeTopLineUpTS
            // 
            this.scourgeTopLineUpTS.AutoSize = false;
            this.scourgeTopLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.scourgeTopLineUpTS.CanOverflow = false;
            this.scourgeTopLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.scourgeTopLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.scourgeTopLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.scourgeTopLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.toolStripButton23,
            this.toolStripButton24,
            this.toolStripButton25});
            this.scourgeTopLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.scourgeTopLineUpTS.Location = new System.Drawing.Point(0, 14);
            this.scourgeTopLineUpTS.Name = "scourgeTopLineUpTS";
            this.scourgeTopLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.scourgeTopLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.scourgeTopLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.scourgeTopLineUpTS.TabIndex = 5;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel5.Text = " ";
            // 
            // toolStripButton23
            // 
            this.toolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton23.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton23.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton23.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton23.Name = "toolStripButton23";
            this.toolStripButton23.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton23.Text = "toolStripButton11";
            // 
            // toolStripButton24
            // 
            this.toolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton24.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton24.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton24.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton24.Name = "toolStripButton24";
            this.toolStripButton24.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton24.Text = "toolStripButton12";
            // 
            // toolStripButton25
            // 
            this.toolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton25.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton25.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton25.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton25.Name = "toolStripButton25";
            this.toolStripButton25.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton25.Text = "toolStripButton13";
            // 
            // scourgeMiddleLineUpTS
            // 
            this.scourgeMiddleLineUpTS.AutoSize = false;
            this.scourgeMiddleLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.scourgeMiddleLineUpTS.CanOverflow = false;
            this.scourgeMiddleLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.scourgeMiddleLineUpTS.GripMargin = new System.Windows.Forms.Padding(0);
            this.scourgeMiddleLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.scourgeMiddleLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.scourgeMiddleLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton20,
            this.toolStripLabel7,
            this.toolStripLabel4,
            this.toolStripButton22});
            this.scourgeMiddleLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.scourgeMiddleLineUpTS.Location = new System.Drawing.Point(116, 97);
            this.scourgeMiddleLineUpTS.Name = "scourgeMiddleLineUpTS";
            this.scourgeMiddleLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.scourgeMiddleLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.scourgeMiddleLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.scourgeMiddleLineUpTS.TabIndex = 4;
            // 
            // toolStripButton20
            // 
            this.toolStripButton20.BackColor = System.Drawing.Color.Red;
            this.toolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton20.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton20.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton20.Name = "toolStripButton20";
            this.toolStripButton20.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton20.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton20.Text = "toolStripButton11";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.AutoSize = false;
            this.toolStripLabel7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabel7.Text = " ";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel4.Text = " ";
            // 
            // toolStripButton22
            // 
            this.toolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton22.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton22.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton22.Name = "toolStripButton22";
            this.toolStripButton22.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton22.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton22.Text = "toolStripButton13";
            // 
            // scourgeBottomLineUpTS
            // 
            this.scourgeBottomLineUpTS.AutoSize = false;
            this.scourgeBottomLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.scourgeBottomLineUpTS.CanOverflow = false;
            this.scourgeBottomLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.scourgeBottomLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.scourgeBottomLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.scourgeBottomLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripButton16});
            this.scourgeBottomLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.scourgeBottomLineUpTS.Location = new System.Drawing.Point(204, 143);
            this.scourgeBottomLineUpTS.Name = "scourgeBottomLineUpTS";
            this.scourgeBottomLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.scourgeBottomLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.scourgeBottomLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.scourgeBottomLineUpTS.TabIndex = 2;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel2.Text = " ";
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton14.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton14.Text = "toolStripButton11";
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton15.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton15.Text = "toolStripButton12";
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.toolStripButton16.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton16.Text = "toolStripButton13";
            // 
            // sentinelBottomLineUpTS
            // 
            this.sentinelBottomLineUpTS.AutoSize = false;
            this.sentinelBottomLineUpTS.BackColor = System.Drawing.Color.Transparent;
            this.sentinelBottomLineUpTS.CanOverflow = false;
            this.sentinelBottomLineUpTS.Dock = System.Windows.Forms.DockStyle.None;
            this.sentinelBottomLineUpTS.GripMargin = new System.Windows.Forms.Padding(0);
            this.sentinelBottomLineUpTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sentinelBottomLineUpTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sentinelBottomLineUpTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13});
            this.sentinelBottomLineUpTS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.sentinelBottomLineUpTS.Location = new System.Drawing.Point(204, 195);
            this.sentinelBottomLineUpTS.Name = "sentinelBottomLineUpTS";
            this.sentinelBottomLineUpTS.Padding = new System.Windows.Forms.Padding(0);
            this.sentinelBottomLineUpTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sentinelBottomLineUpTS.Size = new System.Drawing.Size(52, 50);
            this.sentinelBottomLineUpTS.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(10, 13);
            this.toolStripLabel1.Text = " ";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton11.Text = "toolStripButton11";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton12.Text = "toolStripButton12";
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::DotaHIT.Properties.Resources.armor;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton13.Text = "toolStripButton13";
            // 
            // playerContextMenuStrip
            // 
            this.playerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bringHeroIconToFrontToolStripMenuItem,
            this.copyPlayerNameToolStripMenuItem,
            this.copyHeroNameToolStripMenuItem});
            this.playerContextMenuStrip.Name = "playerContextMenuStrip";
            this.playerContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.playerContextMenuStrip.Size = new System.Drawing.Size(251, 70);
            // 
            // bringHeroIconToFrontToolStripMenuItem
            // 
            this.bringHeroIconToFrontToolStripMenuItem.Name = "bringHeroIconToFrontToolStripMenuItem";
            this.bringHeroIconToFrontToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.bringHeroIconToFrontToolStripMenuItem.Text = "Bring Hero Icon to Front (minimap)";
            this.bringHeroIconToFrontToolStripMenuItem.Click += new System.EventHandler(this.bringHeroIconToFrontToolStripMenuItem_Click);
            // 
            // copyPlayerNameToolStripMenuItem
            // 
            this.copyPlayerNameToolStripMenuItem.Name = "copyPlayerNameToolStripMenuItem";
            this.copyPlayerNameToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.copyPlayerNameToolStripMenuItem.Text = "Copy Player Name";
            this.copyPlayerNameToolStripMenuItem.Click += new System.EventHandler(this.copyPlayerNameToolStripMenuItem_Click);
            // 
            // copyHeroNameToolStripMenuItem
            // 
            this.copyHeroNameToolStripMenuItem.Name = "copyHeroNameToolStripMenuItem";
            this.copyHeroNameToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.copyHeroNameToolStripMenuItem.Text = "Copy Hero Name";
            this.copyHeroNameToolStripMenuItem.Click += new System.EventHandler(this.copyHeroNameToolStripMenuItem_Click);
            // 
            // ReplayBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(869, 515);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.browserMenu;
            this.Name = "ReplayBrowserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotA H.I.T. Replay Browser/Parser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReplayParserForm_FormClosed);
            this.tabControl.ResumeLayout(false);
            this.browseTabPage.ResumeLayout(false);
            this.browseTabPage.PerformLayout();
            this.previewPanel.ResumeLayout(false);
            this.previewPanel.PerformLayout();
            this.userInfoPanel.ResumeLayout(false);
            this.browserMenu.ResumeLayout(false);
            this.browserMenu.PerformLayout();
            this.parseTabPage.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.replayTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.killLogTabPage.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.exportTabPage.ResumeLayout(false);
            this.exportTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exportIconWidthNumUD)).EndInit();
            this.panel12.ResumeLayout(false);
            this.scourgeTeamToolStrip.ResumeLayout(false);
            this.scourgeTeamToolStrip.PerformLayout();
            this.sentinelTeamToolStrip.ResumeLayout(false);
            this.sentinelTeamToolStrip.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.mapPanel.ResumeLayout(false);
            this.sentinelMiddleLineUpTS.ResumeLayout(false);
            this.sentinelMiddleLineUpTS.PerformLayout();
            this.sentinelTopLineUpTS.ResumeLayout(false);
            this.sentinelTopLineUpTS.PerformLayout();
            this.scourgeTopLineUpTS.ResumeLayout(false);
            this.scourgeTopLineUpTS.PerformLayout();
            this.scourgeMiddleLineUpTS.ResumeLayout(false);
            this.scourgeMiddleLineUpTS.PerformLayout();
            this.scourgeBottomLineUpTS.ResumeLayout(false);
            this.scourgeBottomLineUpTS.PerformLayout();
            this.sentinelBottomLineUpTS.ResumeLayout(false);
            this.sentinelBottomLineUpTS.PerformLayout();
            this.playerContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage browseTabPage;
        private System.Windows.Forms.TabPage parseTabPage;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Panel userInfoPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter fixedSplitter;
        private FileBrowser browser;
        private System.Windows.Forms.Label userInfoALabel;
        private System.Windows.Forms.Label userInfoBLabel;
        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox sentinelRTB;
        private System.Windows.Forms.RichTextBox scourgeRTB;
        private System.Windows.Forms.LinkLabel playerColorsLL;
        private System.Windows.Forms.Button parseB;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.ToolStrip sentinelTeamToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip scourgeTeamToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label versusLabel;
        private System.Windows.Forms.RichTextBox chatlogRTB;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ListView infoLV;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip scourgeBottomLineUpTS;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStrip sentinelBottomLineUpTS;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip scourgeTopLineUpTS;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton toolStripButton23;
        private System.Windows.Forms.ToolStripButton toolStripButton24;
        private System.Windows.Forms.ToolStripButton toolStripButton25;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStrip sentinelTopLineUpTS;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton toolStripButton26;
        private System.Windows.Forms.ToolStripButton toolStripButton27;
        private System.Windows.Forms.ToolStripButton toolStripButton28;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button closeReplayB;
        private System.Windows.Forms.TabControl replayTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView statisticsLV;
        private System.Windows.Forms.ColumnHeader playerColumn;
        private System.Windows.Forms.ColumnHeader heroColumn;
        private System.Windows.Forms.ColumnHeader apmColumn;
        private System.Windows.Forms.ColumnHeader killsColumn;
        private System.Windows.Forms.ColumnHeader deathsColumn;
        private System.Windows.Forms.ColumnHeader creepKDColumn;
        private System.Windows.Forms.ColumnHeader slotColumn;
        private System.Windows.Forms.ToolStrip sentinelMiddleLineUpTS;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStrip scourgeMiddleLineUpTS;
        private System.Windows.Forms.ToolStripButton toolStripButton20;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton toolStripButton22;
        private System.Windows.Forms.LinkLabel openBuildsInFormLL;
        private System.Windows.Forms.ColumnHeader wardsColumn;
        private System.Windows.Forms.ContextMenuStrip playerContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem bringHeroIconToFrontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlayerNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHeroNameToolStripMenuItem;
        private System.Windows.Forms.TabPage exportTabPage;
        private System.Windows.Forms.RichTextBox replayExportRTB;
        private System.Windows.Forms.CheckBox exportPreviewB;
        private System.Windows.Forms.Button heroTagB;
        private System.Windows.Forms.CheckBox shortLaneNamesCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown exportIconWidthNumUD;
        private System.Windows.Forms.ComboBox layoutCmbB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox fontTextBox;
        private System.Windows.Forms.Button chooseFontB;
        private System.Windows.Forms.TabPage killLogTabPage;
        private System.Windows.Forms.RichTextBox killLogRTB;
        private System.Windows.Forms.MenuStrip browserMenu;
        private System.Windows.Forms.ToolStripMenuItem replayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replayFinderToolStripMenuItem;
        private System.Windows.Forms.ComboBox namesCmbB;
        private System.Windows.Forms.CheckBox includeNamesCB;        
    }
}