namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    partial class OtherFunctionsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OtherFunctionsUI));
            this.pnlBody = new System.Windows.Forms.Panel();
            this.btnOpenCashDrawer = new System.Windows.Forms.Button();
            this.btnReturnedItem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.btnOpenCashDrawer);
            this.pnlBody.Controls.Add(this.btnReturnedItem);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Location = new System.Drawing.Point(12, 12);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(428, 257);
            this.pnlBody.TabIndex = 17;
            // 
            // btnOpenCashDrawer
            // 
            this.btnOpenCashDrawer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOpenCashDrawer.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOpenCashDrawer.FlatAppearance.BorderSize = 0;
            this.btnOpenCashDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenCashDrawer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCashDrawer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpenCashDrawer.Location = new System.Drawing.Point(65, 152);
            this.btnOpenCashDrawer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenCashDrawer.Name = "btnOpenCashDrawer";
            this.btnOpenCashDrawer.Size = new System.Drawing.Size(290, 40);
            this.btnOpenCashDrawer.TabIndex = 66;
            this.btnOpenCashDrawer.Text = "OPEN CASH DRAWER";
            this.btnOpenCashDrawer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenCashDrawer.UseVisualStyleBackColor = false;
            // 
            // btnReturnedItem
            // 
            this.btnReturnedItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnReturnedItem.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReturnedItem.FlatAppearance.BorderSize = 0;
            this.btnReturnedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnedItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnedItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReturnedItem.Location = new System.Drawing.Point(65, 103);
            this.btnReturnedItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReturnedItem.Name = "btnReturnedItem";
            this.btnReturnedItem.Size = new System.Drawing.Size(290, 40);
            this.btnReturnedItem.TabIndex = 65;
            this.btnReturnedItem.Text = "RETURNED ITEM";
            this.btnReturnedItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReturnedItem.UseVisualStyleBackColor = false;
            this.btnReturnedItem.Click += new System.EventHandler(this.btnReturnedItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(24, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 37);
            this.label1.TabIndex = 63;
            this.label1.Text = "Other Functions";
            // 
            // OtherFunctionsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(451, 281);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherFunctionsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other Functions";
            this.Load += new System.EventHandler(this.OtherFunctionsUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnOpenCashDrawer;
        private System.Windows.Forms.Button btnReturnedItem;
        private System.Windows.Forms.Label label1;

    }
}