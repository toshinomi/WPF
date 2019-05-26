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
    class Binarization : ComImgProc
    {
        private byte m_byteThresh;

        public byte Thresh
        {
            set { m_byteThresh = value; }
            get { return m_byteThresh; }
        }

        public Binarization(BitmapImage _bitmap) : base(_bitmap)
        {
            m_byteThresh = 0;
        }

        public Binarization(BitmapImage _bitmap, byte _byteThresh) : base(_bitmap)
        {
            m_byteThresh = _byteThresh;
        }

        public override bool GoImgProc(CancellationToken _token)
        {
            bool bRst = true;

            int nWidthSize = base.m_bitmap.PixelWidth;
            int nHeightSize = base.m_bitmap.PixelHeight;

            base.m_wBitmap = new WriteableBitmap(base.m_bitmap);
            base.m_wBitmap.Lock();

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
                        byte byteGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                        byte byteBinarization = byteGrayScale >= m_byteThresh ? (byte)255 : (byte)0;
                        pPixel[(int)ComInfo.Pixel.B] = byteBinarization;
                        pPixel[(int)ComInfo.Pixel.G] = byteBinarization;
                        pPixel[(int)ComInfo.Pixel.R] = byteBinarization;
                    }
                }
                base.m_wBitmap.AddDirtyRect(new Int32Rect(0, 0, nWidthSize, nHeightSize));
                base.m_wBitmap.Unlock();
                base.m_wBitmap.Freeze();
            }

            return bRst;
        }
    }
}