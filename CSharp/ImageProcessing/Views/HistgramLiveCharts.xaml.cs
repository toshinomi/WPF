using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class HistgramLiveCharts : Window
    {
        private ComHistgramLiveCharts m_histramgChart;
        private bool m_bIsOpen;

        public BitmapImage Bitmap
        {
            set { m_histramgChart.Bitmap = value; }
            get { return m_histramgChart.Bitmap; }
        }

        public WriteableBitmap WBitmap
        {
            set { m_histramgChart.WBitmap = value; }
            get { return m_histramgChart.WBitmap; }
        }

        public bool IsOpen
        {
            set { m_bIsOpen = value; }
            get { return m_bIsOpen; }
        }

        public HistgramLiveCharts()
        {
            InitializeComponent();

            m_histramgChart = new ComHistgramLiveCharts();
        }

        public void DrawHistgram()
        {
            this.DataContext = m_histramgChart.DrawHistgram();

            return;
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_bIsOpen = false;

            return;
        }

        private void OnClickMenu(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string strHeader = menuItem.Header.ToString();

            switch (strHeader)
            {
                case ComInfo.MENU_SAVE_CSV:
                    SaveCsv();
                    break;
                default:
                    break;
            }

            return;
        }

        public void SaveCsv()
        {
            ComSaveFileDialog saveDialog = new ComSaveFileDialog();
            saveDialog.Filter = "CSV|*.csv";
            saveDialog.Title = "Save the csv file";
            saveDialog.FileName = "default.csv";
            if (saveDialog.ShowDialog() == true)
            {
                String strDelmiter = ",";
                StringBuilder stringBuilder = new StringBuilder();
                int[,] nHistgram = m_histramgChart.Histgram;
                for (int nIdx = 0; nIdx < (m_histramgChart.Histgram.Length >> 1); nIdx++)
                {
                    stringBuilder.Append(nIdx).Append(strDelmiter);
                    stringBuilder.Append(nHistgram[0, nIdx]).Append(strDelmiter);
                    stringBuilder.Append(nHistgram[1, nIdx]).Append(strDelmiter);
                    stringBuilder.Append(Environment.NewLine);
                }
                saveDialog.StreamWrite(stringBuilder.ToString());
            }

            return;
        }
    }
}