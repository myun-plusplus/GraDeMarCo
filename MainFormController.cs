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

        private ImageData imageData;

        private ImageDisplay imageDisplay;
        private ImageRange imageRange;
        private PlanimetricCircle circle;
        private FilterOptions filterOptions;
        private BinarizeOptions binarizeOptions;
        private DotDrawTool dotDrawTool;
        private DrawnDotsData drawnDotsData;

        private RangeSelect rangeSelect;
        private CircleSelect circleSelect;
        private ImageFilter imageFilter;
        private ImageBinarize imageBinarize;
        private GrainDetect grainDetect;
        private DotDraw dotDraw;
        private DotCount dotCount;

        private bool _isImageFormOpened;
        private bool imageFormIsLoaded
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
                if (imageFormIsLoaded)
                {
                    this.imageForm.ActionMode = value;
                }
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

            imageData = new ImageData();
            imageData.OriginalImage = new Bitmap(1280, 1024, PixelFormat.Format24bppRgb);

            imageDisplay = new ImageDisplay(imageData);
            imageDisplay.Initialize();
            imageRange = new ImageRange();
            circle = new PlanimetricCircle();
            filterOptions = new FilterOptions();
            binarizeOptions = new BinarizeOptions();
            dotDrawTool = new DotDrawTool();
            drawnDotsData = new DrawnDotsData();

            rangeSelect = new RangeSelect(imageDisplay, imageRange);
            circleSelect = new CircleSelect(imageDisplay, circle);
            imageFilter = new ImageFilter(imageData, imageDisplay, imageRange, filterOptions);
            imageBinarize = new ImageBinarize(imageData, imageDisplay, imageRange, binarizeOptions);
            // GrainDetectに渡すため、初期化順が逆
            dotDraw = new DotDraw(imageDisplay, dotDrawTool, drawnDotsData);
            grainDetect = new GrainDetect(imageData, imageDisplay, imageRange, circle, dotDraw);
            dotCount = new DotCount(imageData, imageRange);

            InitializeComponent();
            this.imageRangeBindingSource.DataSource = imageRange;
            this.planimetricCircleBindingSource.DataSource = circle;

            imageFormIsLoaded = false;
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
            circle.Color = Color.Blue;

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

            this.blurComboBox.SelectedIndex = 0;
            this.edgeDetectComboBox.SelectedIndex = 0;

            this.dotDrawColorLabel.BackColor = Color.Red;
            dotDrawTool.Color = Color.Red;
            dotDrawTool.Size = (int)this.dotDrawNumericUpDown.Value;

            this.dotCountColorLabel1.BackColor = Color.Red;
            this.dotCountColorLabel2.BackColor = Color.Yellow;
        }

        private void openImageForm()
        {
            imageFormIsLoaded = true;

            this.imageForm = new ImageForm(imageData, imageDisplay, rangeSelect, circleSelect, imageFilter, imageBinarize, dotDraw);
            this.imageForm.Location = new Point(this.Location.X + 320, this.Location.Y);
            this.imageForm.ActionMode = ActionMode.None;
            this.imageForm.FormClosing += imageForm_FormClosing;
            this.imageForm.ChangeZoomMagnification(1.0);

            this.imageForm.Show();

            initializeRangeSelect();
            initializeCircleSelect();
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

            if (imageFormIsLoaded)
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
            this.lowerXNumericUpDown.Maximum = imageData.OriginalImage.Width - 1;
            this.upperXNumericUpDown.Maximum = imageData.OriginalImage.Width - 1;
            this.lowerYNumericUpDown.Maximum = imageData.OriginalImage.Height - 1;
            this.upperYNumericUpDown.Maximum = imageData.OriginalImage.Height - 1;

            imageRange.LowerX = 0;
            imageRange.UpperX = imageData.OriginalImage.Width - 1;
            imageRange.LowerY = 0;
            imageRange.UpperY = imageData.OriginalImage.Height - 1;
        }

        private void initializeCircleSelect()
        {
            this.circleXNumericUpDown.Maximum = imageData.OriginalImage.Width - 1;
            this.circleYNumericUpDown.Maximum = imageData.OriginalImage.Height - 1;
            this.circleDiameterNumericUpDown.Maximum = Math.Min(imageData.OriginalImage.Width, imageData.OriginalImage.Width) - 1;

            circle.LowerX = 0;
            circle.LowerY = 0;
            circle.Diameter = Math.Min(imageData.OriginalImage.Width, imageData.OriginalImage.Width);
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
            ImageRange = 1,
            Circle = 2,
            Binarization = 4,
            DrawnDots = 8
        }

        private Bitmap createModifiedImage(ImageModifyingFlags flags)
        {
            Bitmap image = imageData.OriginalImage.Clone(
                new Rectangle(0, 0, imageData.OriginalImage.Width, imageData.OriginalImage.Height),
                PixelFormat.Format24bppRgb);

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
