using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ComSaveFileDialog : ComFileDialog
    {
        private Stream m_stream;
        private bool m_bCreateStream;

        public Stream Stream
        {
            get { return m_stream; }
        }

        public bool CreateStream
        {
            set { m_bCreateStream = value; }
            get { return m_bCreateStream; }
        }

        public ComSaveFileDialog(bool _bCreateStream = false) : base()
        {
            m_bCreateStream = _bCreateStream;
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
                try
                {
                    if (m_bCreateStream)
                    {
                        m_stream = saveDialog.OpenFile();
                    }
                }
                catch(Exception)
                {
                    bRst = false;
                    return bRst;
                }
                bRst = true;
            }

            return bRst;
        }
    }
}