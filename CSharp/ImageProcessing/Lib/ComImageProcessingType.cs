using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ComImageProcessingType : ICloneable
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

        public ComImageProcessingType()
        {

        }

        public ComImageProcessingType(int _Id, string _Nmae)
        {
            m_Id = _Id;
            m_Name = _Nmae;
        }

        ~ComImageProcessingType()
        {
        }

        public object Clone()
        {
            return (ComImageProcessingType)MemberwiseClone();
        }
    }
}
