''' <summary>
''' 画像処理の設定管理のロジック
''' </summary>
Public Class ComImgInfo
    Private m_strCurImgName As String
    Private m_edgeDetectoinInfo As ComEdgeDetectionInfo
    Private m_grayScaleInfo As ComGrayScaleInfo
    Private m_binarizationInfo As ComBinarizationInfo
    Private m_grayScale2DiffInfo As ComGrayScale2DiffInfo
    Private m_colorReversalInfo As ComColorReversalInfo

    ''' <summary>
    ''' 現在の画像処理の名称
    ''' </summary>
    Public Property CurImgName() As String
        Get
            Return m_strCurImgName
        End Get
        Set(ByVal value As String)
            m_strCurImgName = value
        End Set
    End Property

    ''' <summary>
    ''' エッジ検出の設定
    ''' </summary>
    Public Property ComEdgeDetectionInfo() As ComEdgeDetectionInfo
        Get
            Return m_edgeDetectoinInfo
        End Get
        Set(ByVal value As ComEdgeDetectionInfo)
            m_edgeDetectoinInfo = value
        End Set
    End Property

    ''' <summary>
    ''' グレースケールの設定
    ''' </summary>
    Public Property ComGrayScaleInfo() As ComGrayScaleInfo
        Get
            Return m_grayScaleInfo
        End Get
        Set(ByVal value As ComGrayScaleInfo)
            m_grayScaleInfo = value
        End Set
    End Property

    ''' <summary>
    ''' 2値化の設定
    ''' </summary>
    Public Property ComBinarizationInfo() As ComBinarizationInfo
        Get
            Return m_binarizationInfo
        End Get
        Set(ByVal value As ComBinarizationInfo)
            m_binarizationInfo = value
        End Set
    End Property

    ''' <summary>
    ''' グレースケール2次微分の設定
    ''' </summary>
    Public Property ComGrayScale2DiffInfo() As ComGrayScale2DiffInfo
        Get
            Return m_grayScale2DiffInfo
        End Get
        Set(ByVal value As ComGrayScale2DiffInfo)
            m_grayScale2DiffInfo = value
        End Set
    End Property

    ''' <summary>
    ''' 色反転の設定
    ''' </summary>
    Public Property ComColorReversalInfo() As ComColorReversalInfo
        Get
            Return m_colorReversalInfo
        End Get
        Set(value As ComColorReversalInfo)
            m_colorReversalInfo = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        m_edgeDetectoinInfo = New ComEdgeDetectionInfo()
        m_grayScaleInfo = New ComGrayScaleInfo()
        m_binarizationInfo = New ComBinarizationInfo()
        m_grayScale2DiffInfo = New ComGrayScale2DiffInfo()
        m_colorReversalInfo = New ComColorReversalInfo()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

''' <summary>
''' エッジ検出の設定管理のロジック
''' </summary>
Public Class ComEdgeDetectionInfo
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
    End Sub
End Class

''' <summary>
''' グレースケールの設定管理のロジック
''' </summary>
Public Class ComGrayScaleInfo
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
    End Sub
End Class

''' <summary>
''' 2値化の設定管理のロジック
''' </summary>
Public Class ComBinarizationInfo
    Private m_nThresh As Byte

    ''' <summary>
    ''' 閾値
    ''' </summary>
    Public Property Thresh() As Byte
        Get
            Return m_nThresh
        End Get
        Set(ByVal value As Byte)
            m_nThresh = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
    End Sub
End Class

''' <summary>
''' グレースケール2次微分の設定管理のロジック
''' </summary>
Public Class ComGrayScale2DiffInfo
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
    End Sub
End Class

''' <summary>
''' 色反転の設定管理のロジック
''' </summary>
Public Class ComColorReversalInfo
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
    End Sub
End Class