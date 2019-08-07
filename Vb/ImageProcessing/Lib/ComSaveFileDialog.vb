Imports System.IO
Imports System.Text
Imports Microsoft.Win32

Namespace ImageProcessing
    Public Class ComSaveFileDialog
        Protected m_saveFileDialog As SaveFileDialog

        Public Property FileName() As String
            Set(value As String)
                m_saveFileDialog.FileName = value
            End Set
            Get
                Return m_saveFileDialog.FileName
            End Get
        End Property

        Public Property InitialDirectory() As String
            Set(value As String)
                m_saveFileDialog.InitialDirectory = value
            End Set
            Get
                Return m_saveFileDialog.InitialDirectory
            End Get
        End Property

        Public Property Filter() As String
            Set(value As String)
                m_saveFileDialog.Filter = value
            End Set
            Get
                Return m_saveFileDialog.Filter
            End Get
        End Property

        Public Property FilterIndex() As Integer
            Set(value As Integer)
                m_saveFileDialog.FilterIndex = value
            End Set
            Get
                Return m_saveFileDialog.FilterIndex
            End Get
        End Property

        Public Property Title() As String
            Set(value As String)
                m_saveFileDialog.Title = value
            End Set
            Get
                Return m_saveFileDialog.Title
            End Get
        End Property

        Public Property CheckFileExists() As Boolean
            Set(value As Boolean)
                m_saveFileDialog.CheckFileExists = value
            End Set
            Get
                Return m_saveFileDialog.CheckFileExists
            End Get
        End Property

        Public Property CheckPathExists() As Boolean
            Set(value As Boolean)
                m_saveFileDialog.CheckPathExists = value
            End Set
            Get
                Return m_saveFileDialog.CheckPathExists
            End Get
        End Property

        Public Sub New()
            m_saveFileDialog = New SaveFileDialog()
        End Sub

        Protected Overloads Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Function ShowDialog() As Boolean
            Dim bRst As Boolean = False

            If (m_saveFileDialog.ShowDialog() = True) Then
                bRst = True
            End If

            Return bRst
        End Function

        Public Function SreamWrite(_str As String)
            Dim stream As Stream
            Dim bRst As Boolean = True
            Try
                stream = m_saveFileDialog.OpenFile()
            Catch ex As Exception
                bRst = False
                Return bRst
            End Try
            Dim streamWriter As StreamWriter = New StreamWriter(stream, Encoding.GetEncoding("UTF-8"))
            streamWriter.Write(_str)
            streamWriter.Close()
            stream.Close()

            Return bRst
        End Function
    End Class
End Namespace
