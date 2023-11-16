using Microsoft.Win32;
using MySnipItTool.Enums;
using ScreenshotUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
// using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// TODO: 

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Color color;
        private double strokeThickness;
        public DrawingMode drawingMode { get; set; } = DrawingMode.None;

        public bool EnableDrawingControls
        {
            get;
            set;
        }

        private void TakeFullScreenshot(object sender, RoutedEventArgs e)
        {
            this.Hide();
            // Uses the stored value to sleep.
            // This is so we don't capture the screenshot tool window in the screenshot :)
            Thread.Sleep(Properties.Settings.Default.Delay);
            int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            BitmapImage screenshot = ScreenshotConverter.ConvertToBitmapImage(ScreenshotUtility.TakeScreenshot(width, height, new System.Drawing.Point(0, 0)));
            AddNewTab(screenshot);
            this.Show();
        }

        private void AddNewTab(BitmapImage screenshot)
        {
            // basicToolsPanel.Visibility = Visibility.Visible;
            // drawingToolsPanel.Visibility = Visibility.Visible;
            TabItem item = new TabItem() { Header = GetDateAndTime(), Content = new ScreenshotTab(screenshot, this) };
            tabControl.Items.Add(item);
            TabCountChanged();
            tabControl.SelectedItem = item;
            SetMessageInStatusBar("Screenshot Captured");
            menuItemDrawing.IsEnabled = true;
            radioBtnNormal.IsChecked = true;
            radioBtnRound.IsChecked = true;
        }

        public string GetDateAndTime()
        {
            return $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
        }

        private void TabCountChanged()
        {
            if (tabControl.Items.Count == 0)
            {
                // No screenshots to edit, so hide all the tools.
                basicToolsPanel.Visibility = Visibility.Hidden;
                drawingToolsPanel.Visibility = Visibility.Hidden;
            } 
            else
            {
                basicToolsPanel.Visibility = Visibility.Visible;
                btnCopy.Visibility = Visibility.Visible;
                drawingToolsPanel.Visibility = Visibility.Visible;
            }
            /*if (tabControl.Items.Count == 0)
            {
                this.Width = 275;
                this.Height = 100;
                EnableDrawingControls = false;
                menuItemCopy.IsEnabled = false;
                menuItemSave.IsEnabled = false;
                drawingToolsGroup.Visibility = Visibility.Collapsed;
                this.ResizeMode = ResizeMode.NoResize;
                menuItemDrawing.IsEnabled = false;
            }
            else
            {
                this.Width = 550;
                this.Height = 350;
                EnableDrawingControls = true;
                menuItemCopy.IsEnabled = true;
                menuItemSave.IsEnabled = true;
                drawingToolsGroup.Visibility = Visibility.Visible;
                this.ResizeMode = ResizeMode.CanResize;
                menuItemDrawing.IsEnabled = true;
            }*/
        }

        private void TakeScreenSnippet(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SnipScreenForm form = new SnipScreenForm();
            form.ShowDialog();
            if (form.screenCapture != null)
            {
                AddNewTab(form.screenCapture);
            }
            this.Show();
            this.Width = 750;
            this.Height = 550;
        }

        private void OpenSettingsDialog(object sender, RoutedEventArgs e)
        {
            // Creates a settings dialog and shows it.
            SettingsDialog dialog = new SettingsDialog();
            dialog.ShowDialog();
        }

        /*private void DeselectAllBtnsBut()
        {
            // btn.Background = new SolidColorBrush(Colors.LightBlue);
            for (int i = 0; i < drawingToolsGroup.Children.Count; i++)
            {
                if (drawingToolsGroup.Children[i] is Button)
                {
                    Button button = drawingToolsGroup.Children[i] as Button;
                    button.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        private void DeselectAllBtnsBut(Button btn)
        {
            btn.Background = new SolidColorBrush(Colors.LightBlue);
            for (int i = 0; i < drawingToolsGroup.Children.Count; i++)
            {
                if (drawingToolsGroup.Children[i] is Button)
                {
                    Button button = drawingToolsGroup.Children[i] as Button;
                    if ((button != btn) && (button != btnColor))
                    {
                        button.Background = new SolidColorBrush(Colors.Transparent);
                    }
                }
                
            }
        }

        private void DeselectAllBtnsBut(RadioButton btn)
        {
            btn.Background = new SolidColorBrush(Colors.LightBlue);
            for (int i = 0; i < drawingToolsGroup.Children.Count; i++)
            {
                if (drawingToolsGroup.Children[i] is RadioButton)
                {
                    RadioButton button = drawingToolsGroup.Children[i] as RadioButton;
                    if ((button != btn))
                    {
                        button.Background = new SolidColorBrush(Colors.Transparent);
                    }
                }

            }
        }*/


        private void btnFreeDraw_Click(object sender, RoutedEventArgs e)
        {
            drawingMode = DrawingMode.FreeDraw;
            TabItem item = tabControl.SelectedItem as TabItem;
            ScreenshotTab tab = item.Content as ScreenshotTab;
            tab.ToolSelected = this.drawingMode;

            //DeselectAllBtnsBut(btnFreeDraw);
            //txtDrawingToolMsg.Text = "Free Draw";
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            drawingMode = DrawingMode.Line;
            TabItem item = tabControl.SelectedItem as TabItem;
            ScreenshotTab tab = item.Content as ScreenshotTab;
            tab.ToolSelected = this.drawingMode;
            //DeselectAllBtnsBut(btnLine);
            //txtDrawingToolMsg.Text = "Line";
        }

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {
            drawingMode = DrawingMode.Rectangle;
            // DeselectAllBtnsBut(btnRectangle);
            //txtDrawingToolMsg.Text = "Rectangle";
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
            
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                color.A = dialog.Color.A;
                color.B = dialog.Color.B;
                color.G = dialog.Color.G;
                color.R = dialog.Color.R;
                btnColor.Background = new SolidColorBrush(color);
            }
        }

        private void btnDeleteTab_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this screenshot?",
                    "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TabItem item = tabControl.SelectedItem as TabItem;
                tabControl.Items.Remove(item);
                TabCountChanged();
                // Close the window  
            }
            
        }

        // This is the save method.
        private void SaveScreenshot(object sender, RoutedEventArgs e)
        {
            try
            {
                // We need to access the screenshot which we can access through the content of the current tab
                // which happens to be a ScreenshotTab object, and there we can access the inkcanvas (called canvas)
                TabItem item = tabControl.SelectedItem as TabItem;
                ScreenshotTab currentScreenshotTab = item.Content as ScreenshotTab;

                // Now we show a SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                // Add the image filters
                // TODO - create a helper class to create file types filter class
                saveDialog.Filter = "Bitmap (*.BMP)|*.BMP|JPEG (*.JPEG)|*.JPEG|GIF (*.GIF)|*.GIF|PNG (*.PNG)|*.PNG|TIFF (*.TIFF)|*.TIFF";
                if (saveDialog.ShowDialog().Value == true)
                {
                    // This creates an image of the proper size and format, and then we render it using
                    // the inkcanvas.
                    RenderTargetBitmap targetBitmap =
                        new RenderTargetBitmap((int)currentScreenshotTab.canvas.ActualWidth,
                                               (int)currentScreenshotTab.canvas.ActualHeight,
                                               96d, 96d,
                                               PixelFormats.Default);
                    currentScreenshotTab.canvas.Measure(new Size((int)currentScreenshotTab.canvas.Width, (int)currentScreenshotTab.canvas.Height));
                    currentScreenshotTab.canvas.Arrange(new Rect(new Size((int)currentScreenshotTab.canvas.Width, (int)currentScreenshotTab.canvas.Height)));
                    targetBitmap.Render(currentScreenshotTab.canvas);

                    // Now we need an encoder, set it as new BmpBitmapEncoder as a default.
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    string extension = saveDialog.FileName.Substring(saveDialog.FileName.LastIndexOf('.'));
                    // Check with the lower case extensions if they match up to these.
                    switch (extension.ToLower())
                    {
                        case ".jpg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        case ".bmp":
                            encoder = new BmpBitmapEncoder();
                            break;
                        case ".gif":
                            encoder = new GifBitmapEncoder();
                            break;
                        case ".png":
                            encoder = new PngBitmapEncoder();
                            break;
                        case ".tiff":
                            encoder = new TiffBitmapEncoder();
                            break;
                        default:
                            MessageBox.Show("Sorry, we cannot save with that file extension.");
                            break;
                    }

                    encoder.Frames.Add(BitmapFrame.Create(targetBitmap));

                    // Make a FileStream object to act as a bridge between the program and the system.
                    // The encoder makes the object in a format to cross the bridge.
                    using (FileStream fs = File.Open(saveDialog.FileName, FileMode.OpenOrCreate))
                    {
                        encoder.Save(fs);
                    }
                    currentScreenshotTab.isSaved = true;
                    SetMessageInStatusBar("Image Saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, but there was an error saving the file to disk. Please see the log file for" +
                    "more details.");
                LogEntry entry = new LogEntry(ex.Message, ex.StackTrace, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                LogUtil.LogToFile("Logs/Log.txt", entry.ToStringArray());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnColor.Background = new SolidColorBrush(Colors.Black);
            color = Colors.Black;
            menuItemDrawing.IsEnabled = false;
            radioBtnNormal.IsChecked = true;
            radioBtnRound.IsChecked = true;
            TabCountChanged();
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            drawingMode = DrawingMode.Circle;
            //DeselectAllBtnsBut(btnCircle);
        }

        private void menuItemAboutMySnipItTool_Click(object sender, RoutedEventArgs e)
        {
            AboutPage page = new AboutPage();
            page.ShowDialog();
        }

        private void CopyCurrentScreenshot(object sender, RoutedEventArgs e)
        {
            //TabItem item = tabControl.SelectedItem as TabItem;
            /*ScreenshotTab currentScreenshotTab = item.Content as ScreenshotTab;
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap(
                (int)currentScreenshotTab.Screenshot.Width,
                (int)currentScreenshotTab.Screenshot.Height,
                96d, 
                96d,
                PixelFormats.Default);

            currentScreenshotTab.canvas.Measure(new Size((int)currentScreenshotTab.canvas.Width, (int)currentScreenshotTab.canvas.Height));
            currentScreenshotTab.canvas.Arrange(new Rect(new Size((int)currentScreenshotTab.canvas.Width, (int)currentScreenshotTab.canvas.Height)));

            targetBitmap.Render(currentScreenshotTab.canvas);

            // CroppedBitmap crop = new CroppedBitmap(targetBitmap, new Int32Rect(0, 0, Convert.ToInt32(currentScreenshotTab.canvas.Width), Convert.ToInt32(currentScreenshotTab.canvas.Height)));

            BitmapFrame bitmapFrame = BitmapFrame.Create(targetBitmap);
            Clipboard.SetImage(bitmapFrame);*/
        }
        System.Windows.Forms.NotifyIcon taskbarIcon;

        private void CloseApplicationTBMenuItem_Click(object sender, EventArgs e)
        {
            this.taskbarIcon.Visible = false;
            Application.Current.Shutdown();
        }

        private void OpenMySnipItToolMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void ScreenSnippetMenuItem_Click(object sender, EventArgs e)
        {
            // this.Show();
            Thread.Sleep(1000);
            TakeScreenSnippet(sender, new RoutedEventArgs());
        }

        private void FullScreenshotMenuItem_Click(object sender, EventArgs e)
        {
            // this.Show();
            // Thread.Sleep(1000);
            TakeFullScreenshot(sender, new RoutedEventArgs());
        }

        private void NotifyButton_Clicked(object sender, EventArgs e)
        {
            this.Show();
        }

        private void MinimzeToTaskbar(object sender, RoutedEventArgs e)
        {
            this.Hide();
            if (taskbarIcon == null)
            {
                taskbarIcon = new System.Windows.Forms.NotifyIcon();
                taskbarIcon.Icon = new System.Drawing.Icon(@"../../mysnipittool.ico");
            }

            taskbarIcon.Visible = true;
            taskbarIcon.Click += NotifyButton_Clicked;
            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();

            System.Windows.Forms.MenuItem openMySnipItToolMenuItem = new System.Windows.Forms.MenuItem();
            openMySnipItToolMenuItem.Text = "Open MySnipItTool";
            openMySnipItToolMenuItem.Click += OpenMySnipItToolMenuItem_Click;

            System.Windows.Forms.MenuItem fullScreenshotMenuItem = new System.Windows.Forms.MenuItem();
            fullScreenshotMenuItem.Text = "Full Screenshot";
            fullScreenshotMenuItem.Click += FullScreenshotMenuItem_Click;

            System.Windows.Forms.MenuItem takeScreenSnippetMenuItem = new System.Windows.Forms.MenuItem();
            takeScreenSnippetMenuItem.Text = "Screen Snippet";
            takeScreenSnippetMenuItem.Click += ScreenSnippetMenuItem_Click;

            System.Windows.Forms.MenuItem closeApplicationTBMenuItem = new System.Windows.Forms.MenuItem();
            closeApplicationTBMenuItem.Text = "Close Application";
            closeApplicationTBMenuItem.Click += CloseApplicationTBMenuItem_Click;

            contextMenu.MenuItems.Add(openMySnipItToolMenuItem);
            contextMenu.MenuItems.Add(fullScreenshotMenuItem);
            contextMenu.MenuItems.Add(takeScreenSnippetMenuItem);
            contextMenu.MenuItems.Add(closeApplicationTBMenuItem);
            taskbarIcon.ContextMenu = contextMenu;
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {

            drawingMode = DrawingMode.Text;
            //DeselectAllBtnsBut(btnText);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        private void btnPolygon_Click(object sender, RoutedEventArgs e)
        {
            // mainMode = DrawingTool.Polygon;
        }

        private void btnEquation_Click(object sender, RoutedEventArgs e)
        {
            //mainMode = DrawingTool.Equation;
        }

        #region Event Handlers for ComboBox Item Selection
        private void comboBoxItemSelect_Selected(object sender, RoutedEventArgs e)
        {
            //mainMode = MainMode.Select;
            //DeselectAllBtnsBut();
            this.Cursor = Cursors.Arrow;
            //txtBlockMode.Text = "Select";
            //txtDrawingToolMsg.Text = "";
        }

        private void comboBoxItemErase_Selected(object sender, RoutedEventArgs e)
        {
            //mainMode = MainMode.Erase;
            //DeselectAllBtnsBut();
            this.Cursor = Cursors.Arrow;
            //txtBlockMode.Text = "Erase";
            //txtDrawingToolMsg.Text = "";
        }

        private void comboBoxItemDraw_Selected(object sender, RoutedEventArgs e)
        {
            //mainMode = MainMode.Draw;
            this.Cursor = Cursors.Cross;
            //txtBlockMode.Text = "Draw";
            //txtDrawingToolMsg.Text = "None";
        }
        #endregion

        public void SetMessageInStatusBar(string msg)
        {
            //textBlockMessage.Text = msg;
        }
    }
}

