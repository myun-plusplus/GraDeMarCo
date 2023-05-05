using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GrainDetector
{
    public class CircleSelect : FunctionBase
    {
        enum State
        {
            NotActive,
            NoneSelected,
            StartLocationSelected,
            RangeSelected
        }
        private State state;

        #region LocationCoordinates

        private Point _startLocation, _endLocation;
        public Point StartLocation
        {
            get
            {
                return _startLocation;
            }
            set
            {
                _startLocation = value;
                var t = orderPoints(_startLocation, _endLocation);
                Point sp = imageDisplay.GetAdjustedLocation(t.Item1);
                Point ep = imageDisplay.GetAdjustedLocation(t.Item2);
                StartX = sp.X;
                StartY = sp.Y;
                Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
            }
        }
        public Point EndLocation
        {
            get
            {
                return _endLocation;
            }
            set
            {
                _endLocation = value;
                var t = orderPoints(_startLocation, _endLocation);
                Point sp = imageDisplay.GetAdjustedLocation(t.Item1);
                Point ep = imageDisplay.GetAdjustedLocation(t.Item2);
                StartX = sp.X;
                StartY = sp.Y;
                Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
            }
        }

        private int _startX, _startY, _diameter;
        public int StartX
        {
            get
            {
                return _startX;
            }
            set
            {
                _startX = value;
                OnPropertyChanged(GetName.Of(() => StartX));
            }
        }
        public int StartY
        {
            get
            {
                return _startY;
            }
            set
            {
                _startY = value;
                OnPropertyChanged(GetName.Of(() => StartY));
            }
        }
        public int Diameter
        {
            get
            {
                return _diameter;
            }
            set
            {
                _diameter = value;
                OnPropertyChanged(GetName.Of(() => Diameter));
            }
        }

        #endregion

        private Pen pen;
        private Color _circleColor;
        public Color CircleColor
        {
            get
            {
                return _circleColor;
            }
            set
            {
                _circleColor = value;
                pen.Color = value;
            }
        }

        public CircleSelect(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {
            state = State.NotActive;
            pen = new Pen(Color.Transparent, 1);
            CircleColor = Color.Transparent;
        }

        public override void Start()
        {
            state = State.NoneSelected;
            _startLocation = new Point(0, 0);
            _endLocation = new Point(0, 0);
            StartX = 0;
            StartY = 0;
            Diameter = Math.Min(imageDisplay.Image.Width - 1, imageDisplay.Image.Height - 1);
        }

        public override void Stop()
        {
            state = State.NotActive;
        }

        public override void DrawOnPaintEvent(Graphics graphics)
        {
            if (state == State.StartLocationSelected || state == State.RangeSelected)
            {
                Draw(graphics);
            }
        }

        public override void Draw(Graphics graphics)
        {
            var t = orderPoints(StartLocation, EndLocation);
            int diameter = Math.Min(t.Item2.X - t.Item1.X, t.Item2.Y - t.Item1.Y);
            graphics.DrawEllipse(pen, t.Item1.X, t.Item1.Y, diameter, diameter);
        }

        public void Click(Point location)
        {
            if (state == State.NotActive)
            {
                return;
            }
            else if (state == State.NoneSelected)
            {
                state = State.StartLocationSelected;
                StartLocation = location;
                EndLocation = location;
            }
            else if (state == State.StartLocationSelected)
            {
                state = State.RangeSelected;
                EndLocation = location;
            }
            else
            {
                state = State.NoneSelected;
            }
        }

        public void MouseMove(Point location)
        {
            if (state == State.StartLocationSelected)
            {
                EndLocation = location;
            }
        }

        private Tuple<Point, Point> orderPoints(Point p1, Point p2)
        {
            if (p1.X > p2.X)
            {
                int tmp = p1.X;
                p1.X = p2.X;
                p2.X = tmp;
            }
            if (p1.Y > p2.Y)
            {
                int tmp = p1.Y;
                p1.Y = p2.Y;
                p2.Y = tmp;
            }
            return new Tuple<Point, Point>(p1, p2);
        }

        public void DrawOnImage(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.DrawEllipse(pen, StartX, StartY, Diameter, Diameter);
            }
        }
    }
}
