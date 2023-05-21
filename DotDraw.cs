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

        private class Dot
        {
            public Point Location;
            public SolidBrush Brush;
            public int Size;

            ~Dot()
            {
                Brush.Dispose();
            }

            public Dot Clone()
            {
                return new Dot
                {
                    Location = this.Location,
                    Brush = (SolidBrush)this.Brush.Clone(),
                    Size = this.Size
                };
            }
        }

        private List<Dot> drawnDots;

        private Point mouseLocation;

        // false:draw, true:erase
        private List<Tuple<Dot, bool>> doList, undoList;

        public DotDraw(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
            brush = new SolidBrush(Color.Transparent);
            drawnDots = new List<Dot>();
            doList = new List<Tuple<Dot, bool>>();
            undoList = new List<Tuple<Dot, bool>>();
        }

        ~DotDraw()
        {
            brush.Dispose();
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            foreach (Dot dot in drawnDots)
            {
                Point shown = imageDisplay.GetShownLocation(dot.Location);
                graphics.FillRectangle(
                    dot.Brush,
                    (float)(shown.X - dot.Size * imageDisplay.ZoomMagnification / 2.0),
                    (float)(shown.Y - dot.Size * imageDisplay.ZoomMagnification / 2.0),
                    (float)(dot.Size * imageDisplay.ZoomMagnification),
                    (float)(dot.Size * imageDisplay.ZoomMagnification));
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
                foreach (Dot dot in drawnDots)
                {
                    graphics.FillRectangle(
                        dot.Brush,
                        (float)(dot.Location.X - dot.Size / 2.0),
                        (float)(dot.Location.Y - dot.Size / 2.0),
                        dot.Size,
                        dot.Size);
                }
            }
        }

        public void Click(Point location)
        {
            DrawDot(imageDisplay.GetAdjustedLocation(location), this.brush, this.DotSize);
        }

        public void RightClick(Point location)
        {
            EraseDot(imageDisplay.GetAdjustedLocation(location));
        }

        public void MouseMove(Point location)
        {
            mouseLocation = location;
        }

        public void DrawDot(Point location, SolidBrush brush, int dotSize)
        {
            Dot dot = new Dot
            {
                Location = location,
                Brush = (SolidBrush)brush.Clone(),
                Size = dotSize
            };

            drawnDots.Add(dot);

            doList.Add(Tuple.Create(dot.Clone(), false));
            undoList.Clear();
        }

        public void EraseDot(Point location)
        {
            var di_min = drawnDots
                .Select(dot => getDistance(dot.Location, location))
                .Zip(Enumerable.Range(0, drawnDots.Count), (distance, index) => Tuple.Create(distance, index))
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
                Dot dot = drawnDots[di_min.Item2];

                drawnDots.RemoveAt(di_min.Item2);

                doList.Add(Tuple.Create(dot, true));
                undoList.Clear();
            }
        }

        public void ClearAllDots()
        {
            drawnDots.Reverse();
            doList = drawnDots
                .Select(dot => Tuple.Create(dot, true))
                .ToList();
            undoList.Clear();

            drawnDots.Clear();
        }

        public void UndoDrawing()
        {
            if (doList.Count == 0)
            {
                return;
            }

            var t = doList.Last();
            if (!t.Item2)
            {
                drawnDots.RemoveAt(drawnDots.Count - 1);
            }
            else
            {
                drawnDots.Add(t.Item1.Clone());
            }

            undoList.Add(t);
            doList.RemoveAt(doList.Count - 1);
        }

        public void RedoDrawing()
        {
            if (undoList.Count == 0)
            {
                return;
            }

            var t = undoList.Last();
            if (!t.Item2)
            {
                drawnDots.Add(t.Item1.Clone());
            }
            else
            {
                drawnDots.RemoveAt(drawnDots.Count - 1);
            }

            doList.Add(t);
            undoList.RemoveAt(undoList.Count - 1);
        }

        private static double getDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }
    }
}
