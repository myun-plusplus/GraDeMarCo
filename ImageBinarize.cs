using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace GrainDetector
{
    public class ImageBinarize : FunctionBase
    {
        private Bitmap _originalImage;
        public Bitmap OriginalImage
        {
            get
            {
                return _originalImage;
            }
            set
            {
                _originalImage = value;

                BitmapData originalBitmapData = value.LockBits(new Rectangle(0, 0, value.Width, value.Height), ImageLockMode.WriteOnly, value.PixelFormat);
                orighnalImagePixels = new byte[originalBitmapData.Stride * originalBitmapData.Height];
                Marshal.Copy(originalBitmapData.Scan0, orighnalImagePixels, 0, orighnalImagePixels.Length);
                originalImageStride = originalBitmapData.Stride;
                value.UnlockBits(originalBitmapData);

                //BinarizedImage = new Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format1bppIndexed);
                BinarizedImage = new Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format24bppRgb);
                BitmapData binarizedBitmapData = BinarizedImage.LockBits(
                    new Rectangle(0, 0, BinarizedImage.Width, BinarizedImage.Height),
                    ImageLockMode.WriteOnly, BinarizedImage.PixelFormat);
                binarizedImagePixels = new byte[binarizedBitmapData.Stride * binarizedBitmapData.Height];
                BinarizedImage.UnlockBits(binarizedBitmapData);
            }
        }

        private int _binarizationThreshold;
        public int BinarizationThreshold
        {
            get
            {
                return _binarizationThreshold;
            }
            set
            {
                _binarizationThreshold = value;
                if (OriginalImage != null)
                {
                    binarize();
                }
            }
        }

        private Bitmap _binarizedImage;
        public Bitmap BinarizedImage
        {
            get
            {
                return _binarizedImage;
            }
            set
            {
                if (_binarizedImage != null)
                {
                    _binarizedImage.Dispose();
                }
                _binarizedImage = value;
            }
        }

        private byte[] orighnalImagePixels;
        private int originalImageStride;
        private byte[] binarizedImagePixels;

        public ImageBinarize(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {
            _binarizationThreshold = 0;
        }

        ~ImageBinarize()
        {
            BinarizedImage = null;
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        public override void DrawOnPaintEvent(Graphics graphics)
        {
            Draw(graphics);
        }

        public override void Draw(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                BinarizedImage,
                0,
                0,
                (int)(BinarizedImage.Width * imageDisplay.ZoomMagnification),
                (int)(BinarizedImage.Height * imageDisplay.ZoomMagnification));
        }

        public void DrawOnImage(Bitmap image)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, image.PixelFormat);
            Marshal.Copy(binarizedImagePixels, 0, bitmapData.Scan0, binarizedImagePixels.Length);
            image.UnlockBits(bitmapData);
        }

        private void binarize()
        {
#if false
            // 愚直
            for (int y = 0; y < BinarizedImage.Height; ++y)
            {
                for (int x = 0; x < BinarizedImage.Width; ++x)
                {
                    if (OriginalImage.GetPixel(x, y).R < BinarizationThreshold)
                    {
                        BinarizedImage.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        BinarizedImage.SetPixel(x, y, Color.White);
                    }
                }
            }
#endif
#if false
            BitmapData bmpData = BinarizedImage.LockBits(
                new Rectangle(0, 0, BinarizedImage.Width, BinarizedImage.Height),
                ImageLockMode.WriteOnly, BinarizedImage.PixelFormat);
            int height = bmpData.Height;
            int width = bmpData.Width;
            int stride = bmpData.Stride;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int originalPosition = originalImageStride * y + 3 * x;
                    int binarizedPosition = stride * y + (x >> 3);
                    // compare B
                    if (BinarizationThreshold <= orighnalImagePixels[originalPosition])
                    {
                        binarizedImagePixels[binarizedPosition] |= (byte)(0x80 >> (x & 0x7));
                    }
                    else
                    {
                        binarizedImagePixels[binarizedPosition] &= (byte)(~(0x80 >> (x & 0x7)));
                    }
                }
            }

            Marshal.Copy(binarizedImagePixels, 0, bmpData.Scan0, binarizedImagePixels.Length);

            BinarizedImage.UnlockBits(bmpData);
#endif
#if true
            BitmapData bmpData = BinarizedImage.LockBits(
                new Rectangle(0, 0, BinarizedImage.Width, BinarizedImage.Height),
                ImageLockMode.WriteOnly, BinarizedImage.PixelFormat);
            int height = bmpData.Height;
            int width = bmpData.Width;
            int stride = bmpData.Stride;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int originalPosition = originalImageStride * y + 3 * x;
                    int binarizedPosition = stride * y + 3 * x;
                    // compare B
                    if (BinarizationThreshold <= orighnalImagePixels[originalPosition])
                    {
                        binarizedImagePixels[binarizedPosition + 0] = (byte)0xFF;
                        binarizedImagePixels[binarizedPosition + 1] = (byte)0xFF;
                        binarizedImagePixels[binarizedPosition + 2] = (byte)0xFF;
                    }
                    else
                    {
                        binarizedImagePixels[binarizedPosition + 0] = (byte)0x00;
                        binarizedImagePixels[binarizedPosition + 1] = (byte)0x00;
                        binarizedImagePixels[binarizedPosition + 2] = (byte)0x00;
                    }
                }
            }

            Marshal.Copy(binarizedImagePixels, 0, bmpData.Scan0, binarizedImagePixels.Length);

            BinarizedImage.UnlockBits(bmpData);
#endif
        }
    }
}
