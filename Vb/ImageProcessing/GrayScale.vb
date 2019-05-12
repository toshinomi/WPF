Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

Namespace ImageProcessing
    Public Class GrayScale : Inherits ComImgProc
        Public Sub New(_bitmap As BitmapImage)
            MyBase.New(_bitmap)
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Function GoImgProc(_token As CancellationToken) As Boolean
            Dim bRst As Boolean = True

            Dim nWidthSize As Integer = Bitmap.Width
            Dim nHeightSize As Integer = Bitmap.Height

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
                    Dim bytePixelB As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                    Dim bytePixelG As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                    Dim bytePixelR As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                    Dim byteGrayScale As Integer = (bytePixelB + bytePixelG + bytePixelR) / 3
                    WriteByte(pAdr, nPos + ComInfo.Pixel.B, CByte(byteGrayScale))
                    WriteByte(pAdr, nPos + ComInfo.Pixel.G, CByte(byteGrayScale))
                    WriteByte(pAdr, nPos + ComInfo.Pixel.R, CByte(byteGrayScale))
                Next
            Next
            WriteableBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
            WriteableBitmap.Unlock()
            WriteableBitmap.Freeze()

            Return bRst
        End Function
    End Class
End Namespace
