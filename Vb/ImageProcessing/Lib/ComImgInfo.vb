''' <summary>
''' 画像処理の設定管理のロジック
''' </summary>
Public Class ComImgInfo
    Private m_strCurImgName As String
    Private m_edgeDetectoinInfo As EdgeDetectionInfo
    Private m_grayScaleInfo As GrayScaleInfo
    Private m_binarizationInfo As BinarizationInfo
    Private m_grayScale2DiffInfo As GrayScale2DiffInfo
    Private m_colorReversalInfo As ColorReversalInfo

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
    Public Property EdgeDetectionInfo() As EdgeDetectionInfo
        Get
            Return m_edgeDetectoinInfo
        End Get
        Set(ByVal value As EdgeDetectionInfo)
            m_edgeDetectoinInfo = value
        End Set
    End Property

    ''' <summary>
    ''' グレースケールの設定
    ''' </summary>
    Public Property GrayScaleInfo() As GrayScaleInfo
        Get
            Return m_grayScaleInfo
        End Get
        Set(ByVal value As GrayScaleInfo)
            m_grayScaleInfo = value
        End Set
    End Property

    ''' <summary>
    ''' 2値化の設定
    ''' </summary>
    Public Property BinarizationInfo() As BinarizationInfo
        Get
            Return m_binarizationInfo
        End Get
        Set(ByVal value As BinarizationInfo)
            m_binarizationInfo = value
        End Set
    End Property

    ''' <summary>
    ''' グレースケール2次微分の設定
    ''' </summary>
    Public Property GrayScale2DiffInfo() As GrayScale2DiffInfo
        Get
            Return m_grayScale2DiffInfo
        End Get
        Set(ByVal value As GrayScale2DiffInfo)
            m_grayScale2DiffInfo = value
        End Set
    End Property

    ''' <summary>
    ''' 色反転の設定
    ''' </summary>
    Public Property ColorReversalInfo() As ColorReversalInfo
        Get
            Return m_colorReversalInfo
        End Get
        Set(value As ColorReversalInfo)
            m_colorReversalInfo = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        m_edgeDetectoinInfo = New EdgeDetectionInfo()
        m_grayScaleInfo = New GrayScaleInfo()
        m_binarizationInfo = New BinarizationInfo()
        m_grayScale2DiffInfo = New GrayScale2DiffInfo()
        m_colorReversalInfo = New ColorReversalInfo()
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
Public Class EdgeDetectionInfo
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
Public Class GrayScaleInfo
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
Public Class BinarizationInfo
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
Public Class GrayScale2DiffInfo
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
Public Class ColorReversalInfo
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