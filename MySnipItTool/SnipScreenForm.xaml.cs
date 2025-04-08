using DrawingShapesLibrary;
using MySnipItTool.Properties;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
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
    /// The SnipScreenForm is just a transparent canvas.
    /// It contains rectangles, which get slightly colored to represent
    /// not selected areas. They are the top, bottom, left, and right rectangles.
    /// There is also the center selectionRectangle.
    /// </summary>
    public partial class SnipScreenForm : Window
    {
        public SnipScreenForm()
        {
            InitializeComponent();
        }  

        private Rectangle selectionRectangle = new Rectangle();
        private Point startPoint = new Point { X = 0, Y = 0 };
        private Point endPoint = new Point { X = 0, Y = 0 };
        private Point topLeft = new Point { X = 0, Y = 0 };
        private Point bottomRight = new Point { X = 0, Y = 0 };
        public BitmapImage screenCapture;

        #region Mouse Events
        private void MouseDown_Event(object sender, MouseEventArgs e)
        {
            startPoint = e.GetPosition(canvas);
            endPoint = e.GetPosition(canvas);
            canvas.Children.Add(selectionRectangle);
            Canvas.SetLeft(selectionRectangle, startPoint.X);
            Canvas.SetTop(selectionRectangle, startPoint.Y);
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                endPoint = e.GetPosition(canvas);
                CalculateNewRectanglePosition();

                topLeft = GetRectangleTopLeft();
                bottomRight = GetRectangleBottomRight();

                UpdateBorderRectangles();
            }
        }

        private Point GetRectangleTopLeft()
        {
            double x = Canvas.GetLeft(selectionRectangle);
            double y = Canvas.GetTop(selectionRectangle);
            return new Point { X = x, Y = y };
        }

        private Point GetRectangleBottomRight()
        {
            double x = Canvas.GetLeft(selectionRectangle);
            double y = Canvas.GetTop(selectionRectangle);
            Point p = new Point { X = x, Y = y };
            p.X += selectionRectangle.Width;
            p.Y += selectionRectangle.Height;
            return p;
        }

        private int GetWidth()
        {
            Point topLeft = new Point { X = Canvas.GetLeft(selectionRectangle), Y = Canvas.GetTop(selectionRectangle) };
            Point bottomRight = new Point { X = topLeft.X + selectionRectangle.Width, Y = topLeft.Y + selectionRectangle.Height };
            
            topLeft = PointToScreen(topLeft);
            bottomRight = PointToScreen(bottomRight);
            int width = (int)bottomRight.X - (int)topLeft.X;
            return width;

        }

        private int GetHeight()
        {
            Point topLeft = new Point { X = Canvas.GetLeft(selectionRectangle), Y = Canvas.GetTop(selectionRectangle) };
            Point bottomRight = new Point { X = topLeft.X + selectionRectangle.Width, Y = topLeft.Y + selectionRectangle.Height };

            topLeft = PointToScreen(topLeft);
            bottomRight = PointToScreen(bottomRight);
            int height = (int)bottomRight.Y - (int)topLeft.Y;
            return height;
        }

        private void MouseUp_Event(object sender, MouseEventArgs e)
        {
            this.Hide();
            Point p = new Point { X = Canvas.GetLeft(selectionRectangle), Y = Canvas.GetTop(selectionRectangle) };
            p = PointToScreen(p);
            // Important, here we do a delay!
            int delay = MySnipItTool.Properties.Settings.Default.Delay;
            Thread.Sleep(delay);
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
            verticalLineLeft.X1 = currentPosition.X;
            verticalLineLeft.Y1 = 0;
            verticalLineLeft.X2 = currentPosition.X;
            verticalLineLeft.Y2 = Height;

            // Setting the horizontal line properties
            horizontalLineBottom.X1 = 0;
            horizontalLineBottom.Y1 = currentPosition.Y;
            horizontalLineBottom.X2 = Width;
            horizontalLineBottom.Y2 = currentPosition.Y;
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
            if (Settings.Default.ShowCrosshairs)
            {
                // Set the border colors to be dark.
                EnableDarkBorders();

                // verticalLineLeft.Stroke = new SolidColorBrush(Colors.DarkBlue);
                // horizontalLineBottom.Stroke = new SolidColorBrush(Colors.DarkBlue);
            }

            InitializeRectangleProperties();

            
            

            //rectLeft.Width = this.Width / 2;
            //rectLeft.Height = this.Height;

            //Canvas.SetLeft(rectRight, this.Width / 2);
            //Canvas.SetTop(rectRight, 0);

            //rectRight.Width = this.Width / 2;
            //rectRight.Height = this.Height;
            
            

            
        }

        /// <summary>
        /// Simply sets the stroke colors of rectLeft, rectRight, rectTop, and rectBottom to be black.
        /// This method is called whenever crosshairs are enabled. Colored borders of these rectangles
        /// eliminates the need for 4 lines to be drawn.
        /// </summary>
        private void EnableDarkBorders()
        {
            Brush border = new SolidColorBrush(MySnipItTool.Properties.Settings.Default.CrosshairsColor);
            rectLeft.Stroke = border;
            rectRight.Stroke = border;
            rectTop.Stroke = border;
            rectBottom.Stroke = border;
        }

        /// <summary>
        /// Sets the position of the border rectangles and initializes selection rectangle properties.
        /// </summary>
        private void InitializeRectangleProperties()
        {
            // This is the same regardless of whether dark borders for crosshairs are enabled.
            // selectionRectangle.Stroke = new SolidColorBrush(Colors.DarkGray);
            selectionRectangle.Stroke = new SolidColorBrush(Colors.OrangeRed);
            DoubleCollection c = new DoubleCollection() { 5.0d, 1.0d };
            selectionRectangle.StrokeDashArray = c;
            
            selectionRectangle.StrokeThickness = 2;

            // Left rectangle has top left point of 0,0.
            // It has a width going from left side of screen to topLeft.X.
            Canvas.SetLeft(rectLeft, 0);
            Canvas.SetTop(rectLeft, 0);
            rectLeft.Width = topLeft.X;
            rectLeft.Height = this.Height;
            // Left rectangle height never changes.

            // Right rectangle has top left point of bottomRight.X, 0
            Canvas.SetLeft(rectRight, bottomRight.X);
            Canvas.SetTop(rectRight, 0);
            rectRight.Width = this.Width - bottomRight.X;
            rectRight.Height = this.Height;
            // Right rectangle height never changes.


            // Top rectangle has a top left point of 0, 0.
            // It has a width as wide as the screen.
            Canvas.SetLeft(rectTop, 0);
            Canvas.SetTop(rectTop, 0);
            rectTop.Height = topLeft.Y;
            rectTop.Width = this.Width;
            // Width never changes.

            // The bottom rectangle starts at topLeft.X, topLeft.Y + height of selection rectangle.
            Canvas.SetLeft(rectBottom, 0);
            Canvas.SetTop(rectBottom, topLeft.Y + selectionRectangle.Height);
            rectBottom.Height = this.Height - bottomRight.Y;
            rectBottom.Width = this.Width;
        }

        private void UpdateBorderRectangles()
        {
            // Left rectangle has top left point of 0,0.
            // It has a width going from left side of screen to topLeft.X.
            rectLeft.Width = topLeft.X;
            // Left rectangle height never changes.

            // Right rectangle has top left point of bottomRight.X, 0
            Canvas.SetLeft(rectRight, bottomRight.X);
            rectRight.Width = this.Width - bottomRight.X;
            // Right rectangle height never changes.


            // Top rectangle has a top left point of 0, 0.
            // It has a width as wide as the screen.

            rectTop.Height = topLeft.Y;
            // Width never changes.

            // The bottom rectangle starts at topLeft.X, topLeft.Y + height of selection rectangle.
            Canvas.SetTop(rectBottom, topLeft.Y + selectionRectangle.Height);
            rectBottom.Height = this.Height - bottomRight.Y;
        }

        /// <summary>
        /// Recalculates the rectangle position given the selected area, and repositions it on canvas.
        /// </summary>
        private void CalculateNewRectanglePosition()
        {
            selectionRectangle.Width = Math.Abs(startPoint.X - endPoint.X);
            selectionRectangle.Height = Math.Abs(startPoint.Y - endPoint.Y);

            topLeft.X = Math.Min(startPoint.X, endPoint.X);
            topLeft.Y = Math.Min(startPoint.Y, endPoint.Y);

            bottomRight.X = topLeft.X + selectionRectangle.Width;
            bottomRight.Y = topLeft.Y + selectionRectangle.Height;

            Canvas.SetLeft(selectionRectangle, topLeft.X);
            Canvas.SetTop(selectionRectangle, topLeft.Y);
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
