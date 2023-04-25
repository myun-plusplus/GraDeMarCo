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
        private Bitmap _image;
        private Bitmap image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != null)
                {
                    _image.Dispose();
                }
                _image = value;
            }
        }

        public ImageForm()
        {
            InitializeComponent();
        }

        private void ImageForm_Resize(object sender, EventArgs e)
        {

        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            drawImage(e.Graphics);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

        }

        public void SetImage(Bitmap image)
        {
            this.image = image;

            int defaultWidth = 720;
            Size size = getSizeToWidth(defaultWidth);
            this.ClientSize = size;
            this.pictureBox.Size = size;
        }

        private void drawImage(Graphics graphics)
        {
            Size size;
            if (image.Width * this.pictureBox.Height < this.pictureBox.Width * image.Height)
            {
                size = getSizeToHeight(this.pictureBox.Height);
            }
            else
            {
                size = getSizeToWidth(this.pictureBox.Width);
            }
            graphics.DrawImage(image, 0, 0, size.Width, size.Height);
        }

        private Size getSizeToWidth(int width)
        {
            int height = image.Height * width / image.Width;
            return new Size(width, height);
        }

        private Size getSizeToHeight(int height)
        {
            int width = image.Width * height / image.Height;
            return new Size(width, height);
        }
    }
}
