using System;
using System.Collections.Generic;
using System.Drawing;

namespace GrainDetector
{
    public class GrainDetect
    {
        private static readonly int[] dx = new int[] { 1, 0, -1, 0 }, dy = new int[] { 0, 1, 0, -1 };

        private ImageDisplay imageDisplay;

        public bool IsFinished
        {
            get;
            private set;
        }

        public Bitmap OriginalImage;
        public Bitmap CircleImage;
        public Bitmap BinarizedImage;
        public int MinWhitePixel;
        public int LowerX, UpperX, LowerY, UpperY;
        public int CircleX, CircleY, CircleDiameter;

        private Color circleColor;
        private BitmapPixels originalImagePixels;
        private BitmapPixels circleImagePixels;
        private BitmapPixels binarizedImagePixels;

        private List<Point> dotLocationsInCircle, dotLocationsOnCircle;

        #region DotOptions

        public bool DetectsGrainInCircle, DetectsGrainOnCircle;
        private Color _dotColorInCircle, _dotColorOnCircle;
        public Color DotColorInCircle
        {
            get
            {
                return _dotColorInCircle;
            }
            set
            {
                _dotColorInCircle = value;
                brushInCircle.Color = value;
            }
        }
        public Color DotColorOnCircle
        {
            get
            {
                return _dotColorOnCircle;
            }
            set
            {
                _dotColorOnCircle = value;
                brushOnCircle.Color = value;
            }
        }
        public int DotSizeInCircle, DotSizeOnCircle;

        private SolidBrush brushInCircle;
        private SolidBrush brushOnCircle;

        #endregion

        public GrainDetect(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
            IsFinished = false;
            circleColor = Color.Transparent;
            dotLocationsInCircle = new List<Point>();
            dotLocationsOnCircle = new List<Point>();
            brushInCircle = new SolidBrush(Color.Transparent);
            brushOnCircle = new SolidBrush(Color.Transparent);
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            if (IsFinished)
            {
                foreach (Point location in dotLocationsInCircle)
                {
                    Point shown = imageDisplay.GetShownLocation(location);
                    graphics.FillRectangle(
                        brushInCircle,
                        (float)(shown.X - DotSizeInCircle * imageDisplay.ZoomMagnification / 2.0),
                        (float)(shown.Y - DotSizeInCircle * imageDisplay.ZoomMagnification / 2.0),
                        (float)(DotSizeInCircle * imageDisplay.ZoomMagnification),
                        (float)(DotSizeInCircle * imageDisplay.ZoomMagnification));
                }
                foreach (Point location in dotLocationsOnCircle)
                {
                    Point shown = imageDisplay.GetShownLocation(location);
                    graphics.FillRectangle(
                        brushOnCircle,
                        (float)(shown.X - DotSizeOnCircle * imageDisplay.ZoomMagnification / 2.0),
                        (float)(shown.Y - DotSizeOnCircle * imageDisplay.ZoomMagnification / 2.0),
                        (float)(DotSizeOnCircle * imageDisplay.ZoomMagnification),
                        (float)(DotSizeOnCircle * imageDisplay.ZoomMagnification));
                }
            }
        }

        public void DrawOnImage(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (Point location in dotLocationsInCircle)
                {
                    graphics.FillRectangle(
                        brushInCircle,
                        (float)(location.X - DotSizeInCircle / 2.0),
                        (float)(location.Y - DotSizeInCircle / 2.0),
                        DotSizeInCircle,
                        DotSizeInCircle);
                }
                foreach (Point location in dotLocationsOnCircle)
                {
                    graphics.FillRectangle(
                        brushOnCircle,
                        (float)(location.X - DotSizeOnCircle / 2.0),
                        (float)(location.Y - DotSizeOnCircle / 2.0),
                        DotSizeOnCircle,
                        DotSizeOnCircle);
                }
            }
        }

        public void Detect()
        {
            IsFinished = false;
            dotLocationsInCircle.Clear();
            dotLocationsOnCircle.Clear();

            using (originalImagePixels = new BitmapPixels(OriginalImage))
            using (circleImagePixels = new BitmapPixels(CircleImage))
            using (binarizedImagePixels = new BitmapPixels(BinarizedImage))
            {
                searchCircleColor();

                int width = originalImagePixels.Width;
                int height = originalImagePixels.Height;

                bool[,] circleMap = new bool[height, width];
                {
                    int a = 0;
                    for (int y = LowerY; y < UpperY; ++y)
                    {
                        for (int x = LowerX; x < UpperX; ++x)
                        {
                            circleMap[y, x] = circleImagePixels.Equals(x, y, circleColor);
                            if (circleMap[y, x])
                            {
                                a++;
                            }
                        }
                    }

                    if (CircleDiameter >= 3)
                    {
                        var stack = new Stack<Tuple<int, int>>();

                        circleMap[CircleY + CircleDiameter / 2, CircleX + CircleDiameter / 2] = true;
                        stack.Push(Tuple.Create(CircleX + CircleDiameter / 2, CircleY + CircleDiameter / 2));

                        while (stack.Count != 0)
                        {
                            var t = stack.Pop();

                            for (int d = 0; d < 4; ++d)
                            {
                                int nx = t.Item1 + dx[d], ny = t.Item2 + dy[d];
                                if (nx < LowerX || UpperX <= nx || ny < LowerY || UpperY <= ny || circleMap[ny, nx])
                                {
                                    continue;
                                }
                                circleMap[ny, nx] = true;
                                stack.Push(Tuple.Create(nx, ny));
                            }
                        }
                    }
                }

                bool[,] whiteMap = new bool[height, width];
                for (int y = LowerY; y < UpperY; ++y)
                {
                    for (int x = LowerX; x < UpperX; ++x)
                    {
                        whiteMap[y, x] = binarizedImagePixels.GetValue(x, y, 0) == 255;
                    }
                }

                {
                    bool[,] visited = new bool[height, width];
                    var stack = new Stack<Tuple<int, int>>();

                    for (int y = LowerY; y < UpperY; ++y)
                    {
                        for (int x = LowerX; x < UpperX; ++x)
                        {
                            if (!circleMap[y, x] || !whiteMap[y, x])
                            {
                                continue;
                            }

                            int pixelCount = 1;
                            bool onCircle = false;
                            long sumX = x, sumY = y;
                            visited[y, x] = true;
                            stack.Push(Tuple.Create(x, y));

                            while (stack.Count != 0)
                            {
                                var t = stack.Pop();

                                for (int d = 0; d < 4; ++d)
                                {
                                    int nx = t.Item1 + dx[d], ny = t.Item2 + dy[d];
                                    if (nx < LowerX || UpperX <= nx || ny < LowerY || UpperY <= ny || visited[ny, nx])
                                    {
                                        continue;
                                    }
                                    if (!whiteMap[ny, nx])
                                    {
                                        continue;
                                    }
                                    ++pixelCount;
                                    if (!circleMap[ny, nx])
                                    {
                                        onCircle = true;
                                    }
                                    sumX += nx;
                                    sumY += ny;
                                    visited[ny, nx] = true;
                                    stack.Push(Tuple.Create(nx, ny));
                                }
                            }

                            if (pixelCount >= MinWhitePixel)
                            {
                                sumX /= pixelCount;
                                sumY /= pixelCount;

                                if (!onCircle)
                                {
                                    if (DetectsGrainInCircle)
                                    {
                                        dotLocationsInCircle.Add(new Point((int)sumX, (int)sumY));
                                    }
                                }
                                else
                                {
                                    if (DetectsGrainOnCircle)
                                    {
                                        dotLocationsOnCircle.Add(new Point((int)sumX, (int)sumY));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            IsFinished = true;
        }

        private void searchCircleColor()
        {
            for (int y = LowerY; y < UpperY; ++y)
            {
                for (int x = LowerX; x < UpperX; ++x)
                {
                    if (circleImagePixels.GetValue(x, y, 0) != circleImagePixels.GetValue(x, y, 1) ||
                        circleImagePixels.GetValue(x, y, 0) != circleImagePixels.GetValue(x, y, 2))
                    {
                        circleColor = Color.FromArgb(
                            circleImagePixels.GetValue(x, y, 0),
                            circleImagePixels.GetValue(x, y, 1),
                            circleImagePixels.GetValue(x, y, 2));
                        break;
                    }
                }

                if (circleColor != Color.Transparent)
                {
                    break;
                }
            }
        }
    }
}
