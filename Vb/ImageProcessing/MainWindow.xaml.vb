Imports System.Threading
Imports ImageProcessing.ImageProcessing
Imports Microsoft.Win32

Class MainWindow
    Private m_bitmap As BitmapImage
    Private m_imgProc As Object
    Private m_strOpenFileName As String
    Private m_tokenSource As CancellationTokenSource
    Private m_curImgName As String

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
        m_imgProc = Nothing

        m_curImgName = My.Settings.ImgTypeSelectName
        Title = "Image Processing ( " + m_curImgName + " )"
    End Sub

    Protected Overloads Sub Finalize()
        MyBase.Finalize()

        m_bitmap = Nothing
        m_tokenSource = Nothing
        m_imgProc = Nothing
    End Sub

    Public Function SelectLoadImage(ByVal _imgName As String) As Boolean
        Dim bRst As Boolean = True
        Select Case _imgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                m_imgProc = New EdgeDetection(m_bitmap)
            Case ComInfo.IMG_NAME_GRAY_SCALE
                m_imgProc = New GrayScale(m_bitmap)
            Case Else
        End Select

        Return bRst
    End Function

    Public Function SelectGetBitmap(ByVal _imgName As String) As Boolean
        Dim bRst As Boolean = True
        Select Case _imgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                Dim edge As EdgeDetection = m_imgProc
                pictureBoxAfter.Source = edge.WriteableBitmap
            Case ComInfo.IMG_NAME_GRAY_SCALE
                Dim gray As GrayScale = m_imgProc
                pictureBoxAfter.Source = gray.WriteableBitmap
            Case Else
        End Select

        Return bRst
    End Function

    Public Function SelectGoImgProc(ByVal _imgName As String, ByVal _token As CancellationToken) As Boolean
        Dim bRst As Boolean = True
        Select Case _imgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                Dim edge As EdgeDetection = m_imgProc
                bRst = edge.GoImgProc(_token)
            Case ComInfo.IMG_NAME_GRAY_SCALE
                Dim gray As GrayScale = m_imgProc
                bRst = gray.GoImgProc(_token)
            Case Else
        End Select

        Return bRst
    End Function

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
        openFileDlg.Filter = "JPG|*.jpg|PNG|*.png"
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

        SelectLoadImage(m_curImgName)

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
        menuMain.IsEnabled = False

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
            SelectGetBitmap(m_curImgName)

            Stopwatch.Stop()

            Dispatcher.Invoke(New Action(Of Long)(AddressOf SetTextTime), Stopwatch.ElapsedMilliseconds)
        End If
        Dispatcher.Invoke(New Action(AddressOf SetButtonEnable))
        menuMain.IsEnabled = True

        Stopwatch = Nothing
        m_tokenSource = Nothing
        m_bitmap = Nothing

        Return
    End Sub

    Public Async Function TaskWorkImageProcessing() As Task(Of Boolean)
        m_tokenSource = New CancellationTokenSource()
        Dim token As CancellationToken = m_tokenSource.Token
        Dim bRst = Await Task.Run(Function() SelectGoImgProc(m_curImgName, token))
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

    Private Sub OnClickMenu(sender As Object, e As RoutedEventArgs)
        Dim menuItem As MenuItem = sender
        Dim strHeader As String = menuItem.Header.ToString()

        Select Case strHeader
            Case ComInfo.MENU_FILE_END
                Close()
            Case ComInfo.MENU_SETTING_IMAGE_PROCESSING
                ShowSettingImageProcessing()

        End Select
    End Sub

    Public Sub ShowSettingImageProcessing()
        Dim win As SettingImageProcessing = New SettingImageProcessing()
        Dim DialogResult As Boolean? = win.ShowDialog()

        If (DialogResult = True) Then
            m_curImgName = win.cmbBoxImageProcessingType.DataContext
            Title = "Image Processing ( " + m_curImgName + " )"
        End If

        Return
    End Sub
End Class
