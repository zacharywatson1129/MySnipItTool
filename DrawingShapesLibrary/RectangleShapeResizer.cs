using System;

namespace DrawingShapesLibrary
{
    /// <summary>
    /// This utility class contains static methods for resizing rectangular-based shapes, for instance
    /// the System.Windows.Shapes.Rectangle or System.Windows.Shapes.Ellipse class. Because it relys only on double values,
    /// rather than UI-type specific classes such as System.Windows.Point or System.Windows.Shapes.Shape class, it is portable to
    /// many different technologies. 
    /// </summary>
    public static class RectangleShapeResizer
    {
        /// <summary>
        /// Calculates the width of a rectangle based on the start point and endpoint X values.
        /// </summary>
        /// <param name="startPointX">The starting x coordinate where the user pressed down their mouse to drag and draw the rectangle.</param>
        /// <param name="endPointX">The x coordinate where the user's mouse currently is.</param>
        /// <returns>Returns the new width of the rectangle.</returns>
        public static double CalculateNewWidth(double startPointX, double endPointX)
        {
            return Math.Abs(endPointX - startPointX);
        }

        public static double CalculateNewHeight(double startPointY, double endPointY)
        {
            return Math.Abs(endPointY - startPointY);
        }

        public static double GetTopLeftX(double startPointX, double currentPointX)
        {
            return Math.Min(startPointX, currentPointX);
        }

        public static double GetTopLeftY(double startPointY, double currentPointY)
        {
            return Math.Min(startPointY, currentPointY);
        }
    }
}
