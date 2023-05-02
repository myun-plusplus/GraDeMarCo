using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GrainDetector
{
    public class DotCount
    {
        private Bitmap image;
        private int lowerX, upperX;
        private int lowerY, upperY;

        public List<int> CountDots(Bitmap image, int x, int y, int width, int height, List<Color> targetColors)
        {
            this.image = image;
            this.lowerX = x;
            this.lowerY = y;
            this.upperX = x + width;
            this.upperY = y + height;

            return targetColors.Select(useDFS).ToList();
        }

        private Color targetColor;
        private bool[,] visited;
        Stack<Tuple<int, int>> stack;

        private int useDFS(Color color)
        {
            targetColor = color;
            visited = new bool[image.Height, image.Width];
            stack = new Stack<Tuple<int, int>>();

            int count = 0;

            for (int y = lowerY; y < upperY; ++y)
            {
                for (int x = lowerX; x < upperX; ++x)
                {
                    if (image.GetPixel(x, y) != targetColor)
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

        static readonly int[] dx = new int[] { 1, 0, -1, 0 };
        static readonly int[] dy = new int[] { 0, 1, 0, -1 };

        private void dfs(int x, int y)
        {
            visited[y, x] = true;
            stack.Push(new Tuple<int, int>(x, y));

            while (stack.Count != 0)
            {
                var t = stack.Pop();
                for (int d = 0; d < 4; ++d)
                {
                    int nx = t.Item1 + dx[d];
                    int ny = t.Item2 + dy[d];
                    if (image.GetPixel(nx, ny) != targetColor)
                    {
                        continue;
                    }
                    if (nx < lowerX || upperX <= nx || ny < lowerY || upperY <= ny || visited[ny, nx])
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
