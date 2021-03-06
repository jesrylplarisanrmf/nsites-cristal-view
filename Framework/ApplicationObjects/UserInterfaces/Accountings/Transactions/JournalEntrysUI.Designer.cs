﻿namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    partial class JournalEntrysUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalEntrysUI));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.dgvDetailList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsFunction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmViewAllRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRequestToCheckAndPost = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRequestToCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailList)).BeginInit();
            this.panel1.SuspendLayout();
            this.cmsFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(11, 62);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDetailList);
            this.splitContainer1.Size = new System.Drawing.Size(993, 395);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 82;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(993, 196);
            this.dgvList.TabIndex = 76;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvList_KeyUp);
            this.dgvList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvList_MouseClick);
            // 
            // dgvDetailList
            // 
            this.dgvDetailList.AllowUserToAddRows = false;
            this.dgvDetailList.AllowUserToDeleteRows = false;
            this.dgvDetailList.AllowUserToResizeRows = false;
            this.dgvDetailList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetailList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetailList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetailList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetailList.Location = new System.Drawing.Point(0, 0);
            this.dgvDetailList.MultiSelect = false;
            this.dgvDetailList.Name = "dgvDetailList";
            this.dgvDetailList.ReadOnly = true;
            this.dgvDetailList.RowHeadersVisible = false;
            this.dgvDetailList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetailList.Size = new System.Drawing.Size(993, 195);
            this.dgvDetailList.TabIndex = 77;
            this.dgvDetailList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetailList_CellFormatting);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPost);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnPreview);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(11, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(993, 49);
            this.panel1.TabIndex = 81;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(634, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 40);
            this.btnCancel.TabIndex = 153;
            this.btnCancel.Text = " &Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPost
            // 
            this.btnPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPost.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPost.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost.Image = ((System.Drawing.Image)(resources.GetObject("btnPost.Image")));
            this.btnPost.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPost.Location = new System.Drawing.Point(530, 4);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(101, 40);
            this.btnPost.TabIndex = 77;
            this.btnPost.Text = " &Post";
            this.btnPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.Location = new System.Drawing.Point(426, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(101, 40);
            this.btnRefresh.TabIndex = 76;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(738, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 40);
            this.btnSearch.TabIndex = 68;
            this.btnSearch.Text = " &Search";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreview.Location = new System.Drawing.Point(842, 4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(104, 40);
            this.btnPreview.TabIndex = 63;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.label3.Location = new System.Drawing.Point(6, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 40);
            this.label3.TabIndex = 57;
            this.label3.Text = "Journal Entry";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(949, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 40);
            this.btnClose.TabIndex = 53;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmsFunction
            // 
            this.cmsFunction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewAllRecords,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem,
            this.createToolStripMenuItem,
            this.tsmiCancel,
            this.tsmRequestToCheckAndPost,
            this.tsmiRequestToCancel,
            this.toolStripSeparator2,
            this.searchToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.tsmExportToExcel});
            this.cmsFunction.Name = "cmsFunction";
            this.cmsFunction.Size = new System.Drawing.Size(216, 214);
            // 
            // tsmViewAllRecords
            // 
            this.tsmViewAllRecords.Image = ((System.Drawing.Image)(resources.GetObject("tsmViewAllRecords.Image")));
            this.tsmViewAllRecords.Name = "tsmViewAllRecords";
            this.tsmViewAllRecords.Size = new System.Drawing.Size(215, 22);
            this.tsmViewAllRecords.Text = "View All Records";
            this.tsmViewAllRecords.Click += new System.EventHandler(this.tsmViewAllRecords_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createToolStripMenuItem.Image")));
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.createToolStripMenuItem.Text = "Post";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // tsmiCancel
            // 
            this.tsmiCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCancel.Image")));
            this.tsmiCancel.Name = "tsmiCancel";
            this.tsmiCancel.Size = new System.Drawing.Size(215, 22);
            this.tsmiCancel.Text = "Cancel";
            this.tsmiCancel.Click += new System.EventHandler(this.tsmiCancel_Click);
            // 
            // tsmRequestToCheckAndPost
            // 
            this.tsmRequestToCheckAndPost.Image = ((System.Drawing.Image)(resources.GetObject("tsmRequestToCheckAndPost.Image")));
            this.tsmRequestToCheckAndPost.Name = "tsmRequestToCheckAndPost";
            this.tsmRequestToCheckAndPost.Size = new System.Drawing.Size(215, 22);
            this.tsmRequestToCheckAndPost.Text = "Request to Check and Post";
            this.tsmRequestToCheckAndPost.Click += new System.EventHandler(this.tsmSendEmailForPosting_Click);
            // 
            // tsmiRequestToCancel
            // 
            this.tsmiRequestToCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRequestToCancel.Image")));
            this.tsmiRequestToCancel.Name = "tsmiRequestToCancel";
            this.tsmiRequestToCancel.Size = new System.Drawing.Size(215, 22);
            this.tsmiRequestToCancel.Text = "Request to Cancel";
            this.tsmiRequestToCancel.Click += new System.EventHandler(this.tsmiRequestToCancel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("previewToolStripMenuItem.Image")));
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // tsmExportToExcel
            // 
            this.tsmExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsmExportToExcel.Image")));
            this.tsmExportToExcel.Name = "tsmExportToExcel";
            this.tsmExportToExcel.Size = new System.Drawing.Size(215, 22);
            this.tsmExportToExcel.Text = "Export to Excel";
            // 
            // JournalEntrysUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 469);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "JournalEntrysUI";
            this.Text = "Journal Entry";
            this.Load += new System.EventHandler(this.JournalEntryUI_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridView dgvDetailList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsFunction;
        private System.Windows.Forms.ToolStripMenuItem tsmViewAllRecords;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmRequestToCheckAndPost;
        private System.Windows.Forms.ToolStripMenuItem tsmiRequestToCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmiCancel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmExportToExcel;
    }
}