Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

Public Class ColorReversal : Inherits ComImgProc
    Public Sub New(_bitmap As BitmapImage)
        MyBase.New(_bitmap)
    End Sub

    Public Overrides Function GoImgProc(_token As CancellationToken) As Boolean
        Dim bRst As Boolean = True

        Dim nWidthSize As Integer = MyBase.m_bitmap.PixelWidth
        Dim nHeightSize As Integer = MyBase.m_bitmap.PixelHeight

        MyBase.m_wBitmap = New WriteableBitmap(MyBase.m_bitmap)
        MyBase.m_wBitmap.Lock()

        Dim nIdxWidth As Integer
        Dim nIdxHeight As Integer

        For nIdxHeight = 0 To nHeightSize - 1 Step 1
            If (_token.IsCancellationRequested = True) Then
                bRst = False
                Exit For
            End If

            For nIdxWidth = 0 To nWidthSize - 1 Step 1
                If (_token.IsCancellationRequested = True) Then
                    bRst = False
                    Exit For
                End If

                Dim pAdr As IntPtr = Me.m_wBitmap.BackBuffer
                Dim nPos As Integer = nIdxHeight * Me.m_wBitmap.BackBufferStride + nIdxWidth * 4
                Dim nPixelB As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                Dim nPixelG As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                Dim nPixelR As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                WriteByte(pAdr, nPos + ComInfo.Pixel.B, CByte(255 - nPixelB))
                WriteByte(pAdr, nPos + ComInfo.Pixel.G, CByte(255 - nPixelG))
                WriteByte(pAdr, nPos + ComInfo.Pixel.R, CByte(255 - nPixelR))
            Next
        Next
        Me.m_wBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
        Me.m_wBitmap.Unlock()
        Me.m_wBitmap.Freeze()

        Return bRst
    End Function
End Class