using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Controls_Board
{ 
    public class DrawingException : Exception
    {
        bool errThrowing = true;
        public DrawingException(string message) : base(message)
        {
            if (errThrowing)
                Debug.WriteLine($"ERROR : {message}");
        }
    }
}
