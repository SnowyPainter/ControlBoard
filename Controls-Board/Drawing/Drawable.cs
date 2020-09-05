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

        public Drawable(Canvas canvas, FrameworkElement element, DateTime drewTime)
        {
            Canvas = canvas;
            Element = element;
            DrewTime = drewTime;
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
