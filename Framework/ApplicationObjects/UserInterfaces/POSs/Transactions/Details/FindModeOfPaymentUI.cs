using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.ApplicationObjects.Classes.POSs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class FindModeOfPaymentUI : Form
    {
        ModeOfPayment loModeOfPayment;
        public string lId;
        public string lDescription;
        public string lDetails;
        public bool lFromOK;

        public FindModeOfPaymentUI()
        {
            InitializeComponent();
            loModeOfPayment = new ModeOfPayment();
            lId = "";
            lDescription = "";
            lDetails = "";
            lFromOK = false;
        }

        private void FindCustomerUI_Load(object sender, EventArgs e)
        {
            cboModeOfPayment.DataSource = loModeOfPayment.getAllData("ViewAll", "","");
            cboModeOfPayment.ValueMember = "Id";
            cboModeOfPayment.DisplayMember = "Description";
            cboModeOfPayment.SelectedIndex = -1;

            cboModeOfPayment.SelectedValue = Global.GlobalVariables.DefaultModeOfPaymentId;

            lId = "";
            lDescription = "";
            lDetails = "";
            lFromOK = false;

            txtDetails.Clear();

            cboModeOfPayment.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lFromOK = true;
            lId = cboModeOfPayment.SelectedValue.ToString();
            lDescription = cboModeOfPayment.Text;
            lDetails = txtDetails.Text;
            this.Close();
        }
    }
}
