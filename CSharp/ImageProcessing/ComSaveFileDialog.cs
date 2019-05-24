using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ComSaveFileDialog : ComFileDialog
    {
        public ComSaveFileDialog() : base()
        {
        }

        ~ComSaveFileDialog()
        {
        }

        public override bool ShowDialog()
        {
            bool bRst = false;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = base.FileName;
            saveDialog.InitialDirectory = base.InitialDirectory;
            saveDialog.Filter = base.Filter;
            saveDialog.FilterIndex = base.FilterIndex;
            saveDialog.Title = base.Title;
            saveDialog.CheckFileExists = base.CheckFileExists;
            saveDialog.CheckPathExists = base.CheckPathExists;
            if (saveDialog.ShowDialog() == true)
            {
                base.m_strFilePass = saveDialog.FileName;
                bRst = true;
            }

            return bRst;
        }
    }
}