using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            InitializeComponent();
            btnCrosshairsColor.Background = new SolidColorBrush(MySnipItTool.Properties.Settings.Default.CrosshairsColor);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)
        {
            slider.Value-=250;
        }

        private void btnIncrease_Click(object sender, RoutedEventArgs e)
        {
            slider.Value+=250;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MySnipItTool.Properties.Settings.Default.Delay = (int)slider.Value;
            MySnipItTool.Properties.Settings.Default.ShowCrosshairs = checkBoxCrosshairs.IsChecked.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            slider.Value = MySnipItTool.Properties.Settings.Default.Delay;
            checkBoxCrosshairs.IsChecked = MySnipItTool.Properties.Settings.Default.ShowCrosshairs;
            btnCrosshairsColor.Background = new SolidColorBrush(MySnipItTool.Properties.Settings.Default.CrosshairsColor);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void btnCrosshairsColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
            
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            System.Windows.Media.Color color = new System.Windows.Media.Color();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                color.A = dialog.Color.A;
                color.B = dialog.Color.B;
                color.G = dialog.Color.G;
                color.R = dialog.Color.R;
                btnCrosshairsColor.Background = new SolidColorBrush(color);
            }
        }
    }
}