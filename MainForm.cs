using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm : Form
    {
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!imageFormIsLoaded)
            {
                e.Cancel = true;
            }
            if (actionMode != ActionMode.None)
            {
                e.Cancel = true;
            }
        }

        #region ImageOpening

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = this.openFileDialog.FileName;
            }
        }

        private void imageOpenButton_Click(object sender, EventArgs e)
        {
            closeImageForm();
            //imageDisplay.Image = null;

            String filePath = this.filePathTextBox.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("選択したファイルが存在しません。", "エラー");
                return;
            }

            Bitmap tmp = null;

            try
            {
                tmp = new Bitmap(filePath);
                imageData.OriginalImage = tmp.Clone(new Rectangle(0, 0, tmp.Width, tmp.Height), PixelFormat.Format24bppRgb);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("選択したファイルは画像ファイルではありません。", "エラー");
                return;
            }
            finally
            {
                if (tmp != null)
                {
                    tmp.Dispose();
                }
            }

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
                actionMode = ActionMode.ImageRangeSelect;
                rangeSelect.Start();
            }
            else
            {
                actionMode = ActionMode.None;
                rangeSelect.Stop();
            }

            this.imageForm.Refresh();
        }

        #endregion

        #region CircleSelecting

        private void circleNumericUpDowns_ValueChanged(object sender, EventArgs e)
        {
            decimal x = this.circleXNumericUpDown.Value;
            decimal y = this.circleYNumericUpDown.Value;
            decimal d = this.circleDiameterNumericUpDown.Value;
            if (imageRange.LowerX <= x && x <= imageRange.UpperX &&
                imageRange.LowerY <= y && y <= imageRange.UpperY &&
                x + d - 1 <= imageRange.UpperX &&
                y + d - 1 <= imageRange.UpperY)
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
                actionMode = ActionMode.CircleSelect;
                circleSelect.Start();
            }
            else
            {
                actionMode = ActionMode.None;
                circleSelect.Stop();
            }

            this.imageForm.Refresh();
        }

        private void circleColorSelectLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.circleColorSelectLabel.BackColor = colorDialog.Color;
            }
        }

        #endregion

        #region ImageFiltering

        private void imageFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (imageFilterCheckBox.Checked)
            {
                actionMode = ActionMode.ImageFilter;
            }
            else
            {
                actionMode = ActionMode.None;
            }
            imageFilter.Filter();

            this.imageForm.Refresh();
        }

        private void filterOptionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            imageFilter.Filter();

            if (imageFormIsLoaded)
            {
                this.imageForm.Refresh();
            }
        }

        #endregion

        #region ImageBinarization

        private void binarizationThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            int tmp = binarizationThresholdTrackBar.Value;
            binarizationThresholdNumericUpDown.Value = tmp;
            binarizationThresholdTrackBar.Value = tmp;
        }

        private void binarizationThresholdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal tmp = binarizationThresholdNumericUpDown.Value;
            binarizationThresholdTrackBar.Value = (int)tmp;
            binarizationThresholdNumericUpDown.Value = tmp;

            imageBinarize.BinarizationThreshold = (int)tmp;
            this.imageForm.Refresh();
        }

        private void monochromeInvertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (monochromeInvertCheckBox.Checked)
            {
                binarizeOptions.MonochromeInverts = true;
            }
            else
            {
                binarizeOptions.MonochromeInverts = false;
            }
            imageBinarize.Binarize();

            this.imageForm.Refresh();
        }

        private void binarizationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (binarizationCheckBox.Checked)
            {
                actionMode = ActionMode.ImageBinarize;
            }
            else
            {
                actionMode = ActionMode.None;
            }

            this.imageForm.Refresh();
        }

        #endregion

        #region GrainDetecting

        private void detectInCircleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            grainDetect.DetectsGrainInCircle = detectInCircleCheckBox.Checked;

            if (detectInCircleCheckBox.Checked)
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.detectInCircleCheckBox.BackColor = colorDialog.Color;
                    grainDetect.DotColorInCircle = colorDialog.Color;
                }
            }
        }

        private void detectOnCircleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            grainDetect.DetectsGrainOnCircle = detectOnCircleCheckBox.Checked;

            if (detectOnCircleCheckBox.Checked)
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.detectOnCircleCheckBox.BackColor = colorDialog.Color;
                    grainDetect.DotColorOnCircle = colorDialog.Color;
                }
            }
        }

        private void detectInCircleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            grainDetect.DotSizeInCircle = (int)this.dotSizeInCircleNumericUpDown.Value;
        }

        private void detectOnCircleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            grainDetect.DotSizeOnCircle = (int)this.dotSizeOnCircleNumericUpDown.Value;
        }

        private void whitePixelMinimumNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            grainDetect.MinWhitePixel = (int)this.whitePixelMinimumNumericUpDown.Value;
        }

        private void dotDetectButton_Click(object sender, EventArgs e)
        {
            Bitmap circleImage = imageData.OriginalImage.Clone(
                new Rectangle(0, 0, imageData.OriginalImage.Width, imageData.OriginalImage.Height),
                PixelFormat.Format24bppRgb);
            circleSelect.DrawOnBitmap(circleImage);
            imageData.CircleImage = circleImage;

            Bitmap binarizedImage = imageData.OriginalImage.Clone(
                new Rectangle(0, 0, imageData.OriginalImage.Width, imageData.OriginalImage.Height),
                PixelFormat.Format24bppRgb);
            imageBinarize.DrawOnBitmap(binarizedImage);
            imageData.BinarizedImage = binarizedImage;

            grainDetect.Detect();
        }

        #endregion

        #region DotDrawing

        private void dotDrawCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dotDrawCheckBox.Checked)
            {
                actionMode = ActionMode.DotDraw;
            }
            else
            {
                actionMode = ActionMode.None;
            }
        }

        private void dotDrawColorLabel_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotDrawColorLabel.BackColor = this.colorDialog.Color;
            }
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

        private void clearAllDotsButton_Click(object sender, EventArgs e)
        {
            dotDraw.ClearAllDots();
            this.imageForm.Refresh();
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

        private void dotCountColorLabel2_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotCountColorLabel2.BackColor = colorDialog.Color;
            }
        }

        private void dotCountColorLabel3_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotCountColorLabel3.BackColor = colorDialog.Color;
            }
        }

        private void dotCountColorLabel4_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotCountColorLabel4.BackColor = colorDialog.Color;
            }
        }

        private void dotCountStartButton_Click(object sender, EventArgs e)
        {
            dotCount.TargetColors = new List<Color>
            {
                this.dotCountColorLabel1.BackColor,
                this.dotCountColorLabel2.BackColor,
                this.dotCountColorLabel3.BackColor,
                this.dotCountColorLabel4.BackColor
            };
            dotCount.IsCounted = new List<bool>
            {
                this.dotCountCheckBox1.Checked,
                this.dotCountCheckBox2.Checked,
                this.dotCountCheckBox3.Checked,
                this.dotCountCheckBox4.Checked
            };

            var results = dotCount.CountDots();

            this.dotCountTextBox1.Text = results[0].ToString();
            this.dotCountTextBox2.Text = results[1].ToString();
            this.dotCountTextBox3.Text = results[2].ToString();
            this.dotCountTextBox4.Text = results[3].ToString();
        }

        #endregion

        #region ImageZoomingAndSaving

        // 連打すると表示されないことがある
        private void shownImageSelectCLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var flags = ImageModifyingFlags.None;
            if (this.shownImageSelectCLB.GetItemChecked(0))
            {
                flags |= ImageModifyingFlags.ImageRange;
            }
            if (this.shownImageSelectCLB.GetItemChecked(1))
            {
                flags |= ImageModifyingFlags.Circle;
            }
            if (this.shownImageSelectCLB.GetItemChecked(2))
            {
                flags |= ImageModifyingFlags.Binarization;
            }
            if (this.shownImageSelectCLB.GetItemChecked(3))
            {
                flags |= ImageModifyingFlags.DrawnDots;
            }

            imageData.ShownImage = createModifiedImage(flags);

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

            this.saveFileDialog.FileName = fileName;
            this.saveFileDialog.InitialDirectory = directory;

            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(this.saveFileDialog.FileName);
                if (extension == ".bmp")
                {
                    imageData.ShownImage.Save(this.saveFileDialog.FileName, ImageFormat.Bmp);
                }
                else if (extension == ".exif")
                {
                    imageData.ShownImage.Save(this.saveFileDialog.FileName, ImageFormat.Exif);
                }
                else if (extension == ".gif")
                {
                    imageData.ShownImage.Save(this.saveFileDialog.FileName, ImageFormat.Gif);
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

                    imageData.ShownImage.Save(this.saveFileDialog.FileName, jpgEncoder, eps);
                }
                else if (extension == ".png")
                {
                    imageData.ShownImage.Save(this.saveFileDialog.FileName, ImageFormat.Png);
                }
                else if (extension == ".tiff")
                {
                    imageData.ShownImage.Save(this.saveFileDialog.FileName, ImageFormat.Tiff);
                }
            }
        }

        #endregion

        private void imageForm_FormClosing(object sender, CancelEventArgs e)
        {
            actionMode = ActionMode.None;

            this.tabControl.SelectedIndex = 0;
            for (int i = 0; i < this.shownImageSelectCLB.Items.Count; ++i)
            {
                this.shownImageSelectCLB.SetItemChecked(i, false);
            }

            imageFormIsLoaded = false;
        }

        private void imageForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0 && this.zoomInButton.Enabled)
                {
                    imageForm.MultipleZoomMagnification(2.0);

                    validateZoomMagnification();
                }
                else if (e.Delta < 0 && this.zoomOutButton.Enabled)
                {
                    imageForm.MultipleZoomMagnification(0.5);

                    validateZoomMagnification();
                }
            }
        }
    }
}
