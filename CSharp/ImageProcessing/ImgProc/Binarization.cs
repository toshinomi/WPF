using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

class Binarization : ComImgProc
{
    private byte m_nThresh;

    public byte Thresh
    {
        set { m_nThresh = value; }
        get { return m_nThresh; }
    }

    public Binarization(BitmapImage _bitmap) : base(_bitmap)
    {
        m_nThresh = 0;
    }

    public Binarization(BitmapImage _bitmap, byte _nThresh) : base(_bitmap)
    {
        m_nThresh = _nThresh;
    }

    ~Binarization()
    {
        base.m_bitmap = null;
        base.m_wBitmap = null;
    }

    public override void Init()
    {
        m_nThresh = 0;
        base.Init();
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
                    byte nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                    byte nBinarization = nGrayScale >= m_nThresh ? (byte)255 : (byte)0;
                    pPixel[(int)ComInfo.Pixel.B] = nBinarization;
                    pPixel[(int)ComInfo.Pixel.G] = nBinarization;
                    pPixel[(int)ComInfo.Pixel.R] = nBinarization;
                }
            }
            base.m_wBitmap.AddDirtyRect(new Int32Rect(0, 0, nWidthSize, nHeightSize));
            base.m_wBitmap.Unlock();
            base.m_wBitmap.Freeze();
        }

        return bRst;
    }
}