using System;

namespace GrainDetector
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.imageSelectTabPage = new System.Windows.Forms.TabPage();
            this.imageSelectPanel = new System.Windows.Forms.Panel();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.fileSelectButton = new System.Windows.Forms.Button();
            this.imageOpenButton = new System.Windows.Forms.Button();
            this.rangeSelectPanel = new System.Windows.Forms.Panel();
            this.upperYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rangeSelectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lowerYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rangeYHyphenLabel = new System.Windows.Forms.Label();
            this.upperXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lowerXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rangeSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.rangeXLabel = new System.Windows.Forms.Label();
            this.rangeXHyphenLabel = new System.Windows.Forms.Label();
            this.rangeYLabel = new System.Windows.Forms.Label();
            this.circleSelectPageTab = new System.Windows.Forms.TabPage();
            this.circleSelectPanel = new System.Windows.Forms.Panel();
            this.circleColorSelectLabel = new System.Windows.Forms.Label();
            this.circleDiameterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleSelectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.circleColorLabel = new System.Windows.Forms.Label();
            this.circleXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.circleDiameterLabel = new System.Windows.Forms.Label();
            this.circleXLabel = new System.Windows.Forms.Label();
            this.circleYLabel = new System.Windows.Forms.Label();
            this.grainDetectTabPage = new System.Windows.Forms.TabPage();
            this.dotDrawPanel = new System.Windows.Forms.Panel();
            this.dotDrawCheckBox = new System.Windows.Forms.CheckBox();
            this.dotDrawRedoButton = new System.Windows.Forms.Button();
            this.dotDrawNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dotDrawUndoButton = new System.Windows.Forms.Button();
            this.dotDrawColorLabel = new System.Windows.Forms.Label();
            this.grainDetectPanel = new System.Windows.Forms.Panel();
            this.binarizationThresholdLabel = new System.Windows.Forms.Label();
            this.binarizationThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.detectOnCircleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dotDetectButton = new System.Windows.Forms.Button();
            this.detectInCircleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.detectOnCircleCheckBox = new System.Windows.Forms.CheckBox();
            this.detectInCircleCheckBox = new System.Windows.Forms.CheckBox();
            this.binarizationThresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.dotCountPageTab = new System.Windows.Forms.TabPage();
            this.dotCountPanel = new System.Windows.Forms.Panel();
            this.dotCountTextBox4 = new System.Windows.Forms.TextBox();
            this.dotCountColorLabel4 = new System.Windows.Forms.Label();
            this.dotCountTextBox3 = new System.Windows.Forms.TextBox();
            this.dotCountColorLabel3 = new System.Windows.Forms.Label();
            this.dotCountTextBox2 = new System.Windows.Forms.TextBox();
            this.dotCountColorLabel2 = new System.Windows.Forms.Label();
            this.dotCountStartButton = new System.Windows.Forms.Button();
            this.dotCountTextBox1 = new System.Windows.Forms.TextBox();
            this.dotCountColorLabel1 = new System.Windows.Forms.Label();
            this.lowerPanel = new System.Windows.Forms.Panel();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.shownImageSelectCLB = new System.Windows.Forms.CheckedListBox();
            this.imageSaveButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.imageSelectTabPage.SuspendLayout();
            this.imageSelectPanel.SuspendLayout();
            this.rangeSelectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upperYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeSelectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperXNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerXNumericUpDown)).BeginInit();
            this.circleSelectPageTab.SuspendLayout();
            this.circleSelectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circleDiameterNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleSelectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleXNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleYNumericUpDown)).BeginInit();
            this.grainDetectTabPage.SuspendLayout();
            this.dotDrawPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dotDrawNumericUpDown)).BeginInit();
            this.grainDetectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectOnCircleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectInCircleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThresholdTrackBar)).BeginInit();
            this.dotCountPageTab.SuspendLayout();
            this.dotCountPanel.SuspendLayout();
            this.lowerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.imageSelectTabPage);
            this.tabControl.Controls.Add(this.circleSelectPageTab);
            this.tabControl.Controls.Add(this.grainDetectTabPage);
            this.tabControl.Controls.Add(this.dotCountPageTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(224, 214);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // imageSelectTabPage
            // 
            this.imageSelectTabPage.Controls.Add(this.imageSelectPanel);
            this.imageSelectTabPage.Controls.Add(this.rangeSelectPanel);
            this.imageSelectTabPage.Location = new System.Drawing.Point(4, 22);
            this.imageSelectTabPage.Name = "imageSelectTabPage";
            this.imageSelectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.imageSelectTabPage.Size = new System.Drawing.Size(216, 188);
            this.imageSelectTabPage.TabIndex = 0;
            this.imageSelectTabPage.Text = "画像選択";
            this.imageSelectTabPage.UseVisualStyleBackColor = true;
            // 
            // imageSelectPanel
            // 
            this.imageSelectPanel.BackColor = System.Drawing.Color.Transparent;
            this.imageSelectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageSelectPanel.Controls.Add(this.filePathTextBox);
            this.imageSelectPanel.Controls.Add(this.fileSelectButton);
            this.imageSelectPanel.Controls.Add(this.imageOpenButton);
            this.imageSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.imageSelectPanel.Name = "imageSelectPanel";
            this.imageSelectPanel.Size = new System.Drawing.Size(210, 88);
            this.imageSelectPanel.TabIndex = 0;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTextBox.Location = new System.Drawing.Point(3, 5);
            this.filePathTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(122, 19);
            this.filePathTextBox.TabIndex = 0;
            // 
            // fileSelectButton
            // 
            this.fileSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectButton.Location = new System.Drawing.Point(131, 3);
            this.fileSelectButton.Name = "fileSelectButton";
            this.fileSelectButton.Size = new System.Drawing.Size(74, 23);
            this.fileSelectButton.TabIndex = 0;
            this.fileSelectButton.Text = "選択";
            this.fileSelectButton.UseVisualStyleBackColor = true;
            this.fileSelectButton.Click += new System.EventHandler(this.fileSelectButton_Click);
            // 
            // imageOpenButton
            // 
            this.imageOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageOpenButton.Location = new System.Drawing.Point(3, 32);
            this.imageOpenButton.Name = "imageOpenButton";
            this.imageOpenButton.Size = new System.Drawing.Size(202, 23);
            this.imageOpenButton.TabIndex = 0;
            this.imageOpenButton.Text = "開く";
            this.imageOpenButton.UseVisualStyleBackColor = true;
            this.imageOpenButton.Click += new System.EventHandler(this.imageOpenButton_Click);
            // 
            // rangeSelectPanel
            // 
            this.rangeSelectPanel.BackColor = System.Drawing.Color.Transparent;
            this.rangeSelectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rangeSelectPanel.Controls.Add(this.upperYNumericUpDown);
            this.rangeSelectPanel.Controls.Add(this.lowerYNumericUpDown);
            this.rangeSelectPanel.Controls.Add(this.rangeYHyphenLabel);
            this.rangeSelectPanel.Controls.Add(this.upperXNumericUpDown);
            this.rangeSelectPanel.Controls.Add(this.lowerXNumericUpDown);
            this.rangeSelectPanel.Controls.Add(this.rangeSelectCheckBox);
            this.rangeSelectPanel.Controls.Add(this.rangeXLabel);
            this.rangeSelectPanel.Controls.Add(this.rangeXHyphenLabel);
            this.rangeSelectPanel.Controls.Add(this.rangeYLabel);
            this.rangeSelectPanel.Location = new System.Drawing.Point(3, 94);
            this.rangeSelectPanel.Name = "rangeSelectPanel";
            this.rangeSelectPanel.Size = new System.Drawing.Size(210, 88);
            this.rangeSelectPanel.TabIndex = 0;
            // 
            // upperYNumericUpDown
            // 
            this.upperYNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.rangeSelectBindingSource, "EndY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.upperYNumericUpDown.Location = new System.Drawing.Point(91, 34);
            this.upperYNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.upperYNumericUpDown.Name = "upperYNumericUpDown";
            this.upperYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.upperYNumericUpDown.TabIndex = 0;
            this.upperYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upperYNumericUpDown.ValueChanged += new System.EventHandler(this.upperYNumericUpDown_ValueChanged);
            // 
            // rangeSelectBindingSource
            // 
            this.rangeSelectBindingSource.DataSource = typeof(GrainDetector.RangeSelect);
            // 
            // lowerYNumericUpDown
            // 
            this.lowerYNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.rangeSelectBindingSource, "StartY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lowerYNumericUpDown.Location = new System.Drawing.Point(25, 34);
            this.lowerYNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lowerYNumericUpDown.Name = "lowerYNumericUpDown";
            this.lowerYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.lowerYNumericUpDown.TabIndex = 0;
            this.lowerYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lowerYNumericUpDown.ValueChanged += new System.EventHandler(this.lowerYNumericUpDown_ValueChanged);
            // 
            // rangeYHyphenLabel
            // 
            this.rangeYHyphenLabel.Location = new System.Drawing.Point(76, 38);
            this.rangeYHyphenLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeYHyphenLabel.Name = "rangeYHyphenLabel";
            this.rangeYHyphenLabel.Size = new System.Drawing.Size(9, 12);
            this.rangeYHyphenLabel.TabIndex = 0;
            this.rangeYHyphenLabel.Text = "-";
            // 
            // upperXNumericUpDown
            // 
            this.upperXNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.rangeSelectBindingSource, "EndX", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.upperXNumericUpDown.Location = new System.Drawing.Point(91, 5);
            this.upperXNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.upperXNumericUpDown.Name = "upperXNumericUpDown";
            this.upperXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.upperXNumericUpDown.TabIndex = 3;
            this.upperXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upperXNumericUpDown.ValueChanged += new System.EventHandler(this.upperXNumericUpDown_ValueChanged);
            // 
            // lowerXNumericUpDown
            // 
            this.lowerXNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.rangeSelectBindingSource, "StartX", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lowerXNumericUpDown.Location = new System.Drawing.Point(25, 5);
            this.lowerXNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lowerXNumericUpDown.Name = "lowerXNumericUpDown";
            this.lowerXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.lowerXNumericUpDown.TabIndex = 2;
            this.lowerXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lowerXNumericUpDown.ValueChanged += new System.EventHandler(this.lowerXNumericUpDown_ValueChanged);
            // 
            // rangeSelectCheckBox
            // 
            this.rangeSelectCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.rangeSelectCheckBox.Location = new System.Drawing.Point(142, 3);
            this.rangeSelectCheckBox.Name = "rangeSelectCheckBox";
            this.rangeSelectCheckBox.Size = new System.Drawing.Size(63, 23);
            this.rangeSelectCheckBox.TabIndex = 1;
            this.rangeSelectCheckBox.Text = "範囲選択";
            this.rangeSelectCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rangeSelectCheckBox.UseVisualStyleBackColor = true;
            this.rangeSelectCheckBox.CheckedChanged += new System.EventHandler(this.rangeSelectCheckBox_CheckedChanged);
            // 
            // rangeXLabel
            // 
            this.rangeXLabel.Location = new System.Drawing.Point(5, 9);
            this.rangeXLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeXLabel.Name = "rangeXLabel";
            this.rangeXLabel.Size = new System.Drawing.Size(14, 12);
            this.rangeXLabel.TabIndex = 0;
            this.rangeXLabel.Text = "X:";
            // 
            // rangeXHyphenLabel
            // 
            this.rangeXHyphenLabel.Location = new System.Drawing.Point(76, 9);
            this.rangeXHyphenLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeXHyphenLabel.Name = "rangeXHyphenLabel";
            this.rangeXHyphenLabel.Size = new System.Drawing.Size(9, 12);
            this.rangeXHyphenLabel.TabIndex = 0;
            this.rangeXHyphenLabel.Text = "-";
            // 
            // rangeYLabel
            // 
            this.rangeYLabel.Location = new System.Drawing.Point(5, 38);
            this.rangeYLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeYLabel.Name = "rangeYLabel";
            this.rangeYLabel.Size = new System.Drawing.Size(14, 12);
            this.rangeYLabel.TabIndex = 0;
            this.rangeYLabel.Text = "Y:";
            // 
            // circleSelectPageTab
            // 
            this.circleSelectPageTab.Controls.Add(this.circleSelectPanel);
            this.circleSelectPageTab.Location = new System.Drawing.Point(4, 22);
            this.circleSelectPageTab.Name = "circleSelectPageTab";
            this.circleSelectPageTab.Padding = new System.Windows.Forms.Padding(3);
            this.circleSelectPageTab.Size = new System.Drawing.Size(216, 188);
            this.circleSelectPageTab.TabIndex = 0;
            this.circleSelectPageTab.Text = "円描画";
            this.circleSelectPageTab.UseVisualStyleBackColor = true;
            // 
            // circleSelectPanel
            // 
            this.circleSelectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.circleSelectPanel.Controls.Add(this.circleColorSelectLabel);
            this.circleSelectPanel.Controls.Add(this.circleDiameterNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleColorLabel);
            this.circleSelectPanel.Controls.Add(this.circleXNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleYNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleSelectCheckBox);
            this.circleSelectPanel.Controls.Add(this.circleDiameterLabel);
            this.circleSelectPanel.Controls.Add(this.circleXLabel);
            this.circleSelectPanel.Controls.Add(this.circleYLabel);
            this.circleSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.circleSelectPanel.Name = "circleSelectPanel";
            this.circleSelectPanel.Size = new System.Drawing.Size(210, 182);
            this.circleSelectPanel.TabIndex = 0;
            // 
            // circleColorSelectLabel
            // 
            this.circleColorSelectLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.circleColorSelectLabel.Location = new System.Drawing.Point(154, 32);
            this.circleColorSelectLabel.Margin = new System.Windows.Forms.Padding(3);
            this.circleColorSelectLabel.Name = "circleColorSelectLabel";
            this.circleColorSelectLabel.Size = new System.Drawing.Size(49, 23);
            this.circleColorSelectLabel.TabIndex = 5;
            this.circleColorSelectLabel.Click += new System.EventHandler(this.circleColorSelectLabel_Click);
            // 
            // circleDiameterNumericUpDown
            // 
            this.circleDiameterNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.circleSelectBindingSource, "Diameter", true));
            this.circleDiameterNumericUpDown.Location = new System.Drawing.Point(51, 63);
            this.circleDiameterNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.circleDiameterNumericUpDown.Name = "circleDiameterNumericUpDown";
            this.circleDiameterNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleDiameterNumericUpDown.TabIndex = 3;
            this.circleDiameterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleDiameterNumericUpDown.ValueChanged += new System.EventHandler(this.circleNumericUpDowns_ValueChanged);
            // 
            // circleSelectBindingSource
            // 
            this.circleSelectBindingSource.DataSource = typeof(GrainDetector.CircleSelect);
            // 
            // circleColorLabel
            // 
            this.circleColorLabel.AutoSize = true;
            this.circleColorLabel.Location = new System.Drawing.Point(129, 38);
            this.circleColorLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleColorLabel.Name = "circleColorLabel";
            this.circleColorLabel.Size = new System.Drawing.Size(19, 12);
            this.circleColorLabel.TabIndex = 4;
            this.circleColorLabel.Text = "色:";
            // 
            // circleXNumericUpDown
            // 
            this.circleXNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.circleSelectBindingSource, "StartX", true));
            this.circleXNumericUpDown.Location = new System.Drawing.Point(51, 5);
            this.circleXNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.circleXNumericUpDown.Maximum = new decimal(new int[] {
            1919,
            0,
            0,
            0});
            this.circleXNumericUpDown.Name = "circleXNumericUpDown";
            this.circleXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleXNumericUpDown.TabIndex = 1;
            this.circleXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleXNumericUpDown.ValueChanged += new System.EventHandler(this.circleNumericUpDowns_ValueChanged);
            // 
            // circleYNumericUpDown
            // 
            this.circleYNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.circleSelectBindingSource, "StartY", true));
            this.circleYNumericUpDown.Location = new System.Drawing.Point(51, 34);
            this.circleYNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.circleYNumericUpDown.Maximum = new decimal(new int[] {
            1079,
            0,
            0,
            0});
            this.circleYNumericUpDown.Name = "circleYNumericUpDown";
            this.circleYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleYNumericUpDown.TabIndex = 2;
            this.circleYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleYNumericUpDown.ValueChanged += new System.EventHandler(this.circleNumericUpDowns_ValueChanged);
            // 
            // circleSelectCheckBox
            // 
            this.circleSelectCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.circleSelectCheckBox.Location = new System.Drawing.Point(128, 3);
            this.circleSelectCheckBox.Name = "circleSelectCheckBox";
            this.circleSelectCheckBox.Size = new System.Drawing.Size(75, 23);
            this.circleSelectCheckBox.TabIndex = 2;
            this.circleSelectCheckBox.Text = "円選択";
            this.circleSelectCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.circleSelectCheckBox.UseVisualStyleBackColor = true;
            this.circleSelectCheckBox.CheckedChanged += new System.EventHandler(this.circleSelectCheckBox_CheckedChanged);
            // 
            // circleDiameterLabel
            // 
            this.circleDiameterLabel.Location = new System.Drawing.Point(5, 67);
            this.circleDiameterLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleDiameterLabel.Name = "circleDiameterLabel";
            this.circleDiameterLabel.Size = new System.Drawing.Size(40, 12);
            this.circleDiameterLabel.TabIndex = 1;
            this.circleDiameterLabel.Text = "直径:";
            // 
            // circleXLabel
            // 
            this.circleXLabel.Location = new System.Drawing.Point(5, 9);
            this.circleXLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleXLabel.Name = "circleXLabel";
            this.circleXLabel.Size = new System.Drawing.Size(40, 12);
            this.circleXLabel.TabIndex = 0;
            this.circleXLabel.Text = "左上X:";
            // 
            // circleYLabel
            // 
            this.circleYLabel.Location = new System.Drawing.Point(5, 38);
            this.circleYLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleYLabel.Name = "circleYLabel";
            this.circleYLabel.Size = new System.Drawing.Size(40, 12);
            this.circleYLabel.TabIndex = 0;
            this.circleYLabel.Text = "左上Y:";
            // 
            // grainDetectTabPage
            // 
            this.grainDetectTabPage.Controls.Add(this.dotDrawPanel);
            this.grainDetectTabPage.Controls.Add(this.grainDetectPanel);
            this.grainDetectTabPage.Location = new System.Drawing.Point(4, 22);
            this.grainDetectTabPage.Name = "grainDetectTabPage";
            this.grainDetectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.grainDetectTabPage.Size = new System.Drawing.Size(216, 188);
            this.grainDetectTabPage.TabIndex = 0;
            this.grainDetectTabPage.Text = "粒子検出";
            this.grainDetectTabPage.UseVisualStyleBackColor = true;
            // 
            // dotDrawPanel
            // 
            this.dotDrawPanel.BackColor = System.Drawing.Color.Transparent;
            this.dotDrawPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dotDrawPanel.Controls.Add(this.dotDrawCheckBox);
            this.dotDrawPanel.Controls.Add(this.dotDrawRedoButton);
            this.dotDrawPanel.Controls.Add(this.dotDrawNumericUpDown);
            this.dotDrawPanel.Controls.Add(this.dotDrawUndoButton);
            this.dotDrawPanel.Controls.Add(this.dotDrawColorLabel);
            this.dotDrawPanel.Location = new System.Drawing.Point(3, 94);
            this.dotDrawPanel.Name = "dotDrawPanel";
            this.dotDrawPanel.Size = new System.Drawing.Size(210, 88);
            this.dotDrawPanel.TabIndex = 2;
            // 
            // dotDrawCheckBox
            // 
            this.dotDrawCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.dotDrawCheckBox.Location = new System.Drawing.Point(3, 3);
            this.dotDrawCheckBox.Name = "dotDrawCheckBox";
            this.dotDrawCheckBox.Size = new System.Drawing.Size(75, 23);
            this.dotDrawCheckBox.TabIndex = 13;
            this.dotDrawCheckBox.Text = "点打ち";
            this.dotDrawCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dotDrawCheckBox.UseVisualStyleBackColor = true;
            this.dotDrawCheckBox.CheckedChanged += new System.EventHandler(this.dotDrawCheckBox_CheckedChanged);
            // 
            // dotDrawRedoButton
            // 
            this.dotDrawRedoButton.Location = new System.Drawing.Point(3, 61);
            this.dotDrawRedoButton.Name = "dotDrawRedoButton";
            this.dotDrawRedoButton.Size = new System.Drawing.Size(200, 23);
            this.dotDrawRedoButton.TabIndex = 12;
            this.dotDrawRedoButton.Text = "やり直す";
            this.dotDrawRedoButton.UseVisualStyleBackColor = true;
            this.dotDrawRedoButton.Click += new System.EventHandler(this.dotDrawRedoButton_Click);
            // 
            // dotDrawNumericUpDown
            // 
            this.dotDrawNumericUpDown.Location = new System.Drawing.Point(164, 5);
            this.dotDrawNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dotDrawNumericUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.dotDrawNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dotDrawNumericUpDown.Name = "dotDrawNumericUpDown";
            this.dotDrawNumericUpDown.Size = new System.Drawing.Size(39, 19);
            this.dotDrawNumericUpDown.TabIndex = 11;
            this.dotDrawNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.dotDrawNumericUpDown.ValueChanged += new System.EventHandler(this.dotDrawNumericUpDown_ValueChanged);
            // 
            // dotDrawUndoButton
            // 
            this.dotDrawUndoButton.Location = new System.Drawing.Point(3, 32);
            this.dotDrawUndoButton.Name = "dotDrawUndoButton";
            this.dotDrawUndoButton.Size = new System.Drawing.Size(200, 23);
            this.dotDrawUndoButton.TabIndex = 8;
            this.dotDrawUndoButton.Text = "元に戻す";
            this.dotDrawUndoButton.UseVisualStyleBackColor = true;
            this.dotDrawUndoButton.Click += new System.EventHandler(this.dotDrawUndoButton_Click);
            // 
            // dotDrawColorLabel
            // 
            this.dotDrawColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dotDrawColorLabel.Location = new System.Drawing.Point(109, 3);
            this.dotDrawColorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.dotDrawColorLabel.Name = "dotDrawColorLabel";
            this.dotDrawColorLabel.Size = new System.Drawing.Size(49, 23);
            this.dotDrawColorLabel.TabIndex = 6;
            this.dotDrawColorLabel.Click += new System.EventHandler(this.dotDrawColorLabel_Click);
            // 
            // grainDetectPanel
            // 
            this.grainDetectPanel.BackColor = System.Drawing.Color.Transparent;
            this.grainDetectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grainDetectPanel.Controls.Add(this.binarizationThresholdLabel);
            this.grainDetectPanel.Controls.Add(this.binarizationThresholdNumericUpDown);
            this.grainDetectPanel.Controls.Add(this.detectOnCircleNumericUpDown);
            this.grainDetectPanel.Controls.Add(this.dotDetectButton);
            this.grainDetectPanel.Controls.Add(this.detectInCircleNumericUpDown);
            this.grainDetectPanel.Controls.Add(this.detectOnCircleCheckBox);
            this.grainDetectPanel.Controls.Add(this.detectInCircleCheckBox);
            this.grainDetectPanel.Controls.Add(this.binarizationThresholdTrackBar);
            this.grainDetectPanel.Location = new System.Drawing.Point(3, 3);
            this.grainDetectPanel.Name = "grainDetectPanel";
            this.grainDetectPanel.Size = new System.Drawing.Size(210, 88);
            this.grainDetectPanel.TabIndex = 1;
            // 
            // binarizationThresholdLabel
            // 
            this.binarizationThresholdLabel.Location = new System.Drawing.Point(5, 9);
            this.binarizationThresholdLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.binarizationThresholdLabel.Name = "binarizationThresholdLabel";
            this.binarizationThresholdLabel.Size = new System.Drawing.Size(65, 12);
            this.binarizationThresholdLabel.TabIndex = 12;
            this.binarizationThresholdLabel.Text = "二値化閾値";
            // 
            // binarizationThresholdNumericUpDown
            // 
            this.binarizationThresholdNumericUpDown.Location = new System.Drawing.Point(164, 5);
            this.binarizationThresholdNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.binarizationThresholdNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.binarizationThresholdNumericUpDown.Name = "binarizationThresholdNumericUpDown";
            this.binarizationThresholdNumericUpDown.Size = new System.Drawing.Size(39, 19);
            this.binarizationThresholdNumericUpDown.TabIndex = 11;
            // 
            // detectOnCircleNumericUpDown
            // 
            this.detectOnCircleNumericUpDown.Location = new System.Drawing.Point(93, 63);
            this.detectOnCircleNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.detectOnCircleNumericUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.detectOnCircleNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.detectOnCircleNumericUpDown.Name = "detectOnCircleNumericUpDown";
            this.detectOnCircleNumericUpDown.Size = new System.Drawing.Size(39, 19);
            this.detectOnCircleNumericUpDown.TabIndex = 10;
            this.detectOnCircleNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // dotDetectButton
            // 
            this.dotDetectButton.Location = new System.Drawing.Point(138, 60);
            this.dotDetectButton.Name = "dotDetectButton";
            this.dotDetectButton.Size = new System.Drawing.Size(65, 23);
            this.dotDetectButton.TabIndex = 9;
            this.dotDetectButton.Text = "解析";
            this.dotDetectButton.UseVisualStyleBackColor = true;
            // 
            // detectInCircleNumericUpDown
            // 
            this.detectInCircleNumericUpDown.Location = new System.Drawing.Point(93, 34);
            this.detectInCircleNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.detectInCircleNumericUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.detectInCircleNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.detectInCircleNumericUpDown.Name = "detectInCircleNumericUpDown";
            this.detectInCircleNumericUpDown.Size = new System.Drawing.Size(39, 19);
            this.detectInCircleNumericUpDown.TabIndex = 3;
            this.detectInCircleNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // detectOnCircleCheckBox
            // 
            this.detectOnCircleCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.detectOnCircleCheckBox.Location = new System.Drawing.Point(3, 63);
            this.detectOnCircleCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 4);
            this.detectOnCircleCheckBox.Name = "detectOnCircleCheckBox";
            this.detectOnCircleCheckBox.Size = new System.Drawing.Size(84, 20);
            this.detectOnCircleCheckBox.TabIndex = 2;
            this.detectOnCircleCheckBox.Text = "円周上粒子";
            this.detectOnCircleCheckBox.UseVisualStyleBackColor = false;
            // 
            // detectInCircleCheckBox
            // 
            this.detectInCircleCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.detectInCircleCheckBox.Location = new System.Drawing.Point(3, 34);
            this.detectInCircleCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 4);
            this.detectInCircleCheckBox.Name = "detectInCircleCheckBox";
            this.detectInCircleCheckBox.Size = new System.Drawing.Size(84, 20);
            this.detectInCircleCheckBox.TabIndex = 1;
            this.detectInCircleCheckBox.Text = "円内粒子";
            this.detectInCircleCheckBox.UseVisualStyleBackColor = false;
            // 
            // binarizationThresholdTrackBar
            // 
            this.binarizationThresholdTrackBar.AutoSize = false;
            this.binarizationThresholdTrackBar.Location = new System.Drawing.Point(76, 3);
            this.binarizationThresholdTrackBar.Maximum = 255;
            this.binarizationThresholdTrackBar.Name = "binarizationThresholdTrackBar";
            this.binarizationThresholdTrackBar.Size = new System.Drawing.Size(82, 23);
            this.binarizationThresholdTrackBar.TabIndex = 0;
            // 
            // dotCountPageTab
            // 
            this.dotCountPageTab.Controls.Add(this.dotCountPanel);
            this.dotCountPageTab.Location = new System.Drawing.Point(4, 22);
            this.dotCountPageTab.Name = "dotCountPageTab";
            this.dotCountPageTab.Padding = new System.Windows.Forms.Padding(3);
            this.dotCountPageTab.Size = new System.Drawing.Size(216, 188);
            this.dotCountPageTab.TabIndex = 0;
            this.dotCountPageTab.Text = "点検出";
            this.dotCountPageTab.UseVisualStyleBackColor = true;
            // 
            // dotCountPanel
            // 
            this.dotCountPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dotCountPanel.Controls.Add(this.dotCountTextBox4);
            this.dotCountPanel.Controls.Add(this.dotCountColorLabel4);
            this.dotCountPanel.Controls.Add(this.dotCountTextBox3);
            this.dotCountPanel.Controls.Add(this.dotCountColorLabel3);
            this.dotCountPanel.Controls.Add(this.dotCountTextBox2);
            this.dotCountPanel.Controls.Add(this.dotCountColorLabel2);
            this.dotCountPanel.Controls.Add(this.dotCountStartButton);
            this.dotCountPanel.Controls.Add(this.dotCountTextBox1);
            this.dotCountPanel.Controls.Add(this.dotCountColorLabel1);
            this.dotCountPanel.Location = new System.Drawing.Point(3, 3);
            this.dotCountPanel.Name = "dotCountPanel";
            this.dotCountPanel.Size = new System.Drawing.Size(210, 182);
            this.dotCountPanel.TabIndex = 1;
            // 
            // dotCountTextBox4
            // 
            this.dotCountTextBox4.Location = new System.Drawing.Point(60, 92);
            this.dotCountTextBox4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dotCountTextBox4.Name = "dotCountTextBox4";
            this.dotCountTextBox4.ReadOnly = true;
            this.dotCountTextBox4.Size = new System.Drawing.Size(40, 19);
            this.dotCountTextBox4.TabIndex = 13;
            this.dotCountTextBox4.Text = "0";
            this.dotCountTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dotCountColorLabel4
            // 
            this.dotCountColorLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dotCountColorLabel4.Location = new System.Drawing.Point(5, 90);
            this.dotCountColorLabel4.Margin = new System.Windows.Forms.Padding(3);
            this.dotCountColorLabel4.Name = "dotCountColorLabel4";
            this.dotCountColorLabel4.Size = new System.Drawing.Size(49, 23);
            this.dotCountColorLabel4.TabIndex = 12;
            this.dotCountColorLabel4.Click += new System.EventHandler(this.dotCountColorLabel4_Click);
            // 
            // dotCountTextBox3
            // 
            this.dotCountTextBox3.Location = new System.Drawing.Point(60, 63);
            this.dotCountTextBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dotCountTextBox3.Name = "dotCountTextBox3";
            this.dotCountTextBox3.ReadOnly = true;
            this.dotCountTextBox3.Size = new System.Drawing.Size(40, 19);
            this.dotCountTextBox3.TabIndex = 11;
            this.dotCountTextBox3.Text = "0";
            this.dotCountTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dotCountColorLabel3
            // 
            this.dotCountColorLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dotCountColorLabel3.Location = new System.Drawing.Point(5, 61);
            this.dotCountColorLabel3.Margin = new System.Windows.Forms.Padding(3);
            this.dotCountColorLabel3.Name = "dotCountColorLabel3";
            this.dotCountColorLabel3.Size = new System.Drawing.Size(49, 23);
            this.dotCountColorLabel3.TabIndex = 10;
            this.dotCountColorLabel3.Click += new System.EventHandler(this.dotCountColorLabel3_Click);
            // 
            // dotCountTextBox2
            // 
            this.dotCountTextBox2.Location = new System.Drawing.Point(60, 34);
            this.dotCountTextBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dotCountTextBox2.Name = "dotCountTextBox2";
            this.dotCountTextBox2.ReadOnly = true;
            this.dotCountTextBox2.Size = new System.Drawing.Size(40, 19);
            this.dotCountTextBox2.TabIndex = 9;
            this.dotCountTextBox2.Text = "0";
            this.dotCountTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dotCountColorLabel2
            // 
            this.dotCountColorLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dotCountColorLabel2.Location = new System.Drawing.Point(5, 32);
            this.dotCountColorLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.dotCountColorLabel2.Name = "dotCountColorLabel2";
            this.dotCountColorLabel2.Size = new System.Drawing.Size(49, 23);
            this.dotCountColorLabel2.TabIndex = 8;
            this.dotCountColorLabel2.Click += new System.EventHandler(this.dotCountColorLabel2_Click);
            // 
            // dotCountStartButton
            // 
            this.dotCountStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dotCountStartButton.Location = new System.Drawing.Point(5, 119);
            this.dotCountStartButton.Name = "dotCountStartButton";
            this.dotCountStartButton.Size = new System.Drawing.Size(198, 23);
            this.dotCountStartButton.TabIndex = 7;
            this.dotCountStartButton.Text = "解析";
            this.dotCountStartButton.UseVisualStyleBackColor = true;
            this.dotCountStartButton.Click += new System.EventHandler(this.dotCountStartButton_Click);
            // 
            // dotCountTextBox1
            // 
            this.dotCountTextBox1.Location = new System.Drawing.Point(60, 5);
            this.dotCountTextBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dotCountTextBox1.Name = "dotCountTextBox1";
            this.dotCountTextBox1.ReadOnly = true;
            this.dotCountTextBox1.Size = new System.Drawing.Size(40, 19);
            this.dotCountTextBox1.TabIndex = 6;
            this.dotCountTextBox1.Text = "0";
            this.dotCountTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dotCountColorLabel1
            // 
            this.dotCountColorLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dotCountColorLabel1.Location = new System.Drawing.Point(5, 3);
            this.dotCountColorLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.dotCountColorLabel1.Name = "dotCountColorLabel1";
            this.dotCountColorLabel1.Size = new System.Drawing.Size(49, 23);
            this.dotCountColorLabel1.TabIndex = 5;
            this.dotCountColorLabel1.Click += new System.EventHandler(this.dotCountColorLabel1_Click);
            // 
            // lowerPanel
            // 
            this.lowerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lowerPanel.Controls.Add(this.zoomOutButton);
            this.lowerPanel.Controls.Add(this.zoomInButton);
            this.lowerPanel.Controls.Add(this.shownImageSelectCLB);
            this.lowerPanel.Controls.Add(this.imageSaveButton);
            this.lowerPanel.Location = new System.Drawing.Point(3, 217);
            this.lowerPanel.Name = "lowerPanel";
            this.lowerPanel.Size = new System.Drawing.Size(218, 101);
            this.lowerPanel.TabIndex = 1;
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Location = new System.Drawing.Point(136, 32);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(75, 23);
            this.zoomOutButton.TabIndex = 7;
            this.zoomOutButton.Text = "縮小";
            this.zoomOutButton.UseVisualStyleBackColor = true;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Location = new System.Drawing.Point(136, 3);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(75, 23);
            this.zoomInButton.TabIndex = 6;
            this.zoomInButton.Text = "拡大";
            this.zoomInButton.UseVisualStyleBackColor = true;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // shownImageSelectCLB
            // 
            this.shownImageSelectCLB.CheckOnClick = true;
            this.shownImageSelectCLB.FormattingEnabled = true;
            this.shownImageSelectCLB.Items.AddRange(new object[] {
            "画像範囲",
            "円",
            "粒子点"});
            this.shownImageSelectCLB.Location = new System.Drawing.Point(3, 3);
            this.shownImageSelectCLB.Name = "shownImageSelectCLB";
            this.shownImageSelectCLB.Size = new System.Drawing.Size(120, 88);
            this.shownImageSelectCLB.TabIndex = 5;
            this.shownImageSelectCLB.SelectedIndexChanged += new System.EventHandler(this.shownImageSelectCLB_SelectedIndexChanged);
            // 
            // imageSaveButton
            // 
            this.imageSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSaveButton.Location = new System.Drawing.Point(136, 61);
            this.imageSaveButton.Name = "imageSaveButton";
            this.imageSaveButton.Size = new System.Drawing.Size(75, 23);
            this.imageSaveButton.TabIndex = 4;
            this.imageSaveButton.Text = "保存";
            this.imageSaveButton.UseVisualStyleBackColor = true;
            this.imageSaveButton.Click += new System.EventHandler(this.imageSaveButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 321);
            this.Controls.Add(this.lowerPanel);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "GrainDetector";
            this.tabControl.ResumeLayout(false);
            this.imageSelectTabPage.ResumeLayout(false);
            this.imageSelectPanel.ResumeLayout(false);
            this.imageSelectPanel.PerformLayout();
            this.rangeSelectPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upperYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeSelectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperXNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerXNumericUpDown)).EndInit();
            this.circleSelectPageTab.ResumeLayout(false);
            this.circleSelectPanel.ResumeLayout(false);
            this.circleSelectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circleDiameterNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleSelectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleXNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleYNumericUpDown)).EndInit();
            this.grainDetectTabPage.ResumeLayout(false);
            this.dotDrawPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dotDrawNumericUpDown)).EndInit();
            this.grainDetectPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectOnCircleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectInCircleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThresholdTrackBar)).EndInit();
            this.dotCountPageTab.ResumeLayout(false);
            this.dotCountPanel.ResumeLayout(false);
            this.dotCountPanel.PerformLayout();
            this.lowerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage imageSelectTabPage;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button fileSelectButton;
        private System.Windows.Forms.Button imageOpenButton;
        private System.Windows.Forms.TabPage circleSelectPageTab;
        private System.Windows.Forms.TabPage grainDetectTabPage;
        private System.Windows.Forms.Panel imageSelectPanel;
        private System.Windows.Forms.Panel rangeSelectPanel;
        private System.Windows.Forms.Label rangeXLabel;
        private System.Windows.Forms.Label rangeYLabel;
        private System.Windows.Forms.Label rangeXHyphenLabel;
        private System.Windows.Forms.Panel circleSelectPanel;
        private System.Windows.Forms.Label circleYLabel;
        private System.Windows.Forms.Label circleXLabel;
        private System.Windows.Forms.Label circleDiameterLabel;
        private System.Windows.Forms.Panel lowerPanel;
        private System.Windows.Forms.Button imageSaveButton;
        private System.Windows.Forms.CheckedListBox shownImageSelectCLB;
        private System.Windows.Forms.CheckBox rangeSelectCheckBox;
        private System.Windows.Forms.CheckBox circleSelectCheckBox;
        private System.Windows.Forms.NumericUpDown upperYNumericUpDown;
        private System.Windows.Forms.NumericUpDown lowerYNumericUpDown;
        private System.Windows.Forms.Label rangeYHyphenLabel;
        private System.Windows.Forms.NumericUpDown upperXNumericUpDown;
        private System.Windows.Forms.NumericUpDown lowerXNumericUpDown;
        private System.Windows.Forms.NumericUpDown circleXNumericUpDown;
        private System.Windows.Forms.NumericUpDown circleYNumericUpDown;
        private System.Windows.Forms.NumericUpDown circleDiameterNumericUpDown;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.BindingSource rangeSelectBindingSource;
        private System.Windows.Forms.Label circleColorSelectLabel;
        private System.Windows.Forms.Label circleColorLabel;
        private System.Windows.Forms.BindingSource circleSelectBindingSource;
        private System.Windows.Forms.TabPage dotCountPageTab;
        private System.Windows.Forms.Panel dotCountPanel;
        private System.Windows.Forms.Button dotCountStartButton;
        private System.Windows.Forms.TextBox dotCountTextBox1;
        private System.Windows.Forms.Label dotCountColorLabel1;
        private System.Windows.Forms.Panel grainDetectPanel;
        private System.Windows.Forms.Panel dotDrawPanel;
        private System.Windows.Forms.TrackBar binarizationThresholdTrackBar;
        private System.Windows.Forms.CheckBox detectOnCircleCheckBox;
        private System.Windows.Forms.CheckBox detectInCircleCheckBox;
        private System.Windows.Forms.NumericUpDown detectInCircleNumericUpDown;
        private System.Windows.Forms.Button dotDrawUndoButton;
        private System.Windows.Forms.Label dotDrawColorLabel;
        private System.Windows.Forms.NumericUpDown binarizationThresholdNumericUpDown;
        private System.Windows.Forms.NumericUpDown detectOnCircleNumericUpDown;
        private System.Windows.Forms.Button dotDetectButton;
        private System.Windows.Forms.NumericUpDown dotDrawNumericUpDown;
        private System.Windows.Forms.Label binarizationThresholdLabel;
        private System.Windows.Forms.Button dotDrawRedoButton;
        private System.Windows.Forms.TextBox dotCountTextBox4;
        private System.Windows.Forms.Label dotCountColorLabel4;
        private System.Windows.Forms.TextBox dotCountTextBox3;
        private System.Windows.Forms.Label dotCountColorLabel3;
        private System.Windows.Forms.TextBox dotCountTextBox2;
        private System.Windows.Forms.Label dotCountColorLabel2;
        private System.Windows.Forms.CheckBox dotDrawCheckBox;
    }
}

