using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace Controls_Board.Drawing
{
    public static class Shape
    {
        public class Rectangle:Drawable
        {
            public Rectangle(Canvas target, System.Windows.Shapes.Rectangle rectangle, DateTime time) : base(target, rectangle, time)
            {

            }
        }

        public class Circle:Drawable
        {
            public Circle(Canvas target, System.Windows.Shapes.Ellipse circle, DateTime time) : base(target, circle, time)
            {

            }
        }

    }
}
