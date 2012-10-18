namespace MB.Crammer
{
    partial class ManageDictionary
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDictionary));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTxtAFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTxtBFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnClearFilters = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.First = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timestampEntryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.activateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.allActiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.findDuplicatesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAddEntry = new System.Windows.Forms.Button();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(492, 522);
            this.splitContainer1.SplitterDistance = 39;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripTxtAFilter,
            this.toolStripLabel1,
            this.toolStripTxtBFilter,
            this.toolStripSeparator1,
            this.toolStripBtnClearFilters});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(492, 31);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 28);
            this.toolStripLabel2.Text = "Filters:";
            // 
            // toolStripTxtAFilter
            // 
            this.toolStripTxtAFilter.MaxLength = 100;
            this.toolStripTxtAFilter.Name = "toolStripTxtAFilter";
            this.toolStripTxtAFilter.Size = new System.Drawing.Size(185, 31);
            this.toolStripTxtAFilter.TextChanged += new System.EventHandler(this.toolStripTxtAFilter_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(12, 28);
            this.toolStripLabel1.Text = "/";
            // 
            // toolStripTxtBFilter
            // 
            this.toolStripTxtBFilter.MaxLength = 100;
            this.toolStripTxtBFilter.Name = "toolStripTxtBFilter";
            this.toolStripTxtBFilter.Size = new System.Drawing.Size(185, 31);
            this.toolStripTxtBFilter.TextChanged += new System.EventHandler(this.toolStripTxtBFilter_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripBtnClearFilters
            // 
            this.toolStripBtnClearFilters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnClearFilters.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnClearFilters.Image")));
            this.toolStripBtnClearFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnClearFilters.Name = "toolStripBtnClearFilters";
            this.toolStripBtnClearFilters.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtnClearFilters.Text = "toolStripButton1";
            this.toolStripBtnClearFilters.Click += new System.EventHandler(this.toolStripBtnClearFilters_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cmdAddEntry);
            this.splitContainer2.Panel2.Controls.Add(this.txtSecond);
            this.splitContainer2.Panel2.Controls.Add(this.txtFirst);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer2.Size = new System.Drawing.Size(492, 479);
            this.splitContainer2.SplitterDistance = 412;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.First,
            this.Second,
            this.Active,
            this.Index});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(492, 412);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // First
            // 
            this.First.HeaderText = "First";
            this.First.Name = "First";
            // 
            // Second
            // 
            this.Second.HeaderText = "Second";
            this.Second.Name = "Second";
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Index
            // 
            this.Index.HeaderText = "Column1";
            this.Index.Name = "Index";
            this.Index.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timestampEntryMenuItem,
            this.toolStripSeparator2,
            this.activateMenuItem,
            this.deactivateMenuItem,
            this.toolStripSeparator3,
            this.allActiveMenuItem,
            this.deactivateAllMenuItem,
            this.toolStripSeparator4,
            this.removeMenuItem,
            this.toolStripSeparator5,
            this.findDuplicatesMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 182);
            // 
            // timestampEntryMenuItem
            // 
            this.timestampEntryMenuItem.Name = "timestampEntryMenuItem";
            this.timestampEntryMenuItem.Size = new System.Drawing.Size(155, 22);
            this.timestampEntryMenuItem.Text = "Timestamp";
            this.timestampEntryMenuItem.Click += new System.EventHandler(this.timestampEntryMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // activateMenuItem
            // 
            this.activateMenuItem.Name = "activateMenuItem";
            this.activateMenuItem.Size = new System.Drawing.Size(155, 22);
            this.activateMenuItem.Text = "Activate";
            this.activateMenuItem.Click += new System.EventHandler(this.activateMenuItem_Click);
            // 
            // deactivateMenuItem
            // 
            this.deactivateMenuItem.Name = "deactivateMenuItem";
            this.deactivateMenuItem.Size = new System.Drawing.Size(155, 22);
            this.deactivateMenuItem.Text = "Deactivate";
            this.deactivateMenuItem.Click += new System.EventHandler(this.deactivateMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // allActiveMenuItem
            // 
            this.allActiveMenuItem.Name = "allActiveMenuItem";
            this.allActiveMenuItem.Size = new System.Drawing.Size(155, 22);
            this.allActiveMenuItem.Text = "Activate All";
            this.allActiveMenuItem.Click += new System.EventHandler(this.allActiveMenuItem_Click);
            // 
            // deactivateAllMenuItem
            // 
            this.deactivateAllMenuItem.Name = "deactivateAllMenuItem";
            this.deactivateAllMenuItem.Size = new System.Drawing.Size(155, 22);
            this.deactivateAllMenuItem.Text = "Deactivate All";
            this.deactivateAllMenuItem.Click += new System.EventHandler(this.deactivateAllMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(152, 6);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Name = "removeMenuItem";
            this.removeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeMenuItem.Size = new System.Drawing.Size(155, 22);
            this.removeMenuItem.Text = "Remove";
            this.removeMenuItem.Click += new System.EventHandler(this.removeMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // findDuplicatesMenuItem
            // 
            this.findDuplicatesMenuItem.Name = "findDuplicatesMenuItem";
            this.findDuplicatesMenuItem.Size = new System.Drawing.Size(155, 22);
            this.findDuplicatesMenuItem.Text = "Find Duplicates";
            this.findDuplicatesMenuItem.Click += new System.EventHandler(this.findDuplicatesMenuItem_Click);
            // 
            // cmdAddEntry
            // 
            this.cmdAddEntry.Enabled = false;
            this.cmdAddEntry.Location = new System.Drawing.Point(371, 18);
            this.cmdAddEntry.Name = "cmdAddEntry";
            this.cmdAddEntry.Size = new System.Drawing.Size(52, 34);
            this.cmdAddEntry.TabIndex = 5;
            this.cmdAddEntry.Text = "&Add";
            this.toolTip1.SetToolTip(this.cmdAddEntry, "Adds a New Entry");
            this.cmdAddEntry.UseVisualStyleBackColor = true;
            this.cmdAddEntry.Click += new System.EventHandler(this.cmdAddEntry_Click);
            // 
            // txtSecond
            // 
            this.txtSecond.Location = new System.Drawing.Point(62, 35);
            this.txtSecond.MaxLength = 500;
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(303, 20);
            this.txtSecond.TabIndex = 4;
            this.txtSecond.TextChanged += new System.EventHandler(this.txtSecond_TextChanged);
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(62, 14);
            this.txtFirst.MaxLength = 500;
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(303, 20);
            this.txtFirst.TabIndex = 3;
            this.txtFirst.TextChanged += new System.EventHandler(this.txtFirst_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Second:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&First:";
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(429, 18);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(52, 34);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // ManageDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 522);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ManageDictionary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Dictionary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageDictionary_FormClosing);
            this.Load += new System.EventHandler(this.ManageDictionary_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdAddEntry;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTxtAFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTxtBFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripBtnClearFilters;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn First;
        private System.Windows.Forms.DataGridViewTextBoxColumn Second;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.ToolStripMenuItem timestampEntryMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem allActiveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deactivateAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem activateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deactivateMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem findDuplicatesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}