using System.Drawing;

namespace GrainDetector
{
    public class ImageDisplay
    {
        private Bitmap _image;
        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != null && value != _image)
                {
                    _image.Dispose();
                }
                _image = value;
            }
        }

        public Point ZoomLocation;
        public double ZoomMagnification;

        public void Reset()
        {
            ZoomLocation = new Point(0, 0);
            ZoomMagnification = 1;
        }

        public void DrawImage(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(Image, 0, 0, (int)(Image.Width * ZoomMagnification), (int)(Image.Height * ZoomMagnification));
        }

        public Size GetPictureBoxSize()
        {
            return new Size((int)(Image.Width * ZoomMagnification), (int)(Image.Height * ZoomMagnification));
        }

        public Size GetSizeToWidth(int width)
        {
            return new Size(width, Image.Height * width / Image.Width);
        }

        public Size GetSizeToHeight(int height)
        {
            return new Size(Image.Width * height / Image.Height, height);
        }

        public Point GetAdjustedLocation(Point location)
        {
            return new Point((int)(location.X / ZoomMagnification), (int)(location.Y / ZoomMagnification));
        }

        public Point GetShownLocation(Point location)
        {
            return new Point((int)(location.X * ZoomMagnification), (int)(location.Y * ZoomMagnification));
        }
    }
}
