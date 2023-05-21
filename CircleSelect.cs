using System;
using System.Drawing;

namespace GrainDetector
{
    public class PlanimetricCircle : BindingBase
    {
        public int LowerX
        {
            get
            {
                return _lowerX;
            }
            set
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
            set
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
            set
            {
                _diameter = value;
                OnPropertyChanged(GetName.Of(() => Diameter));
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                Pen.Color = value;
            }
        }

        public Pen Pen
        {
            get;
            private set;
        }

        private int _lowerX, _lowerY;
        private Color _color;

        public PlanimetricCircle()
        {
            Pen = new Pen(Color.Transparent);
            Color = Color.Transparent;
        }
    }

    public class CircleSelect : BindingBase
    {
        enum State
        {
            NotActive,
            NoneSelected,
            StartLocationSelected,
            RangeSelected
        }
        private State state;

        private ImageDisplay imageDisplay;
        private PlanimetricCircle circle;

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
                circle.LowerX = sp.X;
                circle.LowerY = sp.Y;
                circle.Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
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
                circle.LowerX = sp.X;
                circle.LowerY = sp.Y;
                circle.Diameter = Math.Min(ep.X - sp.X, ep.Y - sp.Y);
            }
        }

        private Point _startLocation, _endLocation;

        public CircleSelect(ImageDisplay imageDisplay, PlanimetricCircle circle)
        {
            this.imageDisplay = imageDisplay;
            this.circle = circle;
            state = State.NotActive;
        }

        public void Start()
        {
            state = State.NoneSelected;
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
                graphics.DrawEllipse(
                    circle.Pen,
                    t.Item1.X,
                    t.Item1.Y,
                    diameter,
                    diameter);
            }
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.DrawEllipse(
                    circle.Pen,
                    circle.LowerX,
                    circle.LowerY,
                    circle.Diameter,
                    circle.Diameter);
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
