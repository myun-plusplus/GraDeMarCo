using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GrainDetector
{
    internal class MainController : IDisposable
    {
        private MainForm mainForm;
        private ImageForm imageForm;

        private ImageDisplay imageDisplay;

        private FormState.ActionMode _actionMode;
        public FormState.ActionMode ActionMode
        {
            get
            {
                return _actionMode;
            }
            set
            {
                _actionMode = value;
            }
        }

        private Bitmap _targetImage;
        private Bitmap targetImage
        {
            get
            { 
                return _targetImage;
            }
            set
            {
                if (_targetImage != null)
                {
                    _targetImage.Dispose();
                }
                _targetImage = value;
            }
        }

        public MainController(MainForm mf)
        {
            this.mainForm = mf;

            imageDisplay = new ImageDisplay();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.imageForm != null && !this.imageForm.IsDisposed)
                {
                    this.imageForm.Dispose();
                }
            }

            this.imageForm = null;
            this.mainForm = null;
        }

        public void OpenImageFile(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("選択したファイルが存在しません。", "エラー");
                return;
            }
            try
            {
                targetImage = new Bitmap(filename);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("選択したファイルは画像ファイルではありません。", "エラー");
                return;
            }
        }

        public void OpenImageForm()
        {
            CloseImageForm();

            this.imageForm = new ImageForm(imageDisplay);
            this.imageForm.Location = new Point(this.mainForm.Location.X + 300, this.mainForm.Location.Y);
            this.imageForm.PictureBox_MouseDown_adjusted += test;
            this.imageForm.SetImage(targetImage);
            this.imageForm.Show();
        }

        public void CloseImageForm()
        {
            if (imageDisplay.Image != null)
            {
                imageDisplay.Image.Dispose();
            }
            imageDisplay.Image = null;

            if (this.imageForm != null && !this.imageForm.IsDisposed)
            {
                this.imageForm.Dispose();
            }
            this.imageForm = null;
        }

        public void ZoomInImage()
        {
            imageForm.MultipleZoomMagnification(2.0);
        }

        public void ZoomOutImage()
        {
            imageForm.MultipleZoomMagnification(0.5);
        }

        private void test(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }
    }
}
