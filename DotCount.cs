using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GrainDetector
{
    public class DotCount
    {
        private ImageData imageData;
        private ImageRange imageRange;

        public List<Color> TargetColors;
        public List<bool> IsCounted;

        public DotCount(ImageData imageData, ImageRange imageRange)
        {
            this.imageData = imageData;
            this.imageRange = imageRange;
        }

        public List<int> CountDots()
        {
            return TargetColors.Zip(IsCounted, (c, i) => Tuple.Create(c, i)).Select(t => t.Item2 ? countDotsWithDfs(t.Item1) : 0).ToList();
        }

        private Color targetColor;
        private bool[,] visited;
        private Stack<Tuple<int, int>> stack;

        private int countDotsWithDfs(Color color)
        {
            targetColor = color;
            visited = new bool[imageData.OriginalImage.Height, imageData.OriginalImage.Width];
            stack = new Stack<Tuple<int, int>>();

            int count = 0;

            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;
            for (int y = lowerY; y <= upperY; ++y)
            {
                for (int x = lowerX; x <= upperX; ++x)
                {
                    if (!imageData.ShownImagePixels.Equals(x, y, targetColor))
                    {
                        continue;
                    }
                    if (visited[y, x])
                    {
                        continue;
                    }
                    ++count;
                    dfs(x, y);
                }
            }

            return count;
        }

        private static readonly int[] dx = new int[] { 1, 0, -1, 0 };
        private static readonly int[] dy = new int[] { 0, 1, 0, -1 };

        private void dfs(int x, int y)
        {
            visited[y, x] = true;
            stack.Push(new Tuple<int, int>(x, y));

            int lowerX = imageRange.LowerX, upperX = imageRange.UpperX;
            int lowerY = imageRange.LowerY, upperY = imageRange.UpperY;
            while (stack.Count != 0)
            {
                var t = stack.Pop();
                for (int d = 0; d < 4; ++d)
                {
                    int nx = t.Item1 + dx[d];
                    int ny = t.Item2 + dy[d];
                    if (!imageData.ShownImagePixels.Equals(nx, ny, targetColor))
                    {
                        continue;
                    }
                    if (nx < lowerX || upperX < nx || ny < lowerY || upperY < ny || visited[ny, nx])
                    {
                        continue;
                    }
                    visited[ny, nx] = true;
                    stack.Push(new Tuple<int, int>(nx, ny));
                }
            }
        }
    }
}
