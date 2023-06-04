using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm : Form
    {
        #region Menustrip

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeImageForm();

            setInitialParameters();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace workspace = new Workspace();
            workspace.ImageOpenOptions = imageOpenOptions;
            workspace.ImageRange = imageRange;
            workspace.Circle = circle;
            workspace.FilterOptions = filterOptions;
            workspace.BinarizeOptions = binarizeOptions;
            workspace.GrainDetectOptions = grainDetectOptions;
            workspace.DotInCircleTool = dotInCircleTool;
            workspace.DotOnCircleTool = dotOnCircleTool;
            workspace.DotDrawTool = dotDrawTool;
            workspace.DrawnDotsData = drawnDotsData;

            this.openImageFileDialog.FileName = "";
            if (this.openImageFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            imageOpenOptions.ImageFilePath = this.openImageFileDialog.FileName;

            try
            {
                workspace.Load(imageOpenOptions.ImageFilePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "エラー");
                return;
            }

            this.dotCountListView.Items.Clear();
            this.dotCountListView.Items.AddRange(
                Enumerable.Range(0, workspace.CountedColors.Count)
                .Select(_ => new ListViewItem(new string[] { "", "0" }))
                .ToArray());
            this.dotCountListView.Items.Cast<ListViewItem>()
                .Zip(workspace.CountedColors, (lvi, color) => lvi.SubItems[0].BackColor = color)
                .ToList();

            closeImageForm();

            openImageFile(imageOpenOptions.ImageFilePath);
            openImageForm();
        }

        private void overwriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var workspace = new Workspace();
            workspace.ImageOpenOptions = imageOpenOptions;
            workspace.ImageRange = imageRange;
            workspace.Circle = circle;
            workspace.FilterOptions = filterOptions;
            workspace.BinarizeOptions = binarizeOptions;
            workspace.GrainDetectOptions = grainDetectOptions;
            workspace.DotInCircleTool = dotInCircleTool;
            workspace.DotOnCircleTool = dotOnCircleTool;
            workspace.DotDrawTool = dotDrawTool;
            workspace.DrawnDotsData = drawnDotsData;

            workspace.CountedColors = this.dotCountListView.Items.Cast<ListViewItem>()
                    .Select(lvi => lvi.SubItems[0].BackColor)
                    .ToList();

            try
            {
                workspace.Save(this.saveImageFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImageFileDialog.FileName = Path.GetFileNameWithoutExtension(imageOpenOptions.ImageFilePath) + ".dat";
            if (this.saveImageFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var workspace = new Workspace();
            workspace.ImageOpenOptions = imageOpenOptions;
            workspace.ImageRange = imageRange;
            workspace.Circle = circle;
            workspace.FilterOptions = filterOptions;
            workspace.BinarizeOptions = binarizeOptions;
            workspace.GrainDetectOptions = grainDetectOptions;
            workspace.DotInCircleTool = dotInCircleTool;
            workspace.DotOnCircleTool = dotOnCircleTool;
            workspace.DotDrawTool = dotDrawTool;
            workspace.DrawnDotsData = drawnDotsData;

            workspace.CountedColors = this.dotCountListView.Items.Cast<ListViewItem>()
                    .Select(lvi => lvi.SubItems[0].BackColor)
                    .ToList();

            try
            {
                workspace.Save(this.saveImageFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

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
            if (this.openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageOpenOptions.ImageFilePath = this.openImageFileDialog.FileName;
            }
        }

        private void imageOpenButton_Click(object sender, EventArgs e)
        {
            closeImageForm();

            openImageFile(imageOpenOptions.ImageFilePath);
            openImageForm();

            initializeRangeSelect();
            initializeCircleSelect();
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

        private void filterOptionBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            imageFilter.Filter();

            if (imageFormIsLoaded)
            {
                this.imageForm.Refresh();
            }
        }

        #endregion

        #region ImageBinarization

        private void binarizeOptionBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            imageBinarize.Binarize();

            if (_isImageFormOpened)
            {
                this.imageForm.Refresh();
            }
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

        private void dotColorInCircleLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotColorInCircleLabel.BackColor = colorDialog.Color;
            }
        }

        private void dotColorOnCircleLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotColorOnCircleLabel.BackColor = colorDialog.Color;
            }
        }

        private void dotDetectButton_Click(object sender, EventArgs e)
        {
            Bitmap circleImage = imageData.OriginalImage.Clone(
                new Rectangle(0, 0, imageData.OriginalImage.Width, imageData.OriginalImage.Height),
                PixelFormat.Format24bppRgb);
            circleSelect.DrawOnBitmap(circleImage);
            imageData.CircleImage = circleImage;

            imageBinarize.Binarize();

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

        private void dotCountStartButton_Click(object sender, EventArgs e)
        {
            var counts = dotCount.CountDots(
                this.dotCountListView.Items.Cast<ListViewItem>()
                .Select(lvi => lvi.SubItems[0].BackColor)
                .ToList());

            for (int i = 0; i < this.dotCountListView.Items.Count; ++i)
            {
                this.dotCountListView.Items[i].SubItems[1].Text = counts[i].ToString();
            }
        }

        private void dotCountListView_Click(object sender, EventArgs e)
        {
            Point location = this.dotCountListView.PointToClient(Control.MousePosition);
            var hitTestInfo = this.dotCountListView.HitTest(location);
            int row = hitTestInfo.Item.Index;
            int col = hitTestInfo.Item.SubItems.IndexOf(hitTestInfo.SubItem);

            if (col == 0 && this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.dotCountListView.Items[row].SubItems[0].BackColor = this.colorDialog.Color;
            }
        }

        private void dotCountListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                var lvis = this.dotCountListView.SelectedItems;
                string text = string.Join(
                    "\n",
                    lvis.Cast<ListViewItem>()
                    .Select(lvi => lvi.SubItems[1].Text)
                    .ToArray());
                Clipboard.SetText(text);
            }
        }

        private void addDotCountButton_Click(object sender, EventArgs e)
        {
            var lvi = new ListViewItem();

            // BackColorの効果を指定したSubItemに限定するため
            lvi.UseItemStyleForSubItems = false;

            var lvsi = new ListViewItem.ListViewSubItem(lvi, "0");
            lvi.SubItems.Add(lvsi);

            this.dotCountListView.Items.Add(lvi);
        }

        private void deleteDotCountButton_Click(object sender, EventArgs e)
        {
            var lvis = this.dotCountListView.SelectedItems;
            foreach (var lvi in lvis)
            {
                this.dotCountListView.Items.Remove((ListViewItem)lvi);
            }
        }

        private void dotCountListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.dotCountListView.Columns[e.ColumnIndex].Width;
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

            this.saveImageFileDialog.FileName = fileName;
            this.saveImageFileDialog.InitialDirectory = directory;

            if (this.saveImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(this.saveImageFileDialog.FileName);
                if (extension == ".bmp")
                {
                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Bmp);
                }
                else if (extension == ".exif")
                {
                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Exif);
                }
                else if (extension == ".gif")
                {
                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Gif);
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

                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, jpgEncoder, eps);
                }
                else if (extension == ".png")
                {
                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Png);
                }
                else if (extension == ".tiff")
                {
                    imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Tiff);
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
