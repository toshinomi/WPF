using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    class ComHistgramLiveCharts
    {
        private int[,] m_nHistgram;
        private BitmapImage m_bitmap;
        private WriteableBitmap m_wbitmap;

        public int[,] Histgram
        {
            get { return m_nHistgram; }
        }

        public BitmapImage Bitmap
        {
            set { m_bitmap = value; }
            get { return m_bitmap; }
        }

        public WriteableBitmap WBitmap
        {
            set { m_wbitmap = value; }
            get { return m_wbitmap; }
        }

        public ComHistgramLiveCharts()
        {
            m_nHistgram = new int[2, 256];
        }

        ~ComHistgramLiveCharts()
        {
        }

        public GraphData DrawHistgram()
        {
            GraphData graphData = new GraphData();

            InitHistgram();

            CalHistgram();

            var chartValue1 = new ChartValues<int>();
            var chartValue2 = new ChartValues<int>();
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                chartValue1.Add(m_nHistgram[0, nIdx]);
                if (m_wbitmap == null)
                {
                    chartValue2.Add(0);
                }
                else
                {
                    chartValue2.Add(m_nHistgram[1, nIdx]);
                }
            }

            var seriesCollection = new SeriesCollection();

            var lineSeriesChart1 = new LineSeries()
            {
                Values = chartValue1,
                Title = "Original Image"
            };
            seriesCollection.Add(lineSeriesChart1);

            var lineSeriesChart2 = new LineSeries()
            {
                Values = chartValue2,
                Title = "After Image"
            };
            seriesCollection.Add(lineSeriesChart2);

            graphData.seriesCollection = seriesCollection;

            return graphData;
        }

        private void CalHistgram()
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
