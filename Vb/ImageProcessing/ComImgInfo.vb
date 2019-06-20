Namespace ImageProcessing
    Public Class ComImgInfo
        Private m_strCurImgName As String
        Private m_edgeDetectoinInfo As ComEdgeDetectionInfo
        Private m_grayScaleInfo As ComGrayScaleInfo
        Private m_binarizationInfo As ComBinarizationInfo

        Public Property CurImgName() As String
            Get
                Return m_strCurImgName
            End Get
            Set(ByVal value As String)
                m_strCurImgName = value
            End Set
        End Property

        Public Property ComEdgeDetectionInfo() As ComEdgeDetectionInfo
            Get
                Return m_edgeDetectoinInfo
            End Get
            Set(ByVal value As ComEdgeDetectionInfo)
                m_edgeDetectoinInfo = value
            End Set
        End Property

        Public Property ComGrayScaleInfo() As ComGrayScaleInfo
            Get
                Return m_grayScaleInfo
            End Get
            Set(ByVal value As ComGrayScaleInfo)
                m_grayScaleInfo = value
            End Set
        End Property

        Public Property ComBinarizationInfo() As ComBinarizationInfo
            Get
                Return m_binarizationInfo
            End Get
            Set(ByVal value As ComBinarizationInfo)
                m_binarizationInfo = value
            End Set
        End Property

        Public Sub New()
            m_edgeDetectoinInfo = New ComEdgeDetectionInfo()
            m_grayScaleInfo = New ComGrayScaleInfo()
            m_binarizationInfo = New ComBinarizationInfo()
        End Sub
    End Class

    Public Class ComEdgeDetectionInfo
        Public Sub New()
        End Sub
        Protected Overloads Sub Finalize()
        End Sub
    End Class

    Public Class ComGrayScaleInfo
        Public Sub New()
        End Sub
        Protected Overloads Sub Finalize()
        End Sub
    End Class

    Public Class ComBinarizationInfo
        Private m_nThresh As Byte

        Public Property Thresh() As Byte
            Get
                Return m_nThresh
            End Get
            Set(ByVal value As Byte)
                m_nThresh = value
            End Set
        End Property

        Public Sub New()
        End Sub
        Protected Overloads Sub Finalize()
        End Sub
    End Class
End Namespace