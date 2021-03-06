﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes;
using JCSoftwares_V.ApplicationObjects.Classes.Lendings;
using JCSoftwares_V.ApplicationObjects.Classes.Generics;

using JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Reports.TransactionsRpt;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Transactions
{
    public partial class DownloadDCRSUI : Form
    {
        LoanApplication loLoanApplication;
        LoanApplicationDetail loLoanApplicationDetail;
        Branch loBranch;
        Collector loCollector;
        Common loCommon;
        SearchesUI loSearches;
        DailyCollectionAndReleaseSheetRpt loDailyCollectionAndReleaseSheetRpt;
        ReportViewerUI loReportViewer;

        public DownloadDCRSUI()
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loLoanApplicationDetail = new LoanApplicationDetail();
            loBranch = new Branch();
            loCollector = new Collector();
            loCommon = new Common();
            loDailyCollectionAndReleaseSheetRpt = new DailyCollectionAndReleaseSheetRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void getList()
        {
            try
            {
                dgvCollectionList.DataSource = loLoanApplicationDetail.getDailyCollectionSheet(dtpDate.Value, cboCollector.SelectedValue.ToString());
            }
            catch
            {
                dgvCollectionList.DataSource = null;
            }

            try
            {
                dgvForRelease.DataSource = loLoanApplication.getForReleaseSheet(dtpDate.Value, cboCollector.SelectedValue.ToString());
            }
            catch
            {
                dgvForRelease.DataSource = null;
            }
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

        private void cboCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            getList();
        }

        private void dgvCollectionList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvCollectionList.Columns[e.ColumnIndex].Name == "Loan Amount" ||
                    this.dgvCollectionList.Columns[e.ColumnIndex].Name == "Amount Due" ||
                    this.dgvCollectionList.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvCollectionList.Columns[e.ColumnIndex].Name == "Variance" ||
                    this.dgvCollectionList.Columns[e.ColumnIndex].Name == "Installment Amount")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvCollectionList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvForRelease_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvForRelease.Columns[e.ColumnIndex].Name == "Loan Application Id" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Loan Cycle" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Payment Frequency")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvForRelease.Columns[e.ColumnIndex].Name == "Interest Rate" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Service Fee Rate" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Loan Amount" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Interest Amount" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Service Fee Amount" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Installment Amount Due" ||
                    this.dgvForRelease.Columns[e.ColumnIndex].Name == "Loan Release Amount")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvForRelease_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                loDailyCollectionAndReleaseSheetRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loDailyCollectionAndReleaseSheetRpt.Subreports["DailyCollectionRpt.rpt"].SetDataSource(loLoanApplicationDetail.getDailyCollectionSheet(dtpDate.Value, cboCollector.SelectedValue.ToString()));
                loDailyCollectionAndReleaseSheetRpt.Subreports["ForReleaseRpt.rpt"].SetDataSource(loLoanApplication.getForReleaseSheet(dtpDate.Value, cboCollector.SelectedValue.ToString()));
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("Title", "Daily Collection & Release Sheet");
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("SubTitle", "Daily Collection & Release Sheet");
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("Date", string.Format("{0:MM-dd-yyyy}", dtpDate.Value));
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("PreparedBy", GlobalVariables.Userfullname);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("Branch", cboBranch.Text);
                loDailyCollectionAndReleaseSheetRpt.SetParameterValue("Collector", cboCollector.Text);
                loReportViewer.crystalReportViewer.ReportSource = loDailyCollectionAndReleaseSheetRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadDCRSUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(LoanApplicationDetail);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmDownloadDCRS");
                loSearches.lQuery = "";
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "DownloadDCRSUI_Load");
                em.ShowDialog();
                return;
            }

            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.DisplayMember = "Description";
            cboBranch.ValueMember = "Id";
            cboBranch.SelectedIndex = -1;

            cboCollector.DataSource = loCollector.getAllData("ViewAll", "", "");
            cboCollector.DisplayMember = "Employee Name";
            cboCollector.ValueMember = "Id";
            cboCollector.SelectedIndex = -1;

            cboBranch.SelectedValue = GlobalVariables.CurrentBranchId;
        }
    }
}
