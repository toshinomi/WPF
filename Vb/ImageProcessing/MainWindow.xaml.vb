Imports System.Threading
Imports ImageProcessing.ImageProcessing
Imports Microsoft.Win32

Class MainWindow
    Private m_bitmap As BitmapImage
    Private m_edgeDetection As EdgeDetection
    Private m_strOpenFileName As String
    Private m_tokenSource As CancellationTokenSource
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        btnFileSelect.IsEnabled = True
        btnAllClear.IsEnabled = True
        btnStart.IsEnabled = False
        btnStop.IsEnabled = False

        m_bitmap = Nothing
        m_tokenSource = Nothing
        m_edgeDetection = Nothing

    End Sub

    Protected Overloads Sub Finalize()
        MyBase.Finalize()

        m_bitmap = Nothing
        m_tokenSource = Nothing
    End Sub

    Public Sub SetButtonEnable()
        btnFileSelect.IsEnabled = True
        btnAllClear.IsEnabled = True
        btnStart.IsEnabled = True
        btnStop.IsEnabled = False

        Return
    End Sub

    Public Sub SetTextTime(ByVal _lTime As Long)
        textBoxTime.Text = _lTime.ToString()

        Return
    End Sub

    Private Sub OnClickBtnFileSelect(sender As Object, e As RoutedEventArgs)
        Dim openFileDlg As OpenFileDialog = New OpenFileDialog()

        openFileDlg.FileName = "default.jpg"
        openFileDlg.InitialDirectory = "C:\"
        openFileDlg.Filter = "All Files(*.*)|*.*"
        openFileDlg.FilterIndex = 1
        openFileDlg.Title = "Please select a file to open"
        openFileDlg.RestoreDirectory = True
        openFileDlg.CheckFileExists = True
        openFileDlg.CheckPathExists = True

        If (openFileDlg.ShowDialog() = True) Then
            pictureBoxOriginal.Source = Nothing
            pictureBoxAfter.Source = Nothing
            m_strOpenFileName = openFileDlg.FileName
            Try
                LoadImage()
            Catch ex As Exception
                MessageBox.Show(Me, "Open File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End Try
            pictureBoxOriginal.Source = m_bitmap
            btnStart.IsEnabled = True
            textBoxTime.Text = ""
        End If
        Return
    End Sub

    Public Sub LoadImage()
        m_bitmap = New BitmapImage()
        m_bitmap.BeginInit()
        m_bitmap.UriSource = New Uri(m_strOpenFileName)
        m_bitmap.EndInit()
        m_bitmap.Freeze()

        m_edgeDetection = New EdgeDetection(m_bitmap)

        Return
    End Sub

    Private Sub OnClickBtnAllClear(sender As Object, e As RoutedEventArgs)
        pictureBoxOriginal.Source = Nothing
        pictureBoxAfter.Source = Nothing

        m_bitmap = Nothing
        m_strOpenFileName = ""

        textBoxTime.Text = ""

        btnFileSelect.IsEnabled = True
        btnAllClear.IsEnabled = True
        btnStart.IsEnabled = False

        Return
    End Sub

    Private Async Sub OnClickBtnStart(sender As Object, e As RoutedEventArgs)
        pictureBoxAfter.Source = Nothing

        btnFileSelect.IsEnabled = False
        btnAllClear.IsEnabled = False
        btnStart.IsEnabled = False

        textBoxTime.Text = ""

        LoadImage()

        Dim Stopwatch As Stopwatch = New Stopwatch()
        Stopwatch.Start()
        btnStop.IsEnabled = True
        Dim bResult As Boolean = Await TaskWorkImageProcessing()
        If (bResult) Then
            Dim bitmap As BitmapImage = New BitmapImage()
            bitmap.BeginInit()
            bitmap.UriSource = New Uri(m_strOpenFileName)
            bitmap.EndInit()
            pictureBoxOriginal.Source = bitmap
            pictureBoxAfter.Source = m_edgeDetection.GetBitmap()

            Stopwatch.Stop()

            Dispatcher.Invoke(New Action(Of Long)(AddressOf SetTextTime), Stopwatch.ElapsedMilliseconds)
        End If
        Dispatcher.Invoke(New Action(AddressOf SetButtonEnable))

        Stopwatch = Nothing
        m_tokenSource = Nothing
        m_bitmap = Nothing

        Return
    End Sub

    Public Async Function TaskWorkImageProcessing() As Task(Of Boolean)
        m_tokenSource = New CancellationTokenSource()
        Dim token As CancellationToken = m_tokenSource.Token
        Dim bRst = Await Task.Run(Function() m_edgeDetection.GoEdgeDetection(token))
        Return bRst
    End Function

    Private Sub OnClickBtnStop(sender As Object, e As RoutedEventArgs)
        If (m_tokenSource IsNot Nothing) Then
            m_tokenSource.Cancel()
        End If

        Return
    End Sub

    Private Sub OnClosingWindow(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If (m_tokenSource IsNot Nothing) Then
            e.Cancel = True
        End If

        Return
    End Sub
End Class
