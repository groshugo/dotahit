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

namespace DotaHIT
{
    public partial class LogForm : Form
    {
        internal int mbX = -1;
        internal int mbY = -1;

        private UIRichText richText;

        public LogForm()
        {
            InitializeComponent();
            richText = new UIRichText(logRTB);
        }

        public void SetParent(MainForm parentForm)
        {         
            this.Owner = parentForm;         
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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | Win32Msg.WS_THICKFRAME;
                return cp;
            }
        }

        private void closeB_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void AddToLog(string s)
        {
            if (logRTB.Text.Length > 0) richText.AddText("\n\n");

            richText.AddTaggedText(s + "", UIFonts.normalVerdana, Color.White);            
        }

        public void Clear()
        {
            richText.ClearText();
        }
    }
}