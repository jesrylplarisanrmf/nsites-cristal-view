﻿namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Sales.Reports
{
    partial class SOReceivableReportUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SOReceivableReportUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.crvAllReceivableAccounts = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.crvReceivablesByDueDate = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crvOverdueAccounts = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnPreview);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(11, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 49);
            this.panel1.TabIndex = 114;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreview.Location = new System.Drawing.Point(816, 4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(101, 40);
            this.btnPreview.TabIndex = 71;
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
            this.label3.Size = new System.Drawing.Size(311, 40);
            this.label3.TabIndex = 57;
            this.label3.Text = "S.O. Receivable Report";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(920, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 40);
            this.btnClose.TabIndex = 53;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 461);
            this.tabControl1.TabIndex = 119;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.crvAllReceivableAccounts);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(956, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ALL RECEIVABLES";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // crvAllReceivableAccounts
            // 
            this.crvAllReceivableAccounts.ActiveViewIndex = -1;
            this.crvAllReceivableAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvAllReceivableAccounts.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvAllReceivableAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvAllReceivableAccounts.Location = new System.Drawing.Point(3, 3);
            this.crvAllReceivableAccounts.Name = "crvAllReceivableAccounts";
            this.crvAllReceivableAccounts.Size = new System.Drawing.Size(950, 425);
            this.crvAllReceivableAccounts.TabIndex = 113;
            this.crvAllReceivableAccounts.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.crvReceivablesByDueDate);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(956, 431);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "RECEIVABLES BY DUE DATE";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // crvReceivablesByDueDate
            // 
            this.crvReceivablesByDueDate.ActiveViewIndex = -1;
            this.crvReceivablesByDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReceivablesByDueDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReceivablesByDueDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReceivablesByDueDate.Location = new System.Drawing.Point(3, 3);
            this.crvReceivablesByDueDate.Name = "crvReceivablesByDueDate";
            this.crvReceivablesByDueDate.Size = new System.Drawing.Size(950, 425);
            this.crvReceivablesByDueDate.TabIndex = 114;
            this.crvReceivablesByDueDate.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crvOverdueAccounts);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(956, 431);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "OVERDUE RECEIVABLES";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // crvOverdueAccounts
            // 
            this.crvOverdueAccounts.ActiveViewIndex = -1;
            this.crvOverdueAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvOverdueAccounts.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvOverdueAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvOverdueAccounts.Location = new System.Drawing.Point(0, 0);
            this.crvOverdueAccounts.Name = "crvOverdueAccounts";
            this.crvOverdueAccounts.Size = new System.Drawing.Size(956, 431);
            this.crvOverdueAccounts.TabIndex = 115;
            this.crvOverdueAccounts.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // SOReceivableReportUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 535);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SOReceivableReportUI";
            this.Text = "S.O. Receivable Report";
            this.Load += new System.EventHandler(this.AccountReceivableReportUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvAllReceivableAccounts;
        private System.Windows.Forms.TabPage tabPage3;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvReceivablesByDueDate;
        private System.Windows.Forms.TabPage tabPage2;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvOverdueAccounts;
    }
}