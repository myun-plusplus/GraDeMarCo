using System.Drawing;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class ImageForm : Form
    {
        private ImageDisplay imageDisplay;
        private RangeSelect rangeSelect;
        private CircleSelect circleSelect;
        private ImageFilter imageFilter;
        private ImageBinarize imageBinarize;
        private DotDraw dotDraw;

        private ActionMode _actionMode;
        public ActionMode ActionMode
        {
            get
            {
                return _actionMode;
            }
            set
            {
                _actionMode = value;
                this.Refresh();
            }
        }

        public ImageForm(ImageDisplay imageDisplay, RangeSelect rangeSelect, CircleSelect circleSelect, ImageFilter imageFilter, ImageBinarize imageBinarize, DotDraw dotDraw)
        {
            InitializeComponent();

            this.imageDisplay = imageDisplay;
            this.rangeSelect = rangeSelect;
            this.circleSelect = circleSelect;
            this.imageFilter = imageFilter;
            this.imageBinarize = imageBinarize;
            this.dotDraw = dotDraw;

            int defaultWidth = imageDisplay.Image.Width / 2;
            Size size = imageDisplay.GetSizeToWidth(defaultWidth);
            this.ClientSize = size;
            this.pictureBox.Size = imageDisplay.Image.Size;;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            imageDisplay.DrawImage(e.Graphics);

            if (ActionMode == ActionMode.ImageRangeSelect)
            {
                rangeSelect.DrawOnPaintEvent(e.Graphics);
            }
            else if (ActionMode == ActionMode.CircleSelect)
            {
                circleSelect.DrawOnPaintEvent(e.Graphics);
            }
            // 二値化優先
            else if (ActionMode == ActionMode.ImageBinarize)
            {
                imageBinarize.DrawOnPaintEvent(e.Graphics);
            }
            else if (ActionMode == ActionMode.ImageFilter)
            {
                imageFilter.DrawOnPaintEvent(e.Graphics);
            }
            else if (ActionMode == ActionMode.DotDraw)
            {
                dotDraw.DrawOnPaintEvent(e.Graphics);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (ActionMode == ActionMode.ImageRangeSelect)
            {
                rangeSelect.Click(e.Location);
            }
            else if (ActionMode == ActionMode.CircleSelect)
            {
                circleSelect.Click(e.Location);
            }
            else if (ActionMode == ActionMode.DotDraw)
            {
                dotDraw.Click(e.Location);
            }
            this.pictureBox.Invalidate();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (ActionMode == ActionMode.ImageRangeSelect)
            {
                rangeSelect.MouseMove(e.Location);
            }
            else if (ActionMode == ActionMode.CircleSelect)
            {
                circleSelect.MouseMove(e.Location);
            }
            else if (ActionMode == ActionMode.DotDraw)
            {
                dotDraw.MouseMove(e.Location);
            }
            this.pictureBox.Invalidate();
        }

        private void ImageForm_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                imageDisplay.ZoomLocation.X = e.NewValue;
            }
            else
            {
                imageDisplay.ZoomLocation.Y = e.NewValue;
            }
        }

        public void MultipleZoomMagnification(double coefficient)
        {
            ChangeZoomMagnification(imageDisplay.ZoomMagnification * coefficient);
        }

        public void ChangeZoomMagnification(double zoomMagnification)
        {
            double preValue = imageDisplay.ZoomMagnification;
            imageDisplay.ZoomMagnification = zoomMagnification;

            this.pictureBox.Width = (int)(imageDisplay.Image.Width * zoomMagnification);
            this.pictureBox.Height = (int)(imageDisplay.Image.Height * zoomMagnification);

            if (preValue != 0)
            {
                this.HorizontalScroll.Value = (int)(this.HorizontalScroll.Value * zoomMagnification / preValue);
                this.VerticalScroll.Value = (int)(this.VerticalScroll.Value * zoomMagnification / preValue);
            }

            this.Refresh();
        }
    }
}
