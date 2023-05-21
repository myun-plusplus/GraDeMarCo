using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm
    {
        private ImageForm imageForm;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private ColorDialog colorDialog;

        private ImageDisplay imageDisplay;
        private ImageRange imageRange;

        private RangeSelect rangeSelect;
        private CircleSelect circleSelect;
        private ImageFilter imageFilter;
        private ImageBinarize imageBinarize;
        private GrainDetect grainDetect;
        private DotDraw dotDraw;
        private DotCount dotCount;

        private bool _isImageFormOpened;
        private bool isImageFormOpened
        {
            get
            {
                return _isImageFormOpened;
            }
            set
            {
                _isImageFormOpened = value;
                turnOnOffControls();
            }
        }

        private ActionMode _actionMode;
        private ActionMode actionMode
        {
            get
            {
                return _actionMode;
            }
            set
            {
                _actionMode = value;
                turnOnOffControls();
                if (isImageFormOpened)
                {
                    this.imageForm.ActionMode = value;
                }
            }
        }

        private Bitmap _originalImage;
        private Bitmap originalImage
        {
            get
            { 
                return _originalImage;
            }
            set
            {
                if (_originalImage != null && value != _originalImage)
                {
                    _originalImage.Dispose();
                }
                _originalImage = value;
            }
        }

        public MainForm()
        {
            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.Filter = "画像ファイル(*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff)|*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff|すべてのファイル(*.*)|*.*";
            this.openFileDialog.FilterIndex = 1;
            this.openFileDialog.Title = "開くファイルを選択してください";
            this.openFileDialog.RestoreDirectory = true;

            this.saveFileDialog = new SaveFileDialog();
            this.saveFileDialog.Filter = "BMPファイル(*.bmp)|*.bmp|EXIFファイル(*.exif)|*.exif|GIFファイル(*.gif)|*.gif|JPEGファイル(*.jpg)|*.jpg|PNGファイル(*.png)|*.png|TIFFファイル(*.tiff)|*.tiff|すべてのファイル(*.*)|*.*";
            this.saveFileDialog.FilterIndex = 0;
            this.saveFileDialog.Title = "保存先を選択してください";
            this.saveFileDialog.RestoreDirectory = true;

            this.colorDialog = new ColorDialog();
            this.colorDialog.CustomColors = new int[] {
                0x150088, 0x241CED, 0x277FFF, 0x00F2FF, 0x4CB122, 0xE8A200, 0xCC483F, 0xA449A3,
                0x577AB9, 0xC9AEFF, 0x0EC9FF, 0xB0E4EF, 0x1DE6B5, 0xEAD999, 0xBE9270, 0xE7BFC8 };
            this.colorDialog.FullOpen = true;

            imageDisplay = new ImageDisplay();
            imageDisplay.Initialize();
            imageRange = new ImageRange();

            rangeSelect = new RangeSelect(imageDisplay, imageRange);
            circleSelect = new CircleSelect(imageDisplay);
            imageFilter = new ImageFilter(imageDisplay, imageRange);
            imageBinarize = new ImageBinarize(imageDisplay, imageRange);
            // GrainDetectに渡すため、初期化順が逆
            dotDraw = new DotDraw(imageDisplay);
            grainDetect = new GrainDetect(imageDisplay, imageRange, circleSelect, dotDraw);
            dotCount = new DotCount(imageRange);

            InitializeComponent();
            this.imageRangeBindingSource.DataSource = imageRange;
            this.circleSelectBindingSource.DataSource = circleSelect;

            isImageFormOpened = false;
            actionMode = ActionMode.None;

            setInitialParameters();
        }

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (this.imageForm != null && !this.imageForm.IsDisposed)
            {
                this.imageForm.Dispose();
            }
            this.imageForm = null;
            if (this.openFileDialog != null)
            {
                this.openFileDialog.Dispose();
            }
            this.openFileDialog = null;
            if (this.saveFileDialog != null)
            {
                this.saveFileDialog.Dispose();
            }
            this.saveFileDialog = null;
            if (this.colorDialog != null)
            {
                this.colorDialog.Dispose();
            }
            this.colorDialog = null;

            originalImage = null;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void setInitialParameters()
        {
#if DEBUG
            this.filePathTextBox.Text = @"D:\Projects\GrainDetector\sample3.bmp";
#endif

            this.circleColorSelectLabel.BackColor = Color.Blue;
            circleSelect.CircleColor = Color.Blue;

            imageBinarize.BinarizationThreshold = (int)this.binarizationThresholdNumericUpDown.Value;

            grainDetect.MinWhitePixel = (int)this.whitePixelMinimumNumericUpDown.Value;
            grainDetect.DetectsGrainInCircle = this.detectInCircleCheckBox.Checked;
            grainDetect.DetectsGrainOnCircle = this.detectOnCircleCheckBox.Checked;
            this.detectInCircleCheckBox.BackColor = Color.Red;
            grainDetect.DotColorInCircle = Color.Red;
            grainDetect.DotSizeInCircle = (int)this.dotSizeInCircleNumericUpDown.Value;
            this.detectOnCircleCheckBox.BackColor = Color.Yellow;
            grainDetect.DotColorOnCircle = Color.Yellow;
            grainDetect.DotSizeOnCircle = (int)this.dotSizeOnCircleNumericUpDown.Value;

            this.blurCcomboBox.SelectedIndex = 0;
            this.edgeDetectComboBox.SelectedIndex = 0;

            this.dotDrawColorLabel.BackColor = Color.Red;
            dotDraw.DotColor = Color.Red;
            dotDraw.DotSize = (int)this.dotDrawNumericUpDown.Value;

            this.dotCountColorLabel1.BackColor = Color.Red;
            this.dotCountColorLabel2.BackColor = Color.Yellow;
        }

        private void openImageForm()
        {
            initializeRangeSelect();
            initializeCircleSelect();

            imageDisplay.Image = new Bitmap(originalImage);
            imageDisplay.Initialize();

            imageFilter.OriginalImage = originalImage;
            imageBinarize.OriginalImage = imageFilter.FilteredImage;

            this.imageForm = new ImageForm(imageDisplay, rangeSelect, circleSelect, imageFilter, imageBinarize, dotDraw);
            this.imageForm.Location = new Point(this.Location.X + 320, this.Location.Y);
            this.imageForm.ActionMode = ActionMode.None;
            this.imageForm.FormClosing += imageForm_FormClosing;
            this.imageForm.MouseWheel += imageForm_MouseWheel;

            this.imageForm.Show();
        }

        private void closeImageForm()
        {
            if (this.imageForm != null && !this.imageForm.IsDisposed)
            {
                this.imageForm.Hide();
                this.imageForm.Close();
            }
            this.imageForm = null;
        }

        private void turnOnOffControls()
        {
            this.SuspendLayout();

            if (isImageFormOpened)
            {
                if (actionMode == ActionMode.None)
                {
                    this.rangeSelectPanel.Enabled = true;
                    this.circleSelectPanel.Enabled = true;
                    //this.
                    this.grainDetectPanel.Enabled = true;
                    this.dotDrawPanel.Enabled = true;
                    this.dotCountPanel.Enabled = true;
                    this.shownImageSelectCLB.Enabled = true;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = true;
                }
                else if (actionMode == ActionMode.ImageRangeSelect)
                {
                    this.rangeSelectPanel.Enabled = true;
                    this.circleSelectPanel.Enabled = false;
                    this.grainDetectPanel.Enabled = false;
                    this.dotDrawPanel.Enabled = false;
                    this.dotCountPanel.Enabled = false;
                    this.shownImageSelectCLB.Enabled = false;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = false;
                }
                else if (actionMode == ActionMode.CircleSelect)
                {
                    this.rangeSelectPanel.Enabled = false;
                    this.circleSelectPanel.Enabled = true;
                    this.grainDetectPanel.Enabled = false;
                    this.dotDrawPanel.Enabled = false;
                    this.dotCountPanel.Enabled = false;
                    this.shownImageSelectCLB.Enabled = false;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = false;
                }
                else if (actionMode == ActionMode.ImageFilter)
                {
                    this.rangeSelectPanel.Enabled = false;
                    this.circleSelectPanel.Enabled = false;
                    this.grainDetectPanel.Enabled = true;
                    this.dotDrawPanel.Enabled = false;
                    this.dotCountPanel.Enabled = false;
                    this.shownImageSelectCLB.Enabled = false;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = false;
                }
                else if (actionMode == ActionMode.ImageBinarize)
                {
                    this.rangeSelectPanel.Enabled = false;
                    this.circleSelectPanel.Enabled = false;
                    this.grainDetectPanel.Enabled = true;
                    this.dotDrawPanel.Enabled = false;
                    this.dotCountPanel.Enabled = false;
                    this.shownImageSelectCLB.Enabled = false;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = false;
                }
                else if (actionMode == ActionMode.DotDraw)
                {
                    this.rangeSelectPanel.Enabled = false;
                    this.circleSelectPanel.Enabled = false;
                    this.grainDetectPanel.Enabled = false;
                    this.dotDrawPanel.Enabled = true;
                    this.dotCountPanel.Enabled = false;
                    this.shownImageSelectCLB.Enabled = false;
                    this.zoomInButton.Enabled = true;
                    this.zoomOutButton.Enabled = true;
                    this.imageSaveButton.Enabled = false;
                }
            }
            else
            {
                this.rangeSelectPanel.Enabled = false;
                this.circleSelectPanel.Enabled = false;
                this.grainDetectPanel.Enabled = false;
                this.dotDrawPanel.Enabled = false;
                this.dotCountPanel.Enabled = false;
                this.shownImageSelectCLB.Enabled = false;
                this.zoomInButton.Enabled = false;
                this.zoomOutButton.Enabled = false;
                this.imageSaveButton.Enabled = false;
            }

            this.ResumeLayout(false);
        }

        private void initializeRangeSelect()
        {
            this.lowerXNumericUpDown.Maximum = originalImage.Width - 1;
            this.lowerYNumericUpDown.Maximum = originalImage.Height - 1;
            this.upperXNumericUpDown.Maximum = originalImage.Width - 1;
            this.upperYNumericUpDown.Maximum = originalImage.Height - 1;

            this.lowerXNumericUpDown.Value = 0;
            this.lowerYNumericUpDown.Value = 0;
            this.upperXNumericUpDown.Value = 0;
            this.upperYNumericUpDown.Value = 0;
        }

        private void initializeCircleSelect()
        {
            this.circleXNumericUpDown.Maximum = originalImage.Width - 1;
            this.circleYNumericUpDown.Maximum = originalImage.Height - 1;
            this.circleDiameterNumericUpDown.Maximum = Math.Min(originalImage.Width, originalImage.Width);

            this.circleXNumericUpDown.Value = 0;
            this.circleYNumericUpDown.Value = 0;
            this.circleDiameterNumericUpDown.Value = 1;
        }

        private void validateZoomMagnification()
        {
            if (imageDisplay.ZoomMagnification >= 8)
            {
                this.zoomInButton.Enabled = false;
            }
            else
            {
                this.zoomInButton.Enabled = true;
            }
            if (imageDisplay.ZoomMagnification <= 0.125)
            {
                this.zoomOutButton.Enabled = false;
            }
            else
            {
                this.zoomOutButton.Enabled = true;
            }
        }

        [Flags]
        private enum ImageModifyingFlags
        {
            None = 0,
            ImageRange = 0b00000001,
            Circle = 0b00000010,
            Binarization = 0b00000100,
            DrawnDots = 0b00001000
        }

        private Bitmap createModifiedImage(ImageModifyingFlags flags)
        {
            Bitmap image = originalImage.Clone(new Rectangle(0, 0, originalImage.Width, originalImage.Height), PixelFormat.Format24bppRgb);

            if ((flags & ImageModifyingFlags.Binarization) != ImageModifyingFlags.None)
            {
                imageFilter.DrawOnBitmap(image);
                imageBinarize.DrawOnBitmap(image);
            }
            if ((flags & ImageModifyingFlags.ImageRange) != ImageModifyingFlags.None)
            {
                rangeSelect.DrawOnBitmap(image);
            }
            if ((flags & ImageModifyingFlags.Circle) != ImageModifyingFlags.None)
            {
                circleSelect.DrawOnBitmap(image);
            }
            if ((flags & ImageModifyingFlags.DrawnDots) != ImageModifyingFlags.None)
            {
                dotDraw.DrawOnBitmap(image);
            }

            return image;
        }
    }
}
