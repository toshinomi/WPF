Imports OxyPlot
Imports OxyPlot.Series

Namespace ImageProcessing
    Public Class ComHistgramOxyPlot : Inherits ComCharts
        Private m_pModel As PlotModel

        Public Property PModel() As PlotModel
            Set(value As PlotModel)
                m_pModel = value
            End Set
            Get
                Return m_pModel
            End Get
        End Property

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
            m_pModel = New PlotModel()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Function DrawHistgram() As GraphData
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DrawHistgram2() As PlotModel
            MyBase.InitHistgram()

            MyBase.CalHistgram()

            Dim dataList1 = New List(Of DataPoint)

            For nIdx As Integer = 0 To (MyBase.m_nHistgram.Length >> 1) - 1 Step 1
                Dim DataPoint = New DataPoint(nIdx, MyBase.m_nHistgram(ComInfo.PictureType.Original, nIdx))
                dataList1.Add(DataPoint)
            Next

            Dim series1 = New LineSeries() With
            {
                .Title = "Original",
                .ItemsSource = dataList1
            }
            m_pModel.Series.Add(series1)

            Dim dataList2 = New List(Of DataPoint)

            For nIdx As Integer = 0 To (MyBase.m_nHistgram.Length >> 1) - 1 Step 1
                Dim DataPoint = New DataPoint(nIdx, MyBase.m_nHistgram(ComInfo.PictureType.After, nIdx))
                dataList2.Add(DataPoint)
            Next

            Dim series2 = New LineSeries() With
            {
                .Title = "After",
                .ItemsSource = dataList2
            }
            m_pModel.Series.Add(series2)

            Return m_pModel
        End Function
    End Class
End Namespace