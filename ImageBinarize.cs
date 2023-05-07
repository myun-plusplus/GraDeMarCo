using System.Drawing;
using System.Drawing.Imaging;

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
                BinarizedImage = new Bitmap(value.Width, value.Height, PixelFormat.Format24bppRgb);
                originalImagePixels = new BitmapPixels(value);
                binarizedImagePixels = new BitmapPixels(value); // Copyの必要はないが、処理の共通化のため
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
        public int BinarizationThreshold;

        private BitmapPixels originalImagePixels;
        private BitmapPixels binarizedImagePixels;

        public ImageBinarize(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {

        }

        ~ImageBinarize()
        {
            BinarizedImage = null;
            binarizedImagePixels.Dispose();
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        public override void DrawOnPaintEvent(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                BinarizedImage,
                0,
                0,
                (int)(BinarizedImage.Width * imageDisplay.ZoomMagnification),
                (int)(BinarizedImage.Height * imageDisplay.ZoomMagnification));
        }

        public override void DrawOnBitmap(Bitmap bitmap)
        {
            binarizedImagePixels.CopyToBitmap(bitmap);
        }

        public void Binarize()
        {
            int height = originalImagePixels.Height;
            int width = originalImagePixels.Width;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
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
