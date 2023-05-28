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

                if (value != null)
                {
                    OriginalImagePixels = new BitmapPixels(value);

                    ShownImage = value.Clone(
                        new Rectangle(0, 0, value.Width, value.Height),
                        PixelFormat.Format24bppRgb);
                    CircleImage = value.Clone(
                        new Rectangle(0, 0, value.Width, value.Height),
                        PixelFormat.Format24bppRgb);
                    FilteredImage = value.Clone(
                        new Rectangle(0, 0, value.Width, value.Height),
                        PixelFormat.Format24bppRgb);
                }
            }
        }
        public Bitmap ShownImage
        {
            get
            {
                return _shownImage;
            }
            set
            {
                if (_shownImage != null && value != _shownImage)
                {
                    _shownImage.Dispose();
                }
                _shownImage = value;

                if (value != null)
                {
                    ShownImagePixels = new BitmapPixels(value);
                }
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

                if (value != null)
                {
                    CircleImagePixels = new BitmapPixels(value);
                }
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

                if (value != null)
                {
                    FilteredImagePixels = new BitmapPixels(value);

                    BinarizedImage = value.Clone(
                        new Rectangle(0, 0, value.Width, value.Height),
                        PixelFormat.Format24bppRgb);
                }
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

                if (value != null)
                {
                    BinarizedImagePixels = new BitmapPixels(value);
                }
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

        public BitmapPixels ShownImagePixels
        {
            get
            {
                return _shownImagePixels;
            }
            set
            {
                if (_shownImagePixels != null && value != _shownImagePixels)
                {
                    _shownImagePixels.Dispose();
                }
                _shownImagePixels = value;
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
        private Bitmap _shownImage;
        private Bitmap _circleImage;
        private Bitmap _filteredImage;
        private Bitmap _binarizedImage;

        private BitmapPixels _originalImagePixels;
        private BitmapPixels _shownImagePixels;
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
