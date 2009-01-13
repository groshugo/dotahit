namespace DotaHIT
{
    partial class CustomKeysForm
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
            this.captionB = new System.Windows.Forms.Button();
            this.closeB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hotkeyColorB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveToWar3B = new System.Windows.Forms.Button();
            this.saveCKinfoB = new System.Windows.Forms.Button();
            this.saveCustomKeysB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFromWar3B = new System.Windows.Forms.Button();
            this.loadCKinfoB = new System.Windows.Forms.Button();
            this.loadCustomKeysB = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.noteInfoB = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // captionB
            // 
            this.captionB.BackColor = System.Drawing.Color.Black;
            this.captionB.Dock = System.Windows.Forms.DockStyle.Top;
            this.captionB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.captionB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captionB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.captionB.ForeColor = System.Drawing.Color.Gold;
            this.captionB.Location = new System.Drawing.Point(0, 0);
            this.captionB.Margin = new System.Windows.Forms.Padding(0);
            this.captionB.Name = "captionB";
            this.captionB.Size = new System.Drawing.Size(488, 26);
            this.captionB.TabIndex = 13;
            this.captionB.Text = "CustomKeys Generator";
            this.captionB.UseVisualStyleBackColor = false;
            this.captionB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.captionB_MouseDown);
            this.captionB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.captionB_MouseMove);
            this.captionB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.captionB_MouseUp);
            // 
            // closeB
            // 
            this.closeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeB.BackColor = System.Drawing.Color.Black;
            this.closeB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closeB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeB.ForeColor = System.Drawing.Color.Silver;
            this.closeB.Location = new System.Drawing.Point(461, 3);
            this.closeB.Name = "closeB";
            this.closeB.Size = new System.Drawing.Size(23, 19);
            this.closeB.TabIndex = 15;
            this.closeB.Text = "x";
            this.closeB.UseVisualStyleBackColor = false;
            this.closeB.Click += new System.EventHandler(this.closeB_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 47);
            this.label1.TabIndex = 16;
            this.label1.Text = "DotaHIT is now in CustomKeys-Generation mode. If you want to return to original m" +
                "ode, then close this window.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.noteInfoB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.hotkeyColorB);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 186);
            this.panel1.TabIndex = 18;
            // 
            // hotkeyColorB
            // 
            this.hotkeyColorB.BackColor = System.Drawing.Color.DarkSlateGray;
            this.hotkeyColorB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hotkeyColorB.ForeColor = System.Drawing.Color.DodgerBlue;
            this.hotkeyColorB.Location = new System.Drawing.Point(173, 75);
            this.hotkeyColorB.Name = "hotkeyColorB";
            this.hotkeyColorB.Size = new System.Drawing.Size(129, 27);
            this.hotkeyColorB.TabIndex = 19;
            this.hotkeyColorB.Text = "change hotkey color";
            this.hotkeyColorB.UseVisualStyleBackColor = false;
            this.hotkeyColorB.Click += new System.EventHandler(this.hotkeyColorB_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(461, 28);
            this.label4.TabIndex = 21;
            this.label4.Text = "You can change any skill\'s hotkey by clicking on it and then pressing the button " +
                "on the keyboard. Just go through all the heroes and set your hotkeys.";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.saveToWar3B);
            this.groupBox2.Controls.Add(this.saveCKinfoB);
            this.groupBox2.Controls.Add(this.saveCustomKeysB);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(307, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 73);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate CustomKeys";
            // 
            // saveToWar3B
            // 
            this.saveToWar3B.ForeColor = System.Drawing.Color.Black;
            this.saveToWar3B.Location = new System.Drawing.Point(7, 48);
            this.saveToWar3B.Name = "saveToWar3B";
            this.saveToWar3B.Size = new System.Drawing.Size(117, 20);
            this.saveToWar3B.TabIndex = 22;
            this.saveToWar3B.Text = "save to war3 folder";
            this.saveToWar3B.UseVisualStyleBackColor = true;
            this.saveToWar3B.Click += new System.EventHandler(this.saveToWar3B_Click);
            // 
            // saveCKinfoB
            // 
            this.saveCKinfoB.ForeColor = System.Drawing.Color.Black;
            this.saveCKinfoB.Location = new System.Drawing.Point(130, 23);
            this.saveCKinfoB.Name = "saveCKinfoB";
            this.saveCKinfoB.Size = new System.Drawing.Size(24, 23);
            this.saveCKinfoB.TabIndex = 22;
            this.saveCKinfoB.Text = "?";
            this.saveCKinfoB.UseVisualStyleBackColor = true;
            this.saveCKinfoB.Click += new System.EventHandler(this.saveCKinfoB_Click);
            // 
            // saveCustomKeysB
            // 
            this.saveCustomKeysB.ForeColor = System.Drawing.Color.Black;
            this.saveCustomKeysB.Location = new System.Drawing.Point(7, 23);
            this.saveCustomKeysB.Name = "saveCustomKeysB";
            this.saveCustomKeysB.Size = new System.Drawing.Size(117, 23);
            this.saveCustomKeysB.TabIndex = 18;
            this.saveCustomKeysB.Text = "Save as...";
            this.saveCustomKeysB.UseVisualStyleBackColor = true;
            this.saveCustomKeysB.Click += new System.EventHandler(this.saveCustomKeysB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.openFromWar3B);
            this.groupBox1.Controls.Add(this.loadCKinfoB);
            this.groupBox1.Controls.Add(this.loadCustomKeysB);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(6, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 73);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load CustomKeys";
            // 
            // openFromWar3B
            // 
            this.openFromWar3B.ForeColor = System.Drawing.Color.Black;
            this.openFromWar3B.Location = new System.Drawing.Point(7, 48);
            this.openFromWar3B.Name = "openFromWar3B";
            this.openFromWar3B.Size = new System.Drawing.Size(117, 20);
            this.openFromWar3B.TabIndex = 23;
            this.openFromWar3B.Text = "open from war3 folder";
            this.openFromWar3B.UseVisualStyleBackColor = true;
            this.openFromWar3B.Click += new System.EventHandler(this.openFromWar3B_Click);
            // 
            // loadCKinfoB
            // 
            this.loadCKinfoB.ForeColor = System.Drawing.Color.Black;
            this.loadCKinfoB.Location = new System.Drawing.Point(130, 23);
            this.loadCKinfoB.Name = "loadCKinfoB";
            this.loadCKinfoB.Size = new System.Drawing.Size(24, 23);
            this.loadCKinfoB.TabIndex = 19;
            this.loadCKinfoB.Text = "?";
            this.loadCKinfoB.UseVisualStyleBackColor = true;
            this.loadCKinfoB.Click += new System.EventHandler(this.loadCKinfoB_Click);
            // 
            // loadCustomKeysB
            // 
            this.loadCustomKeysB.ForeColor = System.Drawing.Color.Black;
            this.loadCustomKeysB.Location = new System.Drawing.Point(7, 23);
            this.loadCustomKeysB.Name = "loadCustomKeysB";
            this.loadCustomKeysB.Size = new System.Drawing.Size(117, 23);
            this.loadCustomKeysB.TabIndex = 18;
            this.loadCustomKeysB.Text = "Open CustomKeys";
            this.loadCustomKeysB.UseVisualStyleBackColor = true;
            this.loadCustomKeysB.Click += new System.EventHandler(this.loadCustomKeysB_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "txt files|*.txt|All files|*.*";
            this.saveFileDialog.Title = "Save custom keys as...";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.Filter = "txt files|*.txt|All files|*.*";
            this.openFileDialog.Title = "Open custom keys file...";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.GreenYellow;
            this.label2.Location = new System.Drawing.Point(8, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 14);
            this.label2.TabIndex = 22;
            this.label2.Text = "NOTE: Some abilities may have interchangeable clones, which must be saved as well" +
                "";
            // 
            // noteInfoB
            // 
            this.noteInfoB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noteInfoB.ForeColor = System.Drawing.Color.Black;
            this.noteInfoB.Location = new System.Drawing.Point(444, 163);
            this.noteInfoB.Name = "noteInfoB";
            this.noteInfoB.Size = new System.Drawing.Size(17, 20);
            this.noteInfoB.TabIndex = 23;
            this.noteInfoB.Text = "?";
            this.noteInfoB.UseVisualStyleBackColor = true;
            this.noteInfoB.Click += new System.EventHandler(this.noteInfoB_Click);
            // 
            // CustomKeysForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(488, 223);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.closeB);
            this.Controls.Add(this.captionB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomKeysForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gamestate";
            this.VisibleChanged += new System.EventHandler(this.CustomKeysForm_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button captionB;
        private System.Windows.Forms.Button closeB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button loadCustomKeysB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveCustomKeysB;
        private System.Windows.Forms.ColorDialog colorDialog;
        internal System.Windows.Forms.Button hotkeyColorB;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button loadCKinfoB;
        private System.Windows.Forms.Button saveCKinfoB;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button saveToWar3B;
        private System.Windows.Forms.Button openFromWar3B;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button noteInfoB;
    }
}