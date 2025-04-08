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
        private System.Windows.Media.Color crosshairColor;

        public SettingsDialog()
        {
            InitializeComponent();
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MySnipItTool.Properties.Settings.Default.Delay = (int)slider.Value;
            MySnipItTool.Properties.Settings.Default.ShowCrosshairs = checkBoxCrosshairs.IsChecked.Value;
            MySnipItTool.Properties.Settings.Default.CrosshairsColor = crosshairColor;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // --Set each setting value.--

            // Delay
            slider.Value = MySnipItTool.Properties.Settings.Default.Delay;
            
            // Show Crosshairs
            checkBoxCrosshairs.IsChecked = MySnipItTool.Properties.Settings.Default.ShowCrosshairs;
            
            // Crosshairs color
            crosshairColor = MySnipItTool.Properties.Settings.Default.CrosshairsColor;
            btnCrosshairsColor.Background = new SolidColorBrush(crosshairColor);
        }

        
        private void btnCrosshairsColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
            
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            crosshairColor = new System.Windows.Media.Color();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                crosshairColor.A = dialog.Color.A;
                crosshairColor.B = dialog.Color.B;
                crosshairColor.G = dialog.Color.G;
                crosshairColor.R = dialog.Color.R;
                btnCrosshairsColor.Background = new SolidColorBrush(crosshairColor);
                
            }
        }
    }
}