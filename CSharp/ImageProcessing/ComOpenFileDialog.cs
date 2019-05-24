using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ComOpenFileDialog : ComFileDialog
    {
        public ComOpenFileDialog() : base()
        {
        }

        ~ComOpenFileDialog()
        {
        }

        public override bool ShowDialog()
        {
            bool bRst = false;

            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.FileName = base.FileName;
            openFileDlg.InitialDirectory = base.InitialDirectory;
            openFileDlg.Filter = base.Filter;
            openFileDlg.FilterIndex = base.FilterIndex;
            openFileDlg.Title = base.Title;
            openFileDlg.CheckFileExists = base.CheckFileExists;
            openFileDlg.CheckPathExists = base.CheckPathExists;
            if (openFileDlg.ShowDialog() == true)
            {
                base.m_strFilePass = openFileDlg.FileName;
                bRst = true;
            }

            return bRst;
        }
    }
}