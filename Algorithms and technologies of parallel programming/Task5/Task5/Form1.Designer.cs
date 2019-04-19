namespace Task5
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageFilter = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FilterPreviewPictureAfter = new System.Windows.Forms.PictureBox();
            this.FilterPreviewPictureBefore = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FilterMatrixDataGrid = new System.Windows.Forms.DataGridView();
            this.FilterNextButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.FilterFiltersComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.FilterOffsetNUD = new System.Windows.Forms.NumericUpDown();
            this.FilterDivTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FilterPowerMNUP = new System.Windows.Forms.NumericUpDown();
            this.FilterPowerNNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TabPageImageSelection = new System.Windows.Forms.TabPage();
            this.SettingsBackBTN = new System.Windows.Forms.Button();
            this.SettingsNextBTN = new System.Windows.Forms.Button();
            this.SettingsPreviewPanel = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SettingsThreadCountNUD = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.SettingsEditedPathBttn = new System.Windows.Forms.Button();
            this.SettingsEditedPathTB = new System.Windows.Forms.TextBox();
            this.SettingsImagesPathBttn = new System.Windows.Forms.Button();
            this.SettingsImagesPathTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TabPageProcessing = new System.Windows.Forms.TabPage();
            this.ProcessingBackBTN = new System.Windows.Forms.Button();
            this.ProcessingStartStopBTN = new System.Windows.Forms.Button();
            this.ProcessingPreview = new System.Windows.Forms.GroupBox();
            this.ProcessingPictureBoxAfter = new System.Windows.Forms.PictureBox();
            this.ProcessingPictureBoxBefor = new System.Windows.Forms.PictureBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ProcessingTotalEditingTimeTB = new System.Windows.Forms.TextBox();
            this.ProcessingAvgEditingTimeTB = new System.Windows.Forms.TextBox();
            this.ProcessingEditingImageTB = new System.Windows.Forms.TextBox();
            this.ProcessingImagesLeftTB = new System.Windows.Forms.TextBox();
            this.ProcessingImagesEditedTB = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.TabPageFilter.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPreviewPictureAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPreviewPictureBefore)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterMatrixDataGrid)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterOffsetNUD)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPowerMNUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPowerNNUD)).BeginInit();
            this.TabPageImageSelection.SuspendLayout();
            this.SettingsPreviewPanel.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsThreadCountNUD)).BeginInit();
            this.TabPageProcessing.SuspendLayout();
            this.ProcessingPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessingPictureBoxAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessingPictureBoxBefor)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabPageFilter);
            this.TabControl.Controls.Add(this.TabPageImageSelection);
            this.TabControl.Controls.Add(this.TabPageProcessing);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(553, 451);
            this.TabControl.TabIndex = 0;
            // 
            // TabPageFilter
            // 
            this.TabPageFilter.Controls.Add(this.groupBox2);
            this.TabPageFilter.Controls.Add(this.groupBox1);
            this.TabPageFilter.Controls.Add(this.FilterNextButton);
            this.TabPageFilter.Controls.Add(this.groupBox7);
            this.TabPageFilter.Controls.Add(this.groupBox4);
            this.TabPageFilter.Controls.Add(this.groupBox3);
            this.TabPageFilter.Location = new System.Drawing.Point(4, 22);
            this.TabPageFilter.Name = "TabPageFilter";
            this.TabPageFilter.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageFilter.Size = new System.Drawing.Size(545, 425);
            this.TabPageFilter.TabIndex = 0;
            this.TabPageFilter.Text = "Filter";
            this.TabPageFilter.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FilterPreviewPictureAfter);
            this.groupBox2.Controls.Add(this.FilterPreviewPictureBefore);
            this.groupBox2.Location = new System.Drawing.Point(369, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 377);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Preview";
            // 
            // FilterPreviewPictureAfter
            // 
            this.FilterPreviewPictureAfter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FilterPreviewPictureAfter.Location = new System.Drawing.Point(9, 214);
            this.FilterPreviewPictureAfter.Name = "FilterPreviewPictureAfter";
            this.FilterPreviewPictureAfter.Size = new System.Drawing.Size(150, 150);
            this.FilterPreviewPictureAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FilterPreviewPictureAfter.TabIndex = 19;
            this.FilterPreviewPictureAfter.TabStop = false;
            // 
            // FilterPreviewPictureBefore
            // 
            this.FilterPreviewPictureBefore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FilterPreviewPictureBefore.Location = new System.Drawing.Point(9, 38);
            this.FilterPreviewPictureBefore.Name = "FilterPreviewPictureBefore";
            this.FilterPreviewPictureBefore.Size = new System.Drawing.Size(150, 150);
            this.FilterPreviewPictureBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FilterPreviewPictureBefore.TabIndex = 18;
            this.FilterPreviewPictureBefore.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FilterMatrixDataGrid);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 300);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Matrix";
            // 
            // FilterMatrixDataGrid
            // 
            this.FilterMatrixDataGrid.AllowUserToAddRows = false;
            this.FilterMatrixDataGrid.AllowUserToDeleteRows = false;
            this.FilterMatrixDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FilterMatrixDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.FilterMatrixDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FilterMatrixDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterMatrixDataGrid.Location = new System.Drawing.Point(3, 16);
            this.FilterMatrixDataGrid.Name = "FilterMatrixDataGrid";
            this.FilterMatrixDataGrid.Size = new System.Drawing.Size(352, 281);
            this.FilterMatrixDataGrid.TabIndex = 7;
            this.FilterMatrixDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.FilterMatrixDataGrid_CellValueChanged);
            // 
            // FilterNextButton
            // 
            this.FilterNextButton.Location = new System.Drawing.Point(370, 390);
            this.FilterNextButton.Name = "FilterNextButton";
            this.FilterNextButton.Size = new System.Drawing.Size(169, 27);
            this.FilterNextButton.TabIndex = 18;
            this.FilterNextButton.Text = "Next";
            this.FilterNextButton.UseVisualStyleBackColor = true;
            this.FilterNextButton.Click += new System.EventHandler(this.FilterNextButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.FilterFiltersComboBox);
            this.groupBox7.Location = new System.Drawing.Point(8, 361);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(354, 56);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Filters";
            // 
            // FilterFiltersComboBox
            // 
            this.FilterFiltersComboBox.FormattingEnabled = true;
            this.FilterFiltersComboBox.Items.AddRange(new object[] {
            "Gauss",
            "Sharp",
            "Random"});
            this.FilterFiltersComboBox.Location = new System.Drawing.Point(7, 18);
            this.FilterFiltersComboBox.Name = "FilterFiltersComboBox";
            this.FilterFiltersComboBox.Size = new System.Drawing.Size(341, 21);
            this.FilterFiltersComboBox.TabIndex = 12;
            this.FilterFiltersComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterFiltersComboBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.FilterOffsetNUD);
            this.groupBox4.Controls.Add(this.FilterDivTextBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(190, 313);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(172, 42);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Сoefficients";
            // 
            // FilterOffsetNUD
            // 
            this.FilterOffsetNUD.Location = new System.Drawing.Point(124, 13);
            this.FilterOffsetNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FilterOffsetNUD.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.FilterOffsetNUD.Name = "FilterOffsetNUD";
            this.FilterOffsetNUD.Size = new System.Drawing.Size(42, 20);
            this.FilterOffsetNUD.TabIndex = 13;
            this.FilterOffsetNUD.ValueChanged += new System.EventHandler(this.FilterOffsetNUD_ValueChanged);
            // 
            // FilterDivTextBox
            // 
            this.FilterDivTextBox.Location = new System.Drawing.Point(38, 13);
            this.FilterDivTextBox.Name = "FilterDivTextBox";
            this.FilterDivTextBox.ReadOnly = true;
            this.FilterDivTextBox.Size = new System.Drawing.Size(41, 20);
            this.FilterDivTextBox.TabIndex = 12;
            this.FilterDivTextBox.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Div = ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Off = ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FilterPowerMNUP);
            this.groupBox3.Controls.Add(this.FilterPowerNNUD);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(8, 313);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 42);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Power";
            // 
            // FilterPowerMNUP
            // 
            this.FilterPowerMNUP.Location = new System.Drawing.Point(110, 14);
            this.FilterPowerMNUP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilterPowerMNUP.Name = "FilterPowerMNUP";
            this.FilterPowerMNUP.Size = new System.Drawing.Size(40, 20);
            this.FilterPowerMNUP.TabIndex = 10;
            this.FilterPowerMNUP.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.FilterPowerMNUP.ValueChanged += new System.EventHandler(this.FilterPowerNNUD_ValueChanged);
            // 
            // FilterPowerNNUD
            // 
            this.FilterPowerNNUD.Location = new System.Drawing.Point(30, 14);
            this.FilterPowerNNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilterPowerNNUD.Name = "FilterPowerNNUD";
            this.FilterPowerNNUD.Size = new System.Drawing.Size(40, 20);
            this.FilterPowerNNUD.TabIndex = 9;
            this.FilterPowerNNUD.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.FilterPowerNNUD.ValueChanged += new System.EventHandler(this.FilterPowerNNUD_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "N = ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "M = ";
            // 
            // TabPageImageSelection
            // 
            this.TabPageImageSelection.Controls.Add(this.SettingsBackBTN);
            this.TabPageImageSelection.Controls.Add(this.SettingsNextBTN);
            this.TabPageImageSelection.Controls.Add(this.SettingsPreviewPanel);
            this.TabPageImageSelection.Controls.Add(this.groupBox5);
            this.TabPageImageSelection.Location = new System.Drawing.Point(4, 22);
            this.TabPageImageSelection.Name = "TabPageImageSelection";
            this.TabPageImageSelection.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageImageSelection.Size = new System.Drawing.Size(545, 425);
            this.TabPageImageSelection.TabIndex = 1;
            this.TabPageImageSelection.Text = "Settings";
            this.TabPageImageSelection.UseVisualStyleBackColor = true;
            // 
            // SettingsBackBTN
            // 
            this.SettingsBackBTN.Location = new System.Drawing.Point(6, 392);
            this.SettingsBackBTN.Name = "SettingsBackBTN";
            this.SettingsBackBTN.Size = new System.Drawing.Size(169, 27);
            this.SettingsBackBTN.TabIndex = 20;
            this.SettingsBackBTN.Text = "Back";
            this.SettingsBackBTN.UseVisualStyleBackColor = true;
            this.SettingsBackBTN.Click += new System.EventHandler(this.SettingsBackBTN_Click);
            // 
            // SettingsNextBTN
            // 
            this.SettingsNextBTN.Location = new System.Drawing.Point(368, 392);
            this.SettingsNextBTN.Name = "SettingsNextBTN";
            this.SettingsNextBTN.Size = new System.Drawing.Size(169, 27);
            this.SettingsNextBTN.TabIndex = 19;
            this.SettingsNextBTN.Text = "Next";
            this.SettingsNextBTN.UseVisualStyleBackColor = true;
            this.SettingsNextBTN.Click += new System.EventHandler(this.SettingsNextBTN_Click);
            // 
            // SettingsPreviewPanel
            // 
            this.SettingsPreviewPanel.Controls.Add(this.panel1);
            this.SettingsPreviewPanel.Location = new System.Drawing.Point(8, 113);
            this.SettingsPreviewPanel.Name = "SettingsPreviewPanel";
            this.SettingsPreviewPanel.Size = new System.Drawing.Size(529, 269);
            this.SettingsPreviewPanel.TabIndex = 5;
            this.SettingsPreviewPanel.TabStop = false;
            this.SettingsPreviewPanel.Text = "Preview";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 250);
            this.panel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.SettingsThreadCountNUD);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.SettingsEditedPathBttn);
            this.groupBox5.Controls.Add(this.SettingsEditedPathTB);
            this.groupBox5.Controls.Add(this.SettingsImagesPathBttn);
            this.groupBox5.Controls.Add(this.SettingsImagesPathTB);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(8, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(529, 101);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Directories";
            // 
            // SettingsThreadCountNUD
            // 
            this.SettingsThreadCountNUD.Location = new System.Drawing.Point(80, 74);
            this.SettingsThreadCountNUD.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.SettingsThreadCountNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SettingsThreadCountNUD.Name = "SettingsThreadCountNUD";
            this.SettingsThreadCountNUD.Size = new System.Drawing.Size(40, 20);
            this.SettingsThreadCountNUD.TabIndex = 13;
            this.SettingsThreadCountNUD.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.SettingsThreadCountNUD.ValueChanged += new System.EventHandler(this.SettingsThreadCountNUD_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Thr. count = ";
            // 
            // SettingsEditedPathBttn
            // 
            this.SettingsEditedPathBttn.Location = new System.Drawing.Point(504, 46);
            this.SettingsEditedPathBttn.Name = "SettingsEditedPathBttn";
            this.SettingsEditedPathBttn.Size = new System.Drawing.Size(25, 20);
            this.SettingsEditedPathBttn.TabIndex = 8;
            this.SettingsEditedPathBttn.Text = "..";
            this.SettingsEditedPathBttn.UseVisualStyleBackColor = true;
            this.SettingsEditedPathBttn.Click += new System.EventHandler(this.SettingsImagesPathBttn_Click);
            // 
            // SettingsEditedPathTB
            // 
            this.SettingsEditedPathTB.Location = new System.Drawing.Point(80, 46);
            this.SettingsEditedPathTB.Name = "SettingsEditedPathTB";
            this.SettingsEditedPathTB.ReadOnly = true;
            this.SettingsEditedPathTB.Size = new System.Drawing.Size(420, 20);
            this.SettingsEditedPathTB.TabIndex = 7;
            // 
            // SettingsImagesPathBttn
            // 
            this.SettingsImagesPathBttn.Location = new System.Drawing.Point(504, 16);
            this.SettingsImagesPathBttn.Name = "SettingsImagesPathBttn";
            this.SettingsImagesPathBttn.Size = new System.Drawing.Size(25, 20);
            this.SettingsImagesPathBttn.TabIndex = 6;
            this.SettingsImagesPathBttn.Text = "..";
            this.SettingsImagesPathBttn.UseVisualStyleBackColor = true;
            this.SettingsImagesPathBttn.Click += new System.EventHandler(this.SettingsImagesPathBttn_Click);
            // 
            // SettingsImagesPathTB
            // 
            this.SettingsImagesPathTB.Location = new System.Drawing.Point(80, 16);
            this.SettingsImagesPathTB.Name = "SettingsImagesPathTB";
            this.SettingsImagesPathTB.ReadOnly = true;
            this.SettingsImagesPathTB.Size = new System.Drawing.Size(420, 20);
            this.SettingsImagesPathTB.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Edited path = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Images path = ";
            // 
            // TabPageProcessing
            // 
            this.TabPageProcessing.Controls.Add(this.ProcessingBackBTN);
            this.TabPageProcessing.Controls.Add(this.ProcessingStartStopBTN);
            this.TabPageProcessing.Controls.Add(this.ProcessingPreview);
            this.TabPageProcessing.Controls.Add(this.groupBox8);
            this.TabPageProcessing.Location = new System.Drawing.Point(4, 22);
            this.TabPageProcessing.Name = "TabPageProcessing";
            this.TabPageProcessing.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageProcessing.Size = new System.Drawing.Size(545, 425);
            this.TabPageProcessing.TabIndex = 2;
            this.TabPageProcessing.Text = "Processing";
            this.TabPageProcessing.UseVisualStyleBackColor = true;
            // 
            // ProcessingBackBTN
            // 
            this.ProcessingBackBTN.Location = new System.Drawing.Point(8, 394);
            this.ProcessingBackBTN.Name = "ProcessingBackBTN";
            this.ProcessingBackBTN.Size = new System.Drawing.Size(100, 23);
            this.ProcessingBackBTN.TabIndex = 23;
            this.ProcessingBackBTN.Text = "Back";
            this.ProcessingBackBTN.UseVisualStyleBackColor = true;
            this.ProcessingBackBTN.Click += new System.EventHandler(this.ProcessingBackBTN_Click);
            // 
            // ProcessingStartStopBTN
            // 
            this.ProcessingStartStopBTN.Location = new System.Drawing.Point(8, 190);
            this.ProcessingStartStopBTN.Name = "ProcessingStartStopBTN";
            this.ProcessingStartStopBTN.Size = new System.Drawing.Size(100, 23);
            this.ProcessingStartStopBTN.TabIndex = 22;
            this.ProcessingStartStopBTN.Text = "Start";
            this.ProcessingStartStopBTN.UseVisualStyleBackColor = true;
            this.ProcessingStartStopBTN.Click += new System.EventHandler(this.ProcesingStartStopBTN_Click);
            // 
            // ProcessingPreview
            // 
            this.ProcessingPreview.Controls.Add(this.ProcessingPictureBoxAfter);
            this.ProcessingPreview.Controls.Add(this.ProcessingPictureBoxBefor);
            this.ProcessingPreview.Location = new System.Drawing.Point(109, 189);
            this.ProcessingPreview.Name = "ProcessingPreview";
            this.ProcessingPreview.Size = new System.Drawing.Size(430, 228);
            this.ProcessingPreview.TabIndex = 21;
            this.ProcessingPreview.TabStop = false;
            this.ProcessingPreview.Text = "Preview";
            // 
            // ProcessingPictureBoxAfter
            // 
            this.ProcessingPictureBoxAfter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ProcessingPictureBoxAfter.Location = new System.Drawing.Point(221, 19);
            this.ProcessingPictureBoxAfter.Name = "ProcessingPictureBoxAfter";
            this.ProcessingPictureBoxAfter.Size = new System.Drawing.Size(203, 203);
            this.ProcessingPictureBoxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProcessingPictureBoxAfter.TabIndex = 19;
            this.ProcessingPictureBoxAfter.TabStop = false;
            // 
            // ProcessingPictureBoxBefor
            // 
            this.ProcessingPictureBoxBefor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ProcessingPictureBoxBefor.Location = new System.Drawing.Point(12, 19);
            this.ProcessingPictureBoxBefor.Name = "ProcessingPictureBoxBefor";
            this.ProcessingPictureBoxBefor.Size = new System.Drawing.Size(203, 203);
            this.ProcessingPictureBoxBefor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProcessingPictureBoxBefor.TabIndex = 18;
            this.ProcessingPictureBoxBefor.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ProcessingTotalEditingTimeTB);
            this.groupBox8.Controls.Add(this.ProcessingAvgEditingTimeTB);
            this.groupBox8.Controls.Add(this.ProcessingEditingImageTB);
            this.groupBox8.Controls.Add(this.ProcessingImagesLeftTB);
            this.groupBox8.Controls.Add(this.ProcessingImagesEditedTB);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(8, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(531, 177);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Info";
            // 
            // ProcessingTotalEditingTimeTB
            // 
            this.ProcessingTotalEditingTimeTB.Location = new System.Drawing.Point(111, 143);
            this.ProcessingTotalEditingTimeTB.Name = "ProcessingTotalEditingTimeTB";
            this.ProcessingTotalEditingTimeTB.ReadOnly = true;
            this.ProcessingTotalEditingTimeTB.Size = new System.Drawing.Size(414, 20);
            this.ProcessingTotalEditingTimeTB.TabIndex = 13;
            // 
            // ProcessingAvgEditingTimeTB
            // 
            this.ProcessingAvgEditingTimeTB.Location = new System.Drawing.Point(111, 113);
            this.ProcessingAvgEditingTimeTB.Name = "ProcessingAvgEditingTimeTB";
            this.ProcessingAvgEditingTimeTB.ReadOnly = true;
            this.ProcessingAvgEditingTimeTB.Size = new System.Drawing.Size(414, 20);
            this.ProcessingAvgEditingTimeTB.TabIndex = 12;
            // 
            // ProcessingEditingImageTB
            // 
            this.ProcessingEditingImageTB.Location = new System.Drawing.Point(111, 83);
            this.ProcessingEditingImageTB.Name = "ProcessingEditingImageTB";
            this.ProcessingEditingImageTB.ReadOnly = true;
            this.ProcessingEditingImageTB.Size = new System.Drawing.Size(414, 20);
            this.ProcessingEditingImageTB.TabIndex = 11;
            // 
            // ProcessingImagesLeftTB
            // 
            this.ProcessingImagesLeftTB.Location = new System.Drawing.Point(111, 53);
            this.ProcessingImagesLeftTB.Name = "ProcessingImagesLeftTB";
            this.ProcessingImagesLeftTB.ReadOnly = true;
            this.ProcessingImagesLeftTB.Size = new System.Drawing.Size(414, 20);
            this.ProcessingImagesLeftTB.TabIndex = 10;
            // 
            // ProcessingImagesEditedTB
            // 
            this.ProcessingImagesEditedTB.Location = new System.Drawing.Point(111, 23);
            this.ProcessingImagesEditedTB.Name = "ProcessingImagesEditedTB";
            this.ProcessingImagesEditedTB.ReadOnly = true;
            this.ProcessingImagesEditedTB.Size = new System.Drawing.Size(414, 20);
            this.ProcessingImagesEditedTB.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 146);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Total editing time =";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Avg. editing time =";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Editing image =";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Images left =";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Images edited =";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 451);
            this.Controls.Add(this.TabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl.ResumeLayout(false);
            this.TabPageFilter.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FilterPreviewPictureAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPreviewPictureBefore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FilterMatrixDataGrid)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterOffsetNUD)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPowerMNUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterPowerNNUD)).EndInit();
            this.TabPageImageSelection.ResumeLayout(false);
            this.SettingsPreviewPanel.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsThreadCountNUD)).EndInit();
            this.TabPageProcessing.ResumeLayout(false);
            this.ProcessingPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessingPictureBoxAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessingPictureBoxBefor)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox FilterPreviewPictureAfter;
        private System.Windows.Forms.PictureBox FilterPreviewPictureBefore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView FilterMatrixDataGrid;
        private System.Windows.Forms.Button FilterNextButton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox FilterFiltersComboBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox FilterDivTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown FilterPowerMNUP;
        private System.Windows.Forms.NumericUpDown FilterPowerNNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage TabPageImageSelection;
        private System.Windows.Forms.TabPage TabPageProcessing;
        private System.Windows.Forms.GroupBox SettingsPreviewPanel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button SettingsEditedPathBttn;
        private System.Windows.Forms.TextBox SettingsEditedPathTB;
        private System.Windows.Forms.Button SettingsImagesPathBttn;
        private System.Windows.Forms.TextBox SettingsImagesPathTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SettingsBackBTN;
        private System.Windows.Forms.Button SettingsNextBTN;
        private System.Windows.Forms.NumericUpDown SettingsThreadCountNUD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox ProcessingPreview;
        private System.Windows.Forms.PictureBox ProcessingPictureBoxAfter;
        private System.Windows.Forms.PictureBox ProcessingPictureBoxBefor;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox ProcessingTotalEditingTimeTB;
        private System.Windows.Forms.TextBox ProcessingAvgEditingTimeTB;
        private System.Windows.Forms.TextBox ProcessingEditingImageTB;
        private System.Windows.Forms.TextBox ProcessingImagesLeftTB;
        private System.Windows.Forms.TextBox ProcessingImagesEditedTB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown FilterOffsetNUD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ProcessingBackBTN;
        private System.Windows.Forms.Button ProcessingStartStopBTN;
    }
}

