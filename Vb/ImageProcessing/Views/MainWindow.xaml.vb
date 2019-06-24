Imports System.IO
Imports System.Threading
Imports ImageProcessing.ImageProcessing
Imports Microsoft.Win32

Class MainWindow
    Private m_bitmap As BitmapImage
    Private m_imgProc As Object
    Private m_strOpenFileName As String
    Private m_tokenSource As CancellationTokenSource
    Private m_strCurImgName As String
    Private m_histgram As Histgram

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        btnFileSelect.IsEnabled = True
        btnAllClear.IsEnabled = True
        btnStart.IsEnabled = False
        btnStop.IsEnabled = False
        btnSaveImage.IsEnabled = False

        m_bitmap = Nothing
        m_tokenSource = Nothing
        m_imgProc = Nothing

        m_strCurImgName = My.Settings.ImgTypeSelectName
        Title = "Image Processing ( " + m_strCurImgName + " )"

        canvasBinarization.IsEnabled = If(m_strCurImgName = ComInfo.IMG_NAME_BINARIZATION, True, False)
    End Sub

    Protected Overloads Sub Finalize()
        MyBase.Finalize()

        m_bitmap = Nothing
        m_tokenSource = Nothing
        m_imgProc = Nothing
    End Sub

    Public Function SelectLoadImage(_strImgName As String) As Boolean
        Dim bRst As Boolean = True
        Select Case _strImgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                m_imgProc = New EdgeDetection(m_bitmap)
            Case ComInfo.IMG_NAME_GRAY_SCALE
                m_imgProc = New GrayScale(m_bitmap)
            Case ComInfo.IMG_NAME_BINARIZATION
                m_imgProc = New Binarization(m_bitmap)
            Case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF
                m_imgProc = New GrayScale2Diff(m_bitmap)
            Case ComInfo.IMG_NAME_COLOR_REVERSAL
                m_imgProc = New ColorReversal(m_bitmap)
            Case Else
        End Select

        Return bRst
    End Function

    Public Function SelectGetBitmap(_strImgName As String) As WriteableBitmap
        Dim wBitmap As WriteableBitmap = Nothing

        Select Case _strImgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                Dim edge As EdgeDetection = DirectCast(m_imgProc, EdgeDetection)
                wBitmap = edge.WriteableBitmap
            Case ComInfo.IMG_NAME_GRAY_SCALE
                Dim gray As GrayScale = DirectCast(m_imgProc, GrayScale)
                wBitmap = gray.WriteableBitmap
            Case ComInfo.IMG_NAME_BINARIZATION
                Dim binarization As Binarization = DirectCast(m_imgProc, Binarization)
                wBitmap = binarization.WriteableBitmap
            Case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF
                Dim gray2Diff As GrayScale2Diff = DirectCast(m_imgProc, GrayScale2Diff)
                wBitmap = gray2Diff.WriteableBitmap
            Case ComInfo.IMG_NAME_COLOR_REVERSAL
                Dim colorReversal As ColorReversal = DirectCast(m_imgProc, ColorReversal)
                wBitmap = colorReversal.WriteableBitmap
            Case Else
        End Select

        Return wBitmap
    End Function

    Public Function SelectGoImgProc(_comImgInfo As ComImgInfo, _token As CancellationToken) As Boolean
        Dim bRst As Boolean = True
        Select Case _comImgInfo.CurImgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                Dim edge As EdgeDetection = DirectCast(m_imgProc, EdgeDetection)
                bRst = edge.GoImgProc(_token)
            Case ComInfo.IMG_NAME_GRAY_SCALE
                Dim gray As GrayScale = DirectCast(m_imgProc, GrayScale)
                bRst = gray.GoImgProc(_token)
            Case ComInfo.IMG_NAME_BINARIZATION
                Dim binarization As Binarization = DirectCast(m_imgProc, Binarization)
                binarization.Thresh = _comImgInfo.ComBinarizationInfo.Thresh
                bRst = binarization.GoImgProc(_token)
            Case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF
                Dim gray2Diff As GrayScale2Diff = DirectCast(m_imgProc, GrayScale2Diff)
                bRst = gray2Diff.GoImgProc(_token)
            Case ComInfo.IMG_NAME_COLOR_REVERSAL
                Dim colorReversal As ColorReversal = DirectCast(m_imgProc, ColorReversal)
                bRst = colorReversal.GoImgProc(_token)
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
        Dim openFileDlg As ComOpenFileDialog = New ComOpenFileDialog()
        openFileDlg.Filter = "JPG|*.jpg|PNG|*.png"
        openFileDlg.Title = "Open the file"
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

            If (m_histgram Is Nothing) Then
                m_histgram = New Histgram()
            End If

            m_histgram.Bitmap = m_bitmap
            m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName)
            If (m_histgram.IsOpen = True) Then
                m_histgram.DrawHistgram()
            End If
        End If
            Return
    End Sub

    Public Sub LoadImage()
        m_bitmap = New BitmapImage()
        m_bitmap.BeginInit()
        m_bitmap.UriSource = New Uri(m_strOpenFileName)
        m_bitmap.EndInit()
        m_bitmap.Freeze()

        SelectLoadImage(m_strCurImgName)

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
        btnSaveImage.IsEnabled = False

        If (m_histgram IsNot Nothing) Then
            m_histgram.Close()
        End If

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
        btnSaveImage.IsEnabled = False
        btnShowHistgram.IsEnabled = False
        Dim bResult As Boolean = Await TaskWorkImageProcessing()
        If (bResult) Then
            Dim bitmap As BitmapImage = New BitmapImage()
            bitmap.BeginInit()
            bitmap.UriSource = New Uri(m_strOpenFileName)
            bitmap.EndInit()
            pictureBoxOriginal.Source = bitmap
            pictureBoxAfter.Source = SelectGetBitmap(m_strCurImgName)

            Stopwatch.Stop()

            Dispatcher.Invoke(New Action(Of Long)(AddressOf SetTextTime), Stopwatch.ElapsedMilliseconds)
            btnSaveImage.IsEnabled = True

            m_histgram.Bitmap = m_bitmap
            m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName)
            If (m_histgram.IsOpen = True) Then
                m_histgram.DrawHistgram()
            End If
        End If
        Dispatcher.Invoke(New Action(AddressOf SetButtonEnable))
        menuMain.IsEnabled = True
        btnShowHistgram.IsEnabled = True

        Stopwatch = Nothing
        m_tokenSource = Nothing

        Return
    End Sub

    Public Async Function TaskWorkImageProcessing() As Task(Of Boolean)
        m_tokenSource = New CancellationTokenSource()
        Dim token As CancellationToken = m_tokenSource.Token
        Dim imgInfo As ComImgInfo = New ComImgInfo()
        Dim binarizationInfo As ComBinarizationInfo = New ComBinarizationInfo()
        binarizationInfo.Thresh = CByte(sliderThresh.Value)
        imgInfo.CurImgName = m_strCurImgName
        imgInfo.ComBinarizationInfo = binarizationInfo
        Dim bRst = Await Task.Run(Function() SelectGoImgProc(imgInfo, token))
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

        If (m_histgram IsNot Nothing) Then
            m_histgram.Close()
            m_histgram = Nothing
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
            Dim imgProcType As ComImageProcessingType = win.cmbBoxImageProcessingType.SelectedItem
            m_strCurImgName = imgProcType.Name
            Title = "Image Processing ( " + m_strCurImgName + " )"

            canvasBinarization.IsEnabled = If(m_strCurImgName = ComInfo.IMG_NAME_BINARIZATION, True, False)

            pictureBoxAfter.Source = Nothing
            SelectLoadImage(m_strCurImgName)
            If (m_histgram IsNot Nothing) Then
                If (m_histgram.IsOpen = True) Then
                    OnClickBtnShowHistgram(Me, Nothing)
                End If
            End If
        End If

        Return
    End Sub

    Public Function GetImage(_strImgName As String) As WriteableBitmap
        Dim bitmap As WriteableBitmap = Nothing
        Select Case _strImgName
            Case ComInfo.IMG_NAME_EDGE_DETECTION
                Dim edge As EdgeDetection = DirectCast(m_imgProc, EdgeDetection)
                If edge IsNot Nothing Then
                    bitmap = edge.WriteableBitmap
                End If
            Case ComInfo.IMG_NAME_GRAY_SCALE
                Dim gray As GrayScale = DirectCast(m_imgProc, GrayScale)
                If gray IsNot Nothing Then
                    bitmap = gray.WriteableBitmap
                End If
            Case ComInfo.IMG_NAME_BINARIZATION
                Dim binarization As Binarization = DirectCast(m_imgProc, Binarization)
                If binarization IsNot Nothing Then
                    bitmap = binarization.WriteableBitmap
                End If
            Case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF
                Dim gray2Diff As GrayScale2Diff = DirectCast(m_imgProc, GrayScale2Diff)
                If gray2Diff IsNot Nothing Then
                    bitmap = gray2Diff.WriteableBitmap
                End If
            Case ComInfo.IMG_NAME_COLOR_REVERSAL
                Dim colorReversal As ColorReversal = DirectCast(m_imgProc, ColorReversal)
                If colorReversal IsNot Nothing Then
                    bitmap = colorReversal.WriteableBitmap
                End If
        End Select

        Return bitmap
    End Function

    Private Sub OnClickBtnSaveImage(sender As Object, e As RoutedEventArgs)
        Dim saveDialog As ComSaveFileDialog = New ComSaveFileDialog()
        saveDialog.Filter = "PNG|*.png"
        saveDialog.Title = "Save the file"
        If (saveDialog.ShowDialog() = True) Then
            Dim strFileName = saveDialog.FileName
            Using stream As FileStream = New FileStream(strFileName, FileMode.Create)
                Dim encoder As PngBitmapEncoder = New PngBitmapEncoder()
                Dim bitmap As WriteableBitmap = GetImage(m_strCurImgName)
                If bitmap IsNot Nothing Then
                    encoder.Frames.Add(BitmapFrame.Create(bitmap))
                    encoder.Save(stream)
                End If
            End Using
        End If
        Return
    End Sub

    Private Sub OnSliderPreviewMouseUp(sender As Object, e As MouseButtonEventArgs)
        If (pictureBoxAfter.Source IsNot Nothing) Then
            ParamAjust()
        End If
    End Sub

    Private Sub OnSliderPreviewKeyUp(sender As Object, e As KeyboardEventArgs)
        If (pictureBoxAfter.Source IsNot Nothing) Then
            ParamAjust()
        End If
    End Sub

    Private Async Sub ParamAjust()
        pictureBoxAfter.Source = Nothing

        btnFileSelect.IsEnabled = False
        btnAllClear.IsEnabled = False
        btnStart.IsEnabled = False
        menuMain.IsEnabled = False

        LoadImage()

        btnStop.IsEnabled = True
        btnSaveImage.IsEnabled = False
        Dim bResult As Boolean = Await TaskWorkImageProcessing()
        If (bResult = True) Then
            Dim bitmap As BitmapImage = New BitmapImage()
            bitmap.BeginInit()
            bitmap.UriSource = New Uri(m_strOpenFileName)
            bitmap.EndInit()
            pictureBoxAfter.Source = SelectGetBitmap(m_strCurImgName)

            btnSaveImage.IsEnabled = True

            m_histgram.Bitmap = m_bitmap
            m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName)
            If (m_histgram.IsOpen = True) Then
                m_histgram.DrawHistgram()
            End If
        End If
        Dispatcher.Invoke(New Action(AddressOf SetButtonEnable))
        menuMain.IsEnabled = True

        m_tokenSource = Nothing

        Return
    End Sub

    Private Sub OnClickBtnShowHistgram(sender As Object, e As RoutedEventArgs)
        If (m_bitmap Is Nothing) Then
            Return
        End If

        If (m_histgram IsNot Nothing) Then
            m_histgram.Close()
            m_histgram = Nothing
            m_histgram = New Histgram()
        End If

        m_histgram.Bitmap = m_bitmap
        m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName)
        m_histgram.DrawHistgram()
        m_histgram.IsOpen = True
        m_histgram.Show()

        Return
    End Sub
End Class