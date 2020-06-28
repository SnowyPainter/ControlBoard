using System;
using System.Collections.Generic;
using System.Text;

namespace Controls_Board.Drawing
{
    public enum DrawTool
    {
        Pen,
        Eraser,
        Fill,
        None
    }
    public class Capturer
    {
        public List<IDrawable> Draws { get; private set; }

        public Capturer()
        {
            Draws = new List<IDrawable>();
        }

        public void Add(IDrawable drawable)
        {
            Draws.Add(drawable);
        }

        public int DrawCount() => Draws.Count;
    }
}
