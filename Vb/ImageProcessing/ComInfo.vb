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
            MAX
        End Enum

        Public Const MENU_SETTING_IMAGE_PROCESSING As String = "Image Processing"
        Public Const MENU_FILE_END As String = "End(_X)"
        Public Const BTN_OK As String = "btnOk"
        Public Const BTN_CANCEL As String = "btnCancel"
        Public Const IMG_NAME_EDGE_DETECTION As String = "EdgeDetection"
        Public Const IMG_NAME_GRAY_SCALE As String = "GrayScale"
    End Class
End Namespace