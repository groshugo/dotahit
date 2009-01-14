namespace DotaHIT.Extras.Replay_Parser
{
    partial class ReplayFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplayFinder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.heroesGridView = new System.Windows.Forms.DataGridView();
            this.heroImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.heroNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerNickColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultsLV = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.subfoldersCB = new System.Windows.Forms.CheckBox();
            this.startB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.extractDataB = new System.Windows.Forms.Button();
            this.statisticsCB = new System.Windows.Forms.CheckBox();
            this.killLogCB = new System.Windows.Forms.CheckBox();
            this.chatlogCB = new System.Windows.Forms.CheckBox();
            this.unitsCheckTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.heroesGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.MintCream;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(629, 97);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // heroesGridView
            // 
            this.heroesGridView.AllowUserToAddRows = false;
            this.heroesGridView.AllowUserToDeleteRows = false;
            this.heroesGridView.AllowUserToResizeColumns = false;
            this.heroesGridView.AllowUserToResizeRows = false;
            this.heroesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.heroesGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.heroesGridView.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.heroesGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.heroesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.heroesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.heroesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.heroImageColumn,
            this.heroNameColumn,
            this.playerNickColumn});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.heroesGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.heroesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.heroesGridView.EnableHeadersVisualStyles = false;
            this.heroesGridView.Location = new System.Drawing.Point(12, 109);
            this.heroesGridView.MultiSelect = false;
            this.heroesGridView.Name = "heroesGridView";
            this.heroesGridView.RowHeadersVisible = false;
            this.heroesGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.heroesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.heroesGridView.Size = new System.Drawing.Size(629, 129);
            this.heroesGridView.TabIndex = 30;
            this.heroesGridView.VirtualMode = true;
            this.heroesGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.heroesGridView_KeyDown);
            this.heroesGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.heroesGridView_CellValueNeeded);
            this.heroesGridView.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.heroesGridView_CellValuePushed);
            // 
            // heroImageColumn
            // 
            this.heroImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.heroImageColumn.FillWeight = 60F;
            this.heroImageColumn.HeaderText = "Hero";
            this.heroImageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.heroImageColumn.Name = "heroImageColumn";
            this.heroImageColumn.ReadOnly = true;
            this.heroImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.heroImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.heroImageColumn.Width = 28;
            // 
            // heroNameColumn
            // 
            this.heroNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.heroNameColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.heroNameColumn.HeaderText = "Name";
            this.heroNameColumn.Name = "heroNameColumn";
            this.heroNameColumn.ReadOnly = true;
            this.heroNameColumn.Width = 300;
            // 
            // playerNickColumn
            // 
            this.playerNickColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playerNickColumn.HeaderText = "Player";
            this.playerNickColumn.Name = "playerNickColumn";
            // 
            // resultsLV
            // 
            this.resultsLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsLV.BackColor = System.Drawing.Color.AliceBlue;
            this.resultsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.resultsLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.resultsLV.Location = new System.Drawing.Point(14, 312);
            this.resultsLV.Name = "resultsLV";
            this.resultsLV.Size = new System.Drawing.Size(627, 125);
            this.resultsLV.TabIndex = 31;
            this.resultsLV.UseCompatibleStateImageBehavior = false;
            this.resultsLV.View = System.Windows.Forms.View.Details;
            this.resultsLV.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.resultsLV_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Results";
            this.columnHeader1.Width = 6;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(12, 257);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(547, 20);
            this.pathTextBox.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Search in this folder";
            // 
            // subfoldersCB
            // 
            this.subfoldersCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.subfoldersCB.AutoSize = true;
            this.subfoldersCB.Checked = true;
            this.subfoldersCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.subfoldersCB.Location = new System.Drawing.Point(565, 259);
            this.subfoldersCB.Name = "subfoldersCB";
            this.subfoldersCB.Size = new System.Drawing.Size(74, 17);
            this.subfoldersCB.TabIndex = 34;
            this.subfoldersCB.Text = "subfolders";
            this.subfoldersCB.UseVisualStyleBackColor = true;
            // 
            // startB
            // 
            this.startB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.startB.Location = new System.Drawing.Point(12, 283);
            this.startB.Name = "startB";
            this.startB.Size = new System.Drawing.Size(629, 23);
            this.startB.TabIndex = 35;
            this.startB.Text = "Start Searching";
            this.startB.UseVisualStyleBackColor = true;
            this.startB.Click += new System.EventHandler(this.startB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.extractDataB);
            this.groupBox1.Controls.Add(this.statisticsCB);
            this.groupBox1.Controls.Add(this.killLogCB);
            this.groupBox1.Controls.Add(this.chatlogCB);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(15, 448);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 49);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract data from found replays (OPTIONAL)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(326, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "NOTE: Multiple data of the same type will be saved in one single file";
            // 
            // extractDataB
            // 
            this.extractDataB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extractDataB.Location = new System.Drawing.Point(251, 19);
            this.extractDataB.Name = "extractDataB";
            this.extractDataB.Size = new System.Drawing.Size(367, 23);
            this.extractDataB.TabIndex = 4;
            this.extractDataB.Text = "Extract data from found replays";
            this.extractDataB.UseVisualStyleBackColor = true;
            this.extractDataB.Click += new System.EventHandler(this.extractDataB_Click);
            // 
            // statisticsCB
            // 
            this.statisticsCB.AutoSize = true;
            this.statisticsCB.Location = new System.Drawing.Point(150, 20);
            this.statisticsCB.Name = "statisticsCB";
            this.statisticsCB.Size = new System.Drawing.Size(68, 17);
            this.statisticsCB.TabIndex = 3;
            this.statisticsCB.Text = "Statistics";
            this.statisticsCB.UseVisualStyleBackColor = true;
            // 
            // killLogCB
            // 
            this.killLogCB.AutoSize = true;
            this.killLogCB.Checked = true;
            this.killLogCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.killLogCB.Location = new System.Drawing.Point(84, 20);
            this.killLogCB.Name = "killLogCB";
            this.killLogCB.Size = new System.Drawing.Size(60, 17);
            this.killLogCB.TabIndex = 2;
            this.killLogCB.Text = "Kill Log";
            this.killLogCB.UseVisualStyleBackColor = true;
            // 
            // chatlogCB
            // 
            this.chatlogCB.AutoSize = true;
            this.chatlogCB.Location = new System.Drawing.Point(9, 20);
            this.chatlogCB.Name = "chatlogCB";
            this.chatlogCB.Size = new System.Drawing.Size(69, 17);
            this.chatlogCB.TabIndex = 1;
            this.chatlogCB.Text = "Chat Log";
            this.chatlogCB.UseVisualStyleBackColor = true;
            // 
            // unitsCheckTimer
            // 
            this.unitsCheckTimer.Interval = 1000;
            this.unitsCheckTimer.Tick += new System.EventHandler(this.unitsCheckTimer_Tick);
            // 
            // ReplayFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(653, 509);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startB);
            this.Controls.Add(this.subfoldersCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.resultsLV);
            this.Controls.Add(this.heroesGridView);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "ReplayFinder";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReplayFinder";
            ((System.ComponentModel.ISupportInitialize)(this.heroesGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView heroesGridView;
        private System.Windows.Forms.ListView resultsLV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox subfoldersCB;
        private System.Windows.Forms.Button startB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chatlogCB;
        private System.Windows.Forms.CheckBox killLogCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button extractDataB;
        private System.Windows.Forms.CheckBox statisticsCB;
        private System.Windows.Forms.Timer unitsCheckTimer;
        private System.Windows.Forms.DataGridViewImageColumn heroImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heroNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerNickColumn;
    }
}