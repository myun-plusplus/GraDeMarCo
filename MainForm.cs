using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            imageDisplay = new ImageDisplay();

            #if DEBUG
            this.filePathTextBox.Text = @"D:\Projects\GrainDetector\sample2.jpg";
            #endif
        }

        #region ImageOpening

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "画像ファイル(*.jpg;*.png;*.bmp;*.gif;*.exif;*.tiff)|*.jpg;*.png;*.bmp;*.gif;*.exif;*.tiff|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = ofd.FileName;
            }
        }

        private void imageOpenButton_Click(object sender, EventArgs e)
        {
            String filename = this.filePathTextBox.Text;
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

            closeImageForm();
            openImageForm();
        }

        #endregion

        #region RangeSelecting

        private void lowerXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void upperXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lowerYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void upperYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lowerXNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void upperXNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void lowerYNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void upperYNumericUpDown_Validating(object sender, CancelEventArgs e)
        {

        }

        private void rangeSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {

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

        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (isImageFormOpen())
            {
                imageForm.MultipleZoomMagnification(2.0);
            }
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (isImageFormOpen())
            {
                imageForm.MultipleZoomMagnification(0.5);
            }
        }

        private void imageSaveButton_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
