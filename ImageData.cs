using System.Drawing;
using System.Drawing.Imaging;

namespace GrainDetector
{
    public class ImageData
    {
        public Bitmap OriginalImage
        {
            get
            {
                return _originalImage;
            }
            set
            {
                if (_originalImage != null && value != _originalImage)
                {
                    _originalImage.Dispose();
                }
                _originalImage = value;

                OriginalImagePixels = new BitmapPixels(value);

                FilteredImage = value.Clone(
                    new Rectangle(0, 0, value.Width, value.Height),
                    PixelFormat.Format24bppRgb);
            }
        }

        public Bitmap CircleImage
        {
            get
            {
                return _circleImage;
            }
            set
            {
                if (_circleImage != null && value != _circleImage)
                {
                    _circleImage.Dispose();
                }
                _circleImage = value;

                CircleImagePixels = new BitmapPixels(value);
            }
        }

        public Bitmap FilteredImage
        {
            get
            {
                return _filteredImage;
            }
            set
            {
                if (_filteredImage != null && value != _filteredImage)
                {
                    _filteredImage.Dispose();
                }
                _filteredImage = value;

                FilteredImagePixels = new BitmapPixels(value);

                BinarizedImage = value.Clone(
                    new Rectangle(0, 0, value.Width, value.Height),
                    PixelFormat.Format24bppRgb);
            }
        }

        public Bitmap BinarizedImage
        {
            get
            {
                return _binarizedImage;
            }
            set
            {
                if (_binarizedImage != null && value != _binarizedImage)
                {
                    _binarizedImage.Dispose();
                }
                _binarizedImage = value;

                BinarizedImagePixels = new BitmapPixels(value);
            }
        }

        //public Bitmap ShownImage
        //{
        //    get;
        //    set;
        //}

        public BitmapPixels OriginalImagePixels
        {
            get
            {
                return _originalImagePixels;
            }
            set
            {
                if (_originalImagePixels != null && value != _originalImagePixels)
                {
                    _originalImagePixels.Dispose();
                }
                _originalImagePixels = value;
            }
        }

        public BitmapPixels CircleImagePixels
        {
            get
            {
                return _circleImagePixels;
            }
            set
            {
                if (_circleImagePixels != null && value != _circleImagePixels)
                {
                    _circleImagePixels.Dispose();
                }
                _circleImagePixels = value;
            }
        }

        public BitmapPixels FilteredImagePixels
        {
            get
            {
                return _filteredImagePixels;
            }
            set
            {
                if (_filteredImagePixels != null && value != _filteredImagePixels)
                {
                    _filteredImagePixels.Dispose();
                }
                _filteredImagePixels = value;
            }
        }

        public BitmapPixels BinarizedImagePixels
        {
            get
            {
                return _binarizedImagePixels;
            }
            set
            {
                if (_binarizedImagePixels != null && value != _binarizedImagePixels)
                {
                    _binarizedImagePixels.Dispose();
                }
                _binarizedImagePixels = value;
            }
        }

        private Bitmap _originalImage;
        private Bitmap _circleImage;
        private Bitmap _filteredImage;
        private Bitmap _binarizedImage;

        private BitmapPixels _originalImagePixels;
        private BitmapPixels _circleImagePixels;
        private BitmapPixels _filteredImagePixels;
        private BitmapPixels _binarizedImagePixels;

        ~ImageData()
        {
            OriginalImage = null;
            CircleImagePixels = null;
            FilteredImagePixels = null;
            BinarizedImagePixels = null;

            OriginalImagePixels = null;
            CircleImagePixels = null;
            FilteredImagePixels = null;
            OriginalImagePixels = null;
        }
    }
}
