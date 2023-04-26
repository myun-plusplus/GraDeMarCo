﻿using System;
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

        public Point ZoomLocation;
        private double _zoomMagnification;
        public double ZoomMagnification
        {
            get
            {
                return _zoomMagnification;
            }
            set
            {
                double preValue = _zoomMagnification;
                _zoomMagnification = value;

                this.pictureBox.Width = (int)(image.Width * value);
                this.pictureBox.Height = (int)(image.Height * value);

                if (preValue != 0)
                {
                    this.HorizontalScroll.Value = (int)(this.HorizontalScroll.Value * value / preValue);
                    this.VerticalScroll.Value = (int)(this.VerticalScroll.Value * value / preValue);
                }

                this.Refresh();
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

        private void ImageForm_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                ZoomLocation.X = e.NewValue;
            }
            else
            {
                ZoomLocation.Y = e.NewValue;
            }
        }

        public void SetImage(Bitmap image)
        {
            this.pictureBox.Size = image.Size;
            this.image = image;
            ZoomMagnification = 1.0;

            int defaultWidth = 720;
            Size size = getSizeToWidth(defaultWidth);
            this.ClientSize = size;
        }

        private void drawImage(Graphics graphics)
        {
            graphics.DrawImage(image, 0, 0, (int)(image.Width * ZoomMagnification), (int)(image.Height * ZoomMagnification));
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
