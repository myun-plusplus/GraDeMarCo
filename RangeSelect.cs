using System;
using System.Drawing;

namespace GrainDetector
{
    public class RangeSelect : BindingBase
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
                UpperX = ep.X;
                UpperY = ep.Y;
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
                UpperX = ep.X;
                UpperY = ep.Y;
            }
        }

        private int _lowerX, _upperX;
        private int _lowerY, _upperY;
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
        public int UpperX
        {
            get
            {
                return _upperX;
            }
            private set
            {
                _upperX = value;
                OnPropertyChanged(GetName.Of(() => UpperX));
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
        public int UpperY
        {
            get
            {
                return _upperY;
            }
            private set
            {
                _upperY = value;
                OnPropertyChanged(GetName.Of(() => UpperY));
            }
        }

        #endregion

        private Pen pen;

        public RangeSelect(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
            state = State.NotActive;
            pen = new Pen(Color.Red, 1);
        }

        public void Start()
        {
            state = State.NoneSelected;
            _startLocation = new Point(0, 0);
            _endLocation = new Point(0, 0);
            LowerX = 0;
            UpperX = imageDisplay.Image.Width - 1;
            LowerY = 0;
            UpperY = imageDisplay.Image.Height - 1;
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
                graphics.DrawRectangle(pen, t.Item1.X, t.Item1.Y, t.Item2.X - t.Item1.X, t.Item2.Y - t.Item1.Y);
            }
        }

        public void DrawOnBitmap(Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.DrawRectangle(pen, LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
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
