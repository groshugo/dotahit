using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.IO;

namespace DotaHIT
{
    static class Program
    {        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {                        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);           
            //Application.Run(new DotaHIT.Extras.ReplayParserForm());
            //return;
            //args = new string[1];
            //args[0] = @"D:\Bob\Programming\WorkSpace\DotaHIT\DotaHAB\bin\Release\Anti-Mage test.dhb";

            if (args.Length > 0)
            {
                string filename = args[0];

                if (!File.Exists(filename))
                    MessageBox.Show("File '" + filename + "' does not exists", "DotA H.I.T.");
                else
                {
                    switch (Path.GetExtension(filename).ToLower())
                    {
                        case ".w3x":
                        case ".dhb":
                        case ".w3g":
                            Application.Run(new MainForm(filename));
                            break;                        
                        default:
                            MessageBox.Show("Unknown file extenstion", "DotA H.I.T.");
                            Application.Run(new MainForm(true));
                            break;
                    }
                    return;
                }
            }

            try
            {
                Application.Run(new MainForm(true));
            }
            catch (Exception e)
            {
                Form f = new Form();
                f.Text = "Error report";
                f.Width = 400;
                f.StartPosition = FormStartPosition.CenterScreen;
                TextBox tb = new TextBox();
                tb.Multiline = true;
                tb.ScrollBars = ScrollBars.Vertical;
                tb.Text = "Error message: " + e.Message + "\r\n\r\nStackTrace:\r\n" + e.StackTrace;
                f.Controls.Add(tb);
                tb.Dock = DockStyle.Fill;
                Application.Run(f);
            }
        }
    }    
}