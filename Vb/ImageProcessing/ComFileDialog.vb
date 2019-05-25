Namespace ImageProcessing
    Public MustInherit Class ComFileDialog
        Protected m_strFileName As String
        Protected m_strInitialDirectory As String
        Protected m_strFilter As String
        Protected m_nFilterIndex As Int32
        Protected m_strTitle As String
        Protected m_bCheckFileExists As Boolean
        Protected m_bCheckPathExists As Boolean
        Protected m_strFilePass As String

        Public Property FileName() As String
            Set(value As String)
                m_strFileName = value
            End Set
            Get
                Return m_strFileName
            End Get
        End Property

        Public Property InitialDirectory() As String
            Set(value As String)
                m_strInitialDirectory = value
            End Set
            Get
                Return m_strInitialDirectory
            End Get
        End Property

        Public Property Filter() As String
            Set(value As String)
                m_strFilter = value
            End Set
            Get
                Return m_strFilter
            End Get
        End Property

        Public Property FilterIndex() As Int32
            Set(value As Int32)
                m_nFilterIndex = value
            End Set
            Get
                Return m_nFilterIndex
            End Get
        End Property

        Public Property Title() As String
            Set(value As String)
                m_strTitle = value
            End Set
            Get
                Return m_strTitle
            End Get
        End Property

        Public Property CheckFileExists() As Boolean
            Set(value As Boolean)
                m_bCheckFileExists = value
            End Set
            Get
                Return m_bCheckFileExists
            End Get
        End Property

        Public Property CheckPathExists() As Boolean
            Set(value As Boolean)
                m_bCheckPathExists = value
            End Set
            Get
                Return m_bCheckPathExists
            End Get
        End Property

        Public Property FilePass() As String
            Set(value As String)
                m_strFilePass = value
            End Set
            Get
                Return m_strFilePass
            End Get
        End Property

        Public Sub New()
            m_strFileName = "default.jpg"
            m_strInitialDirectory = "C:\"
            m_strFilter = "All File(*.*)|*.* "
            m_nFilterIndex = 1
            m_strTitle = ""
            m_bCheckFileExists = False
            m_bCheckPathExists = True
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public MustOverride Function ShowDialog() As Boolean
    End Class
End Namespace