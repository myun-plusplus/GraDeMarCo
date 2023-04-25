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
        private MainController controller;

        public MainForm()
        {
            InitializeComponent();

            controller = new MainController(this);

#if DEBUG
            this.filePathTextBox.Text = @"D:\Projects\GrainDetector\sample2.jpg";
#endif
        }

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
            controller.OpenImageFile(this.filePathTextBox.Text);
            controller.OpenImageForm();
        }

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

        private void shownImageSelectCLB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void zoomInButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void zoomOutButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void imageSaveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
