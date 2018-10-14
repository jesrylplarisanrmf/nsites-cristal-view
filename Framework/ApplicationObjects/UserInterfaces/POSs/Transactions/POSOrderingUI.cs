using System;
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
using JCSoftwares_V.ApplicationObjects.Classes;
using JCSoftwares_V.ApplicationObjects.Classes.POSs;
using JCSoftwares_V.ApplicationObjects.Classes.Inventorys;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports.TransactionRpt;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions
{
    public partial class POSOrderingUI : Form
    {
        POSTransaction loPOSTransaction;
        POSTransactionDetail loPOSTransactionDetail;
        Stock loStock;
        CashierPeriod loCashierPeriod;
        Customer loCustomer;
        ModeOfPayment loModeOfPayment;
        Cashier loCashier;

        SecurityUI loSecurity;
        OtherFunctionsUI loOtherFunctions;
        
        RenderUI loRender;
        OrderSlipRpt loOrderSlipRpt;
        ReportViewerUI loReportViewer;
        string lOperation;
        bool lAdditionalItem;
        
        public POSOrderingUI()
        {
            InitializeComponent();
            loPOSTransaction = new POSTransaction();
            loPOSTransactionDetail = new POSTransactionDetail();
            loStock = new Stock();
            loCashierPeriod = new CashierPeriod();
            loCustomer = new Customer();
            loModeOfPayment = new ModeOfPayment();
            loCashier = new Cashier();

            loSecurity = new SecurityUI();
            loOtherFunctions = new OtherFunctionsUI();

            loRender = new RenderUI();
            loOrderSlipRpt = new OrderSlipRpt();
            loReportViewer = new ReportViewerUI();
            lOperation = "";
            lAdditionalItem = false;
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void computeTotal()
        {
            decimal _totalqty = 0;
            decimal _totalamount = 0;

            for (int i = 0; i < dgvDetailStockList.Rows.Count; i++)
            {
                _totalqty += decimal.Parse(dgvDetailStockList.Rows[i].Cells["DetailQty"].Value.ToString());
                _totalamount += decimal.Parse(dgvDetailStockList.Rows[i].Cells["DetailTotalPrice"].Value.ToString());
            }
            try
            {
                string[] _qty = _totalqty.ToString().Split('.');
                if (_qty[1].ToString() == "00")
                {
                    lblTotalQty.Text = string.Format("{0:0}", _totalqty);
                }
                else
                {
                    lblTotalQty.Text = string.Format("{0:n}", _totalqty);
                }
            }
            catch
            {
                lblTotalQty.Text = string.Format("{0:0}", _totalqty);
            }
            
            lblTotalAmount.Text = string.Format("{0:n}", _totalamount);
        }

        private void clear()
        {
            lblTotalQty.Text = "0";
            lblTotalAmount.Text = "0.00";
            //lblTransNo.Text = "";
            //lblTerminal.Text = "";
            cboCustomer.Text = GlobalVariables.DefaultCustomerName;
            cboCustomer.Enabled = true;
            btnCustomerLookUp.Enabled = true;
            dgvTransactionList.ClearSelection();
            dgvDetailStockList.Rows.Clear();
            txtQty.Text = "1";
            txtStockCodeSearch.Clear();
            txtStockDescriptionSearch.Clear();
            dgvSearchStockList.Rows.Clear();
            txtStockCodeSearch.Focus();
        }

        private void autosave()
        {
            /*
            if (!GlobalFunctions.getCashierPeriodStatus())
            {
                MessageBoxUI _mb = new MessageBoxUI("Cashier Period must be Open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
            */
            loPOSTransaction.Date = DateTime.Now;
            loPOSTransaction.CashierPeriodId = GlobalVariables.CashierPeriodId;
            loPOSTransaction.CustomerId = GlobalVariables.DefaultCustomerId;
            loPOSTransaction.OrderType = "Default";
            loPOSTransaction.TableId = "";
            loPOSTransaction.ORNo = "";
            loPOSTransaction.TotalPrice = decimal.Parse(lblTotalAmount.Text);
            loPOSTransaction.TotalDiscount = 0;
            loPOSTransaction.TotalLessVAT = 0;
            loPOSTransaction.TotalDue = decimal.Parse(lblTotalAmount.Text);
            loPOSTransaction.VATSale = 0;
            loPOSTransaction.VATExemptSale = 0;
            loPOSTransaction.VATAmount = 0;
            loPOSTransaction.AmountTendered = 0;
            loPOSTransaction.Paid = "N";
            loPOSTransaction.DiscountId = "";
            loPOSTransaction.ModeOfPaymentId = "0";
            loPOSTransaction.PaymentDetails = "";
            loPOSTransaction.OutletId = GlobalVariables.CurrentLocationId;
            loPOSTransaction.CashierId = "0";
            loPOSTransaction.Terminal = GlobalVariables.Terminal;
            loPOSTransaction.Remarks = "";
            loPOSTransaction.UserId = GlobalVariables.UserId;
            try
            {
                string _TransactionNo;
                if (lOperation == "Add")
                {
                    _TransactionNo = loPOSTransaction.save(GlobalVariables.Operation.Add);
                }
                else if (lOperation == "Edit")
                {
                    loPOSTransaction.Id = dgvTransactionList.CurrentRow.Cells[0].Value.ToString();
                    _TransactionNo = loPOSTransaction.save(GlobalVariables.Operation.Edit);
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Operation not defined!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }


                if (_TransactionNo != "")
                {
                    foreach (DataGridViewRow _dr in dgvDetailStockList.Rows)
                    {
                        loPOSTransactionDetail.TransactionId = _TransactionNo;
                        loPOSTransactionDetail.StockId = _dr.Cells["DetailStockId"].Value.ToString();
                        loPOSTransactionDetail.LocationId = GlobalVariables.CurrentLocationId;
                        loPOSTransactionDetail.VATable = "Y";
                        loPOSTransactionDetail.Qty = decimal.Parse(_dr.Cells["DetailQty"].Value.ToString());
                        loPOSTransactionDetail.UnitCost = decimal.Parse(_dr.Cells["DetailUnitCost"].Value.ToString());
                        loPOSTransactionDetail.BasePrice = decimal.Parse(_dr.Cells["DetailBasePrice"].Value.ToString());
                        loPOSTransactionDetail.UnitPrice = decimal.Parse(_dr.Cells["DetailUnitPrice"].Value.ToString());
                        loPOSTransactionDetail.DiscountId = "0";
                        loPOSTransactionDetail.DiscountAmount = 0;
                        loPOSTransactionDetail.TotalPrice = decimal.Parse(_dr.Cells["DetailTotalPrice"].Value.ToString());
                        loPOSTransactionDetail.UserId = GlobalVariables.UserId;

                        if (_dr.Cells[0].Value.ToString() == "")
                        {
                            loPOSTransactionDetail.save(GlobalVariables.Operation.Add);
                        }
                        else
                        {
                            loPOSTransactionDetail.DetailId = _dr.Cells[0].Value.ToString();
                            loPOSTransactionDetail.save(GlobalVariables.Operation.Edit);
                        }
                    }

                    dgvTransactionList.CurrentRow.Cells["TransactionTotalAmount"].Value = string.Format("{0:n}", decimal.Parse(lblTotalAmount.Text));
                    getTransactionDetailLists();
                }
            }
            catch (Exception ex)
            {
                MessageBoxUI _mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void getTrasactionLists()
        {
            dgvTransactionList.Rows.Clear();
            foreach (DataRow _dr in loPOSTransaction.getPOSTransactionLists(GlobalVariables.CashierPeriodId).Rows)
            {
                int i = dgvTransactionList.Rows.Add();
                dgvTransactionList.Rows[i].Cells["TransactionNo"].Value = _dr["Id"].ToString();
                dgvTransactionList.Rows[i].Cells["TransactionTerminal"].Value = _dr["Terminal"].ToString();
                dgvTransactionList.Rows[i].Cells["TransactionTotalAmount"].Value = string.Format("{0:n}", decimal.Parse(_dr["Total Due"].ToString()));
                dgvTransactionList.Rows[i].Cells["TransactionPaid"].Value = _dr["Paid"].ToString();
                dgvTransactionList.Rows[i].Cells["TransactionCustomerId"].Value = _dr["CustomerId"].ToString();
                dgvTransactionList.Rows[i].Cells["TransactionCustomer"].Value = _dr["Customer"].ToString();
            }

            try
            {
                //lblTransNo.Text = dgvTransactionList.CurrentRow.Cells[0].Value.ToString();
                //lblTerminal.Text = dgvTransactionList.CurrentRow.Cells[1].Value.ToString();
                getTransactionDetailLists();
                btnSave.Enabled = false;

                if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                {
                    btnRender.Enabled = false;
                }
                else
                {
                    btnRender.Enabled = true;
                }
            }
            catch { }
        }

        private void getTransactionDetailLists()
        {
            dgvDetailStockList.Rows.Clear();
            lOperation = "Edit";
            foreach (DataRow _dr in loPOSTransactionDetail.getPOSTransactionDetails(dgvTransactionList.CurrentRow.Cells[0].Value.ToString()).Rows)
            {
                int n = dgvDetailStockList.Rows.Add();
                dgvDetailStockList.Rows[n].Cells["DetailId"].Value = _dr["DetailId"].ToString();
                dgvDetailStockList.Rows[n].Cells["DetailStockId"].Value = _dr["StockId"].ToString();
                dgvDetailStockList.Rows[n].Cells["DetailStockDescription"].Value = _dr["StockDescription"].ToString();
                dgvDetailStockList.Rows[n].Cells["DetailUnit"].Value = _dr["Unit"].ToString();
                try
                {
                    string[] _qty = _dr["Qty"].ToString().Split('.');
                    if (_qty[1].ToString() == "00")
                    {
                        dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", decimal.Parse(_dr["Qty"].ToString()));
                    }
                    else
                    {
                        dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:n}", decimal.Parse(_dr["Qty"].ToString()));
                    }
                }
                catch
                {
                    dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", decimal.Parse(_dr["Qty"].ToString()));
                }
                //dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", decimal.Parse(_dr["Qty"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailUnitCost"].Value = string.Format("{0:n}", decimal.Parse(_dr["UnitCost"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailBasePrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["BasePrice"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailUnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["UnitPrice"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailTotalPrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["TotalPrice"].ToString()));
            }
            computeTotal();
        }

        private void getDefault()
        {
            //get default customer
            foreach (DataRow _dr in loCustomer.getCustomerDefault().Rows)
            {
                GlobalVariables.DefaultCustomerId = _dr[0].ToString();
                GlobalVariables.DefaultCustomerName = _dr[1].ToString();
            }
            //get default mode of payment
            foreach (DataRow _dr in loModeOfPayment.getModeOfPaymentDefault().Rows)
            {
                GlobalVariables.DefaultModeOfPaymentId = _dr[0].ToString();
                GlobalVariables.DefaultModeOfPaymentDescription = _dr[2].ToString();
            }
            /*
            //get default table
            foreach (DataRow _dr in loTable.getTableDefault().Rows)
            {
                GlobalVariables.DefaultTableCode = _dr[0].ToString();
                GlobalVariables.DefaultTableDescription = _dr[1].ToString();
            }
            */
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys
       keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.F1:
                        btnNewOrder_Click(null, new EventArgs());
                        return true;
                    case Keys.F5:
                        btnSave_Click(null, new EventArgs());
                        return true;
                    case Keys.F7:
                        txtQty.Focus();
                        return true;
                    case Keys.F8:
                        txtStockCodeSearch.BackColor = SystemColors.HighlightText;
                        txtStockCodeSearch.Focus();
                        return true;
                    case Keys.F9:
                        txtStockDescriptionSearch.BackColor = SystemColors.HighlightText;
                        txtStockDescriptionSearch.Focus();
                        return true;
                    case Keys.F12:
                        btnRender_Click(null, new EventArgs());
                        return true;
                    case Keys.Insert:
                        return true;
                    case Keys.Delete:
                        //tsmiRemove_Click(null, new EventArgs());
                        return true;
                    case Keys.Escape:
                        return true;
                    case Keys.Add:
                        return true;
                    case Keys.Subtract:
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void printOrderSlip(string pPOSTransactionId, string pORNo)
        {
            foreach (DataRow _dr in loPOSTransaction.getPOSTransaction(pPOSTransactionId).Rows)
            {
                if (GlobalVariables.ORSize == "Plain Continuous-Long")
                {
                    loOrderSlipRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loOrderSlipRpt.Database.Tables[1].SetDataSource(loPOSTransactionDetail.getPOSTransactionDetails(pPOSTransactionId));
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

                    if (GlobalVariables.PreviewSlip)
                    {
                        loReportViewer.crystalReportViewer.ReportSource = loOrderSlipRpt;
                        loReportViewer.ShowDialog();
                    }
                    else
                    {
                        try
                        {
                            loOrderSlipRpt.PrintOptions.PrinterName = GlobalVariables.PrinterName;
                            loOrderSlipRpt.PrintToPrinter(0, false, 0, 0);
                        }
                        catch { }
                    }
                }
                else if (GlobalVariables.ORSize == "Receipt Printed(80mm)")
                {
                    try
                    {
                        foreach (DataRow _dr80mm in loPOSTransaction.getPOSTransaction(pPOSTransactionId).Rows)
                        {
                            loOrderSlipRpt.Subreports["TransactionDetailListRpt.rpt"].SetDataSource(loPOSTransactionDetail.getPOSTransactionDetails(pPOSTransactionId));
                            loOrderSlipRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName.ToUpper());
                            loOrderSlipRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loOrderSlipRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);

                            loOrderSlipRpt.SetParameterValue("Date", string.Format("{0:MM-dd-yyyy}", DateTime.Parse(_dr80mm["Date"].ToString())));
                            loOrderSlipRpt.SetParameterValue("TransactionNo", pPOSTransactionId);
                            loOrderSlipRpt.SetParameterValue("Customer", _dr["Customer"].ToString());
                        }
                    }
                    catch { }

                    if (GlobalVariables.PreviewSlip)
                    {
                        loReportViewer.crystalReportViewer.ReportSource = loOrderSlipRpt;
                        loReportViewer.ShowDialog();
                    }
                    else
                    {
                        try
                        {
                            loOrderSlipRpt.PrintOptions.PrinterName = GlobalVariables.PrinterName;
                            loOrderSlipRpt.PrintToPrinter(0, false, 0, 0);
                        }
                        catch { }
                    }
                }
            }
        }

        private void getStockQty()
        {
            StockQtyUI loStockQty = new StockQtyUI();
            loStockQty.lStockId = dgvSearchStockList.CurrentRow.Cells["SearchStockId"].Value.ToString();
            loStockQty.lStockDescription = dgvSearchStockList.CurrentRow.Cells["SearchStockDescription"].Value.ToString();
            loStockQty.lUnitCost = decimal.Parse(dgvSearchStockList.CurrentRow.Cells["SearchUnitCost"].Value.ToString());
            loStockQty.lBasePrice = decimal.Parse(dgvSearchStockList.CurrentRow.Cells["SearchBasePrice"].Value.ToString());
            loStockQty.lUnitPrice = decimal.Parse(dgvSearchStockList.CurrentRow.Cells["SearchUnitPrice"].Value.ToString());
            loStockQty.lQty = decimal.Parse(txtQty.Text);
            loStockQty.ShowDialog();
            if (loStockQty.lFromOK)
            {
                if (loStockQty.lQty > 0)
                {
                    int n = dgvDetailStockList.Rows.Add();
                    dgvDetailStockList.Rows[n].Cells["DetailId"].Value = "";
                    dgvDetailStockList.Rows[n].Cells["DetailStockId"].Value = dgvSearchStockList.CurrentRow.Cells["SearchStockId"].Value.ToString();
                    dgvDetailStockList.Rows[n].Cells["DetailStockDescription"].Value = dgvSearchStockList.CurrentRow.Cells["SearchStockDescription"].Value.ToString();
                    dgvDetailStockList.Rows[n].Cells["DetailUnit"].Value = dgvSearchStockList.CurrentRow.Cells["SearchUnit"].Value.ToString();
                    try
                    {
                        string[] _qty = loStockQty.lQty.ToString().Split('.');
                        if (_qty[1].ToString() == "00")
                        {
                            dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", loStockQty.lQty);
                        }
                        else
                        {
                            dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:n}", loStockQty.lQty);
                        }
                    }
                    catch
                    {
                        dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", loStockQty.lQty);
                    }
                    dgvDetailStockList.Rows[n].Cells["DetailUnitCost"].Value = string.Format("{0:n}", loStockQty.lUnitCost);
                    dgvDetailStockList.Rows[n].Cells["DetailBasePrice"].Value = string.Format("{0:n}", loStockQty.lBasePrice);
                    dgvDetailStockList.Rows[n].Cells["DetailUnitPrice"].Value = string.Format("{0:n}", loStockQty.lUnitPrice);
                    dgvDetailStockList.Rows[n].Cells["DetailTotalPrice"].Value = string.Format("{0:n}", loStockQty.lQty * loStockQty.lUnitPrice);

                    dgvDetailStockList.FirstDisplayedScrollingRowIndex = dgvDetailStockList.Rows[n].Index;
                    dgvDetailStockList.Rows[n].Selected = true;
                }
                computeTotal();
                if (lAdditionalItem)
                {
                    autosave();
                }
            }
            txtQty.Text = "1";
            txtStockDescriptionSearch.Focus();
        }

        private void getStockDetails(string pStockCode)
        {
            foreach (DataRow _dr in loStock.getSaleableStock(pStockCode, "").Rows)
            {
                int n = dgvDetailStockList.Rows.Add();
                dgvDetailStockList.Rows[n].Cells["DetailId"].Value = "";
                dgvDetailStockList.Rows[n].Cells["DetailStockId"].Value = _dr["Id"].ToString();
                dgvDetailStockList.Rows[n].Cells["DetailStockDescription"].Value = _dr["Description"].ToString();
                dgvDetailStockList.Rows[n].Cells["DetailUnit"].Value = _dr["Unit"].ToString();
                try
                {
                    string[] _qty = txtQty.Text.Split('.');
                    if (_qty[1].ToString() == "00")
                    {
                        dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", decimal.Parse(txtQty.Text));
                    }
                    else
                    {
                        dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:n}", decimal.Parse(txtQty.Text));
                    }
                }
                catch
                {
                    dgvDetailStockList.Rows[n].Cells["DetailQty"].Value = string.Format("{0:0}", decimal.Parse(txtQty.Text));
                }
                dgvDetailStockList.Rows[n].Cells["DetailUnitCost"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Cost"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailBasePrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Base Price"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailUnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                dgvDetailStockList.Rows[n].Cells["DetailTotalPrice"].Value = string.Format("{0:n}", decimal.Parse(txtQty.Text) * decimal.Parse(_dr["Unit Price"].ToString()));

                dgvDetailStockList.FirstDisplayedScrollingRowIndex = dgvDetailStockList.Rows[n].Index;
                dgvDetailStockList.Rows[n].Selected = true;
            }
            computeTotal();
            if (lAdditionalItem)
            {
                autosave();
            }
        }

        private void POSOrderingUI_Load(object sender, EventArgs e)
        {
            //lblUserName.Text = GlobalVariables.Userfullname.ToUpper();
            //lblCompanyName.Text = GlobalVariables.CompanyName.ToUpper();
            getDefault();

            cboCustomer.DataSource = loCustomer.getAllData("ViewAll","","");
            cboCustomer.ValueMember = "Id";
            cboCustomer.DisplayMember = "Name";
            cboCustomer.SelectedIndex = -1;

            foreach (DataRow _dr in loCashier.getCashierDetails(GlobalVariables.UserId).Rows)
            {
                GlobalVariables.CashierId = _dr[0].ToString();
                GlobalVariables.CashierName = _dr[2].ToString();
            }

            foreach (DataRow _dr in loCashierPeriod.getCashierPeriodOpen().Rows)
            {
                GlobalVariables.CashierPeriodId = _dr[0].ToString();
            }
            try
            {
                getTrasactionLists();
            }
            catch { }
            btnSave.Enabled = false;
            btnRender.Enabled = false;
            /*
            if (GlobalVariables.Type != "Cashier")
            {
                btnRender.Visible = false;
                btnOtherFunctions.Visible = false;
            }
            */
            txtStockCodeSearch.Enabled = false;
            txtStockDescriptionSearch.Enabled = false;
            txtStockCodeSearch.BackColor = SystemColors.Control;
            txtStockDescriptionSearch.BackColor = SystemColors.Control;

            cboCustomer.Enabled = false;
            btnCustomerLookUp.Enabled = false;

            try
            {
                if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                {
                    btnRender.Enabled = false;
                }
                else
                {
                    btnRender.Enabled = true;
                }
            }
            catch
            {
                btnRender.Enabled = false;
            }

            btnNewOrder.Focus();
        }

        private void txtStockDescriptionSearch_TextChanged(object sender, EventArgs e)
        {
            dgvSearchStockList.Rows.Clear();
            foreach (DataRow _dr in loStock.getSaleableStock("", txtStockDescriptionSearch.Text).Rows)
            {
                int n = dgvSearchStockList.Rows.Add();
                dgvSearchStockList.Rows[n].Cells["SearchStockId"].Value = _dr["Id"].ToString();
                dgvSearchStockList.Rows[n].Cells["SearchStockCode"].Value = _dr["Code"].ToString();
                dgvSearchStockList.Rows[n].Cells["SearchStockDescription"].Value = _dr["Description"].ToString();
                dgvSearchStockList.Rows[n].Cells["SearchUnit"].Value = _dr["Unit"].ToString();
                dgvSearchStockList.Rows[n].Cells["SearchQtyOnHand"].Value = string.Format("{0:n}", decimal.Parse(_dr["Qty on Hand"].ToString()));
                dgvSearchStockList.Rows[n].Cells["SearchUnitCost"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Cost"].ToString()));
                dgvSearchStockList.Rows[n].Cells["SearchBasePrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Base Price"].ToString()));
                dgvSearchStockList.Rows[n].Cells["SearchUnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                dgvSearchStockList.Rows[n].Cells["SearchRemarks"].Value = _dr["Remarks"].ToString();
            }
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            clear();
            lOperation = "Add";
            lAdditionalItem = false;

            btnSave.Enabled = true;
            btnRender.Enabled = true;

            txtStockCodeSearch.Enabled = true;
            txtStockDescriptionSearch.Enabled = true;
            txtStockCodeSearch.BackColor = SystemColors.HighlightText;
            //txtStockDescriptionSearch.BackColor = SystemColors.HighlightText;
            //txtStockDescriptionSearch.Focus();
            txtStockCodeSearch.BackColor = SystemColors.HighlightText;
            txtStockCodeSearch.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*
            if (GlobalVariables.Type == "Cashier")
            {
                if (!GlobalFunctions.getCashierPeriodStatus())
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier Period must be Open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            */
            if (decimal.Parse(lblTotalAmount.Text) <= 0)
            {
                MessageBoxUI _mb = new MessageBoxUI("Total Amount must be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }

            loPOSTransaction.Date = DateTime.Now;
            loPOSTransaction.CashierPeriodId = GlobalVariables.CashierPeriodId;
            try
            {
                loPOSTransaction.CustomerId = cboCustomer.SelectedValue.ToString();
            }
            catch
            {
                loPOSTransaction.CustomerId = GlobalVariables.DefaultCustomerId;
            }
            loPOSTransaction.OrderType = "Default";
            loPOSTransaction.TableId = "0";
            loPOSTransaction.ORNo = "";
            loPOSTransaction.TotalPrice = decimal.Parse(lblTotalAmount.Text);
            loPOSTransaction.TotalDiscount = 0;
            loPOSTransaction.TotalLessVAT = 0;
            loPOSTransaction.TotalDue = decimal.Parse(lblTotalAmount.Text);
            loPOSTransaction.VATSale = 0;
            loPOSTransaction.VATExemptSale = 0;
            loPOSTransaction.VATAmount = 0;
            loPOSTransaction.AmountTendered = 0;
            loPOSTransaction.Paid = "N";
            loPOSTransaction.DiscountId = "";
            loPOSTransaction.ModeOfPaymentId = "0";
            loPOSTransaction.PaymentDetails = "";
            loPOSTransaction.OutletId = GlobalVariables.CurrentLocationId;
            loPOSTransaction.CashierId = "0";
            loPOSTransaction.Terminal = GlobalVariables.Terminal;
            loPOSTransaction.Remarks = "";
            loPOSTransaction.UserId = GlobalVariables.UserId;
            try
            {
                string _TransactionNo;
                if (lOperation == "Add")
                {
                    _TransactionNo = loPOSTransaction.save(GlobalVariables.Operation.Add);
                }
                else if (lOperation == "Edit")
                {
                    loPOSTransaction.Id = dgvTransactionList.CurrentRow.Cells[0].Value.ToString();
                    _TransactionNo = loPOSTransaction.save(GlobalVariables.Operation.Edit);
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Operation not defined!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }


                if (_TransactionNo != "")
                {
                    foreach (DataGridViewRow _dr in dgvDetailStockList.Rows)
                    {
                        loPOSTransactionDetail.TransactionId = _TransactionNo;
                        loPOSTransactionDetail.StockId = _dr.Cells["DetailStockId"].Value.ToString();
                        loPOSTransactionDetail.LocationId = GlobalVariables.CurrentLocationId;
                        loPOSTransactionDetail.VATable = "Y";
                        loPOSTransactionDetail.Qty = decimal.Parse(_dr.Cells["DetailQty"].Value.ToString());
                        loPOSTransactionDetail.UnitCost = decimal.Parse(_dr.Cells["DetailUnitCost"].Value.ToString());
                        loPOSTransactionDetail.BasePrice = decimal.Parse(_dr.Cells["DetailBasePrice"].Value.ToString());
                        loPOSTransactionDetail.UnitPrice = decimal.Parse(_dr.Cells["DetailUnitPrice"].Value.ToString());
                        loPOSTransactionDetail.DiscountId = "0";
                        loPOSTransactionDetail.DiscountAmount = 0;
                        loPOSTransactionDetail.TotalPrice = decimal.Parse(_dr.Cells["DetailTotalPrice"].Value.ToString());
                        loPOSTransactionDetail.UserId = GlobalVariables.UserId;

                        if (_dr.Cells[0].Value.ToString() == "")
                        {
                            loPOSTransactionDetail.save(GlobalVariables.Operation.Add);
                        }
                        else
                        {
                            loPOSTransactionDetail.DetailId = _dr.Cells[0].Value.ToString();
                            loPOSTransactionDetail.UserId = GlobalVariables.UserId;
                            loPOSTransactionDetail.save(GlobalVariables.Operation.Edit);
                        }
                    }

                    if (lOperation == "Add")
                    {
                        printOrderSlip(_TransactionNo, "");
                    }

                    clear();
                    getTrasactionLists();

                    txtStockCodeSearch.Enabled = false;
                    txtStockDescriptionSearch.Enabled = false;
                    txtStockCodeSearch.BackColor = SystemColors.Control;
                    txtStockDescriptionSearch.BackColor = SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Stock Code already exist!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else if (ex.Message.Contains("Unclosed quotation mark after the character string"))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Do not use this character( ' ).", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CashierId != "0" || GlobalVariables.CashierId == "")
            {
                if (GlobalFunctions.getCashierPeriodStatus(GlobalVariables.CashierId))
                {
                    if (decimal.Parse(lblTotalAmount.Text) > 0)
                    {
                        string _transactiono;
                        if (lOperation == "Edit")
                        {
                            if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                            {
                                MessageBoxUI _mb = new MessageBoxUI("This is a Paid Transaction!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                                _mb.showDialog();
                                return;
                            }
                            _transactiono = dgvTransactionList.CurrentRow.Cells[0].Value.ToString();
                        }
                        else
                        {
                            _transactiono = "";
                        }

                        //RenderUI loRender = new RenderUI_transactiono, decimal.Parse(lblTotalAmount.Text), lblOrderType.Text, lblTableCode.Text, ref dgvDetailStockList, lOperation);
                        loRender.lTransactionId = _transactiono;
                        loRender.lTotalDue = decimal.Parse(lblTotalAmount.Text);
                        try
                        {
                            if (lOperation == "Edit")
                            {
                                loRender.lCustomerId = dgvTransactionList.CurrentRow.Cells["TransactionCustomerId"].Value.ToString();
                                loRender.lCustomerName = dgvTransactionList.CurrentRow.Cells["TransactionCustomer"].Value.ToString();
                            }
                            else
                            {
                                loRender.lCustomerId = cboCustomer.SelectedValue.ToString();
                                loRender.lCustomerName = cboCustomer.Text;
                            }
                        }
                        catch { }
                        loRender.lOrderType = "Default";
                        loRender.lTableId = "0";
                        loRender.loDataGrid = dgvDetailStockList;
                        loRender.lOperation = lOperation;
                        loRender.ShowDialog();
                        if (!loRender.lFromClose)
                        {
                            clear();
                            try
                            {
                                getTrasactionLists();
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        MessageBoxUI _mb = new MessageBoxUI("Total Amount must be greater than zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb.showDialog();
                        return;
                    }
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier Period must be Open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            else
            {
                MessageBoxUI _mb = new MessageBoxUI("You must be a Cashier to Render Payment!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void txtStockCodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                dgvSearchStockList.Rows.Clear();
                foreach (DataRow _dr in loStock.getSaleableStock(txtStockCodeSearch.Text,"").Rows)
                {
                    int n = dgvSearchStockList.Rows.Add();
                    dgvSearchStockList.Rows[n].Cells["SearchStockId"].Value = _dr["Id"].ToString();
                    dgvSearchStockList.Rows[n].Cells["SearchStockCode"].Value = _dr["Code"].ToString();
                    dgvSearchStockList.Rows[n].Cells["SearchStockDescription"].Value = _dr["Description"].ToString();
                    dgvSearchStockList.Rows[n].Cells["SearchUnit"].Value = _dr["Unit"].ToString();
                    dgvSearchStockList.Rows[n].Cells["SearchQtyOnHand"].Value = string.Format("{0:n}", decimal.Parse(_dr["Qty on Hand"].ToString()));
                    dgvSearchStockList.Rows[n].Cells["SearchUnitCost"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Cost"].ToString()));
                    dgvSearchStockList.Rows[n].Cells["SearchBasePrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Base Price"].ToString()));
                    dgvSearchStockList.Rows[n].Cells["SearchUnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    dgvSearchStockList.Rows[n].Cells["SearchRemarks"].Value = _dr["Remarks"].ToString();
                }
            }
        }

        private void dgvDetailStockList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    if (lOperation == "Edit")
                    { 
                        if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                        {
                            tsmiEditQty.Visible = false;
                            tsmiRemove.Visible = false;
                        }
                        else
                        {
                            tsmiEditQty.Visible = true;
                            tsmiRemove.Visible = true;
                        }
                    }
                    else
                    {
                        tsmiEditQty.Visible = true;
                        tsmiRemove.Visible = true;
                    }
                }
                catch
                {
                    tsmiEditQty.Visible = true;
                    tsmiRemove.Visible = true;
                }
                System.Drawing.Point pt = dgvDetailStockList.PointToScreen(e.Location);
                cmsFunctionDettail.Show(pt);
                
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            getTrasactionLists();
        }

        private void tsmiVoid_Click(object sender, EventArgs e)
        {
            if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
            {
                MessageBoxUI _mb = new MessageBoxUI("You cannot void a Paid transaction!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }

            loSecurity.ShowDialog();
            if (loSecurity.lOKToOverride)
            {
                loPOSTransaction.remove(dgvTransactionList.CurrentRow.Cells[0].Value.ToString());
                foreach (DataRow _dr in loPOSTransactionDetail.getPOSTransactionDetails(dgvTransactionList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    loPOSTransactionDetail.remove(_dr[0].ToString());
                }
                getTrasactionLists();
            }
        }

        private void btnOtherFunctions_Click(object sender, EventArgs e)
        {
            loOtherFunctions.ShowDialog();
        }

        private void tsmiEditQty_Click(object sender, EventArgs e)
        {
            StockQtyUI loStockQty = new StockQtyUI();
            loStockQty.lStockId = dgvDetailStockList.CurrentRow.Cells["DetailStockId"].Value.ToString();
            loStockQty.lStockDescription = dgvDetailStockList.CurrentRow.Cells["DetailStockDescription"].Value.ToString();
            loStockQty.lUnitCost = decimal.Parse(dgvDetailStockList.CurrentRow.Cells["DetailUnitCost"].Value.ToString());
            loStockQty.lBasePrice = decimal.Parse(dgvDetailStockList.CurrentRow.Cells["DetailBasePrice"].Value.ToString());
            loStockQty.lUnitPrice = decimal.Parse(dgvDetailStockList.CurrentRow.Cells["DetailUnitPrice"].Value.ToString());
            loStockQty.lQty = int.Parse(dgvDetailStockList.CurrentRow.Cells["DetailQty"].Value.ToString());
            loStockQty.ShowDialog();
            if (loStockQty.lFromOK)
            {
                if (loStockQty.lQty > 0)
                {
                    try
                    {
                        string[] _qty = loStockQty.lQty.ToString().Split('.');
                        if (_qty[1].ToString() == "00")
                        {
                            dgvDetailStockList.CurrentRow.Cells["DetailQty"].Value = string.Format("{0:0}", loStockQty.lQty);
                        }
                        else
                        {
                            dgvDetailStockList.CurrentRow.Cells["DetailQty"].Value = string.Format("{0:n}", loStockQty.lQty);
                        }
                    }
                    catch
                    {
                        dgvDetailStockList.CurrentRow.Cells["DetailQty"].Value = string.Format("{0:0}", loStockQty.lQty);
                    }
                    dgvDetailStockList.CurrentRow.Cells["DetailUnitCost"].Value = string.Format("{0:n}", loStockQty.lUnitCost);
                    dgvDetailStockList.CurrentRow.Cells["DetailBasePrice"].Value = string.Format("{0:n}", loStockQty.lBasePrice);
                    dgvDetailStockList.CurrentRow.Cells["DetailUnitPrice"].Value = string.Format("{0:n}", loStockQty.lUnitPrice);
                    dgvDetailStockList.CurrentRow.Cells["DetailTotalPrice"].Value = string.Format("{0:n}", loStockQty.lQty * loStockQty.lUnitPrice);
                    computeTotal();
                    autosave();
                }
                else
                {
                    //delete the item...
                    try
                    {
                        if (dgvDetailStockList.CurrentRow.Cells[0].Value.ToString() != "")
                        {
                            loPOSTransactionDetail.remove(dgvDetailStockList.CurrentRow.Cells[0].Value.ToString());
                        }
                        dgvDetailStockList.Rows.RemoveAt(dgvDetailStockList.CurrentRow.Index);
                        computeTotal();
                        autosave();
                    }
                    catch { }
                }
            }
        }

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetailStockList.CurrentRow.Cells[0].Value.ToString() != "")
                {
                    loPOSTransactionDetail.remove(dgvDetailStockList.CurrentRow.Cells[0].Value.ToString());
                }
                dgvDetailStockList.Rows.RemoveAt(dgvDetailStockList.CurrentRow.Index);
                computeTotal();
                autosave();
            }
            catch { }
        }

        private void dgvSearchStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            getStockQty();
        }

        private void dgvTransactionList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //lblTransNo.Text = dgvTransactionList.CurrentRow.Cells[0].Value.ToString();
                //lblTerminal.Text = dgvTransactionList.CurrentRow.Cells[1].Value.ToString();
                txtStockCodeSearch.Clear();
                txtStockDescriptionSearch.Clear();
                getTransactionDetailLists();
                dgvSearchStockList.Rows.Clear();

                btnSave.Enabled = false;

                if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                {
                    lAdditionalItem = false;
                    txtStockCodeSearch.Enabled = false;
                    txtStockDescriptionSearch.Enabled = false;
                    txtStockCodeSearch.BackColor = SystemColors.Control;
                    txtStockDescriptionSearch.BackColor = SystemColors.Control;
                    btnRender.Enabled = false;
                }
                else
                {
                    lAdditionalItem = true;
                    txtStockCodeSearch.Enabled = true;
                    txtStockDescriptionSearch.Enabled = true;
                    txtStockCodeSearch.BackColor = SystemColors.HighlightText;
                    txtStockDescriptionSearch.BackColor = SystemColors.HighlightText;
                    btnRender.Enabled = true;
                    txtStockCodeSearch.Focus();
                }
            }
            catch { }
        }

        private void dgvTransactionList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "Y")
                {
                    tsmiVoidTransaction.Visible = false;
                    tsmiPreviewOrderSlip.Visible = true;
                }
                else
                {
                    tsmiVoidTransaction.Visible = true;
                    tsmiPreviewOrderSlip.Visible = false;
                }
                System.Drawing.Point pt = dgvTransactionList.PointToScreen(e.Location);
                cmsFunctionTransaction.Show(pt);
            }
        }

        private void pctPOSIcon_Click(object sender, EventArgs e)
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

        private void tsmiPreviewOrderSlip_Click(object sender, EventArgs e)
        {
            printOrderSlip(dgvTransactionList.CurrentRow.Cells["TransactionNo"].Value.ToString(), "");
        }

        private void dgvDetailStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "N")
            {
                tsmiEditQty_Click(null, new EventArgs());
            }
            else
            {
                MessageBoxUI _mb = new MessageBoxUI("You cannot Edit a Qty of a Paid Transaction!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void dgvTransactionList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTransactionList.CurrentRow.Cells["TransactionPaid"].Value.ToString() == "N")
            {
                btnRender_Click(null, new EventArgs());
            }
            else
            {
                MessageBoxUI _mb = new MessageBoxUI("This transaction is already paid!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void dgvSearchStockList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                getStockQty();
            }
        }

        private void txtStockCodeSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStockCodeSearch.Text.Trim().Length == 13)
                {
                    tmrDelay.Enabled = true;
                    tmrDelay.Start();
                    tmrDelay.Tick += new EventHandler(tmrDelay_Tick);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.showDialog();
            }
        }

        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrDelay.Stop();
                string strCurrentString = txtStockCodeSearch.Text.Trim().ToString();
                if (strCurrentString != "")
                {
                    try
                    {
                        if (txtStockCodeSearch.Text != "")
                        {
                            getStockDetails(txtStockCodeSearch.Text);
                            txtQty.Text = "1";
                            txtStockCodeSearch.Clear();
                            txtStockCodeSearch.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxUI mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        mb.showDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.showDialog();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtStockCodeSearch.Focus();
            }
        }

        private void btnCustomerLookUp_Click(object sender, EventArgs e)
        {
            FindCustomerListUI loFindCustomer = new FindCustomerListUI();
            loFindCustomer.ShowDialog();
            if (loFindCustomer.lFromSelection)
            {
                cboCustomer.Text = loFindCustomer.lCustomerName;
            }
        }
    }
}
