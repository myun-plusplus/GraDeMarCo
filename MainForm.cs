﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GraDeMarCo
{
    public partial class MainForm : Form
    {
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("作業内容を保存しますか。", Application.ProductName, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveAsWorkspaceToolStripMenuItem_Click(this, new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (actionMode == ActionMode.DotDraw)
            {
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    dotDraw.UndoDrawing();
                    this.imageForm.Refresh();
                }
                else if (e.Control && e.KeyCode == Keys.Y)
                {
                    dotDraw.RedoDrawing();
                    this.imageForm.Refresh();
                }
            }
        }

        #region Menustrip

        private void newWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("作業内容を保存しますか。", Application.ProductName, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveAsWorkspaceToolStripMenuItem_Click(this, new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

            closeImageForm();

            startNewWorkspace();
        }

        private void openWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("作業内容を保存しますか。", Application.ProductName, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveAsWorkspaceToolStripMenuItem_Click(this, new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

            closeImageForm();

            openWorkspace();

            openImageFile(imageOpenOptions.ImageFilePath);

            openImageForm();
        }

        private void overwriteWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isWorkspaceSaved)
            {
                return;
            }

            saveWorkspace();
        }

        private void saveAsWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveWorkspaceDialog.FileName = Path.GetFileNameWithoutExtension(imageOpenOptions.ImageFilePath) + ".dat";
            if (this.saveWorkspaceDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            saveWorkspace();
        }

        private void exitWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imageOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageOpenOptions.ImageFilePath = this.openImageFileDialog.FileName;

                closeImageForm();

                openImageFile(imageOpenOptions.ImageFilePath);

                openImageForm();
            }
        }

        private void imageSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImageFile(imageOpenOptions.ImageFilePath);
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDisplay.ZoomMagnification *= 2;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDisplay.ZoomMagnification *= 0.5;
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
        }

        #endregion

        #region RangeSelecting

        private void lowerXNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (this.lowerXNumericUpDown.Value >= imageData.ShownImage.Width)
            {
                e.Cancel = true;
            }
        }

        private void upperXNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (this.upperXNumericUpDown.Value >= imageData.ShownImage.Width)
            {
                e.Cancel = true;
            }
        }

        private void lowerYNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (this.lowerYNumericUpDown.Value >= imageData.ShownImage.Height)
            {
                e.Cancel = true;
            }
        }

        private void upperYNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (this.upperYNumericUpDown.Value >= imageData.ShownImage.Height)
            {
                e.Cancel = true;
            }
        }

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

        private void wholeSlectButton_Click(object sender, EventArgs e)
        {
            imageRange.LowerX = 0;
            imageRange.UpperX = imageData.OriginalImage.Width - 1;
            imageRange.LowerY = 0;
            imageRange.UpperY = imageData.OriginalImage.Height - 1;
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

        private void largestCircleSelectButton_Click(object sender, EventArgs e)
        {
            int centerX = (imageRange.LowerX + imageRange.UpperX) / 2;
            int centerY = (imageRange.LowerY + imageRange.UpperY) / 2;
            int radius = Math.Min(imageRange.UpperX - imageRange.LowerX, imageRange.UpperY - imageRange.LowerY) / 2;
            circle.LowerX = centerX - radius;
            circle.LowerY = centerY - radius;
            circle.Diameter = 2 * radius;
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
            if (actionMode == ActionMode.ImageFilter)
            {
                imageFilter.Filter();

                if (imageFormIsLoaded)
                {
                    this.imageForm.Refresh();
                }
            }
        }

        #endregion

        #region ImageBinarization

        private void binarizeOptionBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (actionMode == ActionMode.ImageBinarize)
            {
                imageBinarize.Binarize();

                if (_isImageFormOpened)
                {
                    this.imageForm.Refresh();
                }
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

            imageBinarize.Binarize();

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
            this.dotCountListView.Items.Add(getListViewItem());
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
            this.Enabled = false;

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

            this.Enabled = true;
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            imageDisplay.ZoomMagnification *= 2;
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            imageDisplay.ZoomMagnification *= 0.5;
        }

        private void imageSaveButton_Click(object sender, EventArgs e)
        {
            saveImageFile(imageOpenOptions.ImageFilePath);
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

        private void MainForm_ZoomMagnificationChanged(object sender, EventArgs e)
        {
            this.zoomInButton.Enabled = imageDisplay.CanZoomIn();
            this.zoomOutButton.Enabled = imageDisplay.CanZoomOut();
        }
    }
}
