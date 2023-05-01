using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GrainDetector
{
    public partial class MainForm : Form
    {
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!isImageFormOpened)
            {
                e.Cancel = true;
            }
            if (actionMode != FormState.ActionMode.None)
            {
                e.Cancel = true;
            }
        }

        #region ImageOpening

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "画像ファイル(*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff)|*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = ofd.FileName;
            }

            ofd.Dispose();
        }

        private void imageOpenButton_Click(object sender, EventArgs e)
        {
            imageDisplay.Image = null;
            closeImageForm();

            String filePath = this.filePathTextBox.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("選択したファイルが存在しません。", "エラー");
                return;
            }
            try
            {
                targetImage = new Bitmap(filePath);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("選択したファイルは画像ファイルではありません。", "エラー");
                return;
            }

            updateValidation();

            imageDisplay.Image = new Bitmap(targetImage);
            imageDisplay.Reset();

            isImageFormOpened = true;

            openImageForm();

            validateZoomMagnification();
        }

        #endregion

        #region RangeSelecting

        private void lowerXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerXNumericUpDown.Value > upperXNumericUpDown.Value)
            {
                decimal tmp = lowerXNumericUpDown.Value;
                upperXNumericUpDown.Value = lowerXNumericUpDown.Value;
                lowerXNumericUpDown.Value = tmp;
            }
        }

        private void lowerYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerYNumericUpDown.Value > upperYNumericUpDown.Value)
            {
                decimal tmp = lowerYNumericUpDown.Value;
                upperYNumericUpDown.Value = lowerYNumericUpDown.Value;
                lowerYNumericUpDown.Value = tmp;
            }
        }

        private void upperXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerXNumericUpDown.Value > upperXNumericUpDown.Value)
            {
                decimal tmp = upperXNumericUpDown.Value;
                lowerXNumericUpDown.Value = upperXNumericUpDown.Value;
                upperXNumericUpDown.Value = tmp;
            }
        }

        private void upperYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerYNumericUpDown.Value > upperYNumericUpDown.Value)
            {
                decimal tmp = upperYNumericUpDown.Value;
                lowerYNumericUpDown.Value = upperYNumericUpDown.Value;
                upperYNumericUpDown.Value = tmp;
            }
        }

        private void numericUpDowns_Validating(object sender, CancelEventArgs e)
        {

        }

        private void rangeSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rangeSelectCheckBox.Checked)
            {
                actionMode = FormState.ActionMode.ImageRangeSelect;
                rangeSelect.Start();
            }
            else
            {
                actionMode = FormState.ActionMode.None;
                rangeSelect.Stop();
            }
        }

        #endregion

        #region CircleSelecting

        private void circleXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void circleYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void circleDiameterNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void circleXNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void circleYNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void circleDiameterNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void circleSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region ImageZoomingAndSaving

        private void shownImageSelectCLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageDisplay.Image = createModifiedImage();

            // 表示されるときとされないときがある
            imageForm.Refresh();
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            imageForm.MultipleZoomMagnification(2.0);

            validateZoomMagnification();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            imageForm.MultipleZoomMagnification(0.5);

            validateZoomMagnification();
        }

        private void imageSaveButton_Click(object sender, EventArgs e)
        {
            string directory, fileName;
            string filePath = this.filePathTextBox.Text;
            try
            {
                directory = Path.GetDirectoryName(filePath);
                fileName = Path.GetFileName(filePath);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("無効なファイルパスです。", "エラー");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = fileName;
            sfd.InitialDirectory = directory;
            sfd.Filter = "BMPファイル(*.bmp)|*.bmp|EXIFファイル(*.exif)|*.exif|GIFファイル(*.gif)|*.gif|JPEGファイル(*.jpg)|*.jpg|PNGファイル(*.png)|*.png|TIFFファイル(*.tiff)|*.tiff|すべてのファイル(*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "保存先を選択してください";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(sfd.FileName);
                ImageCodecInfo codec;
                if (extension == ".bmp")
                {
                    imageDisplay.Image.Save(sfd.FileName, ImageFormat.Bmp);
                }
                else if (extension == ".exif")
                {
                    imageDisplay.Image.Save(sfd.FileName, ImageFormat.Exif);
                }
                else if (extension == ".gif")
                {
                    imageDisplay.Image.Save(sfd.FileName, ImageFormat.Gif);
                }
                else if (extension == ".jpg")
                {
                    var eps = new EncoderParameters(1);
                    var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);
                    eps.Param[0] = ep;

                    ImageCodecInfo jpgEncoder = null;
                    foreach (var ici in ImageCodecInfo.GetImageEncoders())
                    {
                        if (ici.FormatID == ImageFormat.Jpeg.Guid)
                        {
                            jpgEncoder = ici;
                            break;
                        }
                    }

                    imageDisplay.Image.Save(sfd.FileName, jpgEncoder, eps);
                }
                else if (extension == ".png")
                {
                    imageDisplay.Image.Save(sfd.FileName, ImageFormat.Png);
                }
                else if (extension == ".tiff")
                {
                    imageDisplay.Image.Save(sfd.FileName, ImageFormat.Tiff);
                }
            }

            sfd.Dispose();
        }

        #endregion

        private void imageForm_FormClosing(object sender, CancelEventArgs e)
        {
            this.tabControl.SelectedIndex = 0;

            isImageFormOpened = false;
            actionMode = FormState.ActionMode.None;
        }
    }
}
