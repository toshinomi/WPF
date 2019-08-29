Imports LiveCharts
Imports LiveCharts.Wpf
Imports OxyPlot

Public Class ComHistgramLiveCharts : Inherits ComCharts
    Public ReadOnly Property Histgram() As Integer(,)
        Get
            Return MyBase.m_nHistgram
        End Get
    End Property

    Public Property Bitmap() As BitmapImage
        Set(value As BitmapImage)
            MyBase.m_bitmap = value
        End Set
        Get
            Return MyBase.m_bitmap
        End Get
    End Property

    Public Property WBitmap() As WriteableBitmap
        Set(value As WriteableBitmap)
            MyBase.m_wbitmap = value
        End Set
        Get
            Return MyBase.m_wbitmap
        End Get
    End Property

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

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