using System;
using System.Collections.Generic;
using System.Text;

namespace Controls_Board.Extensions
{
    public static class DrawingExtension
    {
        public static bool TypeCheck(this int t, Drawing.Type type)
        {
            return t == (int)type;
        }
    }
}
