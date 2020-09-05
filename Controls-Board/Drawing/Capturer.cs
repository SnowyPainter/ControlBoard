using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;

namespace Controls_Board.Drawing
{
    public enum DrawTool
    {
        Pen,
        Eraser,
        None
    }
    public class Capturer
    {
        public List<Drawable> Draws { get; private set; }

        public int Seek { get; private set; } = 0;

        public Capturer()
        {
            Draws = new List<Drawable>();
        }

        public void ResetAndDrawAll(int seek, List<Drawable> draws)
        {
            Seek = seek;
            Draws = draws;
            for(int i = 0;i < Seek;i++)
            {
                Draws[i].Draw();
            }
        }

        public void Add(Drawable drawable)
        {
            if (drawable == null)
                return;

            Draws.Insert(Seek,drawable);
            Seek++;
        }

        public void Redo()
        {
            if (Draws.Count <= Seek)
                return;
            
            Draws[Seek].Draw();
            Seek++;
        }
        public void Undo()
        {
            if (0 >= Seek)
                return;

            Draws[Seek-1].Delete();
            Seek--;
        }

        public int DrawCount() => Draws.Count;
    }
}
