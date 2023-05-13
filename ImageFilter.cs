using System;
using System.Drawing;
using System.Threading.Tasks;

namespace GrainDetector
{
    public class ImageFilter : BindingBase
    {
        private ImageDisplay imageDisplay;
        private RangeSelect rangeSelect;

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
                FilteredImage = new Bitmap(value);
                originalImagePixels = new BitmapPixels(value);
                filteredImagePixels = new BitmapPixels(value);
            }
        }

        private Bitmap _filteredImage;
        public Bitmap FilteredImage
        {
            get
            {
                return _filteredImage;
            }
            private set
            {
                if (_filteredImage != null && value != _filteredImage)
                {
                    _filteredImage.Dispose();
                }
                _filteredImage = value;
            }
        }

        public bool ApplysGaussian;
        public bool ApplysGaussian3;
        public bool ApplysSobel;
        public bool ApplysLaplacian;

        private BitmapPixels originalImagePixels;
        private BitmapPixels filteredImagePixels;

        public ImageFilter(ImageDisplay imageDisplay, RangeSelect rangeSelect)
        {
            this.imageDisplay = imageDisplay;
            this.rangeSelect = rangeSelect;
        }

        ~ImageFilter()
        {
            FilteredImage = null;
            if (originalImagePixels != null)
            {
                originalImagePixels.Dispose();
            }
            if (filteredImagePixels != null)
            {
                filteredImagePixels.Dispose();
            }
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                FilteredImage,
                0,
                0,
                (int)(FilteredImage.Width * imageDisplay.ZoomMagnification),
                (int)(FilteredImage.Height * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            filteredImagePixels.CopyToBitmap(bitmap);
        }

        private static readonly int[,] Gaussian = new int[,]
        {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };
        private const int GaussianDenominator = 16;

        private static readonly int[,] Sobel_horizontal = new int[,]
        {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };
        private static readonly int[,] Sobel_vertical = new int[,]
        {
            { 1, 2, 1 },
            { 0, 0, 0 },
            { -1, -2, -1 }
        };
        private const int SobelDenominator = 4;

        private static readonly int[,] Laplacian = new int[,]
        {
            { -1, -1, -1 },
            { -1, 8, -1 },
            { -1, -1, -1 }
        };
        private const int LaplacianDenominator = 8;

        public void Filter()
        {
            int width = OriginalImage.Width;
            int height = OriginalImage.Height;
            int lowerX = rangeSelect.LowerX, upperX = rangeSelect.UpperX;
            int lowerY = rangeSelect.LowerY, upperY = rangeSelect.UpperY;

            byte[,] srcPixels = new byte[height + 2, (width + 2) * 3];
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        srcPixels[1 + y, 3 + x * 3 + c] = originalImagePixels.GetValue(x, y, c);
                    }
                }
            }

            if (ApplysGaussian)
            {
                reflectToFrame(srcPixels);
                byte[,] dstPixels = new byte[height + 2, (width + 2) * 3];
                applyFilter(srcPixels, dstPixels, Gaussian, GaussianDenominator);
                swapArray(ref srcPixels, ref dstPixels);
            }
            else if (ApplysGaussian3)
            {
                reflectToFrame(srcPixels);
                byte[,] dstPixels = new byte[height + 2, (width + 2) * 3];
                applyFilter(srcPixels, dstPixels, Gaussian, GaussianDenominator);
                swapArray(ref srcPixels, ref dstPixels);
                reflectToFrame(srcPixels);
                fillArrayWithZero(dstPixels);
                applyFilter(srcPixels, dstPixels, Gaussian, GaussianDenominator);
                swapArray(ref srcPixels, ref dstPixels);
                reflectToFrame(srcPixels);
                fillArrayWithZero(dstPixels);
                applyFilter(srcPixels, dstPixels, Gaussian, GaussianDenominator);
                swapArray(ref srcPixels, ref dstPixels);
            }

            if (ApplysSobel)
            {
                reflectToFrame(srcPixels);

                byte[,] dstPixels_horizontal = new byte[height + 2, (width + 2) * 3];
                applyFilter(srcPixels, dstPixels_horizontal, Sobel_horizontal, SobelDenominator);

                byte[,] dstPixels_vertical = new byte[height + 2, (width + 2) * 3];
                applyFilter(srcPixels, dstPixels_vertical, Sobel_vertical, SobelDenominator);

                for (int y = 1 + lowerY; y <= upperY + 1; ++y)
                {
                    for (int x = 1 + lowerX; x <= upperX + 1; ++x)
                    {
                        for (int c = 0; c < 3; ++c)
                        {
                            double value = Math.Sqrt(
                                dstPixels_horizontal[y, x * 3 + c] * dstPixels_horizontal[y, x * 3 + c] +
                                dstPixels_vertical[y, x * 3 + c] * dstPixels_vertical[y, x * 3 + c]);
                            srcPixels[y, x * 3 + c] = value <= byte.MaxValue ? (byte)value : (byte)255;
                        }
                    }
                }
            }
            else if (ApplysLaplacian)
            {
                reflectToFrame(srcPixels);

                byte[,] dstPixels = new byte[height + 2, (width + 2) * 3];
                applyFilter(srcPixels, dstPixels, Laplacian, LaplacianDenominator);

                swapArray(ref srcPixels, ref dstPixels);
            }

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        filteredImagePixels.SetValue(x, y, c, originalImagePixels.GetValue(x, y, c));
                    }
                }
            }

            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        filteredImagePixels.SetValue(x, y, c, srcPixels[1 + y, 3 + x * 3 + c]);
                    }
                }
            }

            filteredImagePixels.CopyToBitmap(FilteredImage);
        }

        private byte[,] sourcePixels, destPixels;
        private int[,] filter;
        int denominator;

        private void applyFilter(byte[,] sourcePixels, byte[,] destPixels, int[,] filter, int denominator)
        {
            int lowerY = rangeSelect.LowerY, upperY = rangeSelect.UpperY;

            this.sourcePixels = sourcePixels;
            this.destPixels = destPixels;
            this.filter = filter;
            this.denominator = denominator;

            Parallel.For(1 + lowerY, upperY + 2, filterOneLine);
        }

        private void filterOneLine(int y)
        {
            int lowerX = rangeSelect.LowerX, upperX = rangeSelect.UpperX;

            for (int x = 1 + lowerX; x <= upperX + 1; ++x)
            {
                for (int c = 0; c < 3; ++c)
                {
                    int value = destPixels[y, x * 3 + c];
                    for (int fy = -1; fy <= 1; ++fy)
                    {
                        for (int fx = -1; fx <= 1; ++fx)
                        {
                            value += sourcePixels[y + fy, (x + fx) * 3 + c] * filter[fy + 1, fx + 1];
                        }
                    }
                    value = Math.Abs(value) / denominator;
                    destPixels[y, x * 3 + c] = value <= byte.MaxValue ? (byte)value : (byte)255;
                }
            }
        }

        private void reflectToFrame(byte[,] pixels)
        {
            int lowerX = rangeSelect.LowerX, upperX = rangeSelect.UpperX;
            int lowerY = rangeSelect.LowerY, upperY = rangeSelect.UpperY;

            for (int y = 1 + lowerY; y <= upperY + 1; ++y)
            {
                for (int c = 0; c < 3; ++c)
                {
                    pixels[y, lowerX * 3 + c] = pixels[y, (1 + lowerX) * 3 + c];
                    pixels[y, (upperX + 2) * 3 + c] = pixels[y, (upperX + 1) * 3 + c];
                }
            }
            for (int x = lowerX; x <= upperX + 2; ++x)
            {
                for (int c = 0; c < 3; ++c)
                {
                    pixels[lowerY, x * 3 + c] = pixels[1 + lowerY, x * 3 + c];
                    pixels[upperY + 2, x * 3 + c] = pixels[upperY + 1, x * 3 + c];
                }
            }
        }

        private void fillArrayWithZero(byte[,] array)
        {
            int lenght0 = array.GetLength(0);
            int lenght1 = array.GetLength(1);
            for (int i0 = 0; i0 < lenght0; ++i0)
            {
                for (int i1 = 0;i1 < lenght1; ++i1)
                {
                    array[i0, i1] = 0;
                }
            }
        }

        private void swapArray(ref byte[,] array1, ref byte[,] array2)
        {
            var tmp = array1;
            array1 = array2;
            array2 = tmp;
        }
    }
}
