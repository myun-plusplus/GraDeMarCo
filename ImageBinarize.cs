using System.Drawing;
using System.Drawing.Imaging;

namespace GrainDetector
{
    public class ImageBinarize
    {
        private ImageDisplay imageDisplay;
        private ImageRange imageRange;

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
                BinarizedImage = value.Clone(new Rectangle(0, 0, value.Width, value.Height), PixelFormat.Format24bppRgb);
                originalImagePixels = new BitmapPixels(value);
                binarizedImagePixels = new BitmapPixels(value); // Copyの必要はないが、処理の共通化のため
                Binarize();
            }
        }

        private Bitmap _binarizedImage;
        public Bitmap BinarizedImage
        {
            get
            {
                return _binarizedImage;
            }
            private set
            {
                if (_binarizedImage != null && value != _binarizedImage)
                {
                    _binarizedImage.Dispose();
                }
                _binarizedImage = value;
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
                    originalImagePixels.CopyFromBitmap(OriginalImage);
                    Binarize();
                }
            }
        }

        private BitmapPixels originalImagePixels;
        private BitmapPixels binarizedImagePixels;

        public ImageBinarize(ImageDisplay imageDisplay, ImageRange imageRange)
        {
            this.imageDisplay = imageDisplay;
            this.imageRange = imageRange;
            BinarizationThreshold = 0;
        }

        ~ImageBinarize()
        {
            BinarizedImage = null;
            if (originalImagePixels != null)
            {
                originalImagePixels.Dispose();
            }
            if (binarizedImagePixels != null)
            {
                binarizedImagePixels.Dispose();
            }
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                BinarizedImage,
                0,
                0,
                (int)(BinarizedImage.Width * imageDisplay.ZoomMagnification),
                (int)(BinarizedImage.Height * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            binarizedImagePixels.CopyToBitmap(bitmap);
        }

        public void Binarize()
        {
            BitmapPixels.Copy(originalImagePixels, binarizedImagePixels);

            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    // compare R
                    if (BinarizationThreshold <= originalImagePixels.GetValue(x, y, 0))
                    {
                        binarizedImagePixels.SetValue(x, y, 0, 0xFF);
                        binarizedImagePixels.SetValue(x, y, 1, 0xFF);
                        binarizedImagePixels.SetValue(x, y, 2, 0xFF);
                    }
                    else
                    {
                        binarizedImagePixels.SetValue(x, y, 0, 0x00);
                        binarizedImagePixels.SetValue(x, y, 1, 0x00);
                        binarizedImagePixels.SetValue(x, y, 2, 0x00);
                    }
                }
            }

            binarizedImagePixels.CopyToBitmap(BinarizedImage);
        }
    }
}
