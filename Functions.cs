using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GrainDetector
{
    public abstract class FunctionBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ImageDisplay imageDisplay;

        protected FunctionBase(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void DrawOnPaintEvent(Graphics graphics);
        public abstract void Draw(Graphics graphics);

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RangeSelect : FunctionBase
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
                EndX = ep.X;
                EndY = ep.Y;
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
                EndX = ep.X;
                EndY = ep.Y;
            }
        }

        private int _startX, _startY, _endX, _endY;
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
        public int EndX
        {
            get
            {
                return _endX;
            }
            set
            {
                _endX = value;
                OnPropertyChanged(GetName.Of(() => EndX));
            }
        }
        public int EndY
        {
            get
            {
                return _endY;
            }
            set
            {
                _endY = value;
                OnPropertyChanged(GetName.Of(() => EndY));
            }
        }

        #endregion

        private Pen pen;

        public RangeSelect(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {
            state = State.NotActive;
            pen = new Pen(Color.Red, 1);
        }

        public override void Start()
        {
            state = State.NoneSelected;
            _startLocation = new Point(0, 0);
            _endLocation = new Point(0, 0);
            StartX = 0;
            StartY = 0;
            EndX = imageDisplay.Image.Width - 1;
            EndY = imageDisplay.Image.Height - 1;
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
            graphics.DrawRectangle(pen, t.Item1.X, t.Item1.Y, t.Item2.X - t.Item1.X, t.Item2.Y - t.Item1.Y);
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
    }
}
