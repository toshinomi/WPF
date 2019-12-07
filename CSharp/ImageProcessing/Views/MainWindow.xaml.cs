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
using System.Windows.Controls.Primitives;
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
        private string m_strCurImgName;
#if CHART_LIVE_CHART
        private HistgramLiveCharts m_histgram;
#elif CHART_OXY_PLOT
        private HistgramOxyPlot m_histgram;
#else
        private HistgramOxyPlot m_histgram;
#endif

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = false;
            btnSaveImage.IsEnabled = false;

            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;

            m_strCurImgName = Properties.Settings.Default.ImgTypeSelectName;
            Title = "Image Processing ( " + m_strCurImgName + " )";

            canvasBinarization.IsEnabled = m_strCurImgName == ComInfo.IMG_NAME_BINARIZATION ? true : false;
        }

        /// <summary>
        /// デスクトラクタ
        /// </summary>
        ~MainWindow()
        {
            m_bitmap = null;
            m_tokenSource = null;
            m_imgProc = null;
        }

        /// <summary>
        /// 対象の画像処理オブジェクトにイメージをロードする
        /// </summary>
        /// <param name="_strImgName">画像処理オブジェクトの名称</param>
        public void SelectLoadImage(string _strImgName)
        {
            if (m_imgProc != null)
            {
                m_imgProc = null;
            }

            switch (_strImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    m_imgProc = new EdgeDetection(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    m_imgProc = new GrayScale(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    m_imgProc = new Binarization(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    m_imgProc = new GrayScale2Diff(m_bitmap);
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    m_imgProc = new ColorReversal(m_bitmap);
                    break;
                default:
                    break;
            }

            return;
        }

        /// <summary>
        /// 対象の画像処理オブジェクトからWriteableBitmapを取得する
        /// </summary>
        /// <param name="_strImgName">画像処理オブジェクトの名称</param>
        /// <returns>Writeableなビットマップ</returns>
        public WriteableBitmap SelectGetBitmap(string _strImgName)
        {
            WriteableBitmap wBitmap = null;

            switch (_strImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    wBitmap = edge.WriteableBitmap;
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    wBitmap = gray.WriteableBitmap;
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    wBitmap = binarization.WriteableBitmap;
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    wBitmap = gray2Diff.WriteableBitmap;
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    wBitmap = colorReversal.WriteableBitmap;
                    break;
                default:
                    break;
            }

            return wBitmap;
        }

        /// <summary>
        /// 対象の画像処理オブジェクトを実行する
        /// </summary>
        /// <param name="_comImgInfo">画像処理の設定</param>
        /// <param name="_token">キャンセルトークン</param>
        /// <returns>画像処理の実行結果 成功/失敗</returns>
        public bool SelectGoImgProc(ComImgInfo _comImgInfo, CancellationToken _token)
        {
            bool bRst = true;

            switch (_comImgInfo.CurImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    bRst = edge.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    bRst = gray.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    binarization.Thresh = _comImgInfo.BinarizationInfo.Thresh;
                    bRst = binarization.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    bRst = gray2Diff.GoImgProc(_token);
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    bRst = colorReversal.GoImgProc(_token);
                    break;
                default:
                    break;
            }

            return bRst;
        }

        /// <summary>
        /// ボタンのEnableを制御する
        /// </summary>
        public void SetButtonEnable()
        {
            btnFileSelect.IsEnabled = true;
            btnAllClear.IsEnabled = true;
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            return;
        }

        /// <summary>
        /// 時間を表示するテキストボックスに時間を設定する
        /// </summary>
        public void SetTextTime(long _lTime)
        {
            textBoxTime.Text = _lTime.ToString();

            return;
        }

        /// <summary>
        /// ファイル選択ボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickBtnFileSelect(object sender, RoutedEventArgs e)
        {
            ComOpenFileDialog openFileDlg = new ComOpenFileDialog();
            openFileDlg.Filter = "JPG|*.jpg|PNG|*.png";
            openFileDlg.Title = "Open the file";
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

                if (m_histgram == null)
                {
#if CHART_LIVE_CHART
                    m_histgram = new HistgramLiveCharts();
#elif CHART_OXY_PLOT
                    m_histgram = new HistgramOxyPlot();
#else
                    m_histgram = new HistgramOxyPlot();
#endif
                }

                m_histgram.Bitmap = m_bitmap;
                m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName);
                if (m_histgram.IsOpen == true)
                {
                    m_histgram.DrawHistgram();
                }
            }
            return;
        }

        /// <summary>
        /// イメージのロード処理
        /// </summary>
        public void LoadImage()
        {
            m_bitmap = new BitmapImage();
            m_bitmap.BeginInit();
            m_bitmap.UriSource = new Uri(m_strOpenFileName);
            m_bitmap.EndInit();
            m_bitmap.Freeze();

            SelectLoadImage(m_strCurImgName);

            return;
        }

        /// <summary>
        /// オールクリアボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
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
            btnSaveImage.IsEnabled = false;

            if (m_histgram != null)
            {
                m_histgram.Close();
            }

            return;
        }

        /// <summary>
        /// スタートボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
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
            btnSaveImage.IsEnabled = false;
            btnShowHistgram.IsEnabled = false;
            bool bResult = await TaskWorkImageProcessing();
            if (bResult)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(m_strOpenFileName);
                bitmap.EndInit();
                pictureBoxOriginal.Source = bitmap;
                pictureBoxAfter.Source = SelectGetBitmap(m_strCurImgName);

                stopwatch.Stop();

                Dispatcher.Invoke(new Action<long>(SetTextTime), stopwatch.ElapsedMilliseconds);
                btnSaveImage.IsEnabled = true;

                m_histgram.Bitmap = m_bitmap;
                m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName);
                if (m_histgram.IsOpen == true)
                {
                    m_histgram.DrawHistgram();
                }
            }
            Dispatcher.Invoke(new Action(SetButtonEnable));
            menuMain.IsEnabled = true;
            btnShowHistgram.IsEnabled = true;

            stopwatch = null;
            m_tokenSource = null;

            return;
        }

        /// <summary>
        /// 画像処理実行用のタスク
        /// </summary>
        /// <returns>画像処理の実行結果 成功/失敗</returns>
        public async Task<bool> TaskWorkImageProcessing()
        {
            m_tokenSource = new CancellationTokenSource();
            CancellationToken token = m_tokenSource.Token;
            ComImgInfo imgInfo = new ComImgInfo();
            ComBinarizationInfo binarizationInfo = new ComBinarizationInfo();
            binarizationInfo.Thresh = (byte)sliderThresh.Value;
            imgInfo.CurImgName = m_strCurImgName;
            imgInfo.BinarizationInfo = binarizationInfo;
            bool bRst = await Task.Run(() => SelectGoImgProc(imgInfo, token));
            return bRst;
        }

        /// <summary>
        /// ストップボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickBtnStop(object sender, RoutedEventArgs e)
        {
            if (m_tokenSource != null)
            {
                m_tokenSource.Cancel();
            }

            return;
        }

        /// <summary>
        /// Windowsのクローズイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">キャンセルイベントのデータ</param>
        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_tokenSource != null)
            {
                e.Cancel = true;
            }

            if (m_histgram != null)
            {
                m_histgram.Close();
                m_histgram = null;
            }

            return;
        }

        /// <summary>
        /// メニューのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
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

            return;
        }

        /// <summary>
        /// 設定画面の処理
        /// </summary>
        public void ShowSettingImageProcessing()
        {
            SettingImageProcessing win = new SettingImageProcessing();
            bool? dialogResult = win.ShowDialog();

            if (dialogResult == true)
            {
                ComImageProcessingType imgProcType = (ComImageProcessingType)win.cmbBoxImageProcessingType.SelectedItem;
                m_strCurImgName = imgProcType.Name;
                Title = "Image Processing ( " + m_strCurImgName + " )";

                canvasBinarization.IsEnabled = m_strCurImgName == ComInfo.IMG_NAME_BINARIZATION ? true : false;
               
                pictureBoxAfter.Source = null;
                SelectLoadImage(m_strCurImgName);
                if (m_histgram != null && m_histgram.IsOpen == true)
                {
                    OnClickBtnShowHistgram(this, null);
                }
            }

            return;
        }

        /// <summary>
        /// 画像処理のオブジェクトからイメージの取得
        /// </summary>
        /// <param name="_strImgName">画像処理の名称</param>
        /// <returns>Writeableなビットマップ</returns>
        public WriteableBitmap GetImage(string _strImgName)
        {
            WriteableBitmap bitmap = null;
            switch (m_strCurImgName)
            {
                case ComInfo.IMG_NAME_EDGE_DETECTION:
                    EdgeDetection edge = (EdgeDetection)m_imgProc;
                    if (edge != null)
                    {
                        bitmap = edge.WriteableBitmap;
                    }
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE:
                    GrayScale gray = (GrayScale)m_imgProc;
                    if (gray != null)
                    {
                        bitmap = gray.WriteableBitmap;
                    }
                    break;
                case ComInfo.IMG_NAME_BINARIZATION:
                    Binarization binarization = (Binarization)m_imgProc;
                    if (binarization != null)
                    {
                        bitmap = binarization.WriteableBitmap;
                    }
                    break;
                case ComInfo.IMG_NAME_GRAY_SCALE_2DIFF:
                    GrayScale2Diff gray2Diff = (GrayScale2Diff)m_imgProc;
                    if (gray2Diff != null)
                    {
                        bitmap = gray2Diff.WriteableBitmap;
                    }
                    break;
                case ComInfo.IMG_NAME_COLOR_REVERSAL:
                    ColorReversal colorReversal = (ColorReversal)m_imgProc;
                    if (colorReversal != null)
                    {
                        bitmap = colorReversal.WriteableBitmap;
                    }
                    break;
                default:
                    break;
            }

            return bitmap;
        }

        /// <summary>
        /// イメージの保存ボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickBtnSaveImage(object sender, RoutedEventArgs e)
        {
            ComSaveFileDialog saveDialog = new ComSaveFileDialog();
            saveDialog.Filter = "PNG|*.png";
            saveDialog.Title = "Save the file";
            if (saveDialog.ShowDialog() == true)
            {
                string strFileName = saveDialog.FileName;
                using (FileStream stream = new FileStream(strFileName, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    WriteableBitmap bitmap = GetImage(m_strCurImgName);
                    if (bitmap != null)
                    {
                        try
                        {
                            encoder.Frames.Add(BitmapFrame.Create(bitmap));
                            encoder.Save(stream);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(this, "Save Image File Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// 2値化の閾値のスライダのマウスアップイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">マウスボタンイベントのデータ</param>
        private void OnSliderPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (pictureBoxAfter.Source != null)
            {
                ParamAjust();
            }
        }

        /// <summary>
        /// 2値化の閾値のスライダのキーアップイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">キーボードイベントのデータ</param>
        private void OnSliderPreviewKeyUp(object sender, KeyboardEventArgs e)
        {
            if (pictureBoxAfter.Source != null)
            {
                ParamAjust();
            }
        }

        /// <summary>
        /// 2値化のスライダを調整したときの処理
        /// </summary>
        private async void ParamAjust()
        {
            pictureBoxAfter.Source = null;

            btnFileSelect.IsEnabled = false;
            btnAllClear.IsEnabled = false;
            btnStart.IsEnabled = false;
            menuMain.IsEnabled = false;

            LoadImage();

            btnStop.IsEnabled = true;
            btnSaveImage.IsEnabled = false;
            bool bResult = await TaskWorkImageProcessing();
            if (bResult)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(m_strOpenFileName);
                bitmap.EndInit();
                pictureBoxAfter.Source = SelectGetBitmap(m_strCurImgName);

                btnSaveImage.IsEnabled = true;

                m_histgram.Bitmap = m_bitmap;
                m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName);
                if (m_histgram.IsOpen == true)
                {
                    m_histgram.DrawHistgram();
                }
            }
            Dispatcher.Invoke(new Action(SetButtonEnable));
            menuMain.IsEnabled = true;

            m_tokenSource = null;

            return;
        }

        /// <summary>
        /// ヒストグラム表示ボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickBtnShowHistgram(object sender, RoutedEventArgs e)
        {
            if (m_bitmap == null)
            {
                return;
            }

            if (m_histgram != null)
            {
                m_histgram.Close();
                m_histgram = null;
#if CHART_LIVE_CHART
                m_histgram = new HistgramLiveCharts();
#elif CHART_OXY_PLOT
                m_histgram = new HistgramOxyPlot();
#else
                m_histgram = new HistgramOxyPlot();
#endif
            }

            m_histgram.Bitmap = m_bitmap;
            m_histgram.WBitmap = SelectGetBitmap(m_strCurImgName);
            m_histgram.DrawHistgram();
            m_histgram.IsOpen = true;
            m_histgram.Show();

            return;
        }
    }
}