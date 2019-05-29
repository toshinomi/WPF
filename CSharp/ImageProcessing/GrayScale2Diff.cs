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
    class GrayScale2Diff : ComImgProc
    {
        private uint m_nFilterMax;

        public uint FilterMax
        {
            set { m_nFilterMax = value; }
            get { return m_nFilterMax; }
        }

        public GrayScale2Diff(BitmapImage _bitmap) : base(_bitmap)
        {
            m_nFilterMax = 1;
        }

        ~GrayScale2Diff()
        {
        }

        public override bool GoImgProc(CancellationToken _token)
        {
            bool bRst = true;

            double[,] dMask =
            {
                {1.0,  1.0, 1.0},
                {1.0, -8.0, 1.0},
                {1.0,  1.0, 1.0}
            };
            int nWidthSize = base.m_bitmap.PixelWidth;
            int nHeightSize = base.m_bitmap.PixelHeight;
            int nMasksize = dMask.GetLength(0);

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

                        double dCalB = 0.0;
                        double dCalG = 0.0;
                        double dCalR = 0.0;
                        double dCalAve = 0.0;
                        int nIdxWidthMask;
                        int nIdxHightMask;
                        int nFilter = 0;

                        while (nFilter < m_nFilterMax)
                        {
                            for (nIdxHightMask = 0; nIdxHightMask < nMasksize; nIdxHightMask++)
                            {
                                for (nIdxWidthMask = 0; nIdxWidthMask < nMasksize; nIdxWidthMask++)
                                {
                                    if (nIdxWidth + nIdxWidthMask > 0 &&
                                        nIdxWidth + nIdxWidthMask < nWidthSize &&
                                        nIdxHeight + nIdxHightMask > 0 &&
                                        nIdxHeight + nIdxHightMask < nHeightSize)
                                    {
                                        byte* pPixel2 = (byte*)wBitmap.BackBuffer + (nIdxHeight + nIdxHightMask) * wBitmap.BackBufferStride + (nIdxWidth + nIdxWidthMask) * 4;

                                        dCalB = pPixel2[(int)ComInfo.Pixel.B] * dMask[nIdxWidthMask, nIdxHightMask];
                                        dCalG = pPixel2[(int)ComInfo.Pixel.G] * dMask[nIdxWidthMask, nIdxHightMask];
                                        dCalR = pPixel2[(int)ComInfo.Pixel.R] * dMask[nIdxWidthMask, nIdxHightMask];

                                        double dcalGray = (dCalB + dCalG + dCalR) / 3;
                                        dCalAve = (dCalAve + dcalGray) / 2;
                                    }
                                }
                            }
                            nFilter++;
                        }
                        byte byteGrayScale = ComFunc.DoubleToByte(dCalAve);

                        pPixel[(int)ComInfo.Pixel.B] = byteGrayScale;
                        pPixel[(int)ComInfo.Pixel.G] = byteGrayScale;
                        pPixel[(int)ComInfo.Pixel.R] = byteGrayScale;
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
