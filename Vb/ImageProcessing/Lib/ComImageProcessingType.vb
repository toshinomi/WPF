''' <summary>
''' 画像処理設定のロジック
''' </summary>
Public Class ComImageProcessingType
    Private m_Id As Integer
    Private m_Name As String

    ''' <summary>
    ''' ID
    ''' </summary>
    Public Property Id() As Integer
        Get
            Return m_Id
        End Get
        Set(ByVal value As Integer)
            m_Id = value
        End Set
    End Property

    ''' <summary>
    ''' 名称
    ''' </summary>
    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="_nId">ID</param>
    ''' <param name="_strNmae">名称</param>
    Public Sub New(ByVal _nId As Integer, ByVal _strNmae As String)
        m_Id = _nId
        m_Name = _strNmae
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' クローン
    ''' </summary>
    ''' <returns>画像処理設定のクローン</returns>
    Public Function Clone() As Object
        Return MemberwiseClone()
    End Function

End Class
