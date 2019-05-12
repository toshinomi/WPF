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

            cmbBoxImageProcessingType.DataContext = m_items.Find(x => x.Id == cmbBoxImageProcessingType.SelectedIndex + 1)?.Name;
        }

        ~SettingImageProcessing()
        {
        }

        public void LoadParam()
        {
            m_items = new List<ImageProcessingType>();
            m_items.Add(new ImageProcessingType() { Id = Properties.Settings.Default.ImgTypeEdgeId, Name = Properties.Settings.Default.ImgTypeEdgeName });
            m_items.Add(new ImageProcessingType() { Id = Properties.Settings.Default.ImgTypeGrayScaleId, Name = Properties.Settings.Default.ImgTypeGrayScaleName });


            cmbBoxImageProcessingType.ItemsSource = m_items;
            cmbBoxImageProcessingType.SelectedIndex = Properties.Settings.Default.ImgTypeSelectIndex;
            cmbBoxImageProcessingType.DataContext = Properties.Settings.Default.ImgTypeSelectName;

            return;
        }

        public void SaveParam()
        {
            Properties.Settings.Default.ImgTypeSelectIndex = cmbBoxImageProcessingType.SelectedIndex;
            Properties.Settings.Default.ImgTypeSelectName = (string)cmbBoxImageProcessingType.DataContext;
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

        private void OnSelectionChangedCmbBoxImageProcessingType(object sender, SelectionChangedEventArgs e)
        {
            cmbBoxImageProcessingType.DataContext = m_items.Find(x => x.Id == cmbBoxImageProcessingType.SelectedIndex + 1)?.Name;
        }
    }
}
