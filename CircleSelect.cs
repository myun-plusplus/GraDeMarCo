using System;
using System.Drawing;

namespace GrainDetector
{
    public class CircleSelect : BindingBase
    {
        private ImageDisplay imageDisplay;

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
            private set
            {
                _startLocation = value;
                var t = orderPoints(_startLocation, _endLocation);
                Point sp = imageDisplay.GetAdjustedLocation(t.Item1);
                Point ep = imageDisplay.GetAdjustedLocation(t.Item2);
                LowerX = sp.X;
                LowerY = sp.Y;
                Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
            }
        }

        public Point EndLocation
        {
            get
            {
                return _endLocation;
            }
            private set
            {
                _endLocation = value;
                var t = orderPoints(_startLocation, _endLocation);
                Point sp = imageDisplay.GetAdjustedLocation(t.Item1);
                Point ep = imageDisplay.GetAdjustedLocation(t.Item2);
                LowerX = sp.X;
                LowerY = sp.Y;
                Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
            }
        }

        private int _lowerX, _lowerY;
        public int LowerX
        {
            get
            {
                return _lowerX;
            }
            private set
            {
                _lowerX = value;
                OnPropertyChanged(GetName.Of(() => LowerX));
            }
        }
        public int LowerY
        {
            get
            {
                return _lowerY;
            }
            private set
            {
                _lowerY = value;
                OnPropertyChanged(GetName.Of(() => LowerY));
            }
        }

        private int _diameter;
        public int Diameter
        {
            get
            {
                return _diameter;
            }
            private set
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
        {
            this.imageDisplay = imageDisplay;
            state = State.NotActive;
            pen = new Pen(Color.Transparent, 1);
            CircleColor = Color.Transparent;
        }

        public void Start()
        {
            state = State.NoneSelected;
            _startLocation = new Point(0, 0);
            _endLocation = new Point(0, 0);
            LowerX = 0;
            LowerY = 0;
            Diameter = Math.Min(imageDisplay.Image.Width - 1, imageDisplay.Image.Height - 1);
        }

        public void Stop()
        {
            state = State.NotActive;
        }

        public void DrawOnPaintEvent(Graphics graphics)
        {
            if (state == State.StartLocationSelected || state == State.RangeSelected)
            {
                var t = orderPoints(StartLocation, EndLocation);
                int diameter = Math.Min(t.Item2.X - t.Item1.X, t.Item2.Y - t.Item1.Y);
                graphics.DrawEllipse(pen, t.Item1.X, t.Item1.Y, diameter, diameter);
            }
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.DrawEllipse(pen, LowerX, LowerY, Diameter, Diameter);
            }
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

        private static Tuple<Point, Point> orderPoints(Point p1, Point p2)
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
    }
}
