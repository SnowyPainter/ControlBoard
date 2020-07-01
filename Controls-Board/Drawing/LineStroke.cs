using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls_Board.Drawing
{
    public class LineStroke : IDrawable
    {
        public DateTime DrewTime { get; private set; }
        private Canvas canvas;
        private Polyline line;
        public LineStroke(Canvas target, Polyline line, DateTime time)
        {
            DrewTime = time;
            canvas = target;
            this.line = line;
        }
        public void Draw()
        {
            canvas.Children.Add(line);
        }
        public void Delete()
        {
            canvas.Children.Remove(this.line);
        }
    }
}
