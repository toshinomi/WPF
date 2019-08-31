using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

class ComHistgramLiveCharts : ComCharts
{
    public int[,] Histgram
    {
        get { return base.m_nHistgram; }
    }

    public BitmapImage Bitmap
    {
        set { base.m_bitmap = value; }
        get { return base.m_bitmap; }
    }

    public WriteableBitmap WBitmap
    {
        set { base.m_wbitmap = value; }
        get { return base.m_wbitmap; }
    }

    public ComHistgramLiveCharts()
    {
    }

    ~ComHistgramLiveCharts()
    {
    }

    public GraphData DrawHistgram()
    {
        GraphData graphData = new GraphData();

        base.InitHistgram();

        base.CalHistgram();

        var chartValue1 = new ChartValues<int>();
        var chartValue2 = new ChartValues<int>();
        for (int nIdx = 0; nIdx < (base.m_nHistgram.Length >> 1); nIdx++)
        {
            chartValue1.Add(base.m_nHistgram[(int)ComInfo.PictureType.Original, nIdx]);
            if (m_wbitmap == null)
            {
                chartValue2.Add(0);
            }
            else
            {
                chartValue2.Add(base.m_nHistgram[(int)ComInfo.PictureType.After, nIdx]);
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
}