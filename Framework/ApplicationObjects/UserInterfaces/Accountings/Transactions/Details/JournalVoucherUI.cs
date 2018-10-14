﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes.Accountings;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class JournalVoucherUI : Form
    {
        #region "VARIABLES"
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        GlobalVariables.Operation lOperation;
        string lJournalEntryId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public JournalVoucherUI()
        {
            InitializeComponent();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            lOperation = GlobalVariables.Operation.Add;
            lJournalEntryId = "";
        }
        public JournalVoucherUI(string pJournalEntryId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            lJournalEntryId = pJournalEntryId;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void clear()
        {
            
        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvDetail.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetail.CurrentRow.Selected = false;
                dgvDetail.FirstDisplayedScrollingRowIndex = dgvDetail.Rows[n].Index;
                dgvDetail.Rows[n].Selected = true;

                computeTotal();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void updateData(string[] pRecordData)
        {
            try
            {
                string _operator = dgvDetail.CurrentRow.Cells[9].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetail.CurrentRow.Cells[9].Value = "Add";
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void computeTotal()
        {
            try
            {
                decimal _totalDebit = 0;
                decimal _totalCredit = 0;
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        _totalDebit += decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                        _totalCredit += decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                    }
                }
                txtTotalDebit.Text = string.Format("{0:n}", _totalDebit);
                txtTotalCredit.Text = string.Format("{0:n}", _totalCredit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void sendEmailForPosting(string pJournalEntryId,string pCompanyCode, string pCompany, string pDatePrepared,string pExplanation,decimal pTotalDebit,
            decimal pTotalCredit, string pPreparedBy)
        {
            try
            {
                string _bodyhead = "<h3>Journal Entry Id : " + pJournalEntryId + "</h3>" +
                                "<h4>Form : Journal Voucher</h4>" +
                                "<h4>Company : " + pCompany + "</h4>" +
                                "<h4>Date Prepared : " + pDatePrepared + "</h4>" +
                                "<h4>Explanation : " + pExplanation + "</h4>" +
                                "<h4>Total Debit : " + string.Format("{0:n}", pTotalDebit) + "</h4>" +
                                "<h4>Total Credit : " + string.Format("{0:n}", pTotalCredit) + "</h4>" +
                                "<h4>Prepared By : " + pPreparedBy + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">" +
                                "<tr>" +
                                    "<th>Account Code</th>" +//1
                                    "<th>Account Title</th>" +//2
                                    "<th>Debit</th>" +//3
                                    "<th>Credit</th>" +//4
                                    "<th>Subsidiary</th>" +//5
                                    "<th>Description</th>" +//7
                                    "<th>Remarks</th>" +//8
                                "</tr>";
                string _bodycontent = "";
                foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails("ViewAll", pJournalEntryId).Rows)
                {
                    _bodycontent += "<tr>" +
                                        "<td align=\"center\">" + _drbody[1].ToString() + "</td>" +
                                        "<td>" + _drbody[2].ToString() + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[3].ToString())) + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[4].ToString())) + "</td>" +
                                        "<td align=\"center\">" + _drbody[5].ToString() + "</td>" +
                                        "<td>" + _drbody[6].ToString() + "</td>" +
                                        "<td align=\"center\">" + _drbody[7].ToString() + "</td>" +
                                    "</tr>";
                }
                /*
                //get preparer email address
                string _preparerEmailAddress = "";
                foreach (DataRow _dr in loJournalEntry.getAllData("","",pJournalEntryId,"").Rows)
                {
                    foreach (DataRow _dr1 in loJournalEntry.getPreparedByEmailAddress(_dr["Prepared By"].ToString()).Rows)
                    {
                        _preparerEmailAddress = _dr1[0].ToString();
                    }
                } 
                //send approver
                foreach (DataRow _dr in loJournalEntry.getAccountantEmailAddress(pJournalEntryId).Rows)
                {
                    try
                    {
                        if (_dr[1].ToString() != "")
                        {
                            string[] emailAdd = _dr[1].ToString().Split(',');
                            for (int i = 0; i < emailAdd.Length; i++)
                            {
                                foreach (DataRow _dr1 in loJournalEntry.getCheckAccountantEmailAddress(emailAdd[i], pCompanyCode).Rows)
                                {
                                    if (int.Parse(_dr1[0].ToString()) > 0)
                                    {
                                        GlobalFunctions.SendEmail(emailAdd[i], _preparerEmailAddress , "Request to Check and Post (J.E. Id : " + pJournalEntryId + ")", _bodyhead + _bodycontent);
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion "END OF METHODS"

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtTotalDebit.Text) != decimal.Parse(txtTotalCredit.Text))
            {
                MessageBoxUI _mb = new MessageBoxUI("Totals of Debit and Credit must be equal!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
            if (decimal.Parse(txtTotalDebit.Text) == 0 && decimal.Parse(txtTotalCredit.Text) == 0)
            {
                MessageBoxUI _mb = new MessageBoxUI("Totals of Debit and Credit must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
            if (dtpDatePrepared.Value.Year != int.Parse(GlobalVariables.CurrentFinancialYear))
            {
                MessageBoxUI _mb = new MessageBoxUI("Date Prepared must be within Current Financial Year!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
            loJournalEntry.JournalEntryId = lJournalEntryId;
            loJournalEntry.FinancialYear = int.Parse(txtFinancialYear.Text);
            loJournalEntry.Journal = "GJ";
            loJournalEntry.Form = "JV";
            loJournalEntry.VoucherNo = "";
            loJournalEntry.DatePrepared = dtpDatePrepared.Value;
            loJournalEntry.Explanation = GlobalFunctions.replaceChar(txtExplanation.Text);
            loJournalEntry.TotalDebit = decimal.Parse(txtTotalDebit.Text);
            loJournalEntry.TotalCredit = decimal.Parse(txtTotalCredit.Text);
            loJournalEntry.Reference = "";
            loJournalEntry.SupplierId = "";
            loJournalEntry.CustomerId = "";
            loJournalEntry.BegBal = chkBegBal.Checked ? "Y" : "N";
            loJournalEntry.Adjustment = chkAdjustment.Checked ? "Y" : "N";
            loJournalEntry.ClosingEntry = chkClosingEntry.Checked ? "Y" : "N";
            loJournalEntry.PreparedBy = GlobalVariables.Username;
            loJournalEntry.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
            loJournalEntry.SOId = "0";
            loJournalEntry.POId = "0";
            loJournalEntry.UserId = GlobalVariables.UserId;
            
            try
            {
                string _JournalEntryId = loJournalEntry.save(lOperation);
                if (_JournalEntryId != "")
                {
                    for(int i = 0; i < dgvDetail.Rows.Count; i++)
                    {
                        if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                        {
                            loJournalEntryDetail.DetailId = dgvDetail.Rows[i].Cells["Id"].Value.ToString();
                            loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                            loJournalEntryDetail.AccountId = dgvDetail.Rows[i].Cells["AccountId"].Value.ToString();
                            loJournalEntryDetail.Debit = decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                            loJournalEntryDetail.Credit = decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                            loJournalEntryDetail.Subsidiary = dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryId = dgvDetail.Rows[i].Cells["SubsidiaryId"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryDescription = dgvDetail.Rows[i].Cells["Description"].Value.ToString();
                            loJournalEntryDetail.Remarks = dgvDetail.Rows[i].Cells["Remarks"].Value.ToString();
                            loJournalEntryDetail.UserId = GlobalVariables.UserId;
                            loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                        }
                        else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                        {
                            loJournalEntryDetail.DetailId = dgvDetail.Rows[i].Cells["Id"].Value.ToString();
                            loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                            loJournalEntryDetail.AccountId = dgvDetail.Rows[i].Cells["AccountId"].Value.ToString();
                            loJournalEntryDetail.Debit = decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                            loJournalEntryDetail.Credit = decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                            loJournalEntryDetail.Subsidiary = dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryId = dgvDetail.Rows[i].Cells["SubsidiaryId"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryDescription = dgvDetail.Rows[i].Cells["Description"].Value.ToString();
                            loJournalEntryDetail.Remarks = dgvDetail.Rows[i].Cells["Remarks"].Value.ToString();
                            loJournalEntryDetail.UserId = GlobalVariables.UserId;
                            loJournalEntryDetail.save(GlobalVariables.Operation.Edit);
                        }
                        else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                        {
                            loJournalEntryDetail.remove(dgvDetail.Rows[i].Cells[0].Value.ToString());
                        }
                    }
                    
                    MessageBoxUI _mb = new MessageBoxUI("Journal Entry has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    try
                    {
                        if (lOperation == GlobalVariables.Operation.Edit)
                        {
                            //sendEmailForPosting(_JournalEntryId, cboCompany.SelectedValue.ToString(), cboCompany.Text, string.Format("{0:MM-dd-yyyy}", dtpDatePrepared.Value), txtExplanation.Text, decimal.Parse(txtTotalDebit.Text), decimal.Parse(txtTotalCredit.Text), GlobalVariables.Userfullname);
                        }
                    }
                    catch 
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Failure to send email", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb1.showDialog();
                    }
                    ParentList.GetType().GetMethod("refresh").Invoke(ParentList, null);
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unclosed quotation mark after the character string"))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Do not use this character( ' ).", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            VoucherDetailUI loVoucherDetail = new VoucherDetailUI();
            loVoucherDetail.ParentList = this;
            loVoucherDetail.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                VoucherDetailUI loVoucherDetail = new VoucherDetailUI(
                   dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["AccountId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Debit"].Value.ToString()),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Credit"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["Subsidiary"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["SubsidiaryId"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["Description"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loVoucherDetail.ParentList = this;
                loVoucherDetail.ShowDialog();
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Saved" ||
                dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Edit")
            {
                dgvDetail.CurrentRow.Cells["Status"].Value = "Delete";
                dgvDetail.CurrentRow.Visible = false;
            }
            else if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Add")
            {
                if (this.dgvDetail.SelectedRows.Count > 0)
                {
                    dgvDetail.Rows.RemoveAt(this.dgvDetail.SelectedRows[0].Index);
                } 
            }
            computeTotal();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDetail.Rows.Count; i++)
            {
                if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Saved" ||
                    dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                {
                    dgvDetail.Rows[i].Cells["Status"].Value = "Delete";
                    dgvDetail.Rows[i].Visible = false;
                }
                else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                {
                    dgvDetail.Rows.RemoveAt(this.dgvDetail.Rows[i].Index);
                }
            }
            computeTotal();
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void JournalVoucherUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            txtFinancialYear.Text = GlobalVariables.CurrentFinancialYear;

            if (lOperation == GlobalVariables.Operation.Edit)
            {
                foreach (DataRow _dr in loJournalEntry.getAllData("","",lJournalEntryId,"").Rows)
                {
                    txtFinancialYear.Text = _dr["F.Y."].ToString();
                    dtpDatePrepared.Value = GlobalFunctions.ConvertToDate(_dr["Date Prepared"].ToString());
                    txtExplanation.Text = _dr["Explanation"].ToString();
                    txtTotalDebit.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Debit"].ToString()));
                    txtTotalCredit.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Credit"].ToString()));
                    chkBegBal.Checked = _dr["Beg. Bal."].ToString() == "Y" ? true : false;
                    chkAdjustment.Checked = _dr["Adjustment"].ToString() == "Y" ? true : false;
                    chkClosingEntry.Checked = _dr["Closing Entry"].ToString() == "Y" ? true : false;
                    txtRemarks.Text = _dr["Remarks"].ToString();
                    foreach (DataRow _drDetails in loJournalEntryDetail.getJournalEntryDetails("", lJournalEntryId).Rows)
                    {
                        int i = dgvDetail.Rows.Add();
                        dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                        dgvDetail.Rows[i].Cells["AccountId"].Value = _drDetails["AccountId"].ToString();
                        dgvDetail.Rows[i].Cells["AccountCode"].Value = _drDetails["Account Code"].ToString();
                        dgvDetail.Rows[i].Cells["AccountTitle"].Value = _drDetails["Account Title"].ToString();
                        dgvDetail.Rows[i].Cells["Debit"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Debit"].ToString()));
                        dgvDetail.Rows[i].Cells["Credit"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Credit"].ToString()));
                        dgvDetail.Rows[i].Cells["Subsidiary"].Value = _drDetails["Subsidiary"].ToString();
                        dgvDetail.Rows[i].Cells["SubsidiaryId"].Value = _drDetails["SubsidiaryId"].ToString();
                        dgvDetail.Rows[i].Cells["Description"].Value = _drDetails["Subsidiary Description"].ToString();
                        dgvDetail.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                        dgvDetail.Rows[i].Cells["Status"].Value = "Saved";
                    }
                    computeTotal();
                }
            }
        }
    }
}
