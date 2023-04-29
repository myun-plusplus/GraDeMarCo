using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GrainDetector
{
    public partial class ImageForm : Form
    {
        public EventHandler<MouseEventArgs> PictureBox_MouseDown_adjusted;

        private ImageDisplay imageDisplay;

        public ImageForm(ImageDisplay imageDisplay)
        {
            InitializeComponent();

            this.imageDisplay = imageDisplay;
        }

        private void ImageForm_Resize(object sender, EventArgs e)
        {

        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            imageDisplay.DrawImage(e.Graphics);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Point adjusted = imageDisplay.GetAdjustedLocation(e.Location);
            PictureBox_MouseDown_adjusted(this.pictureBox, new MouseEventArgs(e.Button, e.Clicks, adjusted.X, adjusted.Y, e.Delta));
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

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

        public void SetImage(Bitmap image)
        {
            imageDisplay.Image = image;

            int defaultWidth = 720;
            Size size = imageDisplay.GetSizeToWidth(defaultWidth);
            this.ClientSize = size;
            this.pictureBox.Size = image.Size;
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
