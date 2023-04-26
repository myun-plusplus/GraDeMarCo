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

            this.imageForm = new ImageForm();
            this.imageForm.Location = new Point(this.mainForm.Location.X + 300, this.mainForm.Location.Y);
            this.imageForm.SetImage(targetImage);
            this.imageForm.Show();
        }

        public void CloseImageForm()
        {
            if (this.imageForm != null && !this.imageForm.IsDisposed)
            {
                this.imageForm.Dispose();
            }
            this.imageForm = null;
        }

        public void ZoomInImage()
        {
            this.imageForm.ZoomMagnification *= 2;
        }

        public void ZoomOutImage()
        {
            this.imageForm.ZoomMagnification /= 2;
        }
    }
}
