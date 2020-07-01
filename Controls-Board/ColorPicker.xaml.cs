using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls_Board
{
    /// <summary>
    /// ColorPicker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public RGB Selected = new RGB();
        private double currH = 360;

        public ColorPicker()
        {
            InitializeComponent();

            var g6 = HSV.GradientSpectrum();

            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 0);
            for (int i = 0; i < g6.Length; i++) {
                GradientStop stop = new GradientStop(g6[i].Color(), (i) * 0.16);
                gradientBrush.GradientStops.Add(stop);
            }
            SpectrumGrid.Opacity = 1;
            SpectrumGrid.Background = gradientBrush;

            MiddleStop.Color = HSV.RGBFromHSV(0, 1f, 1f).Color();
        }

        private void RgbGradient_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(sender as Canvas);
                var x = pos.X;
                var y = pos.Y;
                RGB c; 
                if (x < Width / 2)
                {
                    c = HSV.RGBFromHSV(currH, 1f, x / (Width / 2));
                }
                else
                {
                    c = HSV.RGBFromHSV(currH, (Width / 2 - (x - Width / 2)) / Width, 1f);
                }
                HexCode.Background = new SolidColorBrush(c.Color());
                HexCode.Text = "#" + c.Hex();
                Selected = c;
            }
            
        }

        private void SpectrumGrid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var x = e.GetPosition(SpectrumGrid).X;
                SpectrumGridBar.Margin = new Thickness(x, 0, 0, 0);
                currH = 360 * (x / this.Width);
                MiddleStop.Color = HSV.RGBFromHSV(currH, 1f, 1f).Color();
            }
        }
    }

    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public RGB() 
        {
            R = 0xff;
            G = 0xff;
            B = 0xff;
        }
        public RGB(double r, double g, double b)
        {
            if (r > 255 || g > 255 || b > 255) throw new Exception("RGB must be under 255 (1byte)");
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
        }
        public string Hex()
        {
            return BitConverter.ToString(new byte[] { R, G, B }).Replace("-", string.Empty);
        }
        public Color Color()
        {
            var color = new Color();
            color.R = R;
            color.G = G;
            color.B = B;
            color.A = 255;
            return color;
        }
    }
    public static class HSV
    {
        public static RGB[] GetSpectrum()
        {
            RGB[] rgbs = new RGB[360];

            for (int h = 0; h < 360; h++)
            {
                rgbs[h] = RGBFromHSV(h, 1f, 1f);
            }
            return rgbs;
        }
        public static RGB[] GradientSpectrum()
        {
            RGB[] rgbs = new RGB[7];

            for (int h = 0; h < 7; h++)
            {
                rgbs[h] = RGBFromHSV(h*60, 1f, 1f);
            }
            return rgbs;
        }
        public static RGB RGBFromHSV(double h, double s, double v)
        {
            if (h > 360 || h < 0 || s > 1 || s < 0 || v > 1 || v < 0)
                return null;

            double c = v * s;
            double x = c *(1 - Math.Abs((h / 60 % 2) - 1));
            double m = v - c;

            double r = 0, g = 0, b = 0;

            if (h < 60)
            {
                r = c;
                g = x;
            }
            else if(h < 120)
            {
                r = x;
                g = c;
            }
            else if(h < 180)
            {
                g = c;
                b = x;
            }
            else if(h < 240)
            {
                g = x;
                b = c;
            }
            else if(h < 300)
            {
                r = x;
                b = c;
            }
            else if(h <= 360)
            {
                r = c;
                b = x;
            }

            return new RGB((r+m)*255, (g + m) * 255, (b + m) * 255);
        }
    }
}
