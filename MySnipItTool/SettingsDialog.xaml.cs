using System;
using System.Windows;

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
        }
    }
}