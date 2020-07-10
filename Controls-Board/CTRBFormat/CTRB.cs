using Controls_Board.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Controls_Board.CTRBFormat
{
    public static class CTRB
    {
        public static string DateTimeFormat = "yyyyMMddHHmmss";
        private static Property SplitToProperty(string line)
        {
            var splited = line.Split("=");
            return new Property { Key = splited[0], Value = splited[1] };
        }
        private static Control SplitToControl(string line)
        {
            int lastIdx = line.LastIndexOf("/");
            return new Control { DateTime = line.Substring(lastIdx+1), XamlElement = line.Substring(0, lastIdx) };
        }

        public static Structure OpenToCanvas(string path, Canvas currCanvas)
        {
            var structure = new Structure();

            using (var filestream = File.OpenRead(path))
            using(var reader = new StreamReader(filestream))
            {
                string line;
                List<IDrawable> ctrls = new List<IDrawable>();
                while ((line = reader.ReadLine()) != null)
                {
                    var onlyContent = line.Substring(1);
                    if(line[0] == '$' && line.Contains("=")) //Property
                    {
                        var property = SplitToProperty(onlyContent);
                        switch(property.Key)
                        {
                            case "seek":
                                structure.Seek = int.Parse(property.Value);
                                break;
                        }
                    }
                    else if(line[0] == '.') //Array
                    {
                        var c = SplitToControl(onlyContent);
                        var drewTime = DateTime.ParseExact(c.DateTime, DateTimeFormat, null);
                        var element = XamlReader.Parse(c.XamlElement) as FrameworkElement;
                        
                        ctrls.Add(new BasicDraw(currCanvas, element, drewTime));
                    }
                    else
                    {
                        throw new Exception("This is not a property or array element");
                    }
                }

                structure.Controls = ctrls.ToArray();
            }

            return structure;
        }
        public static bool Save(Capturer captured, string path)
        {
            var structure = new Structure(captured.Seek, captured.Draws.ToArray());
            var txt = Encoding.UTF8.GetBytes(structure.Build());
            using (Stream fileStream = File.Create(path))
            {
                fileStream.Write(txt, 0, txt.Length);
            }

            return true;
        }
    }
}
