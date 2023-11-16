using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingShapesLibrary
{
    public class RectangleModel : IShape
    {
        public List<PointModel> Points { get; set; } = new List<PointModel>()
        {
            new PointModel { XCoordinate = 0, YCoordinate = 0},
            new PointModel { XCoordinate = 0, YCoordinate = 0},
            new PointModel { XCoordinate = 0, YCoordinate = 0},
            new PointModel { XCoordinate = 0, YCoordinate = 0}
        };
    }
}
