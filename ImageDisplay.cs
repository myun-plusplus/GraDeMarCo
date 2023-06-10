using System.Drawing;
using System.Windows.Forms;

namespace GraDeMarCo
{
    public class ImageDisplay
    {
        public PropertyValueChangedEventHandler ZoomMagnificationChanged;

        private ImageData imageData;

        public double ZoomMagnification
        {
            get
            {
                return _zoomMagnification;
            }
            set
            {
                var oldValue = _zoomMagnification;
                _zoomMagnification = value;
                if (ZoomMagnificationChanged != null)
                {
                    ZoomMagnificationChanged.Invoke(this, new PropertyValueChangedEventArgs(null, oldValue));
                }
            }
        }

        private double _zoomMagnification;

        public ImageDisplay(ImageData imageData)
        {
            this.imageData = imageData;
        }

        public void Initialize()
        {
            ZoomMagnification = 1;
        }

        public void DrawImage(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.DrawImage(
                imageData.ShownImage,
                0,
                0,
                (int)(imageData.ShownImage.Width * ZoomMagnification),
                (int)(imageData.ShownImage.Height * ZoomMagnification));
        }

        public bool CanZoomIn()
        {
            return ZoomMagnification < 8.0;
        }

        public bool CanZoomOut()
        {
            return ZoomMagnification > 0.125;
        }

        public Size GetPictureBoxSize()
        {
            return new Size(
                (int)(imageData.ShownImage.Width * ZoomMagnification),
                (int)(imageData.ShownImage.Height * ZoomMagnification));
        }

        public Size GetSizeToWidth(int width)
        {
            return new Size(width, imageData.ShownImage.Height * width / imageData.ShownImage.Width);
        }

        public Size GetSizeToHeight(int height)
        {
            return new Size(imageData.ShownImage.Width * height / imageData.ShownImage.Height, height);
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
