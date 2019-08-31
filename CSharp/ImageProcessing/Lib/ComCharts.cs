using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

abstract class ComCharts
{
    protected int[,] m_nHistgram;
    protected BitmapImage m_bitmap;
    protected WriteableBitmap m_wbitmap;

    public ComCharts()
    {
        m_nHistgram = new int[(int)ComInfo.PictureType.MAX, ComInfo.RGB_MAX];
    }

    ~ComCharts()
    {
    }

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

                    m_nHistgram[(int)ComInfo.PictureType.Original, nGrayScale] += 1;

                    if (m_wbitmap != null)
                    {
                        pPixel = (byte*)m_wbitmap.BackBuffer + nIdxHeight * m_wbitmap.BackBufferStride + nIdxWidth * 4;
                        nGrayScale = (byte)((pPixel[(int)ComInfo.Pixel.B] + pPixel[(int)ComInfo.Pixel.G] + pPixel[(int)ComInfo.Pixel.R]) / 3);

                        m_nHistgram[(int)ComInfo.PictureType.After, nGrayScale] += 1;
                    }
                }
            }
        }
    }

    public void InitHistgram()
    {
        for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
        {
            m_nHistgram[(int)ComInfo.PictureType.Original, nIdx] = 0;
            m_nHistgram[(int)ComInfo.PictureType.After, nIdx] = 0;
        }
    }

    public bool SaveCsv()
    {
        bool bRst = true;
        ComSaveFileDialog saveDialog = new ComSaveFileDialog();
        saveDialog.Filter = "CSV|*.csv";
        saveDialog.Title = "Save the csv file";
        saveDialog.FileName = "default.csv";
        if (saveDialog.ShowDialog() == true)
        {
            String strDelmiter = ",";
            StringBuilder stringBuilder = new StringBuilder();
            int[,] nHistgram = m_nHistgram;
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                stringBuilder.Append(nIdx).Append(strDelmiter);
                stringBuilder.Append(nHistgram[(int)ComInfo.PictureType.Original, nIdx]).Append(strDelmiter);
                stringBuilder.Append(nHistgram[(int)ComInfo.PictureType.After, nIdx]).Append(strDelmiter);
                stringBuilder.Append(Environment.NewLine);
            }
            if (!saveDialog.StreamWrite(stringBuilder.ToString()))
            {
                bRst = false;
            }
        }

        return bRst;
    }
}