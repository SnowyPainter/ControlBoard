using Controls_Board.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Controls_Board.CTRBFormat
{
    public struct Property
    {
        public string Key;
        public string Value;
    }
    public struct Control
    {
        public string DateTime;
        public string XamlElement;
    }
    
    public class Structure
    {
        /*
         * 
         * $seek=value
         * 
         * .value1
         * .value2
         * .value3
         * 
         * 
         */

        public int Seek { get; set; }
        public IDrawable[] Controls { get; set; } 

        public string Built { get; private set; }

        public Structure()
        {

        }
        public Structure(int seek, IDrawable[] ctrls)
        {
            Seek = seek;
            Controls = ctrls;
        }

        public string Build()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"$seek={Seek}\n");
            foreach(var c in Controls)
            {
                Debug.WriteLine($"Date : {c.DrewTime.ToString(CTRB.DateTimeFormat)}");
                sb.Append($".{c.ToCtrb()}/{c.DrewTime.ToString(CTRB.DateTimeFormat)}\n");
            }
            Built = sb.ToString();
            return Built;
        }
    }
}
