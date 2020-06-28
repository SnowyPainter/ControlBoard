using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Controls_Board.Extensions
{
    public static class BitmapExtension
    {
        public static PngBitmapEncoder ToPng(this RenderTargetBitmap bitmap)
        {
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(bitmap));
            return pngImage;
        }
    }
}
