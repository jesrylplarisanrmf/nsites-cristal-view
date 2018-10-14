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
    public partial class FindCustomerListUI : Form
    {
        Customer loCustomer;
        public string lCustomerId;
        public string lCustomerName;
        public bool lFromSelection;
        
        public FindCustomerListUI()
        {
            InitializeComponent();
            loCustomer = new Customer();
            lFromSelection = false;
        }

        private void displayResult(string pSearchString)
        {
            try
            {
                dgvList.DataSource = null;
                if (pSearchString == "")
                {
                    dgvList.DataSource = loCustomer.getAllData("ViewAll", "", "");
                }
                else
                {
                    dgvList.DataSource = loCustomer.getAllData("", "", pSearchString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FindCustomerUI_Load(object sender, EventArgs e)
        {
            try
            {
                displayResult("");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "FindCustomerUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvList.Rows.Count > 0)
            {
                lCustomerId = dgvList.CurrentRow.Cells[0].Value.ToString();
                lCustomerName = dgvList.CurrentRow.Cells[1].Value.ToString();
                lFromSelection = true;
            }
            else
            {
                lCustomerId = "";
                lCustomerName = "";
                lFromSelection = false;
            }
            this.Close();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvList.Columns[e.ColumnIndex].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                displayResult(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearch_TextChanged");
                em.ShowDialog();
                return;
            }
        }
    }
}
