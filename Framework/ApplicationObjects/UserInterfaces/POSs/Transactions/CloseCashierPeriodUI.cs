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
using JCSoftwares_V.ApplicationObjects.Classes.Accountings;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports.TransactionRpt;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions
{
    public partial class CloseCashierPeriodUI : Form
    {
        CashierPeriod loCashierPeriod;
        Cashier loCashier;
        POSTransaction loPOSTransaction;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        CashierPeriodSummaryRpt loCashierPeriodSummaryRpt;
        CloseCashierPeriodRpt loCloseCashierPeriodRpt;
        ReportViewerUI loReportViewer;
        public string lCashierPeriodId;
        
        public CloseCashierPeriodUI()
        {
            InitializeComponent();
            loCashierPeriod = new CashierPeriod();
            loCashier = new Cashier();
            loPOSTransaction = new POSTransaction();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashierPeriodSummaryRpt = new CashierPeriodSummaryRpt();
            loCloseCashierPeriodRpt = new CloseCashierPeriodRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void insertJournalEntry(decimal pTotalAmount, string pExplanation)
        {
            try
            {
                loJournalEntry.JournalEntryId = "";
                loJournalEntry.FinancialYear = int.Parse(GlobalVariables.CurrentFinancialYear);
                loJournalEntry.Journal = "SJ";
                loJournalEntry.Form = "SV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = DateTime.Now;
                loJournalEntry.Explanation = pExplanation;
                loJournalEntry.TotalDebit = pTotalAmount;
                loJournalEntry.TotalCredit = pTotalAmount;
                loJournalEntry.Reference = "";
                loJournalEntry.SupplierId = "";
                loJournalEntry.CustomerId = "";
                loJournalEntry.BegBal = "N";
                loJournalEntry.Adjustment = "N";
                loJournalEntry.ClosingEntry = "N";
                loJournalEntry.PreparedBy = GlobalVariables.Username;
                loJournalEntry.Remarks = "";
                loJournalEntry.SOId = "0";
                loJournalEntry.POId = "0";
                loJournalEntry.UserId = GlobalVariables.UserId;

                try
                {
                    string _JournalEntryId = loJournalEntry.save(GlobalVariables.Operation.Add);
                    if (_JournalEntryId != "")
                    {
                        //add debit
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.CashierPeriodDebit;
                        loJournalEntryDetail.Debit = pTotalAmount;
                        loJournalEntryDetail.Credit = 0;
                        loJournalEntryDetail.Subsidiary = "";
                        loJournalEntryDetail.SubsidiaryId = "";
                        loJournalEntryDetail.SubsidiaryDescription = "";
                        loJournalEntryDetail.Remarks = "";
                        loJournalEntryDetail.UserId = GlobalVariables.UserId;
                        loJournalEntryDetail.save(GlobalVariables.Operation.Add);

                        //add create
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.CashierPeriodCredit;
                        loJournalEntryDetail.Debit = 0;
                        loJournalEntryDetail.Credit = pTotalAmount;
                        loJournalEntryDetail.Subsidiary = "";
                        loJournalEntryDetail.SubsidiaryId = "";
                        loJournalEntryDetail.SubsidiaryDescription = "";
                        loJournalEntryDetail.Remarks = "";
                        loJournalEntryDetail.UserId = GlobalVariables.UserId;
                        loJournalEntryDetail.save(GlobalVariables.Operation.Add);

                        //post Journal Entry
                        loJournalEntry.post(_JournalEntryId);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CloseCashierPeriodUI_Load(object sender, EventArgs e)
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
                MessageBoxUI _mb = new MessageBoxUI("Only Cashier can close a Cashier Period!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                this.Close();
            }
            else
            {
                decimal _cashDeposit = 0;
                decimal _totalSales = 0;
                decimal _totalReturn = 0;
                decimal _discountAmount = 0;
                decimal _noncashsales = 0;
                decimal _cashsales = 0;
                foreach (DataRow _dr in loCashierPeriod.getAllData("", GlobalVariables.CashierPeriodId, "").Rows)
                {
                    _cashDeposit = decimal.Parse(_dr["Cash Deposit"].ToString());
                }

                foreach (DataRow _dr in loPOSTransaction.getTotalSalesByCashierPeriod(GlobalVariables.CashierPeriodId).Rows)
                {
                    _totalSales = decimal.Parse(_dr[0].ToString());
                }
                foreach (DataRow _dr in loPOSTransaction.getTotalReturnedByCashierPeriod(GlobalVariables.CashierPeriodId).Rows)
                {
                    _totalReturn = decimal.Parse(_dr[0].ToString());
                }
                foreach (DataRow _dr in loPOSTransaction.getTotalDiscountByCashierPeriod(GlobalVariables.CashierPeriodId).Rows)
                {
                    _discountAmount = decimal.Parse(_dr[0].ToString());
                }

                foreach (DataRow _dr in loPOSTransaction.getSalesByCashierPeriod(GlobalVariables.CashierPeriodId).Rows)
                {
                    if (_dr[0].ToString() == "Non-Cash")
                    {
                        _noncashsales = decimal.Parse(_dr[1].ToString());
                    }
                    else
                    {
                        _cashsales = decimal.Parse(_dr[1].ToString());
                    }
                }

                lblCashDeposit.Text = string.Format("{0:n}", _cashDeposit);
                lblTotalSales.Text = string.Format("{0:n}", _totalSales);
                lblReturnedItemTotal.Text = string.Format("{0:n}", _totalReturn);
                lblTotalDiscount.Text = string.Format("{0:n}", _discountAmount);
                lblNetSales.Text = string.Format("{0:n}", _totalSales - (_totalReturn+_discountAmount));
                lblNonCashSales.Text = string.Format("{0:n}", _noncashsales);
                lblCashSales.Text = string.Format("{0:n}", _cashsales);
                lblNetCashSales.Text = string.Format("{0:n}", (_cashsales+_cashDeposit) - _totalReturn);

                txtCashCount.Text = "0.00";
                lblVariance.Text = "0.00";
                txtRemarks.Clear();

                txtCashCount.Focus();
            }
        }

        private void txtCashCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblVariance.Text = string.Format("{0:n}", decimal.Parse(txtCashCount.Text) - decimal.Parse(lblNetCashSales.Text));
            }
            catch
            {
                lblVariance.Text = "0";
            }
        }

        private void txtCashCount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCashCount.Text = string.Format("{0:n}", decimal.Parse(txtCashCount.Text));
            }
            catch
            {
                txtCashCount.Text = "0.00";
            }
            try
            {
                lblVariance.Text = string.Format("{0:n}", decimal.Parse(txtCashCount.Text) - decimal.Parse(lblNetCashSales.Text));
            }
            catch
            {
                lblVariance.Text = "0";
            }
        }

        private void btnClosePeriod_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCloseCashierPeriod", "Close"))
                {
                    return;
                }
                
                string _CashierPeriodId = GlobalVariables.CashierPeriodId;

                if (loCashierPeriod.closeCashierPeriod(_CashierPeriodId, GlobalVariables.CashierId, 
                    decimal.Parse(lblTotalSales.Text), decimal.Parse(lblReturnedItemTotal.Text), 
                    decimal.Parse(lblTotalDiscount.Text),decimal.Parse(lblNetSales.Text),
                    decimal.Parse(lblNonCashSales.Text), decimal.Parse(lblCashSales.Text), 
                    decimal.Parse(lblNetCashSales.Text), decimal.Parse(txtCashCount.Text), 
                    decimal.Parse(lblVariance.Text), txtRemarks.Text))
                {
                    //insert to Accounting entry to Sales Journal
                    insertJournalEntry(decimal.Parse(lblNetSales.Text), "POS Sales ("+string.Format("{0:MM-dd-yyyy}",DateTime.Now)+") || Cashier Period Id:"+GlobalVariables.CashierPeriodId);

                    GlobalVariables.CashierPeriodId = "";
                    MessageBoxUI _mb = new MessageBoxUI("Cashier period has been close successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    this.Close();
                    foreach (DataRow _dr in loCashierPeriod.getAllData("",_CashierPeriodId,"").Rows)
                    {
                        if (GlobalVariables.ORSize == "Plain Continuous-Long")
                        {
                            loCashierPeriodSummaryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loCashierPeriodSummaryRpt.Subreports["CashierPeriodStockSoldRpt.rpt"].SetDataSource(loCashierPeriod.getCashierPeriodStockSold(_CashierPeriodId));
                            loCashierPeriodSummaryRpt.Subreports["CashierPeriodReturnedItemRpt.rpt"].SetDataSource(loCashierPeriod.getCashierPeriodReturnedItem(_CashierPeriodId));
                            //loCashierPeriodSummaryRpt.Database.Tables[1].SetDataSource(loPOSTransactionDetail.getPOSTransactionDetails(pPOSTransactionId));
                            loCashierPeriodSummaryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loCashierPeriodSummaryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loCashierPeriodSummaryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loCashierPeriodSummaryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loCashierPeriodSummaryRpt.SetParameterValue("Title", "Cashier Period Summary");
                            loCashierPeriodSummaryRpt.SetParameterValue("SubTitle", "Cashier Period Summary");
                            loCashierPeriodSummaryRpt.SetParameterValue("CashierPeriodNo", _dr["Id"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("DateOpen", _dr["Date Open"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("DateClose", _dr["Date Close"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("PeriodStatus", _dr["Period Status"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("OpenCashier", _dr["Open Cashier"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("CloseCashier", _dr["Close Cashier"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("CashDeposit", string.Format("{0:n}", decimal.Parse(_dr["Cash Deposit"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("TotalSales", string.Format("{0:n}", decimal.Parse(_dr["Total Sales"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("ReturnedItemTotal", string.Format("{0:n}", decimal.Parse(_dr["Returned Item Total"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("TotalDiscount", string.Format("{0:n}", decimal.Parse(_dr["Total Discount"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("NetSales", string.Format("{0:n}", decimal.Parse(_dr["Net Sales"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("NonCashSales", string.Format("{0:n}", decimal.Parse(_dr["Non-Cash Sales"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("CashSales", string.Format("{0:n}", decimal.Parse(_dr["Cash Sales"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("NetCashSales", string.Format("{0:n}", decimal.Parse(_dr["Net Cash Sales"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("CashCount", string.Format("{0:n}", decimal.Parse(_dr["Cash Count"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("Variance", string.Format("{0:n}", decimal.Parse(_dr["Variance"].ToString())));
                            loCashierPeriodSummaryRpt.SetParameterValue("Remarks", _dr["Remarks"].ToString());
                            loCashierPeriodSummaryRpt.SetParameterValue("PreparedBy", _dr["Close Cashier"].ToString());
                            loReportViewer.crystalReportViewer.ReportSource = loCashierPeriodSummaryRpt;
                            loReportViewer.ShowDialog();
                        }
                        else if (GlobalVariables.ORSize == "Receipt Printed(80mm)")
                        {
                            loCloseCashierPeriodRpt.Subreports["ItemSoldRpt.rpt"].SetDataSource(loCashierPeriod.getCashierPeriodStockSold(_CashierPeriodId));
                            loCloseCashierPeriodRpt.Subreports["ItemReturnedRpt.rpt"].SetDataSource(loCashierPeriod.getCashierPeriodReturnedItem(_CashierPeriodId));
                            loCloseCashierPeriodRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName.ToUpper());
                            loCloseCashierPeriodRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loCloseCashierPeriodRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loCloseCashierPeriodRpt.SetParameterValue("PeriodNo", _dr["Id"].ToString());
                            loCloseCashierPeriodRpt.SetParameterValue("Cashier", _dr["Close Cashier"].ToString());
                            //loCloseCashierPeriodRpt.SetParameterValue("Date", _dr["Date Close"].ToString());
                            loCloseCashierPeriodRpt.SetParameterValue("DateOpen", _dr["Date Open"].ToString());
                            loCloseCashierPeriodRpt.SetParameterValue("DateClose", _dr["Date Close"].ToString());
                            loCloseCashierPeriodRpt.SetParameterValue("CashDeposit", string.Format("{0:n}", decimal.Parse(_dr["Cash Deposit"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("TotalSales", string.Format("{0:n}", decimal.Parse(_dr["Total Sales"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("ReturnedItem", "(" + string.Format("{0:n}", decimal.Parse(_dr["Returned Item Total"].ToString())) + ")");
                            loCloseCashierPeriodRpt.SetParameterValue("TotalDiscount", string.Format("{0:n}", decimal.Parse(_dr["Total Discount"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("NetSales", string.Format("{0:n}", decimal.Parse(_dr["Net Sales"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("NonCashSales", string.Format("{0:n}", decimal.Parse(_dr["Non-Cash Sales"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("CashSales", string.Format("{0:n}", decimal.Parse(_dr["Cash Sales"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("NetCashSales", string.Format("{0:n}", decimal.Parse(_dr["Net Cash Sales"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("CashCount", string.Format("{0:n}", decimal.Parse(_dr["Cash Count"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("Variance", string.Format("{0:n}", decimal.Parse(_dr["Variance"].ToString())));
                            loCloseCashierPeriodRpt.SetParameterValue("Remarks", _dr["Remarks"].ToString());

                            if (GlobalVariables.PreviewOR)
                            {
                                loReportViewer.crystalReportViewer.ReportSource = loCloseCashierPeriodRpt;
                                loReportViewer.ShowDialog();
                            }
                            else
                            {
                                try
                                {
                                    loCloseCashierPeriodRpt.PrintOptions.PrinterName = GlobalVariables.PrinterName;
                                    loCloseCashierPeriodRpt.PrintToPrinter(0, false, 0, 0);
                                }
                                catch { }
                            }
                            
                        }
                    }
                }
            }
            catch { }
        }

        private void txtCashCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnClosePeriod_Click(null, new EventArgs());
            }
        }

        private void pnlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblVariance_TextChanged(object sender, EventArgs e)
        {
            if (decimal.Parse(lblVariance.Text) == 0)
            {
                lblVariance.ForeColor = Color.Green;
            }
            else if (decimal.Parse(lblVariance.Text) > 0)
            {
                lblVariance.ForeColor = Color.Blue;
            }
            else
            {
                lblVariance.ForeColor = Color.Red;
            }
        }
    }
}
