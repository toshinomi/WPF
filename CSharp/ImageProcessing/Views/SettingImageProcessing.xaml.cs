using System;
using System.Collections.Generic;
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

namespace ImageProcessing
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingImageProcessing : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingImageProcessing()
        {
            InitializeComponent();

            LoadParam();
        }

        /// <summary>
        /// デスクトラクタ
        /// </summary>
        ~SettingImageProcessing()
        {
        }

        /// <summary>
        /// 設定の読込
        /// </summary>
        public void LoadParam()
        {
            List<ComImageProcessingType> items = new List<ComImageProcessingType>();
            items.Add(new ComImageProcessingType(Properties.Settings.Default.ImgTypeEdgeId, Properties.Settings.Default.ImgTypeEdgeName));
            items.Add(new ComImageProcessingType(Properties.Settings.Default.ImgTypeGrayScaleId, Properties.Settings.Default.ImgTypeGrayScaleName));
            items.Add(new ComImageProcessingType(Properties.Settings.Default.ImgTypeBinarizationId, Properties.Settings.Default.ImgTypeBinarizationName));
            items.Add(new ComImageProcessingType(Properties.Settings.Default.ImgTypeGrayScale2DiffId, Properties.Settings.Default.ImgTypeGrayScale2DiffName));
            items.Add(new ComImageProcessingType(Properties.Settings.Default.ImgTypeColorReversalId, Properties.Settings.Default.ImgTypeColorReversalName));

            cmbBoxImageProcessingType.ItemsSource = items;
            cmbBoxImageProcessingType.SelectedIndex = (int)items.Find(x => x.Name == Properties.Settings.Default.ImgTypeSelectName)?.Id - 1;

            return;
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public void SaveParam()
        {
            ComImageProcessingType imgProcType = (ComImageProcessingType)cmbBoxImageProcessingType.SelectedItem;
            Properties.Settings.Default.ImgTypeSelectName = imgProcType.Name;
            Properties.Settings.Default.Save();

            return;
        }

        /// <summary>
        /// OKボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            SaveParam();
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Cancelボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">ルーティングイベントのデータ</param>
        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
