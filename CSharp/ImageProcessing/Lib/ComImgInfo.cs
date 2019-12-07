using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 画像処理の設定管理のロジック
/// </summary>
public class ComImgInfo
{
    private string m_strCurImgName;
    private ComEdgeDetectionInfo m_edgeDetectoinInfo;
    private ComGrayScaleInfo m_grayScaleInfo;
    private ComBinarizationInfo m_binarizationInfo;
    private ComGrayScale2DiffInfo m_grayScale2DiffInfo;
    private ComColorReversalInfo m_colorReversalInfo;

    /// <summary>
    /// 現在の画像処理の名称
    /// </summary>
    public string CurImgName
    {
        set { m_strCurImgName = value; }
        get { return m_strCurImgName; }
    }

    /// <summary>
    /// エッジ検出の設定
    /// </summary>
    public ComEdgeDetectionInfo EdgeDetectoinInfo
    {
        set { m_edgeDetectoinInfo = value; }
        get { return m_edgeDetectoinInfo; }
    }

    /// <summary>
    /// グレースケールの設定
    /// </summary>
    public ComGrayScaleInfo GrayScaleInfo
    {
        set { m_grayScaleInfo = value; }
        get { return m_grayScaleInfo; }
    }

    /// <summary>
    /// 2値化の設定
    /// </summary>
    public ComBinarizationInfo BinarizationInfo
    {
        set { m_binarizationInfo = value; }
        get { return m_binarizationInfo; }
    }

    /// <summary>
    /// グレースケール2次微分の設定
    /// </summary>
    public ComGrayScale2DiffInfo GrayScale2DiffInfo
    {
        set { m_grayScale2DiffInfo = value; }
        get { return m_grayScale2DiffInfo; }
    }

    /// <summary>
    /// 色反転の設定
    /// </summary>
    public ComColorReversalInfo ColorReversalInfo
    {
        set { m_colorReversalInfo = value; }
        get { return m_colorReversalInfo; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComImgInfo()
    {
        m_edgeDetectoinInfo = new ComEdgeDetectionInfo();
        m_grayScaleInfo = new ComGrayScaleInfo();
        m_binarizationInfo = new ComBinarizationInfo();
        m_grayScale2DiffInfo = new ComGrayScale2DiffInfo();
        m_colorReversalInfo = new ComColorReversalInfo();
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComImgInfo()
    {
    }
}

/// <summary>
/// エッジ検出の設定管理のロジック
/// </summary>
public class ComEdgeDetectionInfo
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComEdgeDetectionInfo()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComEdgeDetectionInfo()
    {
    }
}

/// <summary>
/// グレースケールの設定管理のロジック
/// </summary>
public class ComGrayScaleInfo
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComGrayScaleInfo()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComGrayScaleInfo()
    {
    }
}

/// <summary>
/// 2値化の設定管理のロジック
/// </summary>
public class ComBinarizationInfo
{
    private byte m_nThresh;

    /// <summary>
    /// 閾値
    /// </summary>
    public byte Thresh
    {
        set { m_nThresh = value; }
        get { return m_nThresh; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComBinarizationInfo()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComBinarizationInfo()
    {
    }
}

/// <summary>
/// グレースケール2次微分の設定管理のロジック
/// </summary>
public class ComGrayScale2DiffInfo
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComGrayScale2DiffInfo()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComGrayScale2DiffInfo()
    {
    }
}

/// <summary>
/// 色反転の設定管理のロジック
/// </summary>
public class ComColorReversalInfo
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ComColorReversalInfo()
    {
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComColorReversalInfo()
    {
    }
}