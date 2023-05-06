using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GrainDetector
{
    public class DotCount
    {
        public Bitmap Image;
        public int LowerX, UpperX;
        public int LowerY, UpperY;
        public List<Color> TargetColors;
        public List<bool> IsCounted;

        public List<int> CountDots()
        {
            return TargetColors.Zip(IsCounted, (c, i) => Tuple.Create(c, i)).Select(t => t.Item2 ? useDFS(t.Item1) : 0).ToList();
        }

        private Color targetColor;
        private bool[,] visited;
        private Stack<Tuple<int, int>> stack;

        private int useDFS(Color color)
        {
            targetColor = color;
            visited = new bool[Image.Height, Image.Width];
            stack = new Stack<Tuple<int, int>>();

            int count = 0;

            for (int y = LowerY; y < UpperY; ++y)
            {
                for (int x = LowerX; x < UpperX; ++x)
                {
                    if (Image.GetPixel(x, y).ToArgb() != targetColor.ToArgb())
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
                    if (Image.GetPixel(nx, ny).ToArgb() != targetColor.ToArgb())
                    {
                        continue;
                    }
                    if (nx < LowerX || UpperX <= nx || ny < LowerY || UpperY <= ny || visited[ny, nx])
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
