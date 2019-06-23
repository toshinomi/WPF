Imports LiveCharts
Imports System.Runtime.InteropServices.Marshal
Imports ImageProcessing.ImageProcessing
Imports LiveCharts.Wpf

Public Class Histgram
    Private m_bitmap As BitmapImage
    Private m_wbitmap As WriteableBitmap
    Private m_bIsOpen As Boolean

    Public Property Bitmap() As BitmapImage
        Set(value As BitmapImage)
            m_bitmap = value
        End Set
        Get
            Return m_bitmap
        End Get
    End Property

    Public Property WBitmap() As WriteableBitmap
        Set(value As WriteableBitmap)
            m_wbitmap = value
        End Set
        Get
            Return m_wbitmap
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

    Private m_nHistgram(1, 255) As Integer

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Public Sub DrawHistgram()
        Dim graphData As GraphData = New GraphData()

        CalHistgram()

        Dim chartValue1 = New ChartValues(Of Integer)()
        Dim chartValue2 = New ChartValues(Of Integer)()
        For nIdx As Integer = 0 To 256 - 1 Step 1
            chartValue1.Add(m_nHistgram(0, nIdx))
            If (m_wbitmap Is Nothing) Then
                chartValue2.Add(0)
            Else
                chartValue2.Add(m_nHistgram(1, nIdx))
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
        Me.DataContext = graphData
    End Sub

    Private Sub OnClosingWindow(sender As Object, e As ComponentModel.CancelEventArgs)
        m_bIsOpen = False

        Return
    End Sub

    Public Sub CalHistgram()
        Dim nWidthSize As Integer = m_bitmap.PixelWidth
        Dim nHeightSize As Integer = m_bitmap.PixelHeight

        Dim wBitmap As WriteableBitmap = New WriteableBitmap(m_bitmap)

        Dim nIdxWidth As Integer
        Dim nIdxHeight As Integer

        ReDim m_nHistgram(1, 255)

        For nIdxHeight = 0 To nHeightSize - 1 Step 1
            For nIdxWidth = 0 To nWidthSize - 1 Step 1
                Dim pAdr As IntPtr = wBitmap.BackBuffer
                Dim nPos As Integer = nIdxHeight * wBitmap.BackBufferStride + nIdxWidth * 4

                Dim nPixelB As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                Dim nPixelG As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                Dim nPixelR As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                Dim nGrayScale As Integer = (nPixelB + nPixelG + nPixelR) / 3

                m_nHistgram(0, nGrayScale) += 1

                If (m_wbitmap IsNot Nothing) Then
                    pAdr = m_wbitmap.BackBuffer
                    nPos = nIdxHeight * m_wbitmap.BackBufferStride + nIdxWidth * 4

                    nPixelB = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                    nPixelG = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                    nPixelR = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                    nGrayScale = (nPixelB + nPixelG + nPixelR) / 3

                    m_nHistgram(1, nGrayScale) += 1
                End If
            Next
        Next
    End Sub

    Private Sub OnClickMenu(sender As Object, e As RoutedEventArgs)
    End Sub

    Public Class GraphData

        Private m_seriesCollection As SeriesCollection

        Public Property seriesCollection() As SeriesCollection
            Set(value As SeriesCollection)
                m_seriesCollection = value
            End Set
            Get
                Return m_seriesCollection
            End Get
        End Property
    End Class
End Class