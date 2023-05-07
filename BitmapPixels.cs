using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;

namespace GrainDetector
{
    public class BitmapPixels : IDisposable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private byte[] pixels;
        private int stride;

        private bool disposedValue;

        public BitmapPixels()
        {

        }

        public BitmapPixels(Bitmap image)
        {
            CopyFromBitmap(image);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    pixels = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void CopyFromBitmap(Bitmap image)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Width = bitmapData.Width;
            Height = bitmapData.Height;
            pixels = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
            stride = bitmapData.Stride;
            image.UnlockBits(bitmapData);
        }

        public void CopyToBitmap(Bitmap image)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            image.UnlockBits(bitmapData);
        }

        public byte GetValue(int x, int y, int rgb)
        {
            return pixels[stride * y + 3 * x + 2 - rgb];
        }

        public void SetValue(int x, int y, int rgb, byte value)
        {
            pixels[stride * y + 3 * x + 2 - rgb] = value;
        }

        public bool Equals(int x, int y, Color color)
        {
            return pixels[stride * y + 3 * x + 2] == color.R &&
                pixels[stride * y + 3 * x + 1] == color.G &&
                pixels[stride * y + 3 * x] == color.B;
        }

        //public bool IsGray(int x, int y)
        //{

        //}
    }
}
