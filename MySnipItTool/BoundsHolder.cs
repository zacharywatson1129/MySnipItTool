﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace MySnipItTool
{
    public class BoundsHolder
    {
        public Rectangle Bounds { get; set; }
        public UIElement CanvasObject { get; set; }
    }
}
