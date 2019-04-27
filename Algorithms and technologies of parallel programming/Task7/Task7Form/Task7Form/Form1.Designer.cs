namespace Task7Form
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
            this.ButtonPath = new System.Windows.Forms.Button();
            this.ButtonStartStop = new System.Windows.Forms.Button();
            this.TextBoxPath = new System.Windows.Forms.TextBox();
            this.DataGridClient = new System.Windows.Forms.DataGridView();
            this.IPClient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridFile = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextBoxLog = new System.Windows.Forms.TextBox();
            this.buttonRemoveConnection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFile)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonPath
            // 
            this.ButtonPath.Location = new System.Drawing.Point(13, 13);
            this.ButtonPath.Name = "ButtonPath";
            this.ButtonPath.Size = new System.Drawing.Size(131, 23);
            this.ButtonPath.TabIndex = 0;
            this.ButtonPath.Text = "Path";
            this.ButtonPath.UseVisualStyleBackColor = true;
            this.ButtonPath.Click += new System.EventHandler(this.ButtonPath_Click);
            // 
            // ButtonStartStop
            // 
            this.ButtonStartStop.Location = new System.Drawing.Point(12, 43);
            this.ButtonStartStop.Name = "ButtonStartStop";
            this.ButtonStartStop.Size = new System.Drawing.Size(131, 23);
            this.ButtonStartStop.TabIndex = 1;
            this.ButtonStartStop.Text = "Start";
            this.ButtonStartStop.UseVisualStyleBackColor = true;
            this.ButtonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);
            // 
            // TextBoxPath
            // 
            this.TextBoxPath.Location = new System.Drawing.Point(150, 15);
            this.TextBoxPath.Name = "TextBoxPath";
            this.TextBoxPath.ReadOnly = true;
            this.TextBoxPath.Size = new System.Drawing.Size(362, 20);
            this.TextBoxPath.TabIndex = 2;
            // 
            // DataGridClient
            // 
            this.DataGridClient.AllowUserToAddRows = false;
            this.DataGridClient.AllowUserToDeleteRows = false;
            this.DataGridClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridClient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IPClient});
            this.DataGridClient.Location = new System.Drawing.Point(13, 72);
            this.DataGridClient.Name = "DataGridClient";
            this.DataGridClient.ReadOnly = true;
            this.DataGridClient.Size = new System.Drawing.Size(131, 175);
            this.DataGridClient.TabIndex = 3;
            // 
            // IPClient
            // 
            this.IPClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPClient.HeaderText = "IP Client";
            this.IPClient.Name = "IPClient";
            this.IPClient.ReadOnly = true;
            // 
            // DataGridFile
            // 
            this.DataGridFile.AllowUserToAddRows = false;
            this.DataGridFile.AllowUserToDeleteRows = false;
            this.DataGridFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.StatusFile});
            this.DataGridFile.Location = new System.Drawing.Point(150, 43);
            this.DataGridFile.Name = "DataGridFile";
            this.DataGridFile.ReadOnly = true;
            this.DataGridFile.Size = new System.Drawing.Size(361, 234);
            this.DataGridFile.TabIndex = 4;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileName.HeaderText = "File";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // StatusFile
            // 
            this.StatusFile.HeaderText = "Status";
            this.StatusFile.Name = "StatusFile";
            this.StatusFile.ReadOnly = true;
            // 
            // TextBoxLog
            // 
            this.TextBoxLog.Location = new System.Drawing.Point(13, 283);
            this.TextBoxLog.Multiline = true;
            this.TextBoxLog.Name = "TextBoxLog";
            this.TextBoxLog.ReadOnly = true;
            this.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxLog.Size = new System.Drawing.Size(499, 238);
            this.TextBoxLog.TabIndex = 5;
            // 
            // buttonRemoveConnection
            // 
            this.buttonRemoveConnection.Location = new System.Drawing.Point(13, 253);
            this.buttonRemoveConnection.Name = "buttonRemoveConnection";
            this.buttonRemoveConnection.Size = new System.Drawing.Size(131, 23);
            this.buttonRemoveConnection.TabIndex = 6;
            this.buttonRemoveConnection.Text = "Remove Connection";
            this.buttonRemoveConnection.UseVisualStyleBackColor = true;
            this.buttonRemoveConnection.Click += new System.EventHandler(this.ButtonRemoveConnection_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 533);
            this.Controls.Add(this.buttonRemoveConnection);
            this.Controls.Add(this.TextBoxLog);
            this.Controls.Add(this.DataGridFile);
            this.Controls.Add(this.DataGridClient);
            this.Controls.Add(this.TextBoxPath);
            this.Controls.Add(this.ButtonStartStop);
            this.Controls.Add(this.ButtonPath);
            this.Name = "Form1";
            this.Text = "Лабораторная работа №7 Сервер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonPath;
        private System.Windows.Forms.Button ButtonStartStop;
        private System.Windows.Forms.TextBox TextBoxPath;
        private System.Windows.Forms.DataGridView DataGridClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPClient;
        private System.Windows.Forms.DataGridView DataGridFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusFile;
        private System.Windows.Forms.TextBox TextBoxLog;
        private System.Windows.Forms.Button buttonRemoveConnection;
    }
}

