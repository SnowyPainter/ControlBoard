using Controls_Board.CTRBFormat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls_Board.Drawing
{
    /// <summary>
    /// LineStroke ignore Drawable's FrameworkElement - Element.
    /// However, It use own property(array), and overrides draw, delete, toctrb.
    /// It's good, if you explicit this class when you want to passed through the method.
    /// </summary>
    public class LineStroke : Drawable
    {
        private List<Ellipse> circles;

        public LineStroke(Canvas target, DateTime time) : base(target, time) {
            circles = new List<Ellipse>();
        }

        
        /// <summary>
        /// Add doesn't draw to Canvas, But add on a list
        /// </summary>
        /// <param name="c"></param>
        public void Add(Ellipse c)
        {
            circles.Add(c);
        }
        public void AddCircleToCanvas(Ellipse c)
        {
            Canvas.Children.Add(c);
        }

        public override void Draw()
        {
            foreach(var c in circles)
            {
                Canvas.Children.Add(c);
            }
        }

        public override void Delete()
        {
            foreach(var c in circles)
            {
                Canvas.Children.Remove(c);
            }
        }
        public override string ToCtrb()
        {
            var s = $"{CTRB.HeaderLineStroke} ";
            int i = 0;
            for (;i < circles.Count-1;i++)
            {
                 s += $"{XamlWriter.Save(circles[i])}+";
            }
            s+= $"{XamlWriter.Save(circles[i])}";
            return s;
        }
    }
}
