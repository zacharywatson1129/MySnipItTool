using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MySnipItTool
{
    /// <summary>
    /// WPF Helper class to calculate the selection bounds for WPF shape(s).
    /// </summary>
    public class SelectionBounds
    {
        public SelectionBounds(Canvas drawingSurface, Shape selectedObject)
        {
            canvas = drawingSurface;
            canvas.Children.Add(MainBounds);
            canvas.Children.Add(TopLeftSelector);
            canvas.Children.Add(TopRightSelector);
            canvas.Children.Add(BottomLeftSelector);
            canvas.Children.Add(BottomRightSelector);
            Update(selectedObject);
            // Update
        }        


        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="selectedObject"></param>
        public void Update(Shape selectedObject)
        {
            MainBounds.Width = selectedObject.Width + 10;
            MainBounds.Height = selectedObject.Height + 10;

            TopLeftSelector.Width = 10;
            TopLeftSelector.Height = 10;
            TopLeftSelector.Stroke = new SolidColorBrush(Colors.Gray);
            TopLeftSelector.Fill = new SolidColorBrush(Colors.LightGray);

            TopRightSelector.Width = 10;
            TopRightSelector.Height = 10;
            TopRightSelector.Stroke = new SolidColorBrush(Colors.Gray);
            TopRightSelector.Fill = new SolidColorBrush(Colors.LightGray);

            BottomLeftSelector.Width = 10;
            BottomLeftSelector.Height = 10;
            BottomLeftSelector.Stroke = new SolidColorBrush(Colors.Gray);
            BottomLeftSelector.Fill = new SolidColorBrush(Colors.LightGray);

            BottomRightSelector.Width = 10;
            BottomRightSelector.Height = 10;
            BottomRightSelector.Stroke = new SolidColorBrush(Colors.Gray);
            BottomRightSelector.Fill = new SolidColorBrush(Colors.LightGray);

            MainBounds.StrokeThickness = 2;
            MainBounds.Stroke = new SolidColorBrush(Colors.DarkGray);

            
            

            Canvas.SetTop(MainBounds, Canvas.GetTop(selectedObject) - 5);
            Canvas.SetLeft(MainBounds, Canvas.GetLeft(selectedObject) - 5);

            Canvas.SetTop(TopLeftSelector, Canvas.GetTop(MainBounds) - 3);
            Canvas.SetLeft(TopLeftSelector, Canvas.GetLeft(MainBounds) - 3);

            Canvas.SetTop(TopRightSelector, Canvas.GetTop(MainBounds) - 3);
            Canvas.SetLeft(TopRightSelector, (Canvas.GetLeft(MainBounds) + MainBounds.Width) - 6);

            Canvas.SetTop(BottomLeftSelector, (Canvas.GetTop(selectedObject) - 12) + MainBounds.Height);
            Canvas.SetLeft(BottomLeftSelector, Canvas.GetLeft(MainBounds) - 3);
        }

        /// <summary>
        /// Removes all selection bounds Rectangles from canvas.
        /// </summary>
        public void reset()
        {
            canvas.Children.Remove(MainBounds);
            canvas.Children.Remove(TopLeftSelector);
            canvas.Children.Remove(TopRightSelector);
            canvas.Children.Remove(BottomLeftSelector);
            canvas.Children.Remove(BottomRightSelector);
        }


        private Canvas canvas;
        public Rectangle MainBounds { get; set; } = new Rectangle();
        public Rectangle TopLeftSelector { get; set; } = new Rectangle();
        public Rectangle TopRightSelector { get; set; } = new Rectangle();
        public Rectangle BottomLeftSelector { get; set; } = new Rectangle();
        public Rectangle BottomRightSelector { get; set; } = new Rectangle();
    }
}
