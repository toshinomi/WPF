using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// 画像処理の共通のロジック
/// </summary>
abstract public class ComImgProc
{
    protected BitmapImage m_bitmap;
    protected WriteableBitmap m_wBitmap;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="_bitmap">ビットマップ</param>
    public ComImgProc(BitmapImage _bitmap)
    {
        m_bitmap = _bitmap;
    }

    /// <summary>
    /// デスクトラクタ
    /// </summary>
    ~ComImgProc()
    {
        m_bitmap = null;
        m_wBitmap = null;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    virtual public void Init()
    {
        m_bitmap = null;
    }

    /// <summary>
    /// ビットマップ
    /// </summary>
    public BitmapImage Bitmap
    {
        set { m_bitmap = value; }
        get { return m_bitmap; }
    }

    /// <summary>
    /// Writeableなビットマップ
    /// </summary>
    public WriteableBitmap WriteableBitmap
    {
        set { m_wBitmap = value; }
        get { return m_wBitmap; }
    }

    /// <summary>
    /// 画像処理実行の抽象
    /// </summary>
    /// <param name="_token">キャンセルトークン</param>
    /// <returns>実行結果 成功/失敗</returns>
    abstract public bool GoImgProc(CancellationToken _token);
}