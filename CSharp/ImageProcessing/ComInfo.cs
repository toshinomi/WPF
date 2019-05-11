using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ComInfo
    {
        public enum Pixel
        {
            B = 0,
            G,
            R,
            A,
            MAX,
        };

        public enum ImgType
        {
            EdgeDetection = 0,
            MAX,
        }
    }
}