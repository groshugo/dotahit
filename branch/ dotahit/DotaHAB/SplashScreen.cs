using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DotaHIT
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            loadPrgB.Value = 0;
        }

        internal void ShowText(string text)
        {
            loadStateLabel.Text = text;
            this.Refresh();
        }
              
        internal void ProgressAdd(int amount)
        {            
            loadPrgB.Value = loadPrgB.Value + amount;
        }
        internal void ShowProgress(double current, double total)
        {
            loadPrgB.Value = (int)((100 / total) * current);
        }
    }
}