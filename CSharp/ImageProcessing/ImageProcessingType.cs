using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ImageProcessingType : ICloneable
    {
        private int m_Id;
        private string m_Name;

        public int Id
        {
            set { m_Id = value; }
            get { return m_Id; }
        }

        public string Name
        {
            set { m_Name = value; }
            get { return m_Name; }
        }

        public ImageProcessingType()
        {

        }

        public ImageProcessingType(int _Id, string _Nmae)
        {
            m_Id = _Id;
            m_Name = _Nmae;
        }

        ~ImageProcessingType()
        {
        }

        public object Clone()
        {
            return (ImageProcessingType)MemberwiseClone();
        }
    }
}
