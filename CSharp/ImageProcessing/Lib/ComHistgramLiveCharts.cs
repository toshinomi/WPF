using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// ライブチャートのヒストグラム表示のロジック
/// </summary>
class ComHistgramLiveCharts : ComCharts
{
    /// <summary>
    /// ヒストグラム用の2次元配列データ　配列の1次元：オリジナルのデータ、配列の2次元：画像処理後のデータ
    /// </summary>
    public int[,] Histgram
    {
        get { return base.m_nHistgram; }
    }

    /// <summary>
    /// ビットマップ
    /// </summary>
    public BitmapImage Bitmap
    {
        set { base.m_bitmap = value; }
        get { return base.m_bitmap; }
    }

    /// <summary>
    /// Writeableなビットマップ
    /// </summary>
    public WriteableBitmap WBitmap
    {
        set { base.m_wbitmap = value; }
        get { return base.m_wbitmap; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComHistgramLiveCharts()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComHistgramLiveCharts()
    {
    }

    /// <summary>
    /// ヒストグラムの描画データ処理
    /// </summary>
    /// <returns>ヒストグラムのデータ</returns>
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