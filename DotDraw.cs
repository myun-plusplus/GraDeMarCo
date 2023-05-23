using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GrainDetector
{
    public class DotDrawTool : BindingBase
    {
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                Brush.Color = value;
                OnPropertyChanged(GetName.Of(() => Color));
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged(GetName.Of(() => Size));
            }
        }

        public SolidBrush Brush
        {
            get;
            private set;
        }

        private Color _color;
        private int _size;

        public DotDrawTool()
        {
            Brush = new SolidBrush(Color.Transparent);
        }

        ~DotDrawTool()
        {
            Brush.Dispose();
        }
    }

    public class Dot
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

    public class DrawnDotsData
    {
        public List<Dot> Dots;

        // false:draw, true:erase
        public List<Tuple<Dot, bool>> DoneList, UndoList;

        public DrawnDotsData()
        {
            Dots = new List<Dot>();
            DoneList = new List<Tuple<Dot, bool>>();
            UndoList = new List<Tuple<Dot, bool>>();
        }
    }

    public class DotDraw : BindingBase
    {
        private ImageDisplay imageDisplay;
        private DotDrawTool tool;
        private DrawnDotsData dotsData;

        private Point mouseLocation;

        public DotDraw(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
            tool = new DotDrawTool();
            dotsData = new DrawnDotsData();
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            foreach (Dot dot in dotsData.Dots)
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
                tool.Brush,
                (float)(mouseLocation.X - tool.Size * imageDisplay.ZoomMagnification / 2.0),
                (float)(mouseLocation.Y - tool.Size * imageDisplay.ZoomMagnification / 2.0),
                (float)(tool.Size * imageDisplay.ZoomMagnification),
                (float)(tool.Size * imageDisplay.ZoomMagnification));
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (Dot dot in dotsData.Dots)
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
            DrawDot(imageDisplay.GetAdjustedLocation(location), tool.Brush, this.tool.Size);
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

            dotsData.Dots.Add(dot);

            dotsData.DoneList.Add(Tuple.Create(dot.Clone(), false));
            dotsData.UndoList.Clear();
        }

        public void EraseDot(Point location)
        {
            var di_min = dotsData.Dots
                .Select(dot => getDistance(dot.Location, location))
                .Zip(Enumerable.Range(0, dotsData.Dots.Count), (distance, index) => Tuple.Create(distance, index))
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
                Dot dot = dotsData.Dots[di_min.Item2];

                dotsData.Dots.RemoveAt(di_min.Item2);

                dotsData.DoneList.Add(Tuple.Create(dot, true));
                dotsData.UndoList.Clear();
            }
        }

        public void ClearAllDots()
        {
            dotsData.Dots.Reverse();
            dotsData.DoneList = dotsData.Dots
                .Select(dot => Tuple.Create(dot, true))
                .ToList();
            dotsData.UndoList.Clear();

            dotsData.Dots.Clear();
        }

        public void UndoDrawing()
        {
            if (dotsData.DoneList.Count == 0)
            {
                return;
            }

            var t = dotsData.DoneList.Last();
            if (!t.Item2)
            {
                dotsData.Dots.RemoveAt(dotsData.Dots.Count - 1);
            }
            else
            {
                dotsData.Dots.Add(t.Item1.Clone());
            }

            dotsData.UndoList.Add(t);
            dotsData.DoneList.RemoveAt(dotsData.DoneList.Count - 1);
        }

        public void RedoDrawing()
        {
            if (dotsData.UndoList.Count == 0)
            {
                return;
            }

            var t = dotsData.UndoList.Last();
            if (!t.Item2)
            {
                dotsData.Dots.Add(t.Item1.Clone());
            }
            else
            {
                dotsData.Dots.RemoveAt(dotsData.Dots.Count - 1);
            }

            dotsData.DoneList.Add(t);
            dotsData.UndoList.RemoveAt(dotsData.UndoList.Count - 1);
        }

        private static double getDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }
    }
}
