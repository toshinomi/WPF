using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        GrayScale,
        Binarization,
        GrayScale2Diff,
        ColorReversal,
        MAX,
    }

    public enum ImgDataType
    {
        Jpg = 0,
        Png,
        MAX,
    }

    public enum PictureType
    {
        Original = 0,
        After,
        MAX,
    }

    public const short RGB_MAX = 256;

    public const string MENU_SETTING_IMAGE_PROCESSING = "Image Processing";
    public const string MENU_FILE_END = "End(_X)";
    public const string MENU_SAVE_CSV = "Save Csv...";
    public const string BTN_OK = "btnOk";
    public const string BTN_CANCEL = "btnCancel";
    public const string IMG_NAME_EDGE_DETECTION = "EdgeDetection";
    public const string IMG_NAME_GRAY_SCALE = "GrayScale";
    public const string IMG_NAME_BINARIZATION = "Binarization";
    public const string IMG_NAME_GRAY_SCALE_2DIFF = "GrayScale 2Diff";
    public const string IMG_NAME_COLOR_REVERSAL = "ColorReversal";
}

public class GraphData
{
    private SeriesCollection m_seriesCollection;
    public SeriesCollection seriesCollection
    {
        set { m_seriesCollection = value; }
        get { return m_seriesCollection; }
    }
}