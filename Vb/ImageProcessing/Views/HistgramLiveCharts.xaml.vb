Imports LiveCharts
Imports System.Runtime.InteropServices.Marshal
Imports ImageProcessing.ImageProcessing
Imports LiveCharts.Wpf
Imports System.Text

Public Class HistgramLiveCharts
    Private m_histramgChart As ComHistgramLiveCharts
    Private m_bIsOpen As Boolean

    Public Property Bitmap() As BitmapImage
        Set(value As BitmapImage)
            m_histramgChart.Bitmap = value
        End Set
        Get
            Return m_histramgChart.Bitmap
        End Get
    End Property

    Public Property WBitmap() As WriteableBitmap
        Set(value As WriteableBitmap)
            m_histramgChart.WBitmap = value
        End Set
        Get
            Return m_histramgChart.WBitmap
        End Get
    End Property

    Public Property IsOpen() As Boolean
        Set(value As Boolean)
            m_bIsOpen = value
        End Set
        Get
            Return m_bIsOpen
        End Get
    End Property

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        m_histramgChart = New ComHistgramLiveCharts()

    End Sub

    Public Sub DrawHistgram()
        Me.DataContext = m_histramgChart.DrawHistgram()

        Dim graphData As GraphData = New GraphData()

        Return
    End Sub

    Private Sub OnClosingWindow(sender As Object, e As ComponentModel.CancelEventArgs)
        m_bIsOpen = False

        Return
    End Sub

    Private Sub OnClickMenu(sender As Object, e As RoutedEventArgs)
        Dim menuItem As MenuItem = sender
        Dim strHeader As String = menuItem.Header.ToString()

        Select Case (strHeader)
            Case ComInfo.MENU_SAVE_FILE
                SaveCsv()
            Case Else
        End Select
    End Sub

    Public Sub SaveCsv()
        If (m_histramgChart.SaveCsv() = False) Then
            MessageBox.Show(Me, "Save CSV File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub
End Class