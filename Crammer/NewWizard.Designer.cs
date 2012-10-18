namespace MB.Crammer
{
    partial class NewWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTitle = new System.Windows.Forms.TabPage();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.grpDividers = new System.Windows.Forms.GroupBox();
            this.txtDivider = new System.Windows.Forms.TextBox();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbCSV = new System.Windows.Forms.RadioButton();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.chkImport = new System.Windows.Forms.CheckBox();
            this.txtImportFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPath = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdFolderBrowse = new System.Windows.Forms.Button();
            this.txtDictPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdFinish = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTitle.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.grpDividers.SuspendLayout();
            this.tabPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer1.Panel2.Controls.Add(this.cmdFinish);
            this.splitContainer1.Panel2.Controls.Add(this.cmdNext);
            this.splitContainer1.Panel2.Controls.Add(this.cmdBack);
            this.splitContainer1.Size = new System.Drawing.Size(553, 271);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTitle);
            this.tabControl1.Controls.Add(this.tabImport);
            this.tabControl1.Controls.Add(this.tabPath);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(553, 228);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabTitle
            // 
            this.tabTitle.Controls.Add(this.txtTitle);
            this.tabTitle.Controls.Add(this.label2);
            this.tabTitle.Controls.Add(this.label1);
            this.tabTitle.Location = new System.Drawing.Point(4, 22);
            this.tabTitle.Name = "tabTitle";
            this.tabTitle.Padding = new System.Windows.Forms.Padding(3);
            this.tabTitle.Size = new System.Drawing.Size(545, 202);
            this.tabTitle.TabIndex = 0;
            this.tabTitle.Text = "Title";
            this.tabTitle.UseVisualStyleBackColor = true;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(64, 91);
            this.txtTitle.MaxLength = 100;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(458, 20);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Title: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dictionary Title";
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.grpDividers);
            this.tabImport.Controls.Add(this.cmdBrowse);
            this.tabImport.Controls.Add(this.chkImport);
            this.tabImport.Controls.Add(this.txtImportFile);
            this.tabImport.Controls.Add(this.label4);
            this.tabImport.Controls.Add(this.label3);
            this.tabImport.Location = new System.Drawing.Point(4, 22);
            this.tabImport.Name = "tabImport";
            this.tabImport.Padding = new System.Windows.Forms.Padding(3);
            this.tabImport.Size = new System.Drawing.Size(545, 202);
            this.tabImport.TabIndex = 1;
            this.tabImport.Text = "Import";
            this.tabImport.UseVisualStyleBackColor = true;
            // 
            // grpDividers
            // 
            this.grpDividers.Controls.Add(this.txtDivider);
            this.grpDividers.Controls.Add(this.rbCustom);
            this.grpDividers.Controls.Add(this.rbCSV);
            this.grpDividers.Enabled = false;
            this.grpDividers.Location = new System.Drawing.Point(15, 120);
            this.grpDividers.Name = "grpDividers";
            this.grpDividers.Size = new System.Drawing.Size(268, 53);
            this.grpDividers.TabIndex = 7;
            this.grpDividers.TabStop = false;
            this.grpDividers.Text = "Separators";
            // 
            // txtDivider
            // 
            this.txtDivider.Enabled = false;
            this.txtDivider.Location = new System.Drawing.Point(141, 20);
            this.txtDivider.MaxLength = 10;
            this.txtDivider.Name = "txtDivider";
            this.txtDivider.Size = new System.Drawing.Size(100, 20);
            this.txtDivider.TabIndex = 2;
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(74, 20);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(60, 17);
            this.rbCustom.TabIndex = 1;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // rbCSV
            // 
            this.rbCSV.AutoSize = true;
            this.rbCSV.Checked = true;
            this.rbCSV.Location = new System.Drawing.Point(22, 20);
            this.rbCSV.Name = "rbCSV";
            this.rbCSV.Size = new System.Drawing.Size(46, 17);
            this.rbCSV.TabIndex = 0;
            this.rbCSV.TabStop = true;
            this.rbCSV.Text = "CSV";
            this.rbCSV.UseVisualStyleBackColor = true;
            this.rbCSV.CheckedChanged += new System.EventHandler(this.rbCSV_CheckedChanged);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Enabled = false;
            this.cmdBrowse.Location = new System.Drawing.Point(494, 92);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(25, 23);
            this.cmdBrowse.TabIndex = 5;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // chkImport
            // 
            this.chkImport.AutoSize = true;
            this.chkImport.Location = new System.Drawing.Point(15, 71);
            this.chkImport.Name = "chkImport";
            this.chkImport.Size = new System.Drawing.Size(139, 17);
            this.chkImport.TabIndex = 4;
            this.chkImport.Text = "Yes I have an import file";
            this.chkImport.UseVisualStyleBackColor = true;
            this.chkImport.CheckedChanged += new System.EventHandler(this.chkImport_CheckedChanged);
            // 
            // txtImportFile
            // 
            this.txtImportFile.Enabled = false;
            this.txtImportFile.Location = new System.Drawing.Point(15, 94);
            this.txtImportFile.Name = "txtImportFile";
            this.txtImportFile.Size = new System.Drawing.Size(478, 20);
            this.txtImportFile.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(326, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Optional choice if you don\'t want to start adding entries from scratch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Text File Import";
            // 
            // tabPath
            // 
            this.tabPath.Controls.Add(this.label6);
            this.tabPath.Controls.Add(this.cmdFolderBrowse);
            this.tabPath.Controls.Add(this.txtDictPath);
            this.tabPath.Controls.Add(this.label5);
            this.tabPath.Location = new System.Drawing.Point(4, 22);
            this.tabPath.Name = "tabPath";
            this.tabPath.Size = new System.Drawing.Size(545, 202);
            this.tabPath.TabIndex = 2;
            this.tabPath.Text = "Path";
            this.tabPath.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Path: ";
            // 
            // cmdFolderBrowse
            // 
            this.cmdFolderBrowse.Location = new System.Drawing.Point(503, 93);
            this.cmdFolderBrowse.Name = "cmdFolderBrowse";
            this.cmdFolderBrowse.Size = new System.Drawing.Size(27, 20);
            this.cmdFolderBrowse.TabIndex = 4;
            this.cmdFolderBrowse.Text = "...";
            this.cmdFolderBrowse.UseVisualStyleBackColor = true;
            this.cmdFolderBrowse.Click += new System.EventHandler(this.cmdFolderBrowse_Click);
            // 
            // txtDictPath
            // 
            this.txtDictPath.Location = new System.Drawing.Point(47, 93);
            this.txtDictPath.Name = "txtDictPath";
            this.txtDictPath.Size = new System.Drawing.Size(455, 20);
            this.txtDictPath.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Dictionary Path";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(466, 9);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdFinish
            // 
            this.cmdFinish.Location = new System.Drawing.Point(279, 9);
            this.cmdFinish.Name = "cmdFinish";
            this.cmdFinish.Size = new System.Drawing.Size(75, 23);
            this.cmdFinish.TabIndex = 0;
            this.cmdFinish.Text = "Finish";
            this.cmdFinish.UseVisualStyleBackColor = true;
            this.cmdFinish.Visible = false;
            this.cmdFinish.Click += new System.EventHandler(this.cmdFinish_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(279, 9);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 1;
            this.cmdNext.Text = "Next >";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.Enabled = false;
            this.cmdBack.Location = new System.Drawing.Point(198, 9);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(75, 23);
            this.cmdBack.TabIndex = 0;
            this.cmdBack.Text = "< Back";
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NewWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 271);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "NewWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewWizard";
            this.Load += new System.EventHandler(this.NewWizard_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabTitle.ResumeLayout(false);
            this.tabTitle.PerformLayout();
            this.tabImport.ResumeLayout(false);
            this.tabImport.PerformLayout();
            this.grpDividers.ResumeLayout(false);
            this.grpDividers.PerformLayout();
            this.tabPath.ResumeLayout(false);
            this.tabPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTitle;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.TabPage tabPath;
        private System.Windows.Forms.Button cmdFinish;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkImport;
        private System.Windows.Forms.TextBox txtImportFile;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox grpDividers;
        private System.Windows.Forms.TextBox txtDivider;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbCSV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDictPath;
        private System.Windows.Forms.Button cmdFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
    }
}