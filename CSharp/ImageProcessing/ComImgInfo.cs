using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ComImgInfo
    {
        private string m_strCurImgName;
        private ComBinarizationInfo m_BinarizationInfo;

        public string CurImgName
        {
            set { m_strCurImgName = value; }
            get { return m_strCurImgName; }
        }

        public ComBinarizationInfo BinarizationInfo
        {
            set { m_BinarizationInfo = value; }
            get { return m_BinarizationInfo; }
        }

        public ComImgInfo()
        {
        }

        ~ComImgInfo()
        {
        }
    }
    public class ComBinarizationInfo
    {
        private byte m_byteThresh;
        public byte Thresh
        {
            set { m_byteThresh = value; }
            get { return m_byteThresh; }
        }

        public ComBinarizationInfo()
        {
        }

        ~ComBinarizationInfo()
        {
        }
    }
}