using DrawingShapesLibrary;
using MySnipItTool.Properties;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for SnipScreenForm.xaml
    /// </summary>
    public partial class SnipScreenForm : Window
    {
        public SnipScreenForm()
        {
            InitializeComponent();
        }  

        private Rectangle rectangle = new Rectangle();
        private Point startPoint;
        private Point endPoint;
        private Point topLeft;
        private bool isDrawing;
        public BitmapImage screenCapture;
        private bool showCrosshairs = false;
        Line verticalLine = new Line();
        Line horizontalLine = new Line();

        #region Mouse Events
        private void MouseDown_Event(object sender, MouseEventArgs e)
        {
            startPoint = e.GetPosition(canvas);
            endPoint = e.GetPosition(canvas);
            canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, startPoint.X);
            Canvas.SetTop(rectangle, startPoint.Y);
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                endPoint = e.GetPosition(canvas);
                CalculateNewRectanglePosition();

                Point topLeft = GetRectangleTopLeft();
                Point bottomRight = GetRectangleBottomRight();

                Canvas.SetLeft(rectLeft, 0);
                Canvas.SetTop(rectLeft, 0);
                rectLeft.Width = topLeft.X;
                
                Canvas.SetLeft(rectRight, bottomRight.X);
                Canvas.SetTop(rectRight, 0);
                rectRight.Width = Width - bottomRight.X;

                Canvas.SetLeft(rectTop, topLeft.X);
                Canvas.SetTop(rectTop, 0);
                rectTop.Width = rectangle.Width;
                rectTop.Height = topLeft.Y;

                Canvas.SetLeft(rectBottom, topLeft.X);
                Canvas.SetTop(rectBottom, topLeft.Y + rectangle.Height);
                rectBottom.Width = rectangle.Width;
                rectBottom.Height = this.Height - bottomRight.Y;
            }
        }

        private Point GetRectangleTopLeft()
        {
            double x = Canvas.GetLeft(rectangle);
            double y = Canvas.GetTop(rectangle);
            return new Point { X = x, Y = y };
        }

        private Point GetRectangleBottomRight()
        {
            double x = Canvas.GetLeft(rectangle);
            double y = Canvas.GetTop(rectangle);
            Point p = new Point { X = x, Y = y };
            p.X += rectangle.Width;
            p.Y += rectangle.Height;
            return p;
        }

        private int GetWidth()
        {
            Point topLeft = new Point { X = Canvas.GetLeft(rectangle), Y = Canvas.GetTop(rectangle) };
            Point bottomRight = new Point { X = topLeft.X + rectangle.Width, Y = topLeft.Y + rectangle.Height };
            
            topLeft = PointToScreen(topLeft);
            bottomRight = PointToScreen(bottomRight);
            int width = (int)bottomRight.X - (int)topLeft.X;
            return width;

        }

        private int GetHeight()
        {
            Point topLeft = new Point { X = Canvas.GetLeft(rectangle), Y = Canvas.GetTop(rectangle) };
            Point bottomRight = new Point { X = topLeft.X + rectangle.Width, Y = topLeft.Y + rectangle.Height };

            topLeft = PointToScreen(topLeft);
            bottomRight = PointToScreen(bottomRight);
            int height = (int)bottomRight.Y - (int)topLeft.Y;
            return height;
        }

        private void MouseUp_Event(object sender, MouseEventArgs e)
        {
            this.Hide();
            Point p = new Point { X = Canvas.GetLeft(rectangle), Y = Canvas.GetTop(rectangle) };
            p = PointToScreen(p);
            this.screenCapture =
                GetFromScreenshot(ScreenshotUtil.ScreenshotUtility.TakeScreenshot(
                GetWidth(),
                
                GetHeight(),
                new System.Drawing.Point
                {
                    X = (int)p.X,
                    Y = (int)p.Y
                }));
            Close();
        }

        #endregion MouseMove Events

        private void SetCrossHairsPosition(Point currentPosition)
        {
            verticalLine.X1 = currentPosition.X;
            verticalLine.Y1 = 0;
            verticalLine.X2 = currentPosition.X;
            verticalLine.Y2 = Height;

            // Setting the horizontal line properties
            horizontalLine.X1 = 0;
            horizontalLine.Y1 = currentPosition.Y;
            horizontalLine.X2 = Width;
            horizontalLine.Y2 = currentPosition.Y;
        }

        /// <summary>
        /// Returns the current mouse position relative to the screen.
        /// </summary>
        /// <returns>A WPF Point</returns>
        Point GetMousePos()
        {
            return this.PointToScreen(Mouse.GetPosition(this));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.showCrosshairs = Settings.Default.ShowCrosshairs;
            if (showCrosshairs)
            {
                canvas.Children.Add(verticalLine);
                canvas.Children.Add(horizontalLine);
            }

            rectLeft.Width = this.Width / 2;
            rectLeft.Height = this.Height;

            Canvas.SetLeft(rectRight, this.Width / 2);
            Canvas.SetTop(rectRight, 0);

            rectRight.Width = this.Width / 2;
            rectRight.Height = this.Height;
            
            rectangle.Stroke = new SolidColorBrush(Colors.DarkGray);
            rectangle.StrokeThickness = 2;
        }

        private void CalculateNewRectanglePosition()
        {
            rectangle.Width = Math.Abs(startPoint.X - endPoint.X);
            rectangle.Height = Math.Abs(startPoint.Y - endPoint.Y);
            double x = Math.Min(startPoint.X, endPoint.X);
            double y = Math.Min(startPoint.Y, endPoint.Y);
            
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
        }

        private BitmapImage GetFromScreenshot(System.Drawing.Bitmap image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

    }
}
