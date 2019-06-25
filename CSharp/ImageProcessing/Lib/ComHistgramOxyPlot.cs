using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    class ComHistgramOxyPlot : ComCharts
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

        public override GraphData DrawHistgram()
        {
            throw new NotImplementedException();
        }

        public override List<DataPoint> DrawHistgram2()
        {
            base.InitHistgram();

            base.CalHistgram();

            var dataList = new List<DataPoint>();
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                var dataPoint = new DataPoint(nIdx, base.m_nHistgram[0, nIdx]);
                dataList.Add(dataPoint);
            }
            for (int nIdx = 0; nIdx < (m_nHistgram.Length >> 1); nIdx++)
            {
                var dataPoint = new DataPoint(nIdx, base.m_nHistgram[1, nIdx]);
                dataList.Add(dataPoint);
            }
            return  dataList;
        }
    }
}
