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
        private List<ImageProcessingType> m_items;

        public SettingImageProcessing()
        {
            InitializeComponent();

            LoadParam();
        }

        ~SettingImageProcessing()
        {
        }

        public void LoadParam()
        {
            m_items = new List<ImageProcessingType>();
            m_items.Add(new ImageProcessingType(Properties.Settings.Default.ImgTypeEdgeId, Properties.Settings.Default.ImgTypeEdgeName));
            m_items.Add(new ImageProcessingType(Properties.Settings.Default.ImgTypeGrayScaleId, Properties.Settings.Default.ImgTypeGrayScaleName));
            m_items.Add(new ImageProcessingType(Properties.Settings.Default.ImgTypeBinarizationId, Properties.Settings.Default.ImgTypeBinarizationName));

            cmbBoxImageProcessingType.ItemsSource = m_items;
            cmbBoxImageProcessingType.SelectedIndex = (int)m_items.Find(x => x.Name == Properties.Settings.Default.ImgTypeSelectName)?.Id - 1;

            return;
        }

        public void SaveParam()
        {
            ImageProcessingType imgProcType = (ImageProcessingType)cmbBoxImageProcessingType.SelectedItem;
            Properties.Settings.Default.ImgTypeSelectName = imgProcType.Name;
            Properties.Settings.Default.Save();

            return;
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            SaveParam();
            DialogResult = true;
            Close();
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
