﻿using DrawingShapesLibrary;
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
using WpfMath.Controls;

namespace MySnipItTool
{
    /// <summary>
    /// Interaction logic for ScreenshotTab.xaml
    /// </summary>
    public partial class ScreenshotTab : UserControl
    {
        // TODO - set up a selection system. Possibly a single selection mode and multiselection mode.
        public ScreenshotTab()
        {
            InitializeComponent();
        }

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
            selectedShapeBounds = new SelectionBounds(canvas, new Rectangle());
        }

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
                // Change event handlers.
                // canvas.MouseDown += Canvas_MouseDown;
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

        private List<BoundsHolder> bounds = new List<BoundsHolder>();
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
            foreach (var b in bounds)
            {
                if (isPositionInBounds(b.Bounds, e.GetPosition(canvas)))
                {
                    return true;
                }
            }
            return false;
        }

        private Point OriginalSelectedPos;
        private Shape selectedShape;
        private SelectionBounds selectedShapeBounds; //= new SelectionBounds(canvas, new Rectangle());

        //private void imgControl_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    hasStartedDrawing = true;
        //    Trace.WriteLine("Mouse down");

        //    switch (mainWindow.mainMode)
        //    {
        //        case MainMode.Select:
        //            {
        //                MouseStart = e.GetPosition(canvas);

        //                HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));
        //                if (hitTestResult != null)
        //                {
        //                    var element = hitTestResult.VisualHit;

        //                    if (element is Rectangle)
        //                    {
        //                        /*
        //                        Rectangle selectedRectangle = element as Rectangle;
        //                        selectedShape = selectedRectangle;
        //                        OriginalSelectedPos = new Point { X = Canvas.GetLeft(selectedRectangle), Y = Canvas.GetTop(selectedRectangle) };
        //                        if (canvas.Children.Contains(selectedShapeBounds.MainBounds))
        //                        {
        //                            canvas.Children.Remove(selectedShapeBounds.MainBounds);
        //                            canvas.Children.Remove(selectedShapeBounds.TopLeftSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.TopRightSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.BottomLeftSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.BottomRightSelector);
        //                        }

        //                        selectedShapeBounds = new SelectionBounds(selectedRectangle);

        //                        // canvas.Children.Add(selectedShapeBounds.MainBounds);
        //                        canvas.Children.Add(selectedShapeBounds.TopLeftSelector);
        //                        canvas.Children.Add(selectedShapeBounds.TopRightSelector);
        //                        canvas.Children.Add(selectedShapeBounds.BottomLeftSelector);
        //                        canvas.Children.Add(selectedShapeBounds.BottomRightSelector);

        //                        Canvas.SetTop(selectedShapeBounds.MainBounds, Canvas.GetTop(selectedRectangle) - 5);
        //                        Canvas.SetLeft(selectedShapeBounds.MainBounds, Canvas.GetLeft(selectedRectangle) - 5);

        //                        Canvas.SetTop(selectedShapeBounds.TopLeftSelector, Canvas.GetTop(selectedShapeBounds.MainBounds) - 3);
        //                        Canvas.SetLeft(selectedShapeBounds.TopLeftSelector, Canvas.GetLeft(selectedShapeBounds.MainBounds) - 3);

        //                        Canvas.SetTop(selectedShapeBounds.TopRightSelector, Canvas.GetTop(selectedShapeBounds.MainBounds) - 3);
        //                        Canvas.SetLeft(selectedShapeBounds.TopRightSelector, (Canvas.GetLeft(selectedShapeBounds.MainBounds) + selectedShapeBounds.MainBounds.Width) - 6);

        //                        Canvas.SetTop(selectedShapeBounds.BottomLeftSelector, (Canvas.GetTop(selectedRectangle) - 12) + selectedShapeBounds.MainBounds.Height);
        //                        Canvas.SetLeft(selectedShapeBounds.BottomLeftSelector, Canvas.GetLeft(selectedShapeBounds.MainBounds) - 3);
        //                        */
        //                        /*Rectangle selectedRect = element as Rectangle;
        //                        mainBounds.Width = selectedRect.Width + 10;
        //                        mainBounds.Height = selectedRect.Height + 10;

        //                        topLeftBounds.Width = 3;
        //                        topLeftBounds.Height = 3;
        //                        topLeftBounds.Stroke = new SolidColorBrush(Colors.Blue);
        //                        topRight.Stroke = new SolidColorBrush(Colors.Blue);
        //                        botLeftBounds.Stroke = new SolidColorBrush(Colors.Blue);
        //                        botRightBounds.Stroke = new SolidColorBrush(Colors.Blue);

        //                        canvas.Children.Add(mainBounds);
        //                        canvas.Children.Add(topLeftBounds);
        //                        canvas.Children.Add(topRight);
        //                        canvas.Children.Add(botLeftBounds);
        //                        canvas.Children.Add(botRightBounds);

        //                        Canvas.SetTop(mainBounds, Canvas.GetTop(selectedRect) - 5);
        //                        Canvas.SetLeft(mainBounds, Canvas.GetLeft(selectedRect) - 5);

        //                        Canvas.SetTop(topLeftBounds, Canvas.GetTop(selectedRect) - 8);
        //                        Canvas.SetLeft(topLeftBounds, Canvas.GetLeft(selectedRect) - 8);


        //                        mainBounds.StrokeDashCap = PenLineCap.Flat;
        //                        mainBounds.StrokeLineJoin = PenLineJoin.Bevel;
        //                        mainBounds.StrokeThickness = 3;
        //                        mainBounds.Stroke = new SolidColorBrush(Colors.Gray);*/
        //                    }
        //                    if (element is Ellipse)
        //                    {

        //                        Ellipse selectedEllipse = element as Ellipse;
        //                        selectedShape = selectedEllipse;
        //                        OriginalSelectedPos = new Point { X = Canvas.GetLeft(selectedEllipse), Y = Canvas.GetTop(selectedEllipse) };

        //                        if (canvas.Children.Contains(selectedShapeBounds.MainBounds))
        //                        {
        //                            selectedShapeBounds.reset();
        //                            /*canvas.Children.Remove(selectedShapeBounds.MainBounds);
        //                            canvas.Children.Remove(selectedShapeBounds.TopLeftSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.TopRightSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.BottomLeftSelector);
        //                            canvas.Children.Remove(selectedShapeBounds.BottomRightSelector);*/
        //                        }

        //                        selectedShapeBounds = new SelectionBounds(canvas, selectedEllipse);

        //                        /*canvas.Children.Add(selectedShapeBounds.MainBounds);
        //                        canvas.Children.Add(selectedShapeBounds.TopLeftSelector);
        //                        canvas.Children.Add(selectedShapeBounds.TopRightSelector);
        //                        canvas.Children.Add(selectedShapeBounds.BottomLeftSelector);
        //                        canvas.Children.Add(selectedShapeBounds.BottomRightSelector);*/



        //                    }
        //                    if (element is Polyline)
        //                    {
        //                        Selector.SetIsSelected(element, true);
        //                    }
        //                }
        //                break;
        //            }
        //        case MainMode.Draw:
        //            {
        //                switch (mainWindow.drawingMode)
        //                {
        //                    case DrawingMode.Circle:
        //                        circle = new Ellipse();
        //                        currentlyDrawingShape = circle;
        //                        SetStrokeProperties(circle);
        //                        startPoint = e.GetPosition(canvas);
        //                        endPoint = e.GetPosition(canvas);
        //                        Canvas.SetLeft(circle, startPoint.X);
        //                        Canvas.SetTop(circle, startPoint.Y);
        //                        canvas.Children.Add(circle);
        //                        break;
        //                    case DrawingMode.Equation:
        //                        // TODO - Replace this code with actual code that loads the LaTeXEquationManager window.
        //                        FormulaControl control = new FormulaControl();
        //                        control.Formula = @"\left(x^2 + 2 \cdot x + 2\right) = 0";
        //                        canvas.Children.Add(control);
        //                        Canvas.SetTop(control, e.GetPosition(canvas).Y);
        //                        Canvas.SetLeft(control, e.GetPosition(canvas).X);
        //                        break;
        //                    case DrawingMode.FreeDraw:
        //                        polyLine = new Polyline();
        //                        polyLine.StrokeMiterLimit = 1;
        //                        currentlyDrawingShape = polyLine;
        //                        SetStrokeProperties(polyLine);
        //                        polyLine.Points.Add(e.GetPosition(canvas));
        //                        canvas.Children.Add(polyLine);
        //                        break;
        //                    case DrawingMode.Line:
        //                        line = new Line();
        //                        currentlyDrawingShape = line;
        //                        SetStrokeProperties(line);
        //                        line.X1 = e.GetPosition(canvas).X;
        //                        line.Y1 = e.GetPosition(canvas).Y;
        //                        line.X2 = e.GetPosition(canvas).X;
        //                        line.Y2 = e.GetPosition(canvas).Y;
        //                        canvas.Children.Add(line);
        //                        break;
        //                    case DrawingMode.Rectangle:
        //                        rectangle = new Rectangle();
        //                        currentlyDrawingShape = rectangle;
        //                        ContextMenu cm = new ContextMenu();
        //                        // cm.Items.Add(new ShapePropertiesPanel());
        //                        // cm.Items.Add(new Label() { Content = "hello" });

        //                        rectangle.ContextMenu = cm;

        //                        // TODO - add context menu to all of these items
        //                        // 1. Set Fill
        //                        // 2. Set Border
        //                        // rectangle.ContextMenu.Items.Add(new TextBlock() { Text = "Testing..." });
        //                        SetStrokeProperties(rectangle);
        //                        startPoint = e.GetPosition(canvas);
        //                        endPoint = e.GetPosition(canvas);
        //                        Canvas.SetLeft(rectangle, startPoint.X);
        //                        Canvas.SetTop(rectangle, startPoint.Y);
        //                        canvas.Children.Add(rectangle);
        //                        break;
        //                    case DrawingMode.Polygon:
        //                        polygon = CreatePolygon(width, 8, e.GetPosition(canvas));
        //                        polygon.Stroke = new SolidColorBrush(Colors.Purple);
        //                        startPoint = e.GetPosition(canvas);
        //                        endPoint = e.GetPosition(canvas);
        //                        canvas.Children.Add(polygon);
        //                        break;
        //                    case DrawingMode.Text:
        //                        startPoint = e.GetPosition(canvas);
        //                        endPoint = e.GetPosition(canvas);
        //                        textBox = new TextBox();
        //                        textBox.Width = 100;
        //                        textBox.Height = 200;
        //                        textBox.MouseEnter += OnMouseOver;
        //                        textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
        //                        textBox.Background = new SolidColorBrush(Colors.Transparent);
        //                        Canvas.SetLeft(textBox, startPoint.X);
        //                        Canvas.SetTop(textBox, startPoint.Y);
        //                        canvas.Children.Add(textBox);
        //                        // I don't know what this does, but it fixes the issue of
        //                        // the textbox not being put into focus.
        //                        Dispatcher.BeginInvoke(
        //                            DispatcherPriority.ContextIdle,
        //                            new Action(delegate ()
        //                            {
        //                                textBox.Focus();
        //                            }));
        //                        textBox.MouseRightButtonDown += TextBox_MouseRightButtonDown;
        //                        // currentlyDrawingShape = textBox;
        //                        break;
        //                }
        //                break;
        //            }
        //    }
        //}

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
                    break;
                case DrawingMode.Line:
                    break;
                case DrawingMode.Rectangle:
                    rectangle = new Rectangle();
                    currentlyDrawingShape = rectangle;
                    ContextMenu cm = new ContextMenu();
                    // cm.Items.Add(new ShapePropertiesPanel());
                    // cm.Items.Add(new Label() { Content = "hello" });

                    rectangle.ContextMenu = cm;

                    // TODO - add context menu to all of these items
                    // 1. Set Fill
                    // 2. Set Border
                    // rectangle.ContextMenu.Items.Add(new TextBlock() { Text = "Testing..." });
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
                    break;
                case DrawingMode.Equation:
                    break;
                case DrawingMode.Select:
                    break;
                default:
                    break;
            }

            /*switch (mainWindow.mainMode)
            {
                case MainMode.Select:
                    {
                        MouseStart = e.GetPosition(canvas);

                        HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));
                        if (hitTestResult != null)
                        {
                            var element = hitTestResult.VisualHit;

                            if (element is Rectangle)
                            {*/
                                /*
                                Rectangle selectedRectangle = element as Rectangle;
                                selectedShape = selectedRectangle;
                                OriginalSelectedPos = new Point { X = Canvas.GetLeft(selectedRectangle), Y = Canvas.GetTop(selectedRectangle) };
                                if (canvas.Children.Contains(selectedShapeBounds.MainBounds))
                                {
                                    canvas.Children.Remove(selectedShapeBounds.MainBounds);
                                    canvas.Children.Remove(selectedShapeBounds.TopLeftSelector);
                                    canvas.Children.Remove(selectedShapeBounds.TopRightSelector);
                                    canvas.Children.Remove(selectedShapeBounds.BottomLeftSelector);
                                    canvas.Children.Remove(selectedShapeBounds.BottomRightSelector);
                                }

                                selectedShapeBounds = new SelectionBounds(selectedRectangle);

                                // canvas.Children.Add(selectedShapeBounds.MainBounds);
                                canvas.Children.Add(selectedShapeBounds.TopLeftSelector);
                                canvas.Children.Add(selectedShapeBounds.TopRightSelector);
                                canvas.Children.Add(selectedShapeBounds.BottomLeftSelector);
                                canvas.Children.Add(selectedShapeBounds.BottomRightSelector);

                                Canvas.SetTop(selectedShapeBounds.MainBounds, Canvas.GetTop(selectedRectangle) - 5);
                                Canvas.SetLeft(selectedShapeBounds.MainBounds, Canvas.GetLeft(selectedRectangle) - 5);

                                Canvas.SetTop(selectedShapeBounds.TopLeftSelector, Canvas.GetTop(selectedShapeBounds.MainBounds) - 3);
                                Canvas.SetLeft(selectedShapeBounds.TopLeftSelector, Canvas.GetLeft(selectedShapeBounds.MainBounds) - 3);

                                Canvas.SetTop(selectedShapeBounds.TopRightSelector, Canvas.GetTop(selectedShapeBounds.MainBounds) - 3);
                                Canvas.SetLeft(selectedShapeBounds.TopRightSelector, (Canvas.GetLeft(selectedShapeBounds.MainBounds) + selectedShapeBounds.MainBounds.Width) - 6);

                                Canvas.SetTop(selectedShapeBounds.BottomLeftSelector, (Canvas.GetTop(selectedRectangle) - 12) + selectedShapeBounds.MainBounds.Height);
                                Canvas.SetLeft(selectedShapeBounds.BottomLeftSelector, Canvas.GetLeft(selectedShapeBounds.MainBounds) - 3);
                                */
                                /*Rectangle selectedRect = element as Rectangle;
                                mainBounds.Width = selectedRect.Width + 10;
                                mainBounds.Height = selectedRect.Height + 10;

                                topLeftBounds.Width = 3;
                                topLeftBounds.Height = 3;
                                topLeftBounds.Stroke = new SolidColorBrush(Colors.Blue);
                                topRight.Stroke = new SolidColorBrush(Colors.Blue);
                                botLeftBounds.Stroke = new SolidColorBrush(Colors.Blue);
                                botRightBounds.Stroke = new SolidColorBrush(Colors.Blue);

                                canvas.Children.Add(mainBounds);
                                canvas.Children.Add(topLeftBounds);
                                canvas.Children.Add(topRight);
                                canvas.Children.Add(botLeftBounds);
                                canvas.Children.Add(botRightBounds);

                                Canvas.SetTop(mainBounds, Canvas.GetTop(selectedRect) - 5);
                                Canvas.SetLeft(mainBounds, Canvas.GetLeft(selectedRect) - 5);

                                Canvas.SetTop(topLeftBounds, Canvas.GetTop(selectedRect) - 8);
                                Canvas.SetLeft(topLeftBounds, Canvas.GetLeft(selectedRect) - 8);


                                mainBounds.StrokeDashCap = PenLineCap.Flat;
                                mainBounds.StrokeLineJoin = PenLineJoin.Bevel;
                                mainBounds.StrokeThickness = 3;
                                mainBounds.Stroke = new SolidColorBrush(Colors.Gray);*/
                           /* }
                            if (element is Ellipse)
                            {

                                Ellipse selectedEllipse = element as Ellipse;
                                selectedShape = selectedEllipse;
                                OriginalSelectedPos = new Point { X = Canvas.GetLeft(selectedEllipse), Y = Canvas.GetTop(selectedEllipse) };

                                if (canvas.Children.Contains(selectedShapeBounds.MainBounds))
                                {
                                    selectedShapeBounds.reset();
                                    /*canvas.Children.Remove(selectedShapeBounds.MainBounds);
                                    canvas.Children.Remove(selectedShapeBounds.TopLeftSelector);
                                    canvas.Children.Remove(selectedShapeBounds.TopRightSelector);
                                    canvas.Children.Remove(selectedShapeBounds.BottomLeftSelector);
                                    canvas.Children.Remove(selectedShapeBounds.BottomRightSelector);*/
        }

                                //selectedShapeBounds = new SelectionBounds(canvas, selectedEllipse);

                                /*canvas.Children.Add(selectedShapeBounds.MainBounds);
                                canvas.Children.Add(selectedShapeBounds.TopLeftSelector);
                                canvas.Children.Add(selectedShapeBounds.TopRightSelector);
                                canvas.Children.Add(selectedShapeBounds.BottomLeftSelector);
                                canvas.Children.Add(selectedShapeBounds.BottomRightSelector);*/



                            /*}
                            if (element is Polyline)
                            {
                                Selector.SetIsSelected(element, true);
                            }
                        }
                        break;
                    }
                case MainMode.Draw:
                    {
                        switch (mainWindow.drawingMode)
                        {
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
                            case DrawingMode.Equation:
                                // TODO - Replace this code with actual code that loads the LaTeXEquationManager window.
                                FormulaControl control = new FormulaControl();
                                control.Formula = @"\left(x^2 + 2 \cdot x + 2\right) = 0";
                                canvas.Children.Add(control);
                                Canvas.SetTop(control, e.GetPosition(canvas).Y);
                                Canvas.SetLeft(control, e.GetPosition(canvas).X);
                                break;
                            case DrawingMode.FreeDraw:
                                polyLine = new Polyline();
                                polyLine.StrokeMiterLimit = 1;
                                currentlyDrawingShape = polyLine;
                                SetStrokeProperties(polyLine);
                                polyLine.Points.Add(e.GetPosition(canvas));
                                canvas.Children.Add(polyLine);
                                break;
                            case DrawingMode.Line:
                                line = new Line();
                                currentlyDrawingShape = line;
                                SetStrokeProperties(line);
                                line.X1 = e.GetPosition(canvas).X;
                                line.Y1 = e.GetPosition(canvas).Y;
                                line.X2 = e.GetPosition(canvas).X;
                                line.Y2 = e.GetPosition(canvas).Y;
                                canvas.Children.Add(line);
                                break;
                            case DrawingMode.Rectangle:
                                rectangle = new Rectangle();
                                currentlyDrawingShape = rectangle;
                                ContextMenu cm = new ContextMenu();
                                // cm.Items.Add(new ShapePropertiesPanel());
                                // cm.Items.Add(new Label() { Content = "hello" });

                                rectangle.ContextMenu = cm;

                                // TODO - add context menu to all of these items
                                // 1. Set Fill
                                // 2. Set Border
                                // rectangle.ContextMenu.Items.Add(new TextBlock() { Text = "Testing..." });
                                SetStrokeProperties(rectangle);
                                startPoint = e.GetPosition(canvas);
                                endPoint = e.GetPosition(canvas);
                                Canvas.SetLeft(rectangle, startPoint.X);
                                Canvas.SetTop(rectangle, startPoint.Y);
                                canvas.Children.Add(rectangle);
                                break;
                            case DrawingMode.Polygon:
                                polygon = CreatePolygon(width, 8, e.GetPosition(canvas));
                                polygon.Stroke = new SolidColorBrush(Colors.Purple);
                                startPoint = e.GetPosition(canvas);
                                endPoint = e.GetPosition(canvas);
                                canvas.Children.Add(polygon);
                                break;
                            case DrawingMode.Text:
                                startPoint = e.GetPosition(canvas);
                                endPoint = e.GetPosition(canvas);
                                textBox = new TextBox();
                                textBox.Width = 100;
                                textBox.Height = 200;
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
                                textBox.MouseRightButtonDown += TextBox_MouseRightButtonDown;
                                // currentlyDrawingShape = textBox;
                                break;*//*
                        }
                        break;
                    }*/
            
        //}

        // TODO - find a way to make textbox right click work.
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

            shape.Stroke = new SolidColorBrush(mainWindow.color);
            shape.Fill = new SolidColorBrush(Colors.Transparent);
            // Set the stroke thickness
            if (mainWindow.radioBtnNormal.IsChecked == true)
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

        // Selection variables
        Point p;
        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;
        UIElement source = null;

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (mainWindow.mainMode == MainMode.Select)
            {
                this.Cursor = Cursors.SizeAll;
            }
            source = (UIElement)sender;
            // selectedShapeBounds = new SelectionBounds(canvas, sender as Shape);
            Mouse.Capture(source);
            captured = true;
            x_shape = Canvas.GetLeft(source);
            x_canvas = e.GetPosition(canvas).X;
            y_shape = Canvas.GetTop(source);
            y_canvas = e.GetPosition(canvas).Y;
        }
        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            // only do this if we are not drawing the shape.
            // without this, the shape fires this event upon mouse release when drawing it.
            if (captured && mainWindow.mainMode == MainMode.Select)
            {
                if (source is Shape)
                {
                    selectedShapeBounds.Update(source as Shape);
                }
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                x_shape += x - x_canvas;
                Canvas.SetLeft(source, x_shape);
                x_canvas = x;
                y_shape += y - y_canvas;
                Canvas.SetTop(source, y_shape);
                y_canvas = y;
            }
        }

        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
            this.Cursor = Cursors.Arrow;
            if (source is Shape)
            {
                selectedShapeBounds.reset();
            }
        }

        // This is our eraser event.
        // It simply removes the element from the object when the tool selected
        // is the eraser tool and we have the mouse down.
        private void OnMouseOver(object sender, MouseEventArgs e)
        {
            if ((e.LeftButton == MouseButtonState.Pressed) && (mainWindow.mainMode == MainMode.Erase))
            {
                canvas.Children.Remove(sender as UIElement);
            }
            else if ((e.LeftButton == MouseButtonState.Pressed) && (mainWindow.mainMode == MainMode.Select))
            {

            }
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

        /*private void imgControl_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.LeftButton == MouseButtonState.Pressed) && hasStartedDrawing)
            {
                //Trace.WriteLine($"Mouse moved to ({e.GetPosition(canvas).X}, {e.GetPosition(canvas).Y})");
                switch (mainWindow.mainMode)
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
                }
            }
        }*/

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
                        currentlyDrawingShape.MouseRightButtonDown += CurrentlyDrawingShape_MouseRightButtonDown;
                        currentlyDrawingShape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
                        currentlyDrawingShape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
                        currentlyDrawingShape.MouseMove += Shape_MouseMove;

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

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.G)
            {
                if (mainWindow.mainMode == MainMode.Select)
                {
                    if (selectedShape != null)
                    {
                        this.moveMode = MoveMode.Free;
                    }
                }
            }
        }
    }
}