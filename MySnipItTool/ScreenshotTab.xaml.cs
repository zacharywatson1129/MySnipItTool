using DrawingShapesLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for ScreenshotTab.xaml
    /// </summary>
    public partial class ScreenshotTab : UserControl
    {
        public ScreenshotTab()
        {
            InitializeComponent();
        }

        double xPointEndSquare;
        double yPointEndSquare;

        public ScreenshotTab(BitmapImage screenshot, Window window)
        {
            InitializeComponent();
            imgControl.Source = screenshot;
            imgControl.MaxWidth = screenshot.Width;
            imgControl.MaxHeight = screenshot.Height;
            canvas.Height = screenshot.Height;
            canvas.MinHeight = screenshot.Height;
            canvas.MaxHeight = screenshot.Height;
            canvas.Width = screenshot.Width;
            canvas.MinWidth = screenshot.Width;
            canvas.MaxWidth = screenshot.Width;
            mainWindow = window as MainWindow;
        }

        public bool IsSaved { get; set; }

        public BitmapImage Screenshot
        {
            get
            {
                if (imgControl.Source is BitmapImage)
                {
                    return imgControl.Source as BitmapImage;
                }
                else
                {
                    return null;
                }
            }
        }

        // Listed in the order they are needed in the logic flow inside the event handlers.
        private MainWindow mainWindow;
        private Ellipse circle;
        private Polyline polyLine;
        private Point startPoint;
        private Point endPoint;
        private Line line;
        private Rectangle rectangle;
        private bool hasStartedDrawing;
        private TextBox textBox;

        private void imgControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hasStartedDrawing = true;
            switch (mainWindow.toolSelected)
            {
                case DrawingTool.FreeDraw:
                    polyLine = new Polyline();
                    SetStrokeProperties(polyLine);
                    polyLine.Points.Add(e.GetPosition(canvas));
                    canvas.Children.Add(polyLine);
                    break;
                case DrawingTool.Line:
                    line = new Line();
                    SetStrokeProperties(line);
                    line.X1 = e.GetPosition(canvas).X;
                    line.Y1 = e.GetPosition(canvas).Y;
                    line.X2 = e.GetPosition(canvas).X;
                    line.Y2 = e.GetPosition(canvas).Y;
                    canvas.Children.Add(line);
                    break;
                case DrawingTool.Rectangle:
                    xPointEndSquare = e.GetPosition(canvas).X;
                    yPointEndSquare = e.GetPosition(canvas).Y;
                    rectangle = new Rectangle();
                    SetStrokeProperties(rectangle);
                    startPoint = e.GetPosition(canvas);
                    endPoint = e.GetPosition(canvas);
                    Canvas.SetLeft(rectangle, startPoint.X);
                    Canvas.SetTop(rectangle, startPoint.Y);
                    canvas.Children.Add(rectangle);
                    break;
                case DrawingTool.Circle:
                    circle = new Ellipse();
                    SetStrokeProperties(circle);
                    startPoint = e.GetPosition(canvas);
                    endPoint = e.GetPosition(canvas);
                    Canvas.SetLeft(circle, startPoint.X);
                    Canvas.SetTop(circle, startPoint.Y);                    
                    canvas.Children.Add(circle);                   
                    break;
                case DrawingTool.Text:
                    startPoint = e.GetPosition(canvas);
                    endPoint = e.GetPosition(canvas);
                    textBox = new TextBox();
                    textBox.Foreground = new SolidColorBrush(mainWindow.color);
                    textBox.MouseEnter += OnMouseOver;
                    textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    textBox.Background = new SolidColorBrush(Colors.Transparent);
                    Canvas.SetLeft(textBox, startPoint.X);
                    Canvas.SetTop(textBox, startPoint.Y);
                    canvas.Children.Add(textBox);
                    // I don't know what this does, but it fixes the issue of
                    // the textbox not being put into focus.
                    Dispatcher.BeginInvoke(
                        DispatcherPriority.ContextIdle,
                        new Action(delegate ()
                        {
                            textBox.Focus();
                        }));
                    break;
            }
        }

        private void SetStrokeProperties(Shape shape)
        {
            shape.MouseEnter += OnMouseOver;
            shape.Stroke = new SolidColorBrush(mainWindow.color);
            // Set the stroke thickness
            if (mainWindow.radioBtnNormal.IsChecked == true)
            {
                shape.StrokeThickness = 3;
            } 
            else if (mainWindow.radioBtnSkinny.IsChecked == true)
            {
                shape.StrokeThickness = 1;
            }
            else if (mainWindow.radioBtnThick.IsChecked == true)
            {
                shape.StrokeThickness = 7;
            }
            // Set the stroke end
            if (mainWindow.radioBtnRound.IsChecked == true)
            {
                shape.StrokeEndLineCap = PenLineCap.Round;
            }
            else if (mainWindow.radioBtnSquare.IsChecked == true)
            {
                shape.StrokeEndLineCap = PenLineCap.Square;
            }
        }

        // This is our eraser event.
        // It simply removes the element from the object when the tool selected
        // is the eraser tool and we have the mouse down.
        private void OnMouseOver(object sender, MouseEventArgs e)
        {
            if ((e.LeftButton == MouseButtonState.Pressed) && (mainWindow.toolSelected == DrawingTool.Eraser))
            {
                canvas.Children.Remove(sender as UIElement);
            }
        }

        private void imgControl_MouseMove(object sender, MouseEventArgs e)
        {
            //TODO I need to make this to where I also have to have started drawing, so I do need a boolean.
            if ((e.LeftButton == MouseButtonState.Pressed) && hasStartedDrawing)
            {
                switch (mainWindow.toolSelected)
                {
                    case DrawingTool.FreeDraw:
                        polyLine.Points.Add(e.GetPosition(canvas));
                        break;
                    case DrawingTool.Line:
                        line.X2 = e.GetPosition(canvas).X;
                        line.Y2 = e.GetPosition(canvas).Y;
                        break;
                    case DrawingTool.Rectangle:
                        {
                            endPoint = e.GetPosition(canvas);
                            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                            {
                                xPointEndSquare = e.GetPosition(canvas).X;
                                CalculateNewShapePosition(startPoint, new Point { X = xPointEndSquare, Y = startPoint.Y + rectangle.ActualWidth }, rectangle);

                            }
                            else if (Keyboard.IsKeyDown(Key.RightCtrl))
                            {
                                yPointEndSquare = e.GetPosition(canvas).Y;
                                CalculateNewShapePosition(startPoint, new Point { X = startPoint.X + rectangle.ActualHeight, Y = yPointEndSquare }, rectangle);
                            }
                            else
                            {
                                CalculateNewShapePosition(startPoint, endPoint, rectangle);
                            }
                        }
                        break;
                    case DrawingTool.Circle:
                        {
                            endPoint = e.GetPosition(canvas);
                            CalculateNewShapePosition(startPoint, endPoint, circle);
                        }
                        break;
                }
            }
        }

        private void imgControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            hasStartedDrawing = false;
            IsSaved = false;
        }

        private void CalculateNewShapePosition(Point startPoint, Point endPoint, Shape shape)
        {
            shape.Width = RectangleShapeResizer.CalculateNewWidth(startPoint.X, endPoint.X);
            shape.Height = RectangleShapeResizer.CalculateNewHeight(startPoint.Y, endPoint.Y);
            double topLeftX = RectangleShapeResizer.GetTopLeftX(startPoint.X, endPoint.X);
            Canvas.SetLeft(shape, topLeftX);
            double topLeftY = RectangleShapeResizer.GetTopLeftY(startPoint.Y, endPoint.Y);
            Canvas.SetTop(shape, topLeftY);
        }

        /*private bool IsInBoundsOfCanvasObject()
        {
            foreach (UIElement item in canvas.Children)
            {
                if (item.PointFromScreen)
            }
            
        }*/

        private double GetWidth(Shape shape)
        {
            return shape.ActualWidth;
        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Space))
            {
                if (e.Delta > 0)
                {

                    st.ScaleX += .07;
                    st.ScaleY += .07;
                    mainWindow.Zoom.Text = "Zoom: " + (st.ScaleX * 100).ToString() + "%";

                }
                else
                {
                    st.ScaleX -= .07;
                    st.ScaleY -= .07;
                    mainWindow.Zoom.Text = "Zoom: " + (st.ScaleX * 100).ToString() + "%";
                }  
            }
        }
    }
}
