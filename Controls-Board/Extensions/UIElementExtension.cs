using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Controls_Board.Extensions
{
    public static class UIElementExtension
    {
        public static RenderTargetBitmap ToBitmap(this FrameworkElement element)
        {
            Rect emptyBox = VisualTreeHelper.GetDescendantBounds(element);
            DrawingVisual visualDraw = new DrawingVisual();
            using (DrawingContext ctx = visualDraw.RenderOpen())
            {
                ctx.DrawRectangle(new VisualBrush(element), null, new Rect(emptyBox.Size));
            }
            //(int)DrawCanvas.ActualWidth
            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Default);
            renderTargetBitmap.Render(visualDraw);

            return renderTargetBitmap;
        }
    }
}
