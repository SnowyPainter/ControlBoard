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
        
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; } = 5;

        public bool IsUp { get; private set; } = true; //One Up down = One capture frame
        private Canvas target;
        private LineStroke ls;
        private Point mousePoint;

        public Pen(Canvas target, double strokeThickness)
        {
            this.target = target;
            Stroke = Brushes.Black;
            StrokeThickness = strokeThickness;
        }
        #region LineStroke -> Drawing line
        /// <summary>
        /// Move() make shapes follow the Mouse
        /// </summary>
        public void Move(MouseEventArgs e)
        {
            if (IsUp || ls == null)
                return;

            mousePoint = e.GetPosition(target);
            var c = new Ellipse
            {
                Width = StrokeThickness,
                Height = StrokeThickness,
                Fill = Stroke,
                Margin = new Thickness(mousePoint.X, mousePoint.Y, 0, 0),
            };

            ls.Add(c);
            ls.AddCircleToCanvas(c);
        }
        /// <summary>
        /// Down() add shapes to target canvas.
        /// </summary>
        public void Down()
        {
            if (!IsUp)
                throw new DrawingException("Down method called before up");
            ls = new LineStroke(target, DateTime.Now);

            IsUp = false;
        }
        /// <summary>
        /// Up() make new Line(as many circles) or complete a Shape.
        /// </summary>
        public Drawable Up()
        {
            if (IsUp || ls == null)
                throw new DrawingException("Up method called after up");

            IsUp = true;

            if (mousePoint == null)
            {
                return null;
            }

            return ls;
        }
        #endregion
    }
}
