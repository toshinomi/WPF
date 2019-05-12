Imports System.Threading

Namespace ImageProcessing
    Public MustInherit Class ComImgProc
        Private m_bitmap As BitmapImage
        Private m_wBitmap As WriteableBitmap

        Public Sub New(ByVal _bitmap As BitmapImage)
            m_bitmap = _bitmap
        End Sub

        Public Property Bitmap() As BitmapImage
            Get
                Return m_bitmap
            End Get
            Set(ByVal value As BitmapImage)
                m_bitmap = value
            End Set
        End Property

        Public Property WriteableBitmap() As WriteableBitmap
            Get
                Return m_wBitmap
            End Get
            Set(ByVal value As WriteableBitmap)
                m_wBitmap = value
            End Set
        End Property

        Public MustOverride Function GoImgProc(ByVal _token As CancellationToken) As Boolean
    End Class
End Namespace