Imports System.Runtime.InteropServices.Marshal

''' <summary>
''' HistgramOxyPlot.xaml の相互作用ロジック
''' </summary>
Public Class HistgramOxyPlot
    Private m_histgramChart As ComHistgramOxyPlot
    Private m_bIsOpen As Boolean

    ''' <summary>
    ''' ビットマップ
    ''' </summary>
    Public Property Bitmap() As BitmapImage
        Set(value As BitmapImage)
            m_histgramChart.Bitmap = value
        End Set
        Get
            Return m_histgramChart.Bitmap
        End Get
    End Property

    ''' <summary>
    ''' Writeableなビットマップ
    ''' </summary>
    Public Property WBitmap() As WriteableBitmap
        Set(value As WriteableBitmap)
            m_histgramChart.WBitmap = value
        End Set
        Get
            Return m_histgramChart.WBitmap
        End Get
    End Property

    ''' <summary>
    ''' Windowのオープン状態
    ''' </summary>
    Public Property IsOpen() As Boolean
        Set(value As Boolean)
            m_bIsOpen = value
        End Set
        Get
            Return m_bIsOpen
        End Get
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        m_histgramChart = New ComHistgramOxyPlot()

    End Sub

    ''' <summary>
    ''' グラフの描画
    ''' </summary>
    Public Sub DrawHistgram()
        If (chart.Model IsNot Nothing) Then
            chart.Model.Series.Clear()
            chart.Model = Nothing
        End If
        chart.Model = m_histgramChart.DrawHistgram()

        Return
    End Sub

    ''' <summary>
    ''' Windowのクローズ処理
    ''' </summary>
    ''' <param name="sender">オブジェクト</param>
    ''' <param name="e">キャンセルイベントのデータ</param>
    Private Sub OnClosingWindow(sender As Object, e As ComponentModel.CancelEventArgs)
        m_bIsOpen = False

        Return
    End Sub

    ''' <summary>
    ''' メニューのクリック
    ''' </summary>
    ''' <param name="sender">オブジェクト</param>
    ''' <param name="e">ルーティングイベントのデータ</param>
    Private Sub OnClickMenu(sender As Object, e As RoutedEventArgs)
        Dim menuItem As MenuItem = sender
        Dim strHeader As String = menuItem.Header.ToString()

        Select Case (strHeader)
            Case ComInfo.MENU_SAVE_FILE
                SaveCsv()
            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' CSVファイル保存
    ''' </summary>
    Public Sub SaveCsv()
        If (m_histgramChart.SaveCsv() = False) Then
            MessageBox.Show(Me, "Save CSV File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub
End Class