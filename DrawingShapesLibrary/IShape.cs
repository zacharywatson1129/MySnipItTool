using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingShapesLibrary
{
    public interface IShape
    {
        List<PointModel> Points { get; set; }
    }
}
