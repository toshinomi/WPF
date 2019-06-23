Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

Namespace ImageProcessing
    Public Class GrayScale2Diff : Inherits ComImgProc

        Public Sub New(_bitmap As BitmapImage)
            MyBase.New(_bitmap)
        End Sub

        Public Overrides Function GoImgProc(_token As CancellationToken) As Boolean
            Dim bRst As Boolean = True

            Dim nMask As Short(,) =
            {
                {1, 1, 1},
                {1, -8, 1},
                {1, 1, 1}
            }

            Dim nWidthSize As Integer = Me.m_bitmap.Width
            Dim nHeightSize As Integer = Me.m_bitmap.Height
            Dim nMasksize As Integer = nMask.GetLength(0)

            Me.m_wBitmap = New WriteableBitmap(Me.m_bitmap)
            Me.m_wBitmap.Lock()

            Dim nIdxWidth As Integer
            Dim nIdxHeight As Integer

            For nIdxHeight = 0 To nHeightSize - 1 Step 1
                For nIdxWidth = 0 To nWidthSize - 1 Step 1
                    If (_token.IsCancellationRequested) Then
                        bRst = False
                        Exit For
                    End If

                    Dim pAdr As IntPtr = Me.m_wBitmap.BackBuffer
                    Dim nPos As Integer = nIdxHeight * Me.m_wBitmap.BackBufferStride + nIdxWidth * 4
                    Dim nPixelB As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                    Dim nPixelG As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                    Dim nPixelR As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                    Dim lCalB As Long = 0
                    Dim lCalG As Long = 0
                    Dim lCalR As Long = 0
                    Dim dCalAve As Double = 0.0
                    Dim nIdxWidthMask As Integer
                    Dim nIdxHightMask As Integer

                    For nIdxHightMask = 0 To nMasksize - 1 Step 1
                        For nIdxWidthMask = 0 To nMasksize - 1 Step 1
                            If (nIdxWidth + nIdxWidthMask > 0 And
                                nIdxWidth + nIdxWidthMask < nWidthSize And
                                nIdxHeight + nIdxHightMask > 0 And
                                nIdxHeight + nIdxHightMask < nHeightSize) Then

                                Dim pAdr2 As IntPtr = Me.m_wBitmap.BackBuffer
                                Dim nPos2 As Integer = (nIdxHeight + nIdxHightMask) * Me.m_wBitmap.BackBufferStride + (nIdxWidth + nIdxWidthMask) * 4

                                lCalB = ReadByte(pAdr2, nPos2 + ComInfo.Pixel.B) * nMask(nIdxWidthMask, nIdxHightMask)
                                lCalG = ReadByte(pAdr2, nPos2 + ComInfo.Pixel.G) * nMask(nIdxWidthMask, nIdxHightMask)
                                lCalR = ReadByte(pAdr2, nPos2 + ComInfo.Pixel.R) * nMask(nIdxWidthMask, nIdxHightMask)

                                Dim dcalGray As Double = (lCalB + lCalG + lCalR) / 3
                                dCalAve = (dCalAve + dcalGray) / 2
                            End If
                        Next
                    Next
                    Dim nGrayScale As Byte = ComFunc.DoubleToByte(dCalAve)

                    WriteByte(pAdr, nPos + ComInfo.Pixel.B, nGrayScale)
                    WriteByte(pAdr, nPos + ComInfo.Pixel.G, nGrayScale)
                    WriteByte(pAdr, nPos + ComInfo.Pixel.R, nGrayScale)
                Next
            Next
            Me.m_wBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
            Me.m_wBitmap.Unlock()
            Me.m_wBitmap.Freeze()

            Return bRst
        End Function
    End Class
End Namespace