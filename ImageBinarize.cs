using System;
using System.Drawing;

namespace GrainDetector
{
    [Serializable]
    public class BinarizeOptions : BindingBase
    {
        public int BinarizationThreshold
        {
            get
            {
                return _binarizationThreshold;
            }
            set
            {
                _binarizationThreshold = value;
                OnPropertyChanged(GetName.Of(() => BinarizationThreshold));
            }
        }

        public bool InvertsMonochrome
        {
            get
            {
                return _monochormeInverts;
            }
            set
            {
                _monochormeInverts = value;
                OnPropertyChanged(GetName.Of(() => InvertsMonochrome));
            }
        }

        private int _binarizationThreshold;
        private bool _monochormeInverts;

        public BinarizeOptions()
        {
            BinarizationThreshold = 0;
            InvertsMonochrome = false;
        }
    }

    public class ImageBinarize
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageRange imageRange;
        private BinarizeOptions options;

        public ImageBinarize(ImageData imageData, ImageDisplay imageDisplay, ImageRange imageRange, BinarizeOptions options)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageRange = imageRange;
            this.options = options;
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
                    if (options.BinarizationThreshold <= imageData.FilteredImagePixels.GetValue(x, y, 0))
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

            if (options.InvertsMonochrome)
            {
                for (int y = lowerY; y <= upperY; ++y)
                {
                    for (int x = lowerX; x <= upperX; ++x)
                    {
                        imageData.BinarizedImagePixels.SetValue(x, y, 0, (byte)(255 - imageData.BinarizedImagePixels.GetValue(x, y, 0)));
                        imageData.BinarizedImagePixels.SetValue(x, y, 1, (byte)(255 - imageData.BinarizedImagePixels.GetValue(x, y, 1)));
                        imageData.BinarizedImagePixels.SetValue(x, y, 2, (byte)(255 - imageData.BinarizedImagePixels.GetValue(x, y, 2)));
                    }
                }
            }

            imageData.BinarizedImagePixels.CopyToBitmap(imageData.BinarizedImage);
        }
    }
}
