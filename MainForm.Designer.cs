using System;

namespace GrainDetector
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.imageSelectTabPage = new System.Windows.Forms.TabPage();
            this.imageSelectPanel = new System.Windows.Forms.Panel();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.fileSelectButton = new System.Windows.Forms.Button();
            this.imageOpenButton = new System.Windows.Forms.Button();
            this.rangeSelectPanel = new System.Windows.Forms.Panel();
            this.upperYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lowerYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rangeYHyphenLabel = new System.Windows.Forms.Label();
            this.upperXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lowerXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rangeSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.rangeXLabel = new System.Windows.Forms.Label();
            this.rangeXHyphenLabel = new System.Windows.Forms.Label();
            this.rangeYLabel = new System.Windows.Forms.Label();
            this.circleDrawPageTab = new System.Windows.Forms.TabPage();
            this.circleSelectPanel = new System.Windows.Forms.Panel();
            this.circleXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleDiameterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.circleSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.circleRadiusLabel = new System.Windows.Forms.Label();
            this.circleXLabel = new System.Windows.Forms.Label();
            this.circleYLabel = new System.Windows.Forms.Label();
            this.grainDetectTabPage = new System.Windows.Forms.TabPage();
            this.dotCountPageTab = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.shownImageSelectCLB = new System.Windows.Forms.CheckedListBox();
            this.imageSaveButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.imageSelectTabPage.SuspendLayout();
            this.imageSelectPanel.SuspendLayout();
            this.rangeSelectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upperYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperXNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerXNumericUpDown)).BeginInit();
            this.circleDrawPageTab.SuspendLayout();
            this.circleSelectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circleXNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleDiameterNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.imageSelectTabPage);
            this.tabControl.Controls.Add(this.circleDrawPageTab);
            this.tabControl.Controls.Add(this.grainDetectTabPage);
            this.tabControl.Controls.Add(this.dotCountPageTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "TabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(224, 214);
            this.tabControl.TabIndex = 0;
            // 
            // imageSelectTabPage
            // 
            this.imageSelectTabPage.Controls.Add(this.imageSelectPanel);
            this.imageSelectTabPage.Controls.Add(this.rangeSelectPanel);
            this.imageSelectTabPage.Location = new System.Drawing.Point(4, 22);
            this.imageSelectTabPage.Name = "ImageSelectTabPage";
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
            this.imageSelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.imageSelectPanel.Name = "ImageSelectPanel";
            this.imageSelectPanel.Size = new System.Drawing.Size(210, 91);
            this.imageSelectPanel.TabIndex = 0;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTextBox.Location = new System.Drawing.Point(3, 5);
            this.filePathTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.filePathTextBox.Name = "FilePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(122, 19);
            this.filePathTextBox.TabIndex = 0;
            // 
            // fileSelectButton
            // 
            this.fileSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectButton.Location = new System.Drawing.Point(131, 3);
            this.fileSelectButton.Name = "FileSelectButton";
            this.fileSelectButton.Size = new System.Drawing.Size(72, 23);
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
            this.imageOpenButton.Name = "ImageOpenButton";
            this.imageOpenButton.Size = new System.Drawing.Size(200, 23);
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
            this.rangeSelectPanel.Location = new System.Drawing.Point(3, 97);
            this.rangeSelectPanel.Name = "RangeSelectPanel";
            this.rangeSelectPanel.Size = new System.Drawing.Size(210, 91);
            this.rangeSelectPanel.TabIndex = 0;
            // 
            // upperYNumericUpDown
            // 
            this.upperYNumericUpDown.Location = new System.Drawing.Point(91, 34);
            this.upperYNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.upperYNumericUpDown.Maximum = new decimal(new int[] {
            1079,
            0,
            0,
            0});
            this.upperYNumericUpDown.Name = "UpperYNumericUpDown";
            this.upperYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.upperYNumericUpDown.TabIndex = 0;
            this.upperYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upperYNumericUpDown.ValueChanged += new System.EventHandler(this.upperYNumericUpDown_ValueChanged);
            this.upperYNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upperYNumericUpDown_Validating);
            // 
            // lowerYNumericUpDown
            // 
            this.lowerYNumericUpDown.Location = new System.Drawing.Point(25, 34);
            this.lowerYNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lowerYNumericUpDown.Maximum = new decimal(new int[] {
            1079,
            0,
            0,
            0});
            this.lowerYNumericUpDown.Name = "LowerYNumericUpDown";
            this.lowerYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.lowerYNumericUpDown.TabIndex = 0;
            this.lowerYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lowerYNumericUpDown.ValueChanged += new System.EventHandler(this.lowerYNumericUpDown_ValueChanged);
            this.lowerYNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.lowerYNumericUpDown_Validating);
            // 
            // rangeYHyphenLabel
            // 
            this.rangeYHyphenLabel.Location = new System.Drawing.Point(76, 38);
            this.rangeYHyphenLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeYHyphenLabel.Name = "RangeYHyphenLabel";
            this.rangeYHyphenLabel.Size = new System.Drawing.Size(9, 12);
            this.rangeYHyphenLabel.TabIndex = 0;
            this.rangeYHyphenLabel.Text = "-";
            // 
            // upperXNumericUpDown
            // 
            this.upperXNumericUpDown.Location = new System.Drawing.Point(91, 5);
            this.upperXNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.upperXNumericUpDown.Maximum = new decimal(new int[] {
            1919,
            0,
            0,
            0});
            this.upperXNumericUpDown.Name = "UpperXNumericUpDown";
            this.upperXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.upperXNumericUpDown.TabIndex = 3;
            this.upperXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upperXNumericUpDown.ValueChanged += new System.EventHandler(this.upperXNumericUpDown_ValueChanged);
            this.upperXNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.upperXNumericUpDown_Validating);
            // 
            // lowerXNumericUpDown
            // 
            this.lowerXNumericUpDown.Location = new System.Drawing.Point(25, 5);
            this.lowerXNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lowerXNumericUpDown.Maximum = new decimal(new int[] {
            1919,
            0,
            0,
            0});
            this.lowerXNumericUpDown.Name = "LowerXNumericUpDown";
            this.lowerXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.lowerXNumericUpDown.TabIndex = 2;
            this.lowerXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lowerXNumericUpDown.ValueChanged += new System.EventHandler(this.lowerXNumericUpDown_ValueChanged);
            this.lowerXNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.lowerXNumericUpDown_Validating);
            // 
            // rangeSelectCheckBox
            // 
            this.rangeSelectCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.rangeSelectCheckBox.Location = new System.Drawing.Point(142, 3);
            this.rangeSelectCheckBox.Name = "RangeSelectCheckBox";
            this.rangeSelectCheckBox.Size = new System.Drawing.Size(61, 23);
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
            this.rangeXLabel.Name = "RangeXLabel";
            this.rangeXLabel.Size = new System.Drawing.Size(14, 12);
            this.rangeXLabel.TabIndex = 0;
            this.rangeXLabel.Text = "X:";
            // 
            // rangeXHyphenLabel
            // 
            this.rangeXHyphenLabel.Location = new System.Drawing.Point(76, 9);
            this.rangeXHyphenLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeXHyphenLabel.Name = "RangeXHyphenLabel";
            this.rangeXHyphenLabel.Size = new System.Drawing.Size(9, 12);
            this.rangeXHyphenLabel.TabIndex = 0;
            this.rangeXHyphenLabel.Text = "-";
            // 
            // rangeYLabel
            // 
            this.rangeYLabel.Location = new System.Drawing.Point(5, 38);
            this.rangeYLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.rangeYLabel.Name = "RangeYLabel";
            this.rangeYLabel.Size = new System.Drawing.Size(14, 12);
            this.rangeYLabel.TabIndex = 0;
            this.rangeYLabel.Text = "Y:";
            // 
            // circleDrawPageTab
            // 
            this.circleDrawPageTab.Controls.Add(this.circleSelectPanel);
            this.circleDrawPageTab.Location = new System.Drawing.Point(4, 22);
            this.circleDrawPageTab.Name = "CircleDrawPageTab";
            this.circleDrawPageTab.Padding = new System.Windows.Forms.Padding(3);
            this.circleDrawPageTab.Size = new System.Drawing.Size(216, 188);
            this.circleDrawPageTab.TabIndex = 0;
            this.circleDrawPageTab.Text = "円描画";
            this.circleDrawPageTab.UseVisualStyleBackColor = true;
            // 
            // circleSelectPanel
            // 
            this.circleSelectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.circleSelectPanel.Controls.Add(this.circleXNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleYNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleDiameterNumericUpDown);
            this.circleSelectPanel.Controls.Add(this.circleSelectCheckBox);
            this.circleSelectPanel.Controls.Add(this.circleRadiusLabel);
            this.circleSelectPanel.Controls.Add(this.circleXLabel);
            this.circleSelectPanel.Controls.Add(this.circleYLabel);
            this.circleSelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.circleSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.circleSelectPanel.Name = "CircleSelectPanel";
            this.circleSelectPanel.Size = new System.Drawing.Size(210, 100);
            this.circleSelectPanel.TabIndex = 0;
            // 
            // circleXNumericUpDown
            // 
            this.circleXNumericUpDown.Location = new System.Drawing.Point(51, 5);
            this.circleXNumericUpDown.Maximum = new decimal(new int[] {
            1919,
            0,
            0,
            0});
            this.circleXNumericUpDown.Name = "CircleXNumericUpDown";
            this.circleXNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleXNumericUpDown.TabIndex = 1;
            this.circleXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleXNumericUpDown.ValueChanged += new System.EventHandler(this.circleXNumericUpDown_ValueChanged);
            this.circleXNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.circleXNumericUpDown_Validating);
            // 
            // circleYNumericUpDown
            // 
            this.circleYNumericUpDown.Location = new System.Drawing.Point(51, 34);
            this.circleYNumericUpDown.Maximum = new decimal(new int[] {
            1079,
            0,
            0,
            0});
            this.circleYNumericUpDown.Name = "CircleYNumericUpDown";
            this.circleYNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleYNumericUpDown.TabIndex = 2;
            this.circleYNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleYNumericUpDown.ValueChanged += new System.EventHandler(this.circleYNumericUpDown_ValueChanged);
            this.circleYNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.circleYNumericUpDown_Validating);
            // 
            // circleDiameterNumericUpDown
            // 
            this.circleDiameterNumericUpDown.Location = new System.Drawing.Point(51, 63);
            this.circleDiameterNumericUpDown.Name = "CircleDiameterNumericUpDown";
            this.circleDiameterNumericUpDown.Size = new System.Drawing.Size(45, 19);
            this.circleDiameterNumericUpDown.TabIndex = 3;
            this.circleDiameterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.circleDiameterNumericUpDown.ValueChanged += new System.EventHandler(this.circleDiameterNumericUpDown_ValueChanged);
            this.circleDiameterNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.circleDiameterNumericUpDown_Validating);
            // 
            // circleSelectCheckBox
            // 
            this.circleSelectCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.circleSelectCheckBox.Location = new System.Drawing.Point(128, 3);
            this.circleSelectCheckBox.Name = "CircleSelectCheckBox";
            this.circleSelectCheckBox.Size = new System.Drawing.Size(75, 23);
            this.circleSelectCheckBox.TabIndex = 2;
            this.circleSelectCheckBox.Text = "円選択";
            this.circleSelectCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.circleSelectCheckBox.UseVisualStyleBackColor = true;
            this.circleSelectCheckBox.CheckedChanged += new System.EventHandler(this.circleSelectCheckBox_CheckedChanged);
            // 
            // circleRadiusLabel
            // 
            this.circleRadiusLabel.Location = new System.Drawing.Point(5, 67);
            this.circleRadiusLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleRadiusLabel.Name = "CircleRadiusLabel";
            this.circleRadiusLabel.Size = new System.Drawing.Size(40, 12);
            this.circleRadiusLabel.TabIndex = 1;
            this.circleRadiusLabel.Text = "直径:";
            // 
            // circleXLabel
            // 
            this.circleXLabel.Location = new System.Drawing.Point(5, 9);
            this.circleXLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleXLabel.Name = "CircleXLabel";
            this.circleXLabel.Size = new System.Drawing.Size(40, 12);
            this.circleXLabel.TabIndex = 0;
            this.circleXLabel.Text = "左上X:";
            // 
            // circleYLabel
            // 
            this.circleYLabel.Location = new System.Drawing.Point(5, 38);
            this.circleYLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 8);
            this.circleYLabel.Name = "CircleYLabel";
            this.circleYLabel.Size = new System.Drawing.Size(40, 12);
            this.circleYLabel.TabIndex = 0;
            this.circleYLabel.Text = "左上Y:";
            // 
            // grainDetectTabPage
            // 
            this.grainDetectTabPage.Location = new System.Drawing.Point(4, 22);
            this.grainDetectTabPage.Name = "GrainDetectTabPage";
            this.grainDetectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.grainDetectTabPage.Size = new System.Drawing.Size(216, 188);
            this.grainDetectTabPage.TabIndex = 0;
            this.grainDetectTabPage.Text = "粒子検出";
            this.grainDetectTabPage.UseVisualStyleBackColor = true;
            // 
            // dotCountPageTab
            // 
            this.dotCountPageTab.Location = new System.Drawing.Point(4, 22);
            this.dotCountPageTab.Name = "DotCountPageTab";
            this.dotCountPageTab.Padding = new System.Windows.Forms.Padding(3);
            this.dotCountPageTab.Size = new System.Drawing.Size(216, 188);
            this.dotCountPageTab.TabIndex = 0;
            this.dotCountPageTab.Text = "点検出";
            this.dotCountPageTab.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.zoomOutButton);
            this.panel1.Controls.Add(this.zoomInButton);
            this.panel1.Controls.Add(this.shownImageSelectCLB);
            this.panel1.Controls.Add(this.imageSaveButton);
            this.panel1.Location = new System.Drawing.Point(3, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 101);
            this.panel1.TabIndex = 1;
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Location = new System.Drawing.Point(136, 32);
            this.zoomOutButton.Name = "ZoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(75, 23);
            this.zoomOutButton.TabIndex = 7;
            this.zoomOutButton.Text = "縮小";
            this.zoomOutButton.UseVisualStyleBackColor = true;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Location = new System.Drawing.Point(136, 3);
            this.zoomInButton.Name = "ZoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(75, 23);
            this.zoomInButton.TabIndex = 6;
            this.zoomInButton.Text = "拡大";
            this.zoomInButton.UseVisualStyleBackColor = true;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // shownImageSelectCLB
            // 
            this.shownImageSelectCLB.FormattingEnabled = true;
            this.shownImageSelectCLB.Items.AddRange(new object[] {
            "画像範囲",
            "円",
            "粒子点"});
            this.shownImageSelectCLB.Location = new System.Drawing.Point(3, 3);
            this.shownImageSelectCLB.Name = "ShownImageSelectCLB";
            this.shownImageSelectCLB.Size = new System.Drawing.Size(120, 88);
            this.shownImageSelectCLB.TabIndex = 5;
            this.shownImageSelectCLB.SelectedIndexChanged += new System.EventHandler(this.shownImageSelectCLB_SelectedIndexChanged);
            // 
            // imageSaveButton
            // 
            this.imageSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSaveButton.Location = new System.Drawing.Point(136, 61);
            this.imageSaveButton.Name = "ImageSaveButton";
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
            this.Controls.Add(this.panel1);
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
            ((System.ComponentModel.ISupportInitialize)(this.lowerYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperXNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerXNumericUpDown)).EndInit();
            this.circleDrawPageTab.ResumeLayout(false);
            this.circleSelectPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.circleXNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleDiameterNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage imageSelectTabPage;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button fileSelectButton;
        private System.Windows.Forms.Button imageOpenButton;
        private System.Windows.Forms.TabPage circleDrawPageTab;
        private System.Windows.Forms.TabPage grainDetectTabPage;
        private System.Windows.Forms.TabPage dotCountPageTab;
        private System.Windows.Forms.Panel imageSelectPanel;
        private System.Windows.Forms.Panel rangeSelectPanel;
        private System.Windows.Forms.Label rangeXLabel;
        private System.Windows.Forms.Label rangeYLabel;
        private System.Windows.Forms.Label rangeXHyphenLabel;
        private System.Windows.Forms.Panel circleSelectPanel;
        private System.Windows.Forms.Label circleYLabel;
        private System.Windows.Forms.Label circleXLabel;
        private System.Windows.Forms.Label circleRadiusLabel;
        private System.Windows.Forms.Panel panel1;
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
    }
}

