using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GrainDetector
{
    public partial class MainForm
    {
        private ImageForm imageForm;
        private OpenFileDialog openImageFileDialog;
        private OpenFileDialog openWorkspaceDialog;
        private SaveFileDialog saveImageFileDialog;
        private SaveFileDialog saveWorkspaceDialog;
        private ColorDialog colorDialog;

        private ImageData imageData;

        private ImageDisplay imageDisplay;
        private ImageOpenOptions imageOpenOptions;
        private ImageRange imageRange;
        private PlanimetricCircle circle;
        private FilterOptions filterOptions;
        private BinarizeOptions binarizeOptions;
        private GrainDetectOptions grainDetectOptions;
        private DotDrawTool dotInCircleTool;
        private DotDrawTool dotOnCircleTool;
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

        private bool isWorkspaceSaved;

        public MainForm()
        {
            this.openImageFileDialog = new OpenFileDialog();
            this.openImageFileDialog.Filter = "画像ファイル(*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff)|*.bmp;*.exif;*.gif;*.jpg;*.png;*.tiff|すべてのファイル(*.*)|*.*";
            this.openImageFileDialog.FilterIndex = 1;
            this.openImageFileDialog.Title = "開くファイルを選択してください";
            this.openImageFileDialog.RestoreDirectory = true;

            this.openWorkspaceDialog = new OpenFileDialog();
            this.openWorkspaceDialog.Filter = "DATファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*";
            this.openWorkspaceDialog.FilterIndex = 0;
            this.openWorkspaceDialog.Title = "開くファイルを選択してください";
            this.openWorkspaceDialog.RestoreDirectory = true;

            this.saveImageFileDialog = new SaveFileDialog();
            this.saveImageFileDialog.AddExtension = true;
            this.saveImageFileDialog.Filter = "BMPファイル(*.bmp)|*.bmp|EXIFファイル(*.exif)|*.exif|GIFファイル(*.gif)|*.gif|JPEGファイル(*.jpg)|*.jpg|PNGファイル(*.png)|*.png|TIFFファイル(*.tiff)|*.tiff|すべてのファイル(*.*)|*.*";
            this.saveImageFileDialog.FilterIndex = 0;
            this.saveImageFileDialog.Title = "保存先を選択してください";
            this.saveImageFileDialog.RestoreDirectory = true;

            this.saveWorkspaceDialog = new SaveFileDialog();
            this.saveWorkspaceDialog.AddExtension = true;
            this.saveWorkspaceDialog.Filter = "DATファイル(*.dat) | *.dat | すべてのファイル(*.*) | *.* ";
            this.saveWorkspaceDialog.FilterIndex = 0;
            this.saveWorkspaceDialog.Title = "保存先を選択してください";
            this.saveWorkspaceDialog.RestoreDirectory = true;

            this.colorDialog = new ColorDialog();
            this.colorDialog.CustomColors = new int[] {
                0x150088, 0x241CED, 0x277FFF, 0x00F2FF, 0x4CB122, 0xE8A200, 0xCC483F, 0xA449A3,
                0x577AB9, 0xC9AEFF, 0x0EC9FF, 0xB0E4EF, 0x1DE6B5, 0xEAD999, 0xBE9270, 0xE7BFC8 };
            this.colorDialog.FullOpen = true;

            imageData = new ImageData();
            imageData.OriginalImage = new Bitmap(1280, 1024, PixelFormat.Format24bppRgb);

            imageDisplay = new ImageDisplay(imageData);
            imageDisplay.Initialize();
            imageOpenOptions = new ImageOpenOptions();
            imageRange = new ImageRange();
            circle = new PlanimetricCircle();
            filterOptions = new FilterOptions();
            binarizeOptions = new BinarizeOptions();
            grainDetectOptions = new GrainDetectOptions();
            dotInCircleTool = new DotDrawTool();
            dotOnCircleTool = new DotDrawTool();
            dotDrawTool = new DotDrawTool();
            drawnDotsData = new DrawnDotsData();

            rangeSelect = new RangeSelect(imageDisplay, imageRange);
            circleSelect = new CircleSelect(imageDisplay, circle);
            imageFilter = new ImageFilter(imageData, imageDisplay, imageRange, filterOptions);
            imageBinarize = new ImageBinarize(imageData, imageDisplay, imageRange, binarizeOptions);
            // GrainDetectに渡すため、初期化順が逆
            dotDraw = new DotDraw(imageDisplay, dotDrawTool, drawnDotsData);
            grainDetect = new GrainDetect(imageData, imageRange, circle, grainDetectOptions, dotInCircleTool, dotOnCircleTool, dotDraw);
            dotCount = new DotCount(imageData, imageRange);

            InitializeComponent();

            this.newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            this.overwriteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            this.saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;

            this.blurComboBox.DataSource = Enum.GetValues(typeof(BlurOption)).Cast<BlurOption>()
                .Select(i => Tuple.Create(i, i.GetType().GetField(i.ToString()).GetCustomAttribute<DisplayAttribute>().Name))
                .ToList();
            this.blurComboBox.ValueMember = "Item1";
            this.blurComboBox.DisplayMember = "Item2";

            this.edgeDetectComboBox.DataSource = Enum.GetValues(typeof(EdgeDetectOption)).Cast<EdgeDetectOption>()
                .Select(i => Tuple.Create(i, i.GetType().GetField(i.ToString()).GetCustomAttribute<DisplayAttribute>().Name))
                .ToList();
            this.edgeDetectComboBox.ValueMember = "Item1";
            this.edgeDetectComboBox.DisplayMember = "Item2";

            this.circleColorSelectLabel.DataBindings.Add(new Binding("BackColor", this.planimetricCircleBindingSource, "Color", true, DataSourceUpdateMode.OnPropertyChanged));
            this.blurComboBox.DataBindings.Add(new Binding("SelectedValue", this.filterOptionBindingSource, "ApplysBlur", true, DataSourceUpdateMode.OnPropertyChanged));
            this.edgeDetectComboBox.DataBindings.Add(new Binding("SelectedValue", this.filterOptionBindingSource, "DetectsEdge", true, DataSourceUpdateMode.OnPropertyChanged));
            this.dotColorInCircleLabel.DataBindings.Add(new Binding("BackColor", this.dotInCircleToolBindingSource, "Color", true, DataSourceUpdateMode.OnPropertyChanged));
            this.dotColorOnCircleLabel.DataBindings.Add(new Binding("BackColor", this.dotOnCircleToolBindingSource, "Color", true, DataSourceUpdateMode.OnPropertyChanged));
            this.dotDrawColorLabel.DataBindings.Add(new Binding("BackColor", this.dotDrawToolBindingSource, "Color", true, DataSourceUpdateMode.OnPropertyChanged));

            this.imageOpenOptionsBindingSource.DataSource = imageOpenOptions;
            this.imageRangeBindingSource.DataSource = imageRange;
            this.planimetricCircleBindingSource.DataSource = circle;
            this.filterOptionBindingSource.DataSource = filterOptions;
            this.binarizeOptionsBindingSource.DataSource = binarizeOptions;
            this.grainDetectOptionsBindingSource.DataSource = grainDetectOptions;
            this.dotInCircleToolBindingSource.DataSource = dotInCircleTool;
            this.dotOnCircleToolBindingSource.DataSource = dotOnCircleTool;
            this.dotDrawToolBindingSource.DataSource = dotDrawTool;

            this.filterOptionBindingSource.CurrentItemChanged += filterOptionBindingSource_CurrentItemChanged;
            this.binarizeOptionsBindingSource.CurrentItemChanged += binarizeOptionBindingSource_CurrentItemChanged;

            imageFormIsLoaded = false;
            actionMode = ActionMode.None;

            startNewWorkspace();
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
            if (this.openImageFileDialog != null)
            {
                this.openImageFileDialog.Dispose();
            }
            this.openImageFileDialog = null;
            if (this.saveImageFileDialog != null)
            {
                this.saveImageFileDialog.Dispose();
            }
            this.saveImageFileDialog = null;
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

        private void startNewWorkspace()
        {
            isWorkspaceSaved = false;

#if DEBUG
            imageOpenOptions.ImageFilePath = @"D:\Projects\GrainDetector\sample3.bmp";
#else
            imageOpenOptions.ImageFilePath = "";
#endif

            imageRange.LowerX = 0;
            imageRange.UpperX = 0;
            imageRange.LowerY = 0;
            imageRange.UpperY = 0;

            circle.LowerX = 0;
            circle.LowerY = 0;
            circle.Diameter = 0;
            circle.Color = Color.Blue;

            filterOptions.ApplysBlur = BlurOption.None;
            filterOptions.DetectsEdge = EdgeDetectOption.None;

            binarizeOptions.BinarizationThreshold = 127;
            binarizeOptions.InvertsMonochrome = false;

            grainDetectOptions.DetectsGrainInCircle = true;
            grainDetectOptions.DetectsGrainOnCircle = false;
            grainDetectOptions.MinWhitePixelCount = 1000;
            dotInCircleTool.Color = Color.Red;
            dotInCircleTool.Size = 5;
            dotOnCircleTool.Color = Color.Yellow;
            dotOnCircleTool.Size = 5;

            dotDrawTool.Color = Color.Red;
            dotDrawTool.Size = 5;

            this.dotCountListView.Items.Clear();
            this.dotCountListView.Items.Add(new ListViewItem(new string[] { "", "0" }));
            this.dotCountListView.Items.Add(new ListViewItem(new string[] { "", "0" }));
            this.dotCountListView.Items[0].UseItemStyleForSubItems = false;
            this.dotCountListView.Items[1].UseItemStyleForSubItems = false;
            this.dotCountListView.Items[0].SubItems[0].BackColor = Color.Red;
            this.dotCountListView.Items[1].SubItems[0].BackColor = Color.Yellow;
        }

        private void openWorkspace()
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
        }

        private void saveWorkspace()
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

        private void openImageFile(string filePath)
        {
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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "エラー");
                return;
            }
            finally
            {
                if (tmp != null)
                {
                    tmp.Dispose();
                }
            }
        }

        private void saveImageFile(string filePath)
        {
            string directory, fileName;
            try
            {
                directory = Path.GetDirectoryName(filePath);
                fileName = Path.GetFileNameWithoutExtension(filePath);
            }
            catch (ArgumentException)
            {
                directory = "";
                fileName = "";
            }

            this.saveImageFileDialog.FileName = fileName;
            this.saveImageFileDialog.InitialDirectory = directory;

            if (this.saveImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (this.saveImageFileDialog.FilterIndex)
                {
                    case 0:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Bmp);
                        break;
                    case 1:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Exif);
                        break;
                    case 2:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Gif);
                        break;
                    case 3:
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

                        break;
                    case 4:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Png);
                        break;
                    case 5:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Tiff);
                        break;
                    case 6:
                        imageData.ShownImage.Save(this.saveImageFileDialog.FileName, ImageFormat.Bmp);
                        break;
                }
            }
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
            circle.Diameter = Math.Min(imageData.OriginalImage.Width, imageData.OriginalImage.Height);
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
