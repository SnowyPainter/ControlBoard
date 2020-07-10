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

    public interface IDrawable
    {
        public Canvas Canvas { get; }
        public FrameworkElement Element { get; }
        public DateTime DrewTime { get;}
        public void Draw();
        public void Delete();
        public string ToCtrb();
    }
    public class BasicDraw:IDrawable
    {
        public Canvas Canvas { get; set; }
        public FrameworkElement Element { get; }
        public DateTime DrewTime { get; }
        public BasicDraw(Canvas target, FrameworkElement obj, DateTime time)
        {
            DrewTime = time;
            Canvas = target;
            Element = obj;
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
