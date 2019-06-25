using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    abstract class ComCharts
    {
        protected int[,] m_nHistgram;
        protected BitmapImage m_bitmap;
        protected WriteableBitmap m_wbitmap;

        public ComCharts()
        {
            m_nHistgram = new int[2, 256];
        }

        ~ComCharts()
        {
        }

        abstract public GraphData DrawHistgram();
        abstract public List<DataPoint> DrawHistgram2();

        public void CalHistgram()
        {
            int nWidthSize = m_bitmap.PixelWidth;
            int nHeightSize = m_bitmap.PixelHeight;

            WriteableBitmap wBitmap = new WriteableBitmap(m_bitmap);

            int nIdxWidth;
            int nIdxHeight;

            unsafe
            {
                for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
                {
                    for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                    {
                        byte* pPixel = (byte*)wBitmap.BackBuffer + nIdxHeight * wBitmap.BackBufferStride + nIdxWidth * 4;
                        byte nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                        m_nHistgram[0, nGrayScale] += 1;

                        if (m_wbitmap != null)
                        {
                            pPixel = (byte*)m_wbitmap.BackBuffer + nIdxHeight * m_wbitmap.BackBufferStride + nIdxWidth * 4;
                            nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                            m_nHistgram[1, nGrayScale] += 1;
                        }
                    }
                }
            }
        }

        public void InitHistgram()
        {
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                m_nHistgram[0, nIdx] = 0;
                m_nHistgram[1, nIdx] = 0;
            }
        }
    }
}
