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
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();

            LoadParam();
        }

        ~SettingWindow()
        {
        }

        public void LoadParam()
        {
            List<ImageProcessingType> items = new List<ImageProcessingType>();
            items.Add(new ImageProcessingType() { Id = Properties.Settings.Default.ImgTypeEdgeId, Name = Properties.Settings.Default.ImgTypeEdgeName });

            cmbBoxImageProcessingType.ItemsSource = items;
            cmbBoxImageProcessingType.SelectedIndex = Properties.Settings.Default.ImgTypeSelect;

            return;
        }

        public void SaveParam()
        {
            Properties.Settings.Default.ImgTypeSelect = cmbBoxImageProcessingType.SelectedIndex;
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
            DialogResult = false;
            Close();
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }
    }
}
