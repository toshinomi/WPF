Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

Public Class GrayScale : Inherits ComImgProc
    Public Sub New(_bitmap As BitmapImage)
        MyBase.New(_bitmap)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Overrides Function GoImgProc(_token As CancellationToken) As Boolean
        Dim bRst As Boolean = True

        Dim nWidthSize As Integer = Me.m_bitmap.Width
        Dim nHeightSize As Integer = Me.m_bitmap.Height

        Me.m_wBitmap = New WriteableBitmap(Me.m_bitmap)
        Me.m_wBitmap.Lock()

        Dim nIdxWidth As Integer
        Dim nIdxHeight As Integer

        For nIdxHeight = 0 To nHeightSize - 1 Step 1
            If (_token.IsCancellationRequested) Then
                bRst = False
                Exit For
            End If

            For nIdxWidth = 0 To nWidthSize - 1 Step 1
                If (_token.IsCancellationRequested) Then
                    bRst = False
                    Exit For
                End If

                Dim pAdr As IntPtr = Me.m_wBitmap.BackBuffer
                Dim nPos As Integer = nIdxHeight * Me.m_wBitmap.BackBufferStride + nIdxWidth * 4
                Dim nPixelB As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                Dim nPixelG As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                Dim nPixelR As Integer = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                Dim nGrayScale As Integer = (nPixelB + nPixelG + nPixelR) / 3
                WriteByte(pAdr, nPos + ComInfo.Pixel.B, CByte(nGrayScale))
                WriteByte(pAdr, nPos + ComInfo.Pixel.G, CByte(nGrayScale))
                WriteByte(pAdr, nPos + ComInfo.Pixel.R, CByte(nGrayScale))
            Next
        Next
        Me.m_wBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
        Me.m_wBitmap.Unlock()
        Me.m_wBitmap.Freeze()

        Return bRst
    End Function
End Class