Imports System.Runtime.InteropServices.Marshal
Imports System.Text
Imports OxyPlot

Namespace ImageProcessing
    Public MustInherit Class ComCharts
        Protected m_nHistgram(ComInfo.PictureType.MAX - 1, ComInfo.RGB_MAX - 1) As Integer
        Protected m_bitmap As BitmapImage
        Protected m_wbitmap As WriteableBitmap

        Public Sub New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public MustOverride Function DrawHistgram() As GraphData
        Public MustOverride Function DrawHistgram2() As PlotModel

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

                    m_nHistgram(ComInfo.PictureType.Original, nGrayScale) += 1

                    If (m_wbitmap IsNot Nothing) Then
                        pAdr = m_wbitmap.BackBuffer
                        nPos = nIdxHeight * m_wbitmap.BackBufferStride + nIdxWidth * 4

                        nPixelB = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                        nPixelG = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                        nPixelR = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                        nGrayScale = (nPixelB + nPixelG + nPixelR) / 3

                        m_nHistgram(ComInfo.PictureType.After, nGrayScale) += 1
                    End If
                Next
            Next
        End Sub

        Public Sub InitHistgram()
            For nIdx As Integer = 0 To (m_nHistgram.Length >> 1) - 1 Step 1
                m_nHistgram(ComInfo.PictureType.Original, nIdx) = 0
                m_nHistgram(ComInfo.PictureType.After, nIdx) = 0
            Next
        End Sub

        Public Function SaveCsv() As Boolean
            Dim bRst As Boolean = True
            Dim saveDialog As ComSaveFileDialog = New ComSaveFileDialog()
            saveDialog.Filter = "CSV|*.csv"
            saveDialog.Title = "Save the csv file"
            saveDialog.FileName = "default.csv"
            If (saveDialog.ShowDialog() = True) Then
                Dim strDelmiter As String = ","
                Dim stringBuilder As StringBuilder = New StringBuilder()
                For nIdx As Integer = 0 To (m_nHistgram.Length >> 1) - 1
                    stringBuilder.Append(nIdx).Append(strDelmiter)
                    stringBuilder.Append(m_nHistgram(0, nIdx)).Append(strDelmiter)
                    stringBuilder.Append(m_nHistgram(1, nIdx)).Append(strDelmiter)
                    stringBuilder.Append(Environment.NewLine)
                Next
                If (saveDialog.SreamWrite(stringBuilder.ToString()) = False) Then
                    bRst = False
                End If
            End If

            Return bRst
        End Function
    End Class
End Namespace
