Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

Namespace ImageProcessing
    Public Class EdgeDetection : Inherits ComImgProc
        Private Const m_nMaskSize As Integer = 3
        Private m_nFilterMax As UInt32

        Public Sub New(_bitmap As BitmapImage)
            MyBase.New(_bitmap)
            m_nFilterMax = 1
        End Sub

        Public Sub New(_bitmap As BitmapImage, _filterMax As UInt32)
            MyBase.New(_bitmap)
            m_nFilterMax = _filterMax
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Function GoImgProc(_token As CancellationToken) As Boolean
            Dim bRst As Boolean = True

            Dim dMask As Double(,) =
            {
                {1.0, 1.0, 1.0},
                {1.0, -8.0, 1.0},
                {1.0, 1.0, 1.0}
            }

            Dim nWidthSize As Integer = Bitmap.Width
            Dim nHeightSize As Integer = Bitmap.Height
            Dim nMasksize As Integer = dMask.GetLength(0)

            WriteableBitmap = New WriteableBitmap(Bitmap)
            WriteableBitmap.Lock()

            Dim nIdxWidth As Integer
            Dim nIdxHeight As Integer

            For nIdxHeight = 0 To nHeightSize - 1 Step 1
                For nIdxWidth = 0 To nWidthSize - 1 Step 1
                    If (_token.IsCancellationRequested) Then
                        bRst = False
                        Exit For
                    End If

                    Dim pAdr As IntPtr = WriteableBitmap.BackBuffer
                    Dim nPos As Integer = nIdxHeight * WriteableBitmap.BackBufferStride + nIdxWidth * 4
                    Dim bytePixelB As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                    Dim bytePixelG As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                    Dim bytePixelR As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                    Dim dCalB As Double = 0.0
                    Dim dCalG As Double = 0.0
                    Dim dCalR As Double = 0.0

                    Dim nIdxWidthMask As Integer
                    Dim nIdxHightMask As Integer
                    Dim nFilter As Integer = 0

                    While nFilter < m_nFilterMax
                        For nIdxHightMask = 0 To nMasksize - 1 Step 1
                            For nIdxWidthMask = 0 To nMasksize - 1 Step 1
                                If (nIdxWidth + nIdxWidthMask > 0 And
                                    nIdxWidth + nIdxWidthMask < nWidthSize And
                                    nIdxHeight + nIdxHightMask > 0 And
                                    nIdxHeight + nIdxHightMask < nHeightSize) Then

                                    Dim pAdr2 As IntPtr = WriteableBitmap.BackBuffer
                                    Dim nPos2 As Integer = (nIdxHeight + nIdxHightMask) * WriteableBitmap.BackBufferStride + (nIdxWidth + nIdxWidthMask) * 4

                                    dCalB += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.B) * dMask(nIdxWidthMask, nIdxHightMask)
                                    dCalG += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.G) * dMask(nIdxWidthMask, nIdxHightMask)
                                    dCalR += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.R) * dMask(nIdxWidthMask, nIdxHightMask)
                                End If
                            Next
                        Next
                        nFilter += 1
                    End While
                    WriteByte(pAdr, nPos + ComInfo.Pixel.B, ComFunc.DoubleToByte(dCalB))
                    WriteByte(pAdr, nPos + ComInfo.Pixel.G, ComFunc.DoubleToByte(dCalG))
                    WriteByte(pAdr, nPos + ComInfo.Pixel.R, ComFunc.DoubleToByte(dCalR))
                Next
            Next
            WriteableBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
            WriteableBitmap.Unlock()
            WriteableBitmap.Freeze()

            Return bRst
        End Function
    End Class
End Namespace