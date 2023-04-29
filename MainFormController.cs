using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm
    {
        private ImageForm imageForm;

        private ImageDisplay imageDisplay;

        private FormState.ActionMode _actionMode;
        private FormState.ActionMode actionMode
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

        private void openImageForm()
        {
            this.imageForm = new ImageForm(imageDisplay);
            this.imageForm.Location = new Point(this.Location.X + 300, this.Location.Y);
            this.imageForm.PictureBox_MouseDown_adjusted += test;
            this.imageForm.SetImage(targetImage);

            this.imageForm.Show();
        }

        private void closeImageForm()
        {
            imageDisplay.Image = null;

            if (this.imageForm != null && !this.imageForm.IsDisposed)
            {
                this.imageForm.Dispose();
            }
            this.imageForm = null;
        }

        private bool isImageFormOpen()
        {
            return this.imageForm != null && !this.imageForm.IsDisposed;
        }

        #if DEBUG
        private void test(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }
        #endif
    }
}
