Imports System.IO
Imports System.Text
Imports Microsoft.Win32

''' <summary>
''' ファイルセーブのロジック
''' </summary>
Public Class ComSaveFileDialog
    Protected m_saveFileDialog As SaveFileDialog

    ''' <summary>
    ''' ファイル名称
    ''' </summary>
    Public Property FileName() As String
        Set(value As String)
            m_saveFileDialog.FileName = value
        End Set
        Get
            Return m_saveFileDialog.FileName
        End Get
    End Property

    ''' <summary>
    ''' ファイルダイアログに表示される初期ディレクトリ
    ''' </summary>
    Public Property InitialDirectory() As String
        Set(value As String)
            m_saveFileDialog.InitialDirectory = value
        End Set
        Get
            Return m_saveFileDialog.InitialDirectory
        End Get
    End Property

    ''' <summary>
    ''' ファイルの種類のフィルタ
    ''' </summary>
    Public Property Filter() As String
        Set(value As String)
            m_saveFileDialog.Filter = value
        End Set
        Get
            Return m_saveFileDialog.Filter
        End Get
    End Property

    ''' <summary>
    ''' 現在選択中のフィルタのインデックス
    ''' </summary>
    Public Property FilterIndex() As Integer
        Set(value As Integer)
            m_saveFileDialog.FilterIndex = value
        End Set
        Get
            Return m_saveFileDialog.FilterIndex
        End Get
    End Property

    ''' <summary>
    ''' ファイルダイアログに表示されるタイトル
    ''' </summary>
    Public Property Title() As String
        Set(value As String)
            m_saveFileDialog.Title = value
        End Set
        Get
            Return m_saveFileDialog.Title
        End Get
    End Property

    ''' <summary>
    ''' 存在しないファイルを指定した場合に警告を表示するかどうかの値
    ''' </summary>
    Public Property CheckFileExists() As Boolean
        Set(value As Boolean)
            m_saveFileDialog.CheckFileExists = value
        End Set
        Get
            Return m_saveFileDialog.CheckFileExists
        End Get
    End Property

    ''' <summary>
    ''' 無効なパスとファイルを入力した場合に警告を表示するかどうかの値
    ''' </summary>
    Public Property CheckPathExists() As Boolean
        Set(value As Boolean)
            m_saveFileDialog.CheckPathExists = value
        End Set
        Get
            Return m_saveFileDialog.CheckPathExists
        End Get
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        m_saveFileDialog = New SaveFileDialog()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' ダイアログの表示
    ''' </summary>
    ''' <returns>結果 成功/失敗</returns>
    Public Function ShowDialog() As Boolean
        Dim bRst As Boolean = False

        If (m_saveFileDialog.ShowDialog() = True) Then
            bRst = True
        End If

        Return bRst
    End Function

    ''' <summary>
    ''' ストリームの書込み
    ''' </summary>
    ''' <param name="_str">ファイル名称</param>
    ''' <returns>実行結果 成功/失敗</returns>
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