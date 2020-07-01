using System;
using System.Collections.Generic;
using System.Text;

namespace Controls_Board.Drawing
{
    public interface IDrawable
    {
        public DateTime DrewTime { get;}
        public void Draw();
        public void Delete();
    }
}
