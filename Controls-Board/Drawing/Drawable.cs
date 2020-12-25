using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Controls_Board.Drawing
{
    public enum Type
    {
        Line,
        InputBox,
        Text,
        Rectangle,
        Circle,
    }

    public class Drawable
    {
        public Canvas Canvas { get; }
        public FrameworkElement Element { get; }
        public DateTime DrewTime { get;}

        public Drawable(Canvas canvas, DateTime drewTime)
        {
            Canvas = canvas;
            DrewTime = drewTime;
        }
        public Drawable(Canvas canvas, FrameworkElement element, DateTime drewTime):this(canvas, drewTime)
        {
            Element = element;
        }

        /// <summary>
        /// Capturer call Draw() when Redo
        /// </summary>
        public virtual void Draw()
        {
            Canvas.Children.Add(Element);
        }
        /// <summary>
        /// Capturer call Delete() when Undo
        /// </summary>
        public virtual void Delete()
        {
            Canvas.Children.Remove(Element);
        }
        public virtual string ToCtrb()
        {
            return $"{XamlWriter.Save(Element)}";
        }
    }
}
