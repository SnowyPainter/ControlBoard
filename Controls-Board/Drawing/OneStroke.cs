using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls_Board.Drawing
{
    public class OneStroke : IDrawable
    {
        public DateTime DrewTime { get; private set; }
        private Canvas canvas;
        private Polyline line;
        public OneStroke(Canvas target, Polyline line, DateTime time)
        {
            DrewTime = time;
            canvas = target;
            this.line = line;
        }
        public void Draw()
        {
            canvas.Children.Add(line);
        }
    }
}
