using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageProcessing
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage m_bitmap;
        private EdgeDetection m_edgeDetection;
        private string m_strOpenFileName;
        private CancellationTokenSource m_tokenSource;
        private int m_curImgType;

        public MainWindow()
        {
            InitializeComponent();

            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = false;

            m_bitmap = null;
            m_tokenSource = null;
            m_edgeDetection = null;

            m_curImgType = Properties.Settings.Default.ImgTypeSelect;
            SelectWIndowTitle(m_curImgType);
        }

        ~MainWindow()
        {
            m_bitmap = null;
            m_tokenSource = null;
            m_edgeDetection = null;
        }

        public bool SelectWIndowTitle(int _imgType)
        {
            bool bRst = true;

            switch (_imgType)
            {
                case (int)ComInfo.ImgType.EdgeDetection:
                    Title += " ( " + Properties.Settings.Default.ImgTypeEdgeName + " )";
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public bool SelectLoadImage(int _imgType)
        {
            bool bRst = true;

            switch (_imgType)
            {
                case (int)ComInfo.ImgType.EdgeDetection:
                    m_edgeDetection = new EdgeDetection(m_bitmap);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public bool SelectGetBitmap(int _imgType)
        {
            bool bRst = true;

            switch (_imgType)
            {
                case (int)ComInfo.ImgType.EdgeDetection:
                    pictureBoxAfter.Source = m_edgeDetection.GetBitmap();
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public bool SelectGoImg(int _imgType, CancellationToken _token)
        {
            bool bRst = true;

            switch (_imgType)
            {
                case (int)ComInfo.ImgType.EdgeDetection:
                    m_edgeDetection.GoEdgeDetection(_token);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public void SetButtonEnable()
        {
            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnSetting.IsEnabled = true;

            return;
        }

        public void SetTextTime(long _lTime)
        {
            textBoxTime.Text = _lTime.ToString();

            return;
        }

        private void OnClickBtnFileSelect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();

            openFileDlg.FileName = "default.jpg";
            openFileDlg.InitialDirectory = @"C:\";
            openFileDlg.Filter = "All Files(*.*)|*.*";
            openFileDlg.FilterIndex = 1;
            openFileDlg.Title = "Please select a file to open";
            openFileDlg.RestoreDirectory = true;
            openFileDlg.CheckFileExists = true;
            openFileDlg.CheckPathExists = true;

            if (openFileDlg.ShowDialog() == true)
            {
                pictureBoxOriginal.Source = null;
                pictureBoxAfter.Source = null;
                m_strOpenFileName = openFileDlg.FileName;
                try
                {
                    LoadImage();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Open File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                pictureBoxOriginal.Source = m_bitmap;
                btnStart.IsEnabled = true;
                textBoxTime.Text = "";
            }
            return;
        }

        public void LoadImage()
        {
            m_bitmap = new BitmapImage();
            m_bitmap.BeginInit();
            m_bitmap.UriSource = new Uri(m_strOpenFileName);
            m_bitmap.EndInit();
            m_bitmap.Freeze();

            SelectLoadImage(m_curImgType);

            return;
        }

        private void OnClickBtnAllClear(object sender, RoutedEventArgs e)
        {
            pictureBoxOriginal.Source = null;
            pictureBoxAfter.Source = null;

            m_bitmap = null;
            m_strOpenFileName = "";

            textBoxTime.Text = "";

            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = false;

            return;
        }

        private async void OnClickBtnStart(object sender, RoutedEventArgs e)
        {
            pictureBoxAfter.Source = null;

            btnFileSelect.IsEnabled = false;
            btnAllClear.IsEnabled = false;
            btnStart.IsEnabled = false;
            btnSetting.IsEnabled = false;

            textBoxTime.Text = "";

            LoadImage();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            btnStop.IsEnabled = true;
            bool bResult = await TaskWorkImageProcessing();
            if (bResult)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(m_strOpenFileName);
                bitmap.EndInit();
                pictureBoxOriginal.Source = bitmap;
                SelectGetBitmap(m_curImgType);

                stopwatch.Stop();

                Dispatcher.Invoke(new Action<long>(SetTextTime), stopwatch.ElapsedMilliseconds);
            }
            Dispatcher.Invoke(new Action(SetButtonEnable));

            stopwatch = null;
            m_tokenSource = null;
            m_bitmap = null;

            return;
        }

        public async Task<bool> TaskWorkImageProcessing()
        {
            m_tokenSource = new CancellationTokenSource();
            CancellationToken token = m_tokenSource.Token;
            bool bRst = await Task.Run(() => SelectGoImg(m_curImgType, token));
            return bRst;
        }

        private void OnClickBtnStop(object sender, RoutedEventArgs e)
        {
            if (m_tokenSource != null)
            {
                m_tokenSource.Cancel();
            }

            return;
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_tokenSource != null)
            {
                e.Cancel = true;
            }

            return;
        }

        private void OnClickBtnSetting(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            bool? dialogResult = settingWindow.ShowDialog();

            if (dialogResult == true)
            {
                m_curImgType = settingWindow.cmbBoxImageProcessingType.SelectedIndex;
                SelectWIndowTitle(m_curImgType);
            }
        }
    }
}