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
        private ComHistgramLiveCharts m_histgramChart;
        private bool m_bIsOpen;

        public BitmapImage Bitmap
        {
            set { m_histgramChart.Bitmap = value; }
            get { return m_histgramChart.Bitmap; }
        }

        public WriteableBitmap WBitmap
        {
            set { m_histgramChart.WBitmap = value; }
            get { return m_histgramChart.WBitmap; }
        }

        public bool IsOpen
        {
            set { m_bIsOpen = value; }
            get { return m_bIsOpen; }
        }

        public HistgramLiveCharts()
        {
            InitializeComponent();

            m_histgramChart = new ComHistgramLiveCharts();
        }

        public void DrawHistgram()
        {
            this.DataContext = m_histgramChart.DrawHistgram();

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
            if (!m_histgramChart.SaveCsv())
            {
                MessageBox.Show(this, "Save CSV File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return;
        }
    }
}