Imports LiveCharts
Imports LiveCharts.Wpf
Imports OxyPlot

''' <summary>
''' ライブチャートのヒストグラム表示のロジック
''' </summary>
Public Class ComHistgramLiveCharts : Inherits ComCharts
    ''' <summary>
    ''' ヒストグラム用の2次元配列データ　配列の1次元：オリジナルのデータ、配列の2次元：画像処理後のデータ
    ''' </summary>
    Public ReadOnly Property Histgram() As Integer(,)
        Get
            Return MyBase.m_nHistgram
        End Get
    End Property

    ''' <summary>
    ''' ビットマップ
    ''' </summary>
    Public Property Bitmap() As BitmapImage
        Set(value As BitmapImage)
            MyBase.m_bitmap = value
        End Set
        Get
            Return MyBase.m_bitmap
        End Get
    End Property

    ''' <summary>
    ''' Writeableなビットマップ
    ''' </summary>
    Public Property WBitmap() As WriteableBitmap
        Set(value As WriteableBitmap)
            MyBase.m_wbitmap = value
        End Set
        Get
            Return MyBase.m_wbitmap
        End Get
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' ヒストグラムの描画データ処理
    ''' </summary>
    ''' <returns>ヒストグラムのデータ</returns>
    Public Function DrawHistgram() As GraphData
        Dim graphData = New GraphData()

        MyBase.InitHistgram()

        MyBase.CalHistgram()

        Dim chartValue1 = New ChartValues(Of Integer)
        Dim chartValue2 = New ChartValues(Of Integer)

        For nIdx As Integer = 0 To (MyBase.m_nHistgram.Length >> 1) - 1 Step 1
            chartValue1.Add(MyBase.m_nHistgram(ComInfo.PictureType.Original, nIdx))
            If (m_wbitmap Is Nothing) Then
                chartValue2.Add(0)
            Else
                chartValue2.Add(MyBase.m_nHistgram(ComInfo.PictureType.After, nIdx))
            End If
        Next

        Dim seriesCollection = New SeriesCollection()

        Dim lineSeriesChart1 = New LineSeries() With
        {
            .Values = chartValue1,
            .Title = "Original Image"
        }
        seriesCollection.Add(lineSeriesChart1)

        Dim lineSeriesChart2 = New LineSeries() With
        {
            .Values = chartValue2,
            .Title = "After Image"
        }
        seriesCollection.Add(lineSeriesChart2)

        graphData.seriesCollection = seriesCollection

        Return graphData

    End Function
End Class