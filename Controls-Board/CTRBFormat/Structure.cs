﻿using Controls_Board.Drawing;
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
          
         $seek=value
         .header xaml+xaml/datetime
         .xaml/datetime
        
        */

        public int Seek { get; set; }
        public Drawable[] Controls { get; set; } 

        public string Built { get; private set; }

        public Structure()
        {

        }
        public Structure(int seek, Drawable[] ctrls)
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
                sb.Append($".{c.ToCtrb()}/{c.DrewTime.ToString(CTRB.DateTimeFormat)}\n");
            }
            Built = sb.ToString();
            return Built;
        }
    }
}
