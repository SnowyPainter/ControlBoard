using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls_Board.Drawing
{

    public class Pen
    {
        
        public Brush Stroke { 
            get { return polyLine.Stroke; } 
            set { stroke = value; } 
        }
        public double StrokeThickness { 
            get { return polyLine.StrokeThickness; }
            set { strokeThickness = value; } 
        }

        public int IgnoreDrawingRange { get; set; } = 5;

        public bool IsUp { get; private set; } = true;
        private Brush stroke;
        private double strokeThickness;
        private Canvas target;
        private Polyline polyLine;
        private Point mousePoint;

        public Pen(Canvas target)
        {
            this.target = target;
            polyLine = new Polyline();
            Stroke = Brushes.Black;
            StrokeThickness = 3f;
            this.target.Children.Add(polyLine);
        }

        public void Move(MouseEventArgs e)
        {
            if (IsUp)
                return;

            mousePoint = e.GetPosition(target);
            polyLine.Points.Add(mousePoint);
        }
        public void Down()
        {
            if (!IsUp)
                throw new DrawingException("Down method called before up");
            polyLine = new Polyline();
            polyLine.Stroke = stroke;
            polyLine.StrokeThickness = strokeThickness;
            target.Children.Add(polyLine);

            IsUp = false;
        }

        public Drawable Up()
        {
            if (IsUp)
                throw new DrawingException("Up method called after up");

            IsUp = true;

            if (mousePoint == null || polyLine.Points.Count < IgnoreDrawingRange)
            {
                return null;
            }
                

            var stroke = new LineStroke(target, polyLine, DateTime.Now);

            return stroke;
        }
    }
}
