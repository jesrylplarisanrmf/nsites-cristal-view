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
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports.ReportRpt;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports
{
    public partial class CashierPeriodsUI : Form
    {
        CashierPeriod loCashierPeriod;
        CashierPeriodsRpt loCashierPeriodsRpt;
        DataTable ldtCashierPeriod;
        ReportViewerUI loReportViewer;

        public CashierPeriodsUI()
        {
            InitializeComponent();
            loCashierPeriod = new CashierPeriod();
            loCashierPeriodsRpt = new CashierPeriodsRpt();
            ldtCashierPeriod = new DataTable();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void CashierPeriodsUI_Load(object sender, EventArgs e)
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashierPeriods", "Preview"))
                {
                    return;
                }

                try
                {
                    if (ldtCashierPeriod.Rows.Count > 0)
                    {
                        loCashierPeriodsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loCashierPeriodsRpt.Database.Tables[1].SetDataSource(ldtCashierPeriod);
                        loCashierPeriodsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loCashierPeriodsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loCashierPeriodsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loCashierPeriodsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loCashierPeriodsRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loCashierPeriodsRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loCashierPeriodsRpt.SetParameterValue("Title", "Cashier Periods");
                        loCashierPeriodsRpt.SetParameterValue("SubTitle", "Cashier Periods");
                        loReportViewer.crystalReportViewer.ReportSource = loCashierPeriodsRpt;
                        loReportViewer.ShowDialog();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ldtCashierPeriod = loCashierPeriod.getCashierPeriodByDate(dtpFromDate.Value, dtpToDate.Value);
            GlobalFunctions.refreshGrid(ref dgvLists, ldtCashierPeriod);
        }

        private void dgvLists_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvLists.Columns[e.ColumnIndex].Name == "Id" || 
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Period Status")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Cash Deposit" || this.dgvLists.Columns[e.ColumnIndex].Name == "Total Sales" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Returned Item Total" || this.dgvLists.Columns[e.ColumnIndex].Name == "Total Discount" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Net Sales" || this.dgvLists.Columns[e.ColumnIndex].Name == "Non Cash Sales" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Cash Sales" || this.dgvLists.Columns[e.ColumnIndex].Name == "Net Cash Sales" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Cash Count" || this.dgvLists.Columns[e.ColumnIndex].Name == "Variance")
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

        private void dgvLists_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    System.Drawing.Point pt = dgvLists.PointToScreen(e.Location);
                    cmsFunction.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvLists_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvLists, ldtCashierPeriod);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
