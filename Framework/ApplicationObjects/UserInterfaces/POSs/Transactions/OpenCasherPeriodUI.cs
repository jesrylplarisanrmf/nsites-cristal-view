using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes.POSs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions
{
    public partial class OpenCasherPeriodUI : Form
    {
        CashierPeriod loCasherPeriod;
        Cashier loCashier;
        
        public OpenCasherPeriodUI()
        {
            InitializeComponent();
            loCasherPeriod = new CashierPeriod();
            loCashier = new Cashier();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void OpenCasherPeriodUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            GlobalVariables.CashierId = "0";
            GlobalVariables.CashierName = "";
            foreach (DataRow _dr in loCashier.getCashierDetails(GlobalVariables.UserId).Rows)
            {
                GlobalVariables.CashierId = _dr[0].ToString();
                GlobalVariables.CashierName = _dr[2].ToString();
            }
            if (GlobalVariables.CashierId == "0")
            {
                MessageBoxUI _mb = new MessageBoxUI("Only Cashier can open a Cashier Period!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                this.Close();
            }
            else
            {
                txtCashDeposit.Text = "0.00";
                txtRemarks.Clear();
                txtCashDeposit.Focus();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmOpenCashierPeriod", "Open"))
                {
                    return;
                }
                
                if (loCasherPeriod.openCashierPeriod(GlobalVariables.CashierId,decimal.Parse(txtCashDeposit.Text), txtRemarks.Text))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier period has been open successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    this.Close();
                }
            }
            catch { }
        }

        private void txtCashDeposit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCashDeposit.Text = string.Format("{0:n}", decimal.Parse(txtCashDeposit.Text));
            }
            catch
            {
                txtCashDeposit.Text = "0.00";
            }
        }
    }
}
