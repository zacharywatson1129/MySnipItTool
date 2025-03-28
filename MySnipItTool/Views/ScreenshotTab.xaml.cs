using DrawingShapesLibrary;
using MySnipItTool.Enums;
using MySnipItTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
//using WpfMath.Controls;

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for ScreenshotTab.xaml
    /// This class represents a single screenshot tab, and should handle the logic.
    /// The drawing state is represented through the DrawingMode enum:
    /// { None, Eraser, FreeDraw, Line, Rectangle, Circle, Polygon, Text, Equation, Select }
    /// </summary>
    public partial class ScreenshotTab : UserControl
    {
        // TODO - set up a selection system. Possibly a single selection mode and multiselection mode.
        public ScreenshotTab()
        {
            InitializeComponent();
        }

        public ScreenshotTab(BitmapSource screenshot, Window window)
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

        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }

        public static readonly DependencyProperty ToolSelectedProperty =
            DependencyProperty.Register("ToolSelected", typeof(DrawingMode), typeof(ScreenshotTab));

        public DrawingMode ToolSelected
        {
            get => (DrawingMode)GetValue(ToolSelectedProperty);
            set 
            {
                RemoveEventHandlers();
                SetValue(ToolSelectedProperty, value);
                SetEventHandlers();
            } 
        }

        private void RemoveEventHandlers()
        {
            switch (ToolSelected)
            {
                case DrawingMode.None:
                    break;
                case DrawingMode.Eraser:
                    break;
                case DrawingMode.FreeDraw:
                    break;
                case DrawingMode.Line:
                    break;
                case DrawingMode.Rectangle:
                    break;
                case DrawingMode.Circle:
                    break;
                case DrawingMode.Polygon:
                    break;
                case DrawingMode.Text:
                    break;
                case DrawingMode.Equation:
                    break;
                case DrawingMode.Select:
                    break;
                default:
                    break;
            }
        }

        private void SetEventHandlers()
        {
            // switch()
        }

        private void line_down(object sender, MouseButtonEventArgs e)
        {

        }


        public bool isSaved { get; set; }

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

        //private List<BoundsHolder> bounds = new List<BoundsHolder>();
        private double width = 20;
        private int counter = 0;
        private Thumb lineHolder;
        private MainWindow mainWindow;
        private Ellipse circle;
        private Polyline polyLine;
        private Polygon polygon;
        private Point startPoint;
        private Point endPoint;
        private Line line;
        private Rectangle rectangle;
        private bool hasStartedDrawing;
        /// <summary>
        /// If true we are moving a shape. False means we are not.
        /// </summary>
        private bool hasStartedMoving;
        private TextBox textBox;
        private Shape shape;
        private Point MouseStart;
        private Shape currentlyDrawingShape;

        double CalculateXMoveOver(Point now)
        {
            return now.X - MouseStart.X;
        }

        double CalculateYMoveOver(Point now)
        {
            return now.Y - MouseStart.Y;
        }

        bool isCurrentlyInBoundsOfAShape(MouseButtonEventArgs e)
        {
            /*foreach (var b in bounds)
            {
                if (isPositionInBounds(b.Bounds, e.GetPosition(canvas)))
                {
                    return true;
                }
            }*/
            return false;
        }

        private Point OriginalSelectedPos;
        private Shape selectedShape;
        //private SelectionBounds selectedShapeBounds; //= new SelectionBounds(canvas, new Rectangle());

        /// <summary>
        /// This is actually the mouse down event associated with the canvas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hasStartedDrawing = true;
            Trace.WriteLine("Mouse down");

            switch (mainWindow.drawingMode)
            {
                case DrawingMode.None:
                    break;
                case DrawingMode.Eraser:
                    break;
                case DrawingMode.FreeDraw:
                    polyLine = new Polyline();
                    SetStrokeProperties(polyLine);
                    canvas.Children.Add(polyLine);
                    polyLine.Points.Add(e.GetPosition(canvas));
                    break;
                case DrawingMode.Line:
                    line = new Line();
                    SetStrokeProperties(line);
                    line.X1 = e.GetPosition(canvas).X;
                    line.Y1 = e.GetPosition(canvas).Y;
                    line.X2 = e.GetPosition(canvas).X;
                    line.Y2 = e.GetPosition(canvas).Y;
                    canvas.Children.Add(line);
                    currentlyDrawingShape = line;
                    break;
                case DrawingMode.Rectangle:
                    rectangle = new Rectangle();
                    currentlyDrawingShape = rectangle;
                    SetStrokeProperties(rectangle);
                    startPoint = e.GetPosition(canvas);
                    endPoint = e.GetPosition(canvas);
                    Canvas.SetLeft(rectangle, startPoint.X);
                    Canvas.SetTop(rectangle, startPoint.Y);
                    canvas.Children.Add(rectangle);
                    break;
                case DrawingMode.Circle:
                    circle = new Ellipse();
                    currentlyDrawingShape = circle;
                    SetStrokeProperties(circle);
                    startPoint = e.GetPosition(canvas);
                    endPoint = e.GetPosition(canvas);
                    Canvas.SetLeft(circle, startPoint.X);
                    Canvas.SetTop(circle, startPoint.Y);
                    canvas.Children.Add(circle);
                    break;
                case DrawingMode.Polygon:
                    break;
                case DrawingMode.Text:
                    textBox = new TextBox();
                    textBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    textBox.BorderThickness = new Thickness(4);
                    textBox.MouseEnter += OnMouseOver;

                    Canvas.SetLeft(textBox, e.GetPosition(canvas).X);
                    Canvas.SetTop(textBox, e.GetPosition(canvas).Y);
                    canvas.Children.Add(textBox);
                    // This brings focus to the textbox so user can start typing in it right away.
                    Dispatcher.BeginInvoke(
                                    DispatcherPriority.ContextIdle,
                                    new Action(delegate ()
                                    {
                                        textBox.Focus();
                                    }));
                    break;
                case DrawingMode.Equation:
                    break;
                case DrawingMode.Select:
                    break;
                default:
                    break;
            }
        }

        private void TextBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FontDialog dialog = new System.Windows.Forms.FontDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                textBox.FontFamily = new FontFamily(dialog.Font.Name);
                // Not sure what the 96.0 / 72.0 does...my best guess is it fixes conversion issues?
                textBox.FontSize = dialog.Font.Size * 96.0 / 72.0;
                textBox.FontWeight = dialog.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                textBox.FontStyle = dialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void SetStrokeProperties(Shape shape)
        {
            shape.MouseEnter += OnMouseOver;

            shape.Stroke = new SolidColorBrush(mainWindow.strokeColor);
            shape.StrokeThickness = mainWindow.strokeThickness;
            shape.Fill = new SolidColorBrush(Colors.Transparent);
            // Set the stroke thickness
            /*if (mainWindow.radioBtnNormal.IsChecked == true)
            {
                shape.StrokeThickness = 7;
            }
            else if (mainWindow.radioBtnSkinny.IsChecked == true)
            {
                shape.StrokeThickness = 7;
            }
            else if (mainWindow.radioBtnThick.IsChecked == true)
            {
                shape.StrokeThickness = 7;
            }*/
            // Set the stroke end
            if (mainWindow.CapEndType == 0)
            {
                shape.StrokeStartLineCap = PenLineCap.Round;
                shape.StrokeEndLineCap = PenLineCap.Round;
            }
            else
            {
                shape.StrokeStartLineCap = PenLineCap.Square;
                shape.StrokeEndLineCap = PenLineCap.Square;
            }
        }

        // Selection variables
        Point p;
        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;
        UIElement source = null;

        // This is our eraser event.
        // It simply removes the element from the object when the tool selected
        // is the eraser tool and we have the mouse down.
        private void OnMouseOver(object sender, MouseEventArgs e)
        {
            if ((e.LeftButton == MouseButtonState.Pressed) && (mainWindow.drawingMode == DrawingMode.Eraser))
            {
                canvas.Children.Remove(sender as UIElement);
            }
            /*else if ((e.LeftButton == MouseButtonState.Pressed) && (mainWindow.mainMode == MainMode.Select))
            {

            }*/
        }

        private Polygon CreatePolygon(double radius, int numSides, Point center)
        {
            List<Point> pts = new List<Point>();
            pts.Add(new Point { X = center.X, Y = center.Y + radius });
            double theta = (2 * Math.PI) / numSides;
            for (int i = 1; i < numSides; i++)
            {
                pts.Add(new Point { X = center.X + radius * Math.Sin(theta), Y = center.Y + radius * Math.Cos(theta) });
                theta += (2 * Math.PI) / numSides;
            }
            Polygon output = new Polygon();
            pts.ForEach(x => output.Points.Add(x));

            if (pts.Count % 2 == 0)
            {
                RotateTransform rotation = new RotateTransform((360 / numSides) / 2);
                rotation.CenterX = center.X;
                rotation.CenterY = center.Y;
                output.RenderTransform = rotation;
            }

            return output;
        }

        private void imgControl_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.LeftButton == MouseButtonState.Pressed) && hasStartedDrawing)
            {
                switch (mainWindow.drawingMode)
                {
                    case DrawingMode.None:
                        break;
                    case DrawingMode.Eraser:
                        break;
                    case DrawingMode.FreeDraw:
                        polyLine.Points.Add(e.GetPosition(canvas));
                        break;
                    case DrawingMode.Line:
                        line.X2 = e.GetPosition(canvas).X;
                        line.Y2 = e.GetPosition(canvas).Y;
                        break;
                    case DrawingMode.Rectangle:
                        endPoint = e.GetPosition(canvas);
                        CalculateNewShapePosition(startPoint, endPoint, rectangle);
                        break;
                    case DrawingMode.Circle:
                        endPoint = e.GetPosition(canvas);
                        CalculateNewShapePosition(startPoint, endPoint, circle);
                        break;
                    case DrawingMode.Polygon:
                        break;
                    case DrawingMode.Text:
                        break;
                    case DrawingMode.Equation:
                        break;
                    case DrawingMode.Select:
                        break;
                    default:
                        break;
                }

                //Trace.WriteLine($"Mouse moved to ({e.GetPosition(canvas).X}, {e.GetPosition(canvas).Y})");
                /*switch (mainWindow.mainMode)
                {
                    case MainMode.Draw:
                        switch (mainWindow.drawingMode)
                        {
                            case DrawingMode.FreeDraw:

                                polyLine.Points.Add(e.GetPosition(canvas));
                                break;
                            case DrawingMode.Line:
                                line.X2 = e.GetPosition(canvas).X;
                                line.Y2 = e.GetPosition(canvas).Y;
                                break;
                            case DrawingMode.Rectangle:
                                endPoint = e.GetPosition(canvas);
                                CalculateNewShapePosition(startPoint, endPoint, rectangle);
                                break;
                            case DrawingMode.Circle:
                                endPoint = e.GetPosition(canvas);
                                CalculateNewShapePosition(startPoint, endPoint, circle);
                                break;
                        }
                        break;
                }*/
            }
        }

        Rectangle SetSelectionBounds(Polyline element)
        {
            double minX = getMinX(element.Points);
            double minY = getMinY(element.Points);
            double maxX = getMaxX(element.Points);
            double maxY = getMaxY(element.Points);

            Rectangle output = new Rectangle();

            output.Width = maxX - minX;
            output.Height = maxY - minY;

            Canvas.SetTop(output, minY);
            Canvas.SetLeft(output, minX);

            return output;

        }

        double getMinX(PointCollection collection)
        {
            double min = 1000000;
            foreach (Point point in collection)
            {
                if (point.X < min)
                {
                    min = point.X;
                }
            }
            return min;
        }

        double getMinY(PointCollection collection)
        {
            double min = 1000000;
            foreach (Point point in collection)
            {
                if (point.Y < min)
                {
                    min = point.Y;
                }
            }
            return min;
        }

        double getMaxX(PointCollection collection)
        {
            double max = -5;
            foreach (Point point in collection)
            {
                if (point.X > max)
                {
                    max = point.X;
                }
            }
            return max;
        }

        double getMaxY(PointCollection collection)
        {
            double max = -5;
            foreach (Point point in collection)
            {
                if (point.Y > max)
                {
                    max = point.Y;
                }
            }
            return max;
        }

        bool isPositionInBounds(Rectangle rect, Point pos)
        {
            if (pos.X >= Canvas.GetLeft(rect) || pos.X <= Canvas.GetLeft(rect) + rect.Width)
            {
                if (pos.Y >= Canvas.GetTop(rect) || pos.X <= Canvas.GetTop(rect) + rect.Height)
                {
                    return true;
                }
            }
            return false;
        }

        bool SetShapeBounds(MouseButtonEventArgs e)
        {
            foreach (UIElement item in canvas.Children)
            {
                if (item is Polyline)
                {
                    Polyline p = (Polyline)item;
                    Rectangle r = SetSelectionBounds(p);
                    if (isPositionInBounds(r, e.GetPosition(canvas)))
                    {
                        canvas.Children.Add(r);
                        r.Stroke = new SolidColorBrush(Colors.Red);
                        r.StrokeThickness = 5;
                    }
                }
            }
            return false;
        }

        private void fillClickEvent(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Test!");
            // TODO: Actually show a colorbox.
        }

        /// <summary>
        /// Event happens when your mouse button is released on the canvas. We set this as PreviewMouseUp.
        /// </summary>
        /// <param name="sender">The canvas</param>
        /// <param name="e">Info about the state of the mouse.</param>
        private void imgControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (mainWindow.drawingMode)
            {
                case DrawingMode.None:
                    break;
                case DrawingMode.Eraser:
                    break;
                case DrawingMode.FreeDraw:

                    polyLine = null;
                    break;
                case DrawingMode.Line:
                    p = line.TranslatePoint(new Point(0, 0), canvas);

                    double currentLeft = p.X;
                    double currentTop = p.Y;

                    Canvas.SetLeft(line, currentLeft);
                    Canvas.SetTop(line, currentTop);

                    break;
                case DrawingMode.Rectangle:
                    break;
                case DrawingMode.Circle:
                    break;
                case DrawingMode.Polygon:
                    break;
                case DrawingMode.Text:
                    break;
                case DrawingMode.Equation:
                    break;
                case DrawingMode.Select:
                    break;
                default:

                    break;
            }
            currentlyDrawingShape = null;

            /*switch (mainWindow.mainMode)
            {
                case MainMode.Draw:
                    {
                        if (mainWindow.drawingMode == DrawingMode.Text) { return; }
                        /*ContextMenu menu = new ContextMenu();
                        MenuItem fill = new MenuItem() { Header = "Fill" };
                        menu.Items.Add(fill);
                        fill.Click += fillClickEvent;
                        // menu.Items.Add(new Menu() { fill });
                        currentlyDrawingShape.ContextMenu = menu;*/
                        //currentlyDrawingShape.MouseRightButtonDown += CurrentlyDrawingShape_MouseRightButtonDown;
                        //currentlyDrawingShape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
                        //currentlyDrawingShape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
                        //currentlyDrawingShape.MouseMove += Shape_MouseMove;

                       /*switch (mainWindow.drawingMode)
                        {
                            case DrawingMode.Line:

                                p = line.TranslatePoint(new Point(0, 0), canvas);

                                double currentLeft = p.X;
                                double currentTop = p.Y;

                                Canvas.SetLeft(line, currentLeft);
                                Canvas.SetTop(line, currentTop);

                                break;
                        }
                        currentlyDrawingShape = null;
                        break;
                    }
            }*/
            hasStartedDrawing = false;
            isSaved = false;
            scrollViewer.IsEnabled = true;
            //MessageBox.Show("T")
        }

        private void CurrentlyDrawingShape_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Line_MouseUp(object sender, MouseButtonEventArgs e)
        {
            selectedShape = null;
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

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && hasStartedDrawing)
            {
                /*if (mainWindow.mainMode == DrawingTool.Polygon)
                {

                    if (e.Delta > 0)
                    {
                        // System.Windows.MessageBox.Show("e.Delta equals " + e.Delta.ToString());
                        width += 1;

                    }
                    else if (e.Delta < 0)
                    {
                        width -= 1;
                    }
                    canvas.Children.Remove(polygon);
                    // width += 1;
                    polygon = CreatePolygon(width, 8, e.GetPosition(canvas));
                    polygon.Stroke = new SolidColorBrush(Colors.Purple);
                    canvas.Children.Add(polygon);
                }*/
            }
        }
    }
}
