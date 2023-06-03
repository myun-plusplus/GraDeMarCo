using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class ImageForm : Form
    {
        private ImageData imageData;
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

        public ImageForm(ImageData imageData, ImageDisplay imageDisplay, RangeSelect rangeSelect, CircleSelect circleSelect, ImageFilter imageFilter, ImageBinarize imageBinarize, DotDraw dotDraw)
        {
            InitializeComponent();
            this.MouseWheel += imageForm_MouseWheel;

            this.imageData = imageData;
            this.imageDisplay = imageDisplay;
            this.imageDisplay.ZoomMagnificationChanged += ImageForm_ZoomMagnificationChanged;
            this.rangeSelect = rangeSelect;
            this.circleSelect = circleSelect;
            this.imageFilter = imageFilter;
            this.imageBinarize = imageBinarize;
            this.dotDraw = dotDraw;

            int defaultWidth = imageData.ShownImage.Width / 2;
            Size size = imageDisplay.GetSizeToWidth(defaultWidth);
            this.ClientSize = size;
            this.pictureBox.Size = imageData.ShownImage.Size;;
        }

        private void imageForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0 && imageDisplay.CanZoomIn())
                {
                    imageDisplay.ZoomMagnification *= 2;
                }
                else if (e.Delta < 0 && imageDisplay.CanZoomOut())
                {
                    imageDisplay.ZoomMagnification *= 0.5;
                }
            }
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
                if (e.Button == MouseButtons.Left)
                {
                    dotDraw.Click(e.Location);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    dotDraw.RightClick(e.Location);
                }
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

        private void ImageForm_ZoomMagnificationChanged(object sender, PropertyValueChangedEventArgs e)
        {
            double oldValue = (double)e.OldValue;

            this.pictureBox.Width = (int)(imageData.ShownImage.Width * imageDisplay.ZoomMagnification);
            this.pictureBox.Height = (int)(imageData.ShownImage.Height * imageDisplay.ZoomMagnification);

            if (oldValue != 0)
            {
                this.HorizontalScroll.Value = (int)(this.HorizontalScroll.Value * imageDisplay.ZoomMagnification / oldValue);
                this.VerticalScroll.Value = (int)(this.VerticalScroll.Value * imageDisplay.ZoomMagnification / oldValue);
            }

            this.Refresh();
        }
    }
}
