using System;
using System.Collections.Generic;
using System.Drawing;

namespace GrainDetector
{
    public class GrainDetectOptions : BindingBase
    {
        public bool DetectsGrainInCircle
        {
            get
            {
                return _detectsGrainInCircle;
            }
            set
            {
                _detectsGrainInCircle = value;
                OnPropertyChanged(GetName.Of(() => DetectsGrainInCircle));
            }
        }

        public bool DetectsGrainOnCircle
        {
            get
            {
                return _detectsGrainOnCircle;
            }
            set
            {
                _detectsGrainOnCircle = value;
                OnPropertyChanged(GetName.Of(() => DetectsGrainOnCircle));
            }
        }

        public int MinWhitePixelCount
        {
            get
            {
                return _minWhitePixelCount;
            }
            set
            {
                _minWhitePixelCount = value;
                OnPropertyChanged(GetName.Of(() => MinWhitePixelCount));
            }
        }

        public Color CircleColor
        {
            get
            {
                return _circleColor;
            }
            set
            {
                _circleColor = value;
                OnPropertyChanged(GetName.Of(() => CircleColor));
            }
        }

        private bool _detectsGrainInCircle;
        private bool _detectsGrainOnCircle;
        private int _minWhitePixelCount;
        private Color _circleColor;

        public GrainDetectOptions()
        {
            MinWhitePixelCount = 1000;
            CircleColor = Color.Transparent;
        }
    }

    public class GrainDetect
    {
        private ImageData imageData;
        private ImageRange imageRange;
        private PlanimetricCircle circle;
        private GrainDetectOptions options;
        private DotDrawTool dotInCircleTool;
        private DotDrawTool dotOnCircleTool;
        private DotDraw dotDraw;

        public GrainDetect(
            ImageData imageData,
            ImageRange imageRange,
            PlanimetricCircle circle,
            GrainDetectOptions options,
            DotDrawTool dotInCircleTool,
            DotDrawTool dotOnCircleTool,
            DotDraw dotDraw)
        {
            this.imageData = imageData;
            this.imageRange = imageRange;
            this.circle = circle;
            this.options = options;
            this.dotInCircleTool = dotInCircleTool;
            this.dotOnCircleTool = dotOnCircleTool;
            this.dotDraw = dotDraw;
        }

        private static readonly int[] dx = new int[] { 1, 0, -1, 0 };
        private static readonly int[] dy = new int[] { 0, 1, 0, -1 };

        public void Detect()
        {
            searchCircleColor();

            int width = imageData.OriginalImagePixels.Width;
            int height = imageData.OriginalImagePixels.Height;
            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;

            bool[,] circleMap = new bool[height, width];
            {

                for (int y = lowerY; y <= upperY; ++y)
                {
                    for (int x = lowerX; x <= upperX; ++x)
                    {
                        circleMap[y, x] = imageData.CircleImagePixels.Equals(x, y, options.CircleColor);
                    }
                }

                if (circle.Diameter >= 3)
                {
                    var stack = new Stack<Tuple<int, int>>();

                    circleMap[circle.LowerY + circle.Diameter / 2, circle.LowerX + circle.Diameter / 2] = true;
                    stack.Push(Tuple.Create(circle.LowerX + circle.Diameter / 2, circle.LowerY + circle.Diameter / 2));

                    while (stack.Count != 0)
                    {
                        var t = stack.Pop();

                        for (int d = 0; d < 4; ++d)
                        {
                            int nx = t.Item1 + dx[d], ny = t.Item2 + dy[d];
                            if (nx < lowerX || upperX < nx || ny < lowerY || upperY < ny || circleMap[ny, nx])
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
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    whiteMap[y, x] = imageData.BinarizedImagePixels.GetValue(x, y, 0) == 255;
                }
            }

            List<Point> dotLocationsInCircle = new List<Point>();
            List<Point> dotLocationsOnCircle = new List<Point>();
            {
                bool[,] visited = new bool[height, width];
                var stack = new Stack<Tuple<int, int>>();

                for (int y = lowerY; y <= upperY; ++y)
                {
                    for (int x = lowerX; x <= upperX; ++x)
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
                                if (nx < lowerX || upperX < nx || ny < lowerY || upperY < ny || visited[ny, nx])
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

                        if (pixelCount >= options.MinWhitePixelCount)
                        {
                            sumX /= pixelCount;
                            sumY /= pixelCount;

                            if (!onCircle)
                            {
                                if (options.DetectsGrainInCircle)
                                {
                                    dotLocationsInCircle.Add(new Point((int)sumX, (int)sumY));
                                }
                            }
                            else
                            {
                                if (options.DetectsGrainOnCircle)
                                {
                                    dotLocationsOnCircle.Add(new Point((int)sumX, (int)sumY));
                                }
                            }
                        }
                    }
                }
            }

            foreach (Point location in dotLocationsInCircle)
            {
                dotDraw.DrawDot(location, dotInCircleTool.Brush, dotInCircleTool.Size);
            }
            foreach (Point location in dotLocationsOnCircle)
            {
                dotDraw.DrawDot(location, dotOnCircleTool.Brush, dotOnCircleTool.Size);
            }
        }

        private void searchCircleColor()
        {
            options.CircleColor = Color.Transparent;

            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    if (imageData.CircleImagePixels.GetValue(x, y, 0) != imageData.CircleImagePixels.GetValue(x, y, 1) ||
                        imageData.CircleImagePixels.GetValue(x, y, 0) != imageData.CircleImagePixels.GetValue(x, y, 2))
                    {
                        options.CircleColor = Color.FromArgb(
                            imageData.CircleImagePixels.GetValue(x, y, 0),
                            imageData.CircleImagePixels.GetValue(x, y, 1),
                            imageData.CircleImagePixels.GetValue(x, y, 2));
                        break;
                    }
                }

                if (options.CircleColor != Color.Transparent)
                {
                    break;
                }
            }
        }
    }
}
