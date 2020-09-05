using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Controls_Board.Drawing
{
    public class LineStroke : Drawable
    {
        public LineStroke(Canvas target, Polyline line, DateTime time) : base(target, line, time) {

        }
    }
}
