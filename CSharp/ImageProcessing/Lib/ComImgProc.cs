using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

abstract public class ComImgProc
{
    protected BitmapImage m_bitmap;
    protected WriteableBitmap m_wBitmap;

    public ComImgProc(BitmapImage _bitmap)
    {
        m_bitmap = _bitmap;
    }

    ~ComImgProc()
    {
        m_bitmap = null;
        m_wBitmap = null;
    }

    virtual public void Init()
    {
        m_bitmap = null;
    }

    public BitmapImage Bitmap
    {
        set { m_bitmap = value; }
        get { return m_bitmap; }
    }

    public WriteableBitmap WriteableBitmap
    {
        set { m_wBitmap = value; }
        get { return m_wBitmap; }
    }

    abstract public bool GoImgProc(CancellationToken _token);
}