Imports LiveCharts
Imports System.Runtime.InteropServices.Marshal
Imports ImageProcessing.ImageProcessing
Imports LiveCharts.Wpf
Imports System.Text

Namespace ImageProcessing
    Public Class ComInfo
        Public Enum Pixel
            B = 0
            G
            R
            A
        End Enum

        Public Enum ImgType
            EdgeDetection = 0
            GrayScale
            Binarization
            GrayScale2Diff
            ColorReversal
            MAX
        End Enum

        Public Enum ImgDataType
            Jpg = 0
            Png
            MAX
        End Enum

        Public Enum PictureType
            Original = 0
            After
            MAX
        End Enum

        Public Const RGB_MAX As Short = 256

        Public Const MENU_SETTING_IMAGE_PROCESSING As String = "Image Processing"
        Public Const MENU_FILE_END As String = "End(_X)"
        Public Const MENU_SAVE_FILE As String = "Save Csv..."
        Public Const BTN_OK As String = "btnOk"
        Public Const BTN_CANCEL As String = "btnCancel"
        Public Const IMG_NAME_EDGE_DETECTION As String = "EdgeDetection"
        Public Const IMG_NAME_GRAY_SCALE As String = "GrayScale"
        Public Const IMG_NAME_BINARIZATION As String = "Binarization"
        Public Const IMG_NAME_GRAY_SCALE_2DIFF As String = "GrayScale 2Diff"
        Public Const IMG_NAME_COLOR_REVERSAL = "ColorReversal"
    End Class

    Public Class GraphData
        Private m_seriesCollection As SeriesCollection
        Public Property seriesCollection() As SeriesCollection
            Get
                Return m_seriesCollection
            End Get
            Set(ByVal value As SeriesCollection)
                m_seriesCollection = value
            End Set
        End Property
    End Class
End Namespace