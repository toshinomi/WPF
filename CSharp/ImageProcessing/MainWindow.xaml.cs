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
        private object m_imgProc;
        private string m_strOpenFileName;
        private CancellationTokenSource m_tokenSource;
        private string m_curImgName;

        public MainWindow()
        {
            InitializeComponent();

            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = false;

            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;

            m_curImgName = Properties.Settings.Default.ImgTypeSelectName;
            Title = "Image Processing ( " + m_curImgName + " )";
        }

        ~MainWindow()
        {
            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;
        }

        public bool SelectLoadImage(string _imgName)
        {
            bool bRst = true;

            switch (_imgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    m_imgProc = new EdgeDetection(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    m_imgProc = new GrayScale(m_bitmap);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public bool SelectGetBitmap(string _imgName)
        {
            bool bRst = true;

            switch (_imgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    pictureBoxAfter.Source = edge.WriteableBitmap;
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    pictureBoxAfter.Source = gray.WriteableBitmap;
                    break;
                default:
                    break;
            }

            return bRst;
        }

        public bool SelectGoImgProc(string _imgName, CancellationToken _token)
        {
            bool bRst = true;

            switch (_imgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    bRst = edge.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    bRst = gray.GoImgProc(_token);
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
            openFileDlg.Filter = "JPG|*.jpg|PNG|*.png";
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

            SelectLoadImage(m_curImgName);

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
            menuMain.IsEnabled = false;

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
                SelectGetBitmap(m_curImgName);

                stopwatch.Stop();

                Dispatcher.Invoke(new Action<long>(SetTextTime), stopwatch.ElapsedMilliseconds);
            }
            Dispatcher.Invoke(new Action(SetButtonEnable));
            menuMain.IsEnabled = true;

            stopwatch = null;
            m_tokenSource = null;
            m_bitmap = null;

            return;
        }

        public async Task<bool> TaskWorkImageProcessing()
        {
            m_tokenSource = new CancellationTokenSource();
            CancellationToken token = m_tokenSource.Token;
            bool bRst = await Task.Run(() => SelectGoImgProc(m_curImgName, token));
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

        private void OnClickMenu(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string strHeader = menuItem.Header.ToString();

            switch (strHeader)
            {
                case ComInfo.MENU_FILE_END:
                    Close();
                    break;
                case ComInfo.MENU_SETTING_IMAGE_PROCESSING:
                    ShowSettingImageProcessing();
                    break;
                default:
                    break;
            }
        }

        public void ShowSettingImageProcessing()
        {
            SettingImageProcessing win = new SettingImageProcessing();
            bool? dialogResult = win.ShowDialog();

            if (dialogResult == true)
            {
                ImageProcessingType imgProcType = (ImageProcessingType)win.cmbBoxImageProcessingType.SelectedItem;
                m_curImgName = imgProcType.Name;
                Title = "Image Processing ( " + m_curImgName + " )";
            }

            return;
        }
    }
}