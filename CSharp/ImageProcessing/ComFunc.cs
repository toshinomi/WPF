using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ComFunc
    {
        static public byte DoubleToByte(double _dValue)
        {
            byte byteCnvValue = 0;

            if (_dValue > 255.0)
            {
                byteCnvValue = 255;
            }
            else if (_dValue < 0)
            {
                byteCnvValue = 0;
            }
            else
            {
                byteCnvValue = (byte)_dValue;
            }

            return byteCnvValue;
        }
    }
}