using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes.Sales;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class AddDiscountUI : Form
    {
        SalesDiscount loDiscount;
        public string lDiscountId;
        public string lDiscountDescription;
        public decimal lAmount;
        public decimal lPercentage;
        public bool lVATExempt;
        public string lDetails;
        public bool lFromClose;
        
        public AddDiscountUI()
        {
            InitializeComponent();
            loDiscount = new SalesDiscount();
            lDiscountId = "";
            lDiscountDescription = "";
            lAmount = 0;
            lPercentage = 0;
            lVATExempt = false;
            lDetails = "";
            lFromClose = false;
        }

        private void AddDiscountUI_Load(object sender, EventArgs e)
        {
            cboDiscount.DataSource = loDiscount.getAllData("ViewAll", "","");
            cboDiscount.DisplayMember = "Description";
            cboDiscount.ValueMember = "Id";
            cboDiscount.SelectedIndex = -1;

            txtAmount.Text = "0.00";
            txtPercentage.Text = "0.00";
            chkVATExempt.Checked = false;
            txtDetails.Clear();

            lDiscountId = "";
            lDiscountDescription = "";
            lAmount = 0;
            lPercentage = 0;
            lVATExempt = false;
            lDetails = "";
            lFromClose = false;

            txtAmount.Enabled = false;
            txtPercentage.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                lDiscountId = cboDiscount.SelectedValue.ToString();
                lDiscountDescription = cboDiscount.Text;
                lAmount = decimal.Parse(txtAmount.Text);
                lPercentage = decimal.Parse(txtPercentage.Text);
                lVATExempt = chkVATExempt.Checked;
                lDetails = txtDetails.Text;
                lFromClose = false;

                this.Close();
            }
            catch { }
        }

        private void cboDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loDiscount.getAllData("",cboDiscount.SelectedValue.ToString(),"").Rows)
                {
                    if(_dr["Type"].ToString() == "Percentage")
                    {
                        txtPercentage.ReadOnly = true;
                        txtAmount.Text = "0.00";
                        txtPercentage.ReadOnly = false;
                        txtPercentage.Text = string.Format("{0:n}", decimal.Parse(_dr["Value"].ToString()));
                    }
                    else if (_dr["Type"].ToString() == "Amount")
                    {
                        txtPercentage.ReadOnly = false;
                        txtAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Value"].ToString()));
                        txtPercentage.ReadOnly = true;
                        txtPercentage.Text = "0.00";
                    }
                    else
                    {
                        txtAmount.Text = "0.00";
                        txtPercentage.Text = "0.00";
                        chkVATExempt.Checked = false;
                    }
                    
                }
            }
            catch
            {
                txtAmount.Text = "0.00";
                txtPercentage.Text = "0.00";
                chkVATExempt.Checked = false;
            }
            if (txtAmount.Text == "0.00")
            {
                txtAmount.Enabled = false;
                txtPercentage.Enabled = true;
            }
            else if (txtPercentage.Text == "0.00")
            {
                txtPercentage.Enabled = false;
                txtAmount.Enabled = true;
            }
        }
    }
}
