using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.Core;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.DatabaseModel.Abilities;
using DotaHIT.DatabaseModel.Data;

namespace DotaHIT
{
    public partial class HpsConfigForm : Form
    {
        Font smallFont = new Font("Arial", 3, FontStyle.Regular);
        Font boldFont = new Font("Arial", 8, FontStyle.Bold);

        internal int mbX = -1;
        internal int mbY = -1;

        private HabProperties hpsConfig = null;

        public HpsConfigForm(string config_name,HabProperties hpsConfig)
        {       
            InitializeComponent();
            this.captionLabel.Text = config_name + " Configuration:";
            this.hpsConfig = hpsConfig;
        }

        public HpsConfigForm(string config_name,Form Owner, HabProperties hpsConfig)
        {
            InitializeComponent();            
            this.Owner = Owner;
            this.captionLabel.Text = config_name + " Configuration:";
            this.hpsConfig = hpsConfig;
            this.CenterToScreen();

            if (hpsConfig != null)
                hpsDataGridView.RowCount = hpsConfig.Count;
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public void Display()
        {
            if (this.Visible == false)
                this.Visible = true;
        }

        public void DisplayAtCursor(Point position)
        {           
            Rectangle rect = this.Bounds;            
            rect.X = position.X + Cursor.HotSpot.X;
            rect.Y = position.Y + Cursor.HotSpot.Y;

            if (Screen.PrimaryScreen.WorkingArea.Contains(rect) == false)
            {
                if ((rect.X + rect.Width) > Screen.PrimaryScreen.WorkingArea.Width)
                    rect.X -= Cursor.HotSpot.X + rect.Width;

                if ((rect.Y + rect.Height) > Screen.PrimaryScreen.WorkingArea.Height)
                    rect.Y -= Cursor.HotSpot.Y + rect.Height;

                if (rect.Y < 0)
                    rect.Y = Screen.PrimaryScreen.WorkingArea.Height - rect.Height;
                
                if (rect.X < 0)
                    rect.X = Screen.PrimaryScreen.WorkingArea.Width - rect.Width;
            }

            this.Location = rect.Location;            

            if (this.Visible == false)
                this.Visible = true;            
        }

        private void contentRTB_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            (sender as RichTextBox).Size = e.NewRectangle.Size;
        }

        private void hpsDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = "";//hpsConfig[e.RowIndex].Name;
                    break;

                case 1:
                    e.Value = "";//(hpsConfig[e.RowIndex].Value as DbUnit).Text;
                    break;
            }
        }

        private void hpsDataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            switch (e.ColumnIndex)
            {                
                case 1:
                    //(hpsConfig[e.RowIndex].Value as DbUnit).Value = e.Value;
                    break;
            }
        }

        private void captionLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mbX = MousePosition.X - this.Location.X;
                mbY = MousePosition.Y - this.Location.Y;
            }
            else
                mbX = mbY = -1;
        }

        private void captionLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mbX != -1 && mbY != -1)
                if ((MousePosition.X - mbX) != 0 && (MousePosition.Y - mbY) != 0)
                    this.SetDesktopLocation(MousePosition.X - mbX, MousePosition.Y - mbY);
        }

        private void captionLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mbX = mbY = -1;
        }        
    }
}