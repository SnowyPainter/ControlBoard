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
    public class LineStroke : IDrawable
    {
        public DateTime DrewTime { get; private set; }
        public Canvas Canvas { get; private set; }
        public FrameworkElement Element { get; }
        public LineStroke(Canvas target, Polyline line, DateTime time)
        {
            DrewTime = time;
            Canvas = target;
            Element = line;
        }
        public void Draw()
        {
            Canvas.Children.Add(Element);
        }
        public void Delete()
        {
            Canvas.Children.Remove(Element);
        }
        public string ToCtrb()
        {
            return $"{XamlWriter.Save(Element)}";
        }
    }
}
