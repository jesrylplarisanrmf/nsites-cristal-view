namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Payrolls.Masterfiles
{
    partial class IncomeTaxTableDetailUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncomeTaxTableDetailUI));
            this.pnlBody = new System.Windows.Forms.Panel();
            this.txtPercentOver = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBaseTax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBaseAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUpperLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLowerLimit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.pnlBody.Controls.Add(this.txtPercentOver);
            this.pnlBody.Controls.Add(this.label6);
            this.pnlBody.Controls.Add(this.txtBaseTax);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.txtBaseAmount);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.txtUpperLimit);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.txtLowerLimit);
            this.pnlBody.Controls.Add(this.label5);
            this.pnlBody.Controls.Add(this.btnSave);
            this.pnlBody.Controls.Add(this.txtRemarks);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Location = new System.Drawing.Point(12, 12);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(430, 313);
            this.pnlBody.TabIndex = 22;
            // 
            // txtPercentOver
            // 
            this.txtPercentOver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtPercentOver.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPercentOver.Location = new System.Drawing.Point(117, 151);
            this.txtPercentOver.Name = "txtPercentOver";
            this.txtPercentOver.Size = new System.Drawing.Size(76, 25);
            this.txtPercentOver.TabIndex = 24;
            this.txtPercentOver.Text = "0.00";
            this.txtPercentOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPercentOver.Leave += new System.EventHandler(this.txtPercentOver_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "Percent Over";
            // 
            // txtBaseTax
            // 
            this.txtBaseTax.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBaseTax.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseTax.Location = new System.Drawing.Point(117, 120);
            this.txtBaseTax.Name = "txtBaseTax";
            this.txtBaseTax.Size = new System.Drawing.Size(139, 25);
            this.txtBaseTax.TabIndex = 22;
            this.txtBaseTax.Text = "0.00";
            this.txtBaseTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBaseTax.Leave += new System.EventHandler(this.txtBaseTax_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Base Tax";
            // 
            // txtBaseAmount
            // 
            this.txtBaseAmount.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBaseAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseAmount.Location = new System.Drawing.Point(117, 89);
            this.txtBaseAmount.Name = "txtBaseAmount";
            this.txtBaseAmount.Size = new System.Drawing.Size(139, 25);
            this.txtBaseAmount.TabIndex = 20;
            this.txtBaseAmount.Text = "0.00";
            this.txtBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBaseAmount.Leave += new System.EventHandler(this.txtBaseAmount_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Base Amount";
            // 
            // txtUpperLimit
            // 
            this.txtUpperLimit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtUpperLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpperLimit.Location = new System.Drawing.Point(117, 58);
            this.txtUpperLimit.Name = "txtUpperLimit";
            this.txtUpperLimit.Size = new System.Drawing.Size(139, 25);
            this.txtUpperLimit.TabIndex = 18;
            this.txtUpperLimit.Text = "0.00";
            this.txtUpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUpperLimit.Leave += new System.EventHandler(this.txtUpperLimit_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Uppder Limit";
            // 
            // txtLowerLimit
            // 
            this.txtLowerLimit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtLowerLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLowerLimit.Location = new System.Drawing.Point(117, 27);
            this.txtLowerLimit.Name = "txtLowerLimit";
            this.txtLowerLimit.Size = new System.Drawing.Size(139, 25);
            this.txtLowerLimit.TabIndex = 0;
            this.txtLowerLimit.Text = "0.00";
            this.txtLowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLowerLimit.Leave += new System.EventHandler(this.txtLowerLimit_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Lower Limit";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(153, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = " &Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(117, 182);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(278, 50);
            this.txtRemarks.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Remarks";
            // 
            // IncomeTaxTableDetailUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(454, 336);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IncomeTaxTableDetailUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Income Tax Table Detail";
            this.Load += new System.EventHandler(this.IncomeTaxTableDetailUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TextBox txtLowerLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPercentOver;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBaseTax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBaseAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUpperLimit;
        private System.Windows.Forms.Label label1;
    }
}