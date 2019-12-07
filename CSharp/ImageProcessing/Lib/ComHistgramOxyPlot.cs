using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// OXYプロットのヒストグラム表示のロジック
/// </summary>
class ComHistgramOxyPlot : ComCharts
{
    private PlotModel m_pModel;

    /// <summary>
    /// プロットモデル
    /// </summary>
    public PlotModel PModel
    {
        get { return m_pModel; }
        set { m_pModel = value; }
    }

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
    public ComHistgramOxyPlot()
    {
        m_pModel = new PlotModel();
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComHistgramOxyPlot()
    {
    }

    /// <summary>
    /// ヒストグラムの描画データ処理
    /// </summary>
    /// <returns>ヒストグラムのプロットデータ</returns>
    public PlotModel DrawHistgram()
    {
        base.InitHistgram();

        base.CalHistgram();

        var dataList1 = new List<DataPoint>();
        for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
        {
            var dataPoint = new DataPoint(nIdx, base.m_nHistgram[(int)ComInfo.PictureType.Original, nIdx]);
            dataList1.Add(dataPoint);
        }
        var series1 = new LineSeries
        {
            Title = "Original",
            ItemsSource = dataList1,
        };
        m_pModel.Series.Add(series1);

        var dataList2 = new List<DataPoint>();
        for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
        {
            var dataPoint = new DataPoint(nIdx, base.m_nHistgram[(int)ComInfo.PictureType.After, nIdx]);
            dataList2.Add(dataPoint);
        }
        var series2= new LineSeries
        {
            Title = "After",
            ItemsSource = dataList2,
        };
        m_pModel.Series.Add(series2);

        return m_pModel;
    }
}