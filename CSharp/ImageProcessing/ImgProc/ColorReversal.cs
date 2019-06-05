using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    class ColorReversal : ComImgProc
    {
        public ColorReversal(BitmapImage _bitmap) : base(_bitmap)
        {
        }

        public override bool GoImgProc(CancellationToken _token)
        {
            bool bRst = true;

            int nWidthSize = base.m_bitmap.PixelWidth;
            int nHeightSize = base.m_bitmap.PixelHeight;

            base.m_wBitmap = new WriteableBitmap(base.m_bitmap);
            base.m_wBitmap.Lock();
            WriteableBitmap wBitmap = new WriteableBitmap(base.m_bitmap);
            wBitmap.Lock();

            int nIdxWidth;
            int nIdxHeight;

            unsafe
            {
                for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
                {
                    for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                    {
                        if (_token.IsCancellationRequested)
                        {
                            bRst = false;
                            break;
                        }

                        byte* pPixel = (byte*)base.m_wBitmap.BackBuffer + nIdxHeight * base.m_wBitmap.BackBufferStride + nIdxWidth * 4;
                        byte* pPixelOrg = (byte*)wBitmap.BackBuffer + nIdxHeight * wBitmap.BackBufferStride + nIdxWidth * 4;

                        pPixel[(int)ComInfo.Pixel.B] = (byte)(255 - pPixelOrg[(int)ComInfo.Pixel.B]);
                        pPixel[(int)ComInfo.Pixel.G] = (byte)(255 - pPixelOrg[(int)ComInfo.Pixel.G]);
                        pPixel[(int)ComInfo.Pixel.R] = (byte)(255 - pPixelOrg[(int)ComInfo.Pixel.R]);
                    }
                }
                base.m_wBitmap.AddDirtyRect(new Int32Rect(0, 0, nWidthSize, nHeightSize));
                base.m_wBitmap.Unlock();
                base.m_wBitmap.Freeze();
                wBitmap.Unlock();
                wBitmap.Freeze();
            }

            return bRst;
        }
    }
}