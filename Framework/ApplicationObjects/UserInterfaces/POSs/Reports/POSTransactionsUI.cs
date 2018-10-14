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
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports.TransactionRpt;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports
{
    public partial class POSTransactionsUI : Form
    {
        POSTransaction loPOSTransaction;
        POSTransactionDetail loPOSTransactionDetail;
        OrderSlipRpt loOrderSlipRpt;

        public POSTransactionsUI()
        {
            InitializeComponent();
            loPOSTransaction = new POSTransaction();
            loPOSTransactionDetail = new POSTransactionDetail();
            loOrderSlipRpt = new OrderSlipRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void POSTransactionsUI_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmPOSTransactions", "Refresh"))
            {
                return;
            }
            dgvPOSTransactions.DataSource = loPOSTransaction.getPOSTransactionsByDate(dtpFromDate.Value, dtpToDate.Value);
        }

        private void dgvPOSTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataRow _dr in loPOSTransaction.getPOSTransaction(dgvPOSTransactions.CurrentRow.Cells[0].Value.ToString()).Rows)
            {
                if (GlobalVariables.ORSize == "Plain Continuous-Long")
                {
                    DataTable _dtTransactions = loPOSTransactionDetail.getPOSTransactionDetails(dgvPOSTransactions.CurrentRow.Cells[0].Value.ToString());
                    if (_dtTransactions.Rows.Count > 0)
                    {
                        loOrderSlipRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loOrderSlipRpt.Database.Tables[1].SetDataSource(_dtTransactions);
                        loOrderSlipRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loOrderSlipRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loOrderSlipRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loOrderSlipRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loOrderSlipRpt.SetParameterValue("Title", "Order Slip");
                        loOrderSlipRpt.SetParameterValue("SubTitle", "Order Slip");
                        loOrderSlipRpt.SetParameterValue("TransactionNo", _dr["Id"].ToString());
                        loOrderSlipRpt.SetParameterValue("Date", _dr["Date"].ToString());
                        loOrderSlipRpt.SetParameterValue("TotalPrice", string.Format("{0:n}", decimal.Parse(_dr["Total Price"].ToString())));
                        loOrderSlipRpt.SetParameterValue("TotalDiscount", string.Format("{0:n}", decimal.Parse(_dr["Total Discount"].ToString())));
                        loOrderSlipRpt.SetParameterValue("TotalDue", string.Format("{0:n}", decimal.Parse(_dr["Total Due"].ToString())));
                        loOrderSlipRpt.SetParameterValue("PreparedBy", _dr["PreparedBy"].ToString());
                        loOrderSlipRpt.SetParameterValue("Customer", _dr["Customer"].ToString());
                        loOrderSlipRpt.SetParameterValue("Address", _dr["CustomerAddress"].ToString());
                        crvList.ReportSource = loOrderSlipRpt;
                    }
                    else
                    {
                        crvList.ReportSource = null;
                    }
                }
                else if (GlobalVariables.ORSize == "Receipt Printed(80mm)")
                {
                    try
                    {
                        foreach (DataRow _dr80mm in loPOSTransaction.getPOSTransaction(dgvPOSTransactions.CurrentRow.Cells[0].Value.ToString()).Rows)
                        {
                            loOrderSlipRpt.Subreports["TransactionDetailListRpt.rpt"].SetDataSource(loPOSTransactionDetail.getPOSTransactionDetails(dgvPOSTransactions.CurrentRow.Cells[0].Value.ToString()));
                            loOrderSlipRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName.ToUpper());
                            loOrderSlipRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loOrderSlipRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);

                            loOrderSlipRpt.SetParameterValue("Date", string.Format("{0:MM-dd-yyyy}", DateTime.Parse(_dr80mm["Date"].ToString())));
                            loOrderSlipRpt.SetParameterValue("TransactionNo", dgvPOSTransactions.CurrentRow.Cells[0].Value.ToString());
                            loOrderSlipRpt.SetParameterValue("Customer", _dr["Customer"].ToString());
                            crvList.ReportSource = loOrderSlipRpt;
                        }
                    }
                    catch { }
                }
            }
        }

        private void dgvPOSTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "Id" || this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "O.R. No.")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "Total Price" || this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "Total Due" ||
                    this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "Total Discount" || this.dgvPOSTransactions.Columns[e.ColumnIndex].Name == "Amount Tendered")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvPOSTransactions_CellFormatting");
                em.ShowDialog();
                return;
            }
        }
    }
}
