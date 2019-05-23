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

        public enum ImgDataType
        {
            Jpg = 0,
            Png,
            MAX,
        }

        public const string MENU_SETTING_IMAGE_PROCESSING = "Image Processing";
        public const string MENU_FILE_END = "End(_X)";
        public const string BTN_OK = "btnOk";
        public const string BTN_CANCEL = "btnCancel";
        public const string IMG_NAME_EDGE_DETECTION = "EdgeDetection";
        public const string IMG_NAME_GRAY_SCALE = "GrayScale";
    }
}