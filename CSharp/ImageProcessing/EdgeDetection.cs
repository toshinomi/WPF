using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    public class EdgeDetection
    {
        private const int m_nMaskSize = 3;
        private uint m_nFilterMax;
        private BitmapImage m_bitmap;
        private WriteableBitmap m_wBitmap;

        public EdgeDetection(BitmapImage _bitmap)
        {
            m_nFilterMax = 1;
            m_bitmap = _bitmap;
        }

        public EdgeDetection(BitmapImage _bitmap, uint _filterMax)
        {
            m_nFilterMax = _filterMax;
            m_bitmap = _bitmap;
        }

        ~EdgeDetection()
        {
            m_bitmap = null;
        }

        public WriteableBitmap GetBitmap()
        {
            return m_wBitmap;
        }

        public void SetBitmap(WriteableBitmap _bitmap)
        {
            m_wBitmap = _bitmap;
        }

        public bool GoEdgeDetection(CancellationToken _token)
        {
            bool bRst = true;

            double[,] dMask =
            {
                {1.0,  1.0, 1.0},
                {1.0, -8.0, 1.0},
                {1.0,  1.0, 1.0}
            };

            int nWidthSize = m_bitmap.PixelWidth;
            int nHeightSize = m_bitmap.PixelHeight;
            int nMasksize = dMask.GetLength(0);

            m_wBitmap = new WriteableBitmap(m_bitmap);
            m_wBitmap.Lock();

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

                        byte* pPixel = (byte*)m_wBitmap.BackBuffer + nIdxHeight * m_wBitmap.BackBufferStride + nIdxWidth * 4;

                        double dCalB = 0.0;
                        double dCalG = 0.0;
                        double dCalR = 0.0;
                        double dCalA = 0.0;
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
                                        byte* pPixel2 = (byte*)m_wBitmap.BackBuffer + (nIdxHeight + nIdxHightMask) * m_wBitmap.BackBufferStride + (nIdxWidth + nIdxWidthMask) * 4;

                                        dCalB += pPixel2[(int)ComInfo.Pixel.B] * dMask[nIdxWidthMask, nIdxHightMask];
                                        dCalG += pPixel2[(int)ComInfo.Pixel.G] * dMask[nIdxWidthMask, nIdxHightMask];
                                        dCalR += pPixel2[(int)ComInfo.Pixel.R] * dMask[nIdxWidthMask, nIdxHightMask];
                                        dCalA += pPixel2[(int)ComInfo.Pixel.A] * dMask[nIdxWidthMask, nIdxHightMask];
                                    }
                                }
                            }
                            nFilter++;
                        }
                        pPixel[(int)ComInfo.Pixel.B] = ComFunc.DoubleToByte(dCalB);
                        pPixel[(int)ComInfo.Pixel.G] = ComFunc.DoubleToByte(dCalG);
                        pPixel[(int)ComInfo.Pixel.R] = ComFunc.DoubleToByte(dCalR);
                        pPixel[(int)ComInfo.Pixel.A] = ComFunc.DoubleToByte(dCalA);
                    }
                }
                m_wBitmap.AddDirtyRect(new Int32Rect(0, 0, nWidthSize, nHeightSize));
                m_wBitmap.Unlock();
                m_wBitmap.Freeze();
            }

            return bRst;
        }
    }
}