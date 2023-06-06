using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Threading.Tasks;

namespace GrainDetector
{
    public enum BlurOption
    {
        [Display(Name = "ぼかしなし")]
        None = 0,
        [Display(Name = "ガウシアン")]
        Gaussian,
        [Display(Name = "ガウシアン×3")]
        Gaussian3Times,
        [Display(Name = "ガウシアン×6")]
        Gaussian6Times
    }

    public enum EdgeDetectOption
    {
        [Display(Name = "エッジ検出なし")]
        None,
        [Display(Name = "ソーベル")]
        Sobel,
        [Display(Name = "ラプラシアン")]
        Laplacian
    }

    [Serializable]
    public class FilterOptions : BindingBase
    {
        public BlurOption ApplysBlur
        {
            get
            {
                return _applysBlur;
            }
            set
            {
                _applysBlur = value;
                OnPropertyChanged(GetName.Of(() => ApplysBlur));
            }
        }

        public EdgeDetectOption DetectsEdge
        {
            get
            {
                return _detectsEdge;
            }
            set
            {
                _detectsEdge = value;
                OnPropertyChanged(GetName.Of(() => DetectsEdge));
            }
        }

        private BlurOption _applysBlur;
        private EdgeDetectOption _detectsEdge;

        public FilterOptions()
        {
            ApplysBlur = BlurOption.None;
            DetectsEdge = EdgeDetectOption.None;
        }
    }

    public class ImageFilter : BindingBase
    {
        private ImageData imageData;
        private ImageDisplay imageDisplay;
        private ImageRange imageRange;
        private FilterOptions filterOptions;

        public ImageFilter(ImageData imageData, ImageDisplay imageDisplay, ImageRange imageRange, FilterOptions filterOptions)
        {
            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageRange = imageRange;
            this.filterOptions = filterOptions;
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                imageData.FilteredImage,
                0,
                0,
                (int)(imageData.FilteredImage.Width * imageDisplay.ZoomMagnification),
                (int)(imageData.FilteredImage.Height * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            imageData.FilteredImagePixels.CopyToBitmap(bitmap);
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
            int width = imageData.OriginalImage.Width;
            int height = imageData.OriginalImage.Height;
            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;

            byte[,] srcPixels = new byte[height + 2, (width + 2) * 3];
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        srcPixels[1 + y, 3 + x * 3 + c] = imageData.OriginalImagePixels.GetValue(x, y, c);
                    }
                }
            }

            switch (filterOptions.ApplysBlur)
            {
                case BlurOption.Gaussian:
                    {
                        reflectToFrame(srcPixels);
                        byte[,] dstPixels = new byte[height + 2, (width + 2) * 3];
                        applyFilter(srcPixels, dstPixels, Gaussian, GaussianDenominator);
                        swapArray(ref srcPixels, ref dstPixels);
                    }
                    break;
                case BlurOption.Gaussian3Times:
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
                    break;
                case BlurOption.Gaussian6Times:
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
                        reflectToFrame(srcPixels);
                        fillArrayWithZero(dstPixels);
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
                    break;
            }

            switch (filterOptions.DetectsEdge)
            {
                case EdgeDetectOption.Sobel:
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
                    break;
                case EdgeDetectOption.Laplacian:
                    {
                        reflectToFrame(srcPixels);

                        byte[,] dstPixels = new byte[height + 2, (width + 2) * 3];
                        applyFilter(srcPixels, dstPixels, Laplacian, LaplacianDenominator);

                        swapArray(ref srcPixels, ref dstPixels);
                    }
                    break;
            }

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        imageData.FilteredImagePixels.SetValue(x, y, c, imageData.OriginalImagePixels.GetValue(x, y, c));
                    }
                }
            }

            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    for (int c = 0; c < 3; ++c)
                    {
                        imageData.FilteredImagePixels.SetValue(x, y, c, srcPixels[1 + y, 3 + x * 3 + c]);
                    }
                }
            }

            imageData.FilteredImagePixels.CopyToBitmap(imageData.FilteredImage);
        }

        private byte[,] sourcePixels, destPixels;
        private int[,] filter;
        int denominator;

        private void applyFilter(byte[,] sourcePixels, byte[,] destPixels, int[,] filter, int denominator)
        {
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;

            this.sourcePixels = sourcePixels;
            this.destPixels = destPixels;
            this.filter = filter;
            this.denominator = denominator;

            Parallel.For(1 + lowerY, upperY + 2, filterOneLine);
        }

        private void filterOneLine(int y)
        {
            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;

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
            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;

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
