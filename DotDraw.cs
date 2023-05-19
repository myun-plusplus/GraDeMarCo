using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GrainDetector
{
    public class DotDraw : BindingBase
    {
        private ImageDisplay imageDisplay;

        private SolidBrush brush;

        private Color _dotColor;
        public Color DotColor
        {
            get
            {
                return _dotColor;
            }
            set
            {
                _dotColor = value;
                brush.Color = value;
            }
        }

        private int _dotSize;
        public int DotSize
        {
            get
            {
                return _dotSize;
            }
            set
            {
                _dotSize = value;
            }
        }

        private List<Tuple<Point, SolidBrush, int>> dots;
        private List<Tuple<Point, SolidBrush, int>> undoDots;

        private Point mouseLocation;

        public DotDraw(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
            brush = new SolidBrush(Color.Transparent);
            dots = new List<Tuple<Point, SolidBrush, int>>();
            undoDots = new List<Tuple<Point, SolidBrush, int>>();
        }

        ~DotDraw()
        {
            brush.Dispose();
            foreach (var dot in dots)
            {
                dot.Item2.Dispose();
            }
            foreach (var dot in undoDots)
            {
                dot.Item2.Dispose();
            }
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            foreach (var dot in dots)
            {
                Point shown = imageDisplay.GetShownLocation(dot.Item1);
                graphics.FillRectangle(
                    dot.Item2,
                    (float)(shown.X - dot.Item3 * imageDisplay.ZoomMagnification / 2.0),
                    (float)(shown.Y - dot.Item3 * imageDisplay.ZoomMagnification / 2.0),
                    (float)(dot.Item3 * imageDisplay.ZoomMagnification),
                    (float)(dot.Item3 * imageDisplay.ZoomMagnification));
            }

            graphics.FillRectangle(
                brush,
                (float)(mouseLocation.X - DotSize * imageDisplay.ZoomMagnification / 2.0),
                (float)(mouseLocation.Y - DotSize * imageDisplay.ZoomMagnification / 2.0),
                (float)(DotSize * imageDisplay.ZoomMagnification),
                (float)(DotSize * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var dot in dots)
                {
                    graphics.FillRectangle(dot.Item2, (float)(dot.Item1.X - dot.Item3 / 2.0), (float)(dot.Item1.Y - dot.Item3 / 2.0), dot.Item3, dot.Item3);
                }
            }
        }

        public void Click(Point location)
        {
            Point adjusted = imageDisplay.GetAdjustedLocation(location);
            dots.Add(Tuple.Create(adjusted, (SolidBrush)brush.Clone(), DotSize));
            foreach (var dot in undoDots)
            {
                dot.Item2.Dispose();
            }
            undoDots.Clear();
        }

        public void RightClick(Point location)
        {
            var di_min = dots.Select(t => getDistance(t.Item1, location))
                .Zip(Enumerable.Range(0, dots.Count), (d, i) => Tuple.Create(d, i))
                .Min();

            //int index = -1;
            //double minDistance = double.MaxValue;
            //for (int i = 0; i < dots.Count; ++i)
            //{
            //    double d = getDistance(dots[i].Item1, location);
            //    if (d < minDistance)
            //    {
            //        index = i;
            //        minDistance = d;
            //    }
            //}

            // 適当
            if (di_min.Item1 < 16.0)
            {
                //var t = dots[di_min.Item2];
                dots.RemoveAt(di_min.Item2);
                //undoDots.Add(t);
            }
        }

        public void MouseMove(Point location)
        {
            mouseLocation = location;
        }

        public void UndoDrawing()
        {
            if (dots.Count != 0)
            {
                var t = dots.Last();
                dots.RemoveAt(dots.Count - 1);
                undoDots.Add(t);
            }
        }

        public void RedoDrawing()
        {
            if (undoDots.Count != 0)
            {
                var t = undoDots.Last();
                undoDots.RemoveAt(undoDots.Count - 1);
                dots.Add(t);
            }
        }

        private static double getDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }
    }
}
