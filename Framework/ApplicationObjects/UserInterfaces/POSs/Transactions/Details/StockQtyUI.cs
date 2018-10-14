using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes.Inventorys;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class StockQtyUI : Form
    {
        public bool lFromOK;
        public string lStockId;
        public string lStockDescription;
        public decimal lQty;
        public decimal lUnitCost;
        public decimal lBasePrice;
        public decimal lUnitPrice;
        bool lStart;
        Stock loStock;
        
        public StockQtyUI()
        {
            InitializeComponent();
            lFromOK = false;
            lStockId = "";
            lStockDescription = "";
            lQty = 0;
            lUnitCost = 0;
            lBasePrice = 0;
            lUnitPrice = 0;
            loStock = new Stock();
        }

        private void StockQtyUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            lblStockDescription.Text = lStockDescription;
            lblBasePrice.Text = string.Format("{0:n}", lBasePrice);
            txtUnitPrice.Text = string.Format("{0:n}", lUnitPrice);
            txtQty.Text = string.Format("{0:0}", lQty);
            lblTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtQty.Text) * decimal.Parse(txtUnitPrice.Text));
            lFromOK = false;
            lStart = true;
            txtQty.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtUnitPrice.Text) < decimal.Parse(lblBasePrice.Text))
            {
                MessageBoxUI _mb = new MessageBoxUI("Unit Price must not be lesser than Base Price!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtUnitPrice.Focus();
                return;
            }

            if (decimal.Parse(lblTotalPrice.Text) <= 0)
            {
                MessageBoxUI _mb = new MessageBoxUI("Total Price must not be lesser than or equal to Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtUnitPrice.Focus();
                return;
            }

            if (decimal.Parse(txtQty.Text) <= 0)
            {
                MessageBoxUI _mb = new MessageBoxUI("Qty must not be lesser than or equal to Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtQty.Focus();
                return;
            }

            try
            {
                lQty = decimal.Parse(txtQty.Text);
                lUnitPrice = decimal.Parse(txtUnitPrice.Text);
                lFromOK = true;
            }
            catch
            {
                lQty = 0;
                lUnitPrice = 0;
                lFromOK = false;
            }

            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQty.Clear();
            txtQty.Focus();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnOK_Click(null, new EventArgs());
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("9");
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText(".");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtQty.Clear();
                lStart = false;
            }
            txtQty.AppendText("0");
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtQty.Text) * decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                lblTotalPrice.Text = "0.00";
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtUnitPrice.Text = "0.00";
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtQty.Text) * decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                lblTotalPrice.Text = "0.00";
            }
        }
    }
}
