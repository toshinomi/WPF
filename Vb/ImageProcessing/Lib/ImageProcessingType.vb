Public Class ImageProcessingType
    Private m_Id As Integer
    Private m_Name As String

    Public Property Id() As Integer
        Get
            Return m_Id
        End Get
        Set(ByVal value As Integer)
            m_Id = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal _Id As Integer, ByVal _Nmae As String)
        m_Id = _Id
        m_Name = _Nmae
    End Sub

    Public Function Clone() As Object
        Return MemberwiseClone()
    End Function

End Class
