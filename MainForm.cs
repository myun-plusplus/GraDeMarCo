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
using System.Threading;
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

            initializeRangeSelectValidation();
            initializeCircleSelectValidation();

            imageDisplay.Image = new Bitmap(targetImage);
            imageDisplay.Reset();

            isImageFormOpened = true;
            openImageForm();
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

        private void circleNumericUpDowns_ValueChanged(object sender, EventArgs e)
        {
            decimal x = this.circleXNumericUpDown.Value;
            decimal y = this.circleYNumericUpDown.Value;
            decimal d = this.circleDiameterNumericUpDown.Value;
            if (rangeSelect.StartX <= x && x <= rangeSelect.EndX &&
                rangeSelect.StartY <= y && y <= rangeSelect.EndY &&
                x + d - 1 <= rangeSelect.EndX &&
                y + d - 1 <= rangeSelect.EndY)
            {
                this.circleXNumericUpDown.BackColor = SystemColors.Window;
                this.circleYNumericUpDown.BackColor = SystemColors.Window;
                this.circleDiameterNumericUpDown.BackColor = SystemColors.Window;
            }
            else
            {
                this.circleXNumericUpDown.BackColor = Color.LightCoral;
                this.circleYNumericUpDown.BackColor = Color.LightCoral;
                this.circleDiameterNumericUpDown.BackColor = Color.LightCoral;
            }
        }

        private void circleSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (circleSelectCheckBox.Checked)
            {
                actionMode = FormState.ActionMode.CircleSelect;
                circleSelect.Start();
            }
            else
            {
                actionMode = FormState.ActionMode.None;
                circleSelect.Stop();
            }
        }

        private void circleColorSelectLabel_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.CustomColors = new int[]
            {
                0x150088, 0x241CED, 0x277FFF, 0x00F2FF, 0x4CB122, 0xE8A200, 0xCC483F, 0xA449A3,
                0x577AB9, 0xC9AEFF, 0x0EC9FF, 0xB0E4EF, 0x1DE6B5, 0xEAD999, 0xBE9270, 0xE7BFC8
            };
            cd.FullOpen = true;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.circleColorSelectLabel.BackColor = cd.Color;
                circleSelect.Pen.Color = cd.Color;
            }
        }

        #endregion

        #region DotCounting

        private void dotCountColorLabel1_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotCountColorLabel1.BackColor = colorDialog.Color;
            }
        }

        private void dotCountStartButton_Click(object sender, EventArgs e)
        {
            Bitmap image = createModifiedImage();

            var targetColors = new List<Color>
            {
                this.dotCountColorLabel1.BackColor
            };

            var results = dotCount.CountDots(image,
                rangeSelect.StartY,
                rangeSelect.StartY,
                rangeSelect.EndX - rangeSelect.StartY + 1,
                rangeSelect.EndY - rangeSelect.StartY + 1,
                targetColors);

            this.dotCountTextBox1.Text = results[0].ToString();
        }

        #endregion

        #region DotDrawing

        private void datDrawCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (datDrawCheckBox.Checked)
            {
                actionMode = FormState.ActionMode.DotDraw;
                dotDraw.Start();
            }
            else
            {
                actionMode = FormState.ActionMode.None;
                dotDraw.Stop();
            }
        }

        private void dotDrawColorLabel_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotDrawColorLabel.BackColor = this.colorDialog.Color;
                dotDraw.DotColor = this.colorDialog.Color;
            }
        }

        private void dotDrawNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            dotDraw.DotSize = (int)this.dotDrawNumericUpDown.Value;
        }

        private void dotDrawUndoButton_Click(object sender, EventArgs e)
        {
            dotDraw.UndoDrawing();
            this.imageForm.Refresh();
        }

        private void dotDrawRedoButton_Click(object sender, EventArgs e)
        {
            dotDraw.RedoDrawing();
            this.imageForm.Refresh();
        }

        #endregion

        #region ImageZoomingAndSaving

        // 連打すると表示されないことがある
        private void shownImageSelectCLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageDisplay.Image = createModifiedImage();

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
            actionMode = FormState.ActionMode.None;

            this.tabControl.SelectedIndex = 0;

            isImageFormOpened = false;
        }
    }
}
