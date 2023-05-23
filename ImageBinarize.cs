using System.Drawing;

namespace GrainDetector
{
    public class ImageBinarize
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageRange imageRange;

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
                Binarize();
            }
        }

        public ImageBinarize(ImageData imageData, ImageDisplay imageDisplay, ImageRange imageRange)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageRange = imageRange;
            BinarizationThreshold = 0;
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                imageData.BinarizedImage,
                0,
                0,
                (int)(imageData.BinarizedImage.Width * imageDisplay.ZoomMagnification),
                (int)(imageData.BinarizedImage.Height * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            imageData.BinarizedImagePixels.CopyToBitmap(bitmap);
        }

        public void Binarize()
        {
            BitmapPixels.Copy(imageData.OriginalImagePixels, imageData.BinarizedImagePixels);

            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    // compare R
                    if (BinarizationThreshold <= imageData.OriginalImagePixels.GetValue(x, y, 0))
                    {
                        imageData.BinarizedImagePixels.SetValue(x, y, 0, 0xFF);
                        imageData.BinarizedImagePixels.SetValue(x, y, 1, 0xFF);
                        imageData.BinarizedImagePixels.SetValue(x, y, 2, 0xFF);
                    }
                    else
                    {
                        imageData.BinarizedImagePixels.SetValue(x, y, 0, 0x00);
                        imageData.BinarizedImagePixels.SetValue(x, y, 1, 0x00);
                        imageData.BinarizedImagePixels.SetValue(x, y, 2, 0x00);
                    }
                }
            }

            imageData.BinarizedImagePixels.CopyToBitmap(imageData.BinarizedImage);
        }
    }
}
