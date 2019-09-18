using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

public class EdgeDetection : ComImgProc
{
    public EdgeDetection(BitmapImage _bitmap) : base(_bitmap)
    {
    }

    ~EdgeDetection()
    {
    }

    public override void Init()
    {
        base.Init();
    }

    public override bool GoImgProc(CancellationToken _token)
    {
        bool bRst = true;

        short[,] nMask =
        {
            {1,  1, 1},
            {1, -8, 1},
            {1,  1, 1}
        };
        int nWidthSize = base.m_bitmap.PixelWidth;
        int nHeightSize = base.m_bitmap.PixelHeight;
        int nMasksize = nMask.GetLength(0);

        base.m_wBitmap = new WriteableBitmap(base.m_bitmap);
        base.m_wBitmap.Lock();

        int nIdxWidth;
        int nIdxHeight;

        unsafe
        {
            for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
            {
                if (_token.IsCancellationRequested)
                {
                    bRst = false;
                    break;
                }

                for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                {
                    if (_token.IsCancellationRequested)
                    {
                        bRst = false;
                        break;
                    }

                    byte* pPixel = (byte*)base.m_wBitmap.BackBuffer + nIdxHeight * base.m_wBitmap.BackBufferStride + nIdxWidth * 4;

                    long lCalB = 0;
                    long lCalG = 0;
                    long lCalR = 0;
                    int nIdxWidthMask;
                    int nIdxHightMask;

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

                                lCalB += pPixel2[(int)ComInfo.Pixel.B] * nMask[nIdxWidthMask, nIdxHightMask];
                                lCalG += pPixel2[(int)ComInfo.Pixel.G] * nMask[nIdxWidthMask, nIdxHightMask];
                                lCalR += pPixel2[(int)ComInfo.Pixel.R] * nMask[nIdxWidthMask, nIdxHightMask];
                            }
                        }
                    }
                    pPixel[(int)ComInfo.Pixel.B] = ComFunc.LongToByte(lCalB);
                    pPixel[(int)ComInfo.Pixel.G] = ComFunc.LongToByte(lCalG);
                    pPixel[(int)ComInfo.Pixel.R] = ComFunc.LongToByte(lCalR);
                }
            }
            base.m_wBitmap.AddDirtyRect(new Int32Rect(0, 0, nWidthSize, nHeightSize));
            base.m_wBitmap.Unlock();
            base.m_wBitmap.Freeze();
        }

        return bRst;
    }
}