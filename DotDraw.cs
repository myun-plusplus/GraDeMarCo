using System;
using System.Collections.Generic;
using System.Drawing;

namespace GrainDetector
{
    public class DotDraw : FunctionBase
    {
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

        private Stack<Tuple<Point, SolidBrush, int>> dots;
        private Stack<Tuple<Point, SolidBrush, int>> undoDots;

        private Point mouseLocation;

        public DotDraw(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {
            brush = new SolidBrush(Color.Transparent);
            dots = new Stack<Tuple<Point, SolidBrush, int>>();
            undoDots = new Stack<Tuple<Point, SolidBrush, int>>();
        }

        ~DotDraw()
        {
            // pen.Dispose();
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

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        public override void DrawOnPaintEvent(Graphics graphics)
        {
            Draw(graphics);
        }
        public override void Draw(Graphics graphics)
        {
            foreach (var dot in dots)
            {
                Point shown = imageDisplay.GetShownLocation(dot.Item1);
                graphics.FillRectangle(dot.Item2,
                    (float)(shown.X - dot.Item3 * imageDisplay.ZoomMagnification / 2.0),
                    (float)(shown.Y - dot.Item3 * imageDisplay.ZoomMagnification / 2.0),
                    (float)(dot.Item3 * imageDisplay.ZoomMagnification),
                    (float)(dot.Item3 * imageDisplay.ZoomMagnification));
            }

            graphics.FillRectangle(brush,
                (float)(mouseLocation.X - DotSize * imageDisplay.ZoomMagnification / 2.0),
                (float)(mouseLocation.Y - DotSize * imageDisplay.ZoomMagnification / 2.0),
                (float)(DotSize * imageDisplay.ZoomMagnification),
                (float)(DotSize * imageDisplay.ZoomMagnification));
        }

        public void Click(Point location)
        {
            Point adjusted = imageDisplay.GetAdjustedLocation(location);
            dots.Push(Tuple.Create(adjusted, (SolidBrush)brush.Clone(), DotSize));
            foreach (var dot in undoDots)
            {
                dot.Item2.Dispose();
            }
            undoDots.Clear();
        }

        public void MouseMove(Point location)
        {
            mouseLocation = location;
        }

        public void UndoDrawing()
        {
            if (dots.Count != 0)
            {
                undoDots.Push(dots.Pop());
            }
        }

        public void RedoDrawing()
        {
            if (undoDots.Count != 0)
            {
                dots.Push(undoDots.Pop());
            }
        }

        public void DrawOnImage(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (var dot in dots)
                {
                    graphics.FillRectangle(dot.Item2, (float)(dot.Item1.X - dot.Item3 / 2.0), (float)(dot.Item1.Y - dot.Item3 / 2.0), dot.Item3, dot.Item3);
                }
            }
        }
    }
}
