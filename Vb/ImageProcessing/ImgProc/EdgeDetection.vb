Imports System.Threading
Imports System.Runtime.InteropServices.Marshal

''' <summary>
''' エッジ検出のロジック
''' </summary>
Public Class EdgeDetection : Inherits ComImgProc
    Private Const m_nMaskSize As Integer = 3

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="_bitmap">ビットマップ</param>
    Public Sub New(_bitmap As BitmapImage)
        MyBase.New(_bitmap)
    End Sub

    ''' <summary>
    ''' デスクトラクタ
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' 初期化
    ''' </summary>
    Public Overrides Sub Init()
        MyBase.Init()
    End Sub

    ''' <summary>
    ''' エッジ検出の実行
    ''' </summary>
    ''' <param name="_token">キャンセルトークン</param>
    ''' <returns>実行結果 成功/失敗</returns>
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
                Dim nPixelB As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.B)
                Dim nPixelG As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.G)
                Dim nPixelR As Byte = ReadByte(pAdr, nPos + ComInfo.Pixel.R)

                Dim lCalB As Long = 0
                Dim lCalG As Long = 0
                Dim lCalR As Long = 0

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

                            lCalB += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.B) * nMask(nIdxWidthMask, nIdxHightMask)
                            lCalG += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.G) * nMask(nIdxWidthMask, nIdxHightMask)
                            lCalR += ReadByte(pAdr2, nPos2 + ComInfo.Pixel.R) * nMask(nIdxWidthMask, nIdxHightMask)
                        End If
                    Next
                Next
                WriteByte(pAdr, nPos + ComInfo.Pixel.B, ComFunc.LongToByte(lCalB))
                WriteByte(pAdr, nPos + ComInfo.Pixel.G, ComFunc.LongToByte(lCalG))
                WriteByte(pAdr, nPos + ComInfo.Pixel.R, ComFunc.LongToByte(lCalR))
            Next
        Next
        Me.m_wBitmap.AddDirtyRect(New Int32Rect(0, 0, nWidthSize, nHeightSize))
        Me.m_wBitmap.Unlock()
        Me.m_wBitmap.Freeze()

        Return bRst
    End Function
End Class