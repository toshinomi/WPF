using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ImageProcessing
{
    /// <summary>
    /// Histgram.xaml の相互作用ロジック
    /// </summary>
    public partial class Histgram : Window
    {
        private BitmapImage m_bitmap;
        private WriteableBitmap m_wbitmap;
        private bool m_bIsOpen;

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

        public bool IsOpen
        {
            set { m_bIsOpen = value; }
            get { return m_bIsOpen; }
        }

        private int[,] m_byteHistgram;

        public Histgram()
        {
            InitializeComponent();
        }

        public void DrawHistgram()
        {
            GraphData graphData = new GraphData();

            CalHistgram();

            var chartValue1 = new ChartValues<int>();
            var chartValue2 = new ChartValues<int>();
            for (int nIdx = 0; nIdx < 256; nIdx++)
            {
                chartValue1.Add(m_byteHistgram[0, nIdx]);
                if (m_wbitmap == null)
                {
                    chartValue2.Add(0);
                }
                else
                {
                    chartValue2.Add(m_byteHistgram[1, nIdx]);
                }
            }

            var seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = chartValue1,
                    Title = "Original Image",
                },
                new LineSeries
                {
                    Values = chartValue2,
                    Title = "After Image",
                }
            };

            graphData.seriesCollection = seriesCollection;
            this.DataContext = graphData;
        }

        private void OnClickDraw(object sender, RoutedEventArgs e)
        {
            DrawHistgram();
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_bIsOpen = false;

            return;
        }

        public void CalHistgram()
        {
            int nWidthSize = m_bitmap.PixelWidth;
            int nHeightSize = m_bitmap.PixelHeight;

            WriteableBitmap wBitmap = new WriteableBitmap(m_bitmap);

            int nIdxWidth;
            int nIdxHeight;

            m_byteHistgram = new int[2, 256];

            unsafe
            {
                for (nIdxHeight = 0; nIdxHeight < nHeightSize; nIdxHeight++)
                {
                    for (nIdxWidth = 0; nIdxWidth < nWidthSize; nIdxWidth++)
                    {
                        byte* pPixel = (byte*)wBitmap.BackBuffer + nIdxHeight * wBitmap.BackBufferStride + nIdxWidth * 4;
                        byte byteGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                        m_byteHistgram[0, byteGrayScale] += 1;

                        if (m_wbitmap != null)
                        {
                            pPixel = (byte*)m_wbitmap.BackBuffer + nIdxHeight * m_wbitmap.BackBufferStride + nIdxWidth * 4;
                            byteGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                            m_byteHistgram[1, byteGrayScale] += 1;
                        }
                    }
                }
            }
        }

        public class GraphData
        {
            private SeriesCollection m_seriesCollection;
            public SeriesCollection seriesCollection
            {
                set { m_seriesCollection = value; }
                get { return m_seriesCollection; }
            }
        }
    }
}