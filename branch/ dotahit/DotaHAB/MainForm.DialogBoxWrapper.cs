using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.DatabaseModel.Data;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.DatabaseModel.Abilities;
using DotaHIT.DatabaseModel.Upgrades;
using DotaHIT.Jass.Native.Types;
using System.Collections;
using System.CodeDom;
using DotaHIT.Core;
using DotaHIT.Core.Resources.Media;
using DotaHIT.Core.Resources;
using DotaHIT.DatabaseModel.Format;

namespace DotaHIT
{
    public partial class MainForm : Form
    {
        private OpenFileDialogBoxWrapper openFileDialogWrapper = new OpenFileDialogBoxWrapper();
        private FolderBrowserDialogBoxWrapper folderBrowserDialogWrapper = new FolderBrowserDialogBoxWrapper();
    }

    class OpenFileDialogBoxWrapper
    {        
        public OpenFileDialog openFileDialog;
        private FileBrowserForm m_fileBrowserForm;
        public FileBrowserForm fileBrowserForm
        {
            get
            {
                if (m_fileBrowserForm == null)
                    m_fileBrowserForm = new FileBrowserForm();
                return m_fileBrowserForm;
            }
        }

        public string InitialDirectory
        {
            get
            {
                return UIDialogs.UseStandardDialogs ? openFileDialog.InitialDirectory : fileBrowserForm.InitialDirectory;
            }
            set
            {
                if (UIDialogs.UseStandardDialogs)
                    openFileDialog.InitialDirectory = value;
                else
                    fileBrowserForm.InitialDirectory = value;
            }
        }        
        public string FileName
        {
            get
            {
                return UIDialogs.UseStandardDialogs ? openFileDialog.FileName : fileBrowserForm.FileName;
            }
            set
            {
                if (UIDialogs.UseStandardDialogs)
                    openFileDialog.FileName = value;
                else
                    fileBrowserForm.FileName = value;
            }
        }
        public string Filter
        {
            get
            {
                return UIDialogs.UseStandardDialogs ? openFileDialog.Filter : fileBrowserForm.Filter;
            }
            set
            {
                if (UIDialogs.UseStandardDialogs)
                    openFileDialog.Filter = value;
                else
                    fileBrowserForm.Filter = value;
            }
        }        

        public DialogResult ShowDialog()
        {
            if (UIDialogs.UseStandardDialogs) 
                return openFileDialog.ShowDialog();
            else
                return fileBrowserForm.ShowDialog();
        }
    }
    class FolderBrowserDialogBoxWrapper
    {        
        public FolderBrowserDialog folderBrowsingDialog = null;
        private FolderBrowserForm m_folderBrowserForm;
        public FolderBrowserForm folderBrowserForm
        {
            get
            {
                if (m_folderBrowserForm == null)
                    m_folderBrowserForm = new FolderBrowserForm();
                return m_folderBrowserForm;
            }
        }
        
        public string SelectedPath
        {
            get
            {
                return UIDialogs.UseStandardDialogs ? folderBrowsingDialog.SelectedPath : folderBrowserForm.SelectedPath;
            }
        }

        public DialogResult ShowDialog()
        {
            if (UIDialogs.UseStandardDialogs)
                return folderBrowsingDialog.ShowDialog();
            else
                return folderBrowserForm.ShowDialog();
        }
    }
}
