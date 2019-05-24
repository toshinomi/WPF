using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    abstract public class ComFileDialog
    {
        protected string m_strFileName;
        protected string m_strInitialDirectory;
        protected string m_strFilter;
        protected int m_nFilterIndex;
        protected string m_strTitle;
        protected bool m_bCheckFileExists;
        protected bool m_bCheckPathExists;
        protected string m_strFilePass;

        public string FileName
        {
            set { m_strFileName = value; }
            get { return m_strFileName; }
        }

        public string InitialDirectory
        {
            set { m_strInitialDirectory = value; }
            get { return m_strInitialDirectory; }
        }

        public string Filter
        {
            set { m_strFilter = value; }
            get { return m_strFilter; }
        }

        public int FilterIndex
        {
            set { m_nFilterIndex = value; }
            get { return m_nFilterIndex; }
        }

        public string Title
        {
            set { m_strTitle = value; }
            get { return m_strTitle; }
        }

        public bool CheckFileExists
        {
            set { m_bCheckFileExists = value; }
            get { return m_bCheckFileExists; }
        }

        public bool CheckPathExists
        {
            set { m_bCheckPathExists = value; }
            get { return m_bCheckPathExists; }
        }

        public string FilePass
        {
            set { m_strFilePass = value; }
            get { return m_strFilePass; }
        }

        public ComFileDialog()
        {
            m_strFileName = "default.jpg";
            m_strInitialDirectory = @"C:\";
            m_strFilter = "All File(*.*)|*.* ";
            m_nFilterIndex = 1;
            m_strTitle = "";
            m_bCheckFileExists = false;
            m_bCheckPathExists = true;
        }

        ~ComFileDialog()
        {
        }

        abstract public bool ShowDialog();
    }
}