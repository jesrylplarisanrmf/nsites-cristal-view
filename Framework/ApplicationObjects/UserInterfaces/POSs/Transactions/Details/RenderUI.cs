using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes.Systems;
using JCSoftwares_V.ApplicationObjects.Classes.POSs;
using JCSoftwares_V.ApplicationObjects.Classes.Inventorys;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports.TransactionRpt;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class RenderUI : Form
    {
        POSTransaction loPOSTransaction;
        POSTransactionDetail loPOSTransactionDetail;
        Customer loCustomer;
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        SystemConfiguration loSystemConfiguration;

        public string lTransactionId;
        public decimal lTotalDue = 0;
        public string lCustomerId = "";
        public string lCustomerName = "";
        public string lOrderType;
        public string lTableId;
        public DataGridView loDataGrid = new DataGridView();
        public string lOperation;

        public bool lFromClose;
        string lModeOfPaymentId;
        string lPaymentDetails;
        string lDiscountId;
        bool lStart;

        //ORPrePrintedRpt loORPrePrintedRpt;
        //ORReceiptPrinter80mmRpt loORReceiptPrinter80mmRpt;
        OrderSlipRpt loOrderSlipRpt;
        ReportViewerUI loReportViewer;
        
        public RenderUI()
        {
            InitializeComponent();
            loPOSTransaction = new POSTransaction();
            loPOSTransactionDetail = new POSTransactionDetail();
            loCustomer = new Customer();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loSystemConfiguration = new SystemConfiguration();
            lFromClose = false;

            //loORPrePrintedRpt = new ORPrePrintedRpt();
            //loORReceiptPrinter80mmRpt = new ORReceiptPrinter80mmRpt();
            loOrderSlipRpt = new OrderSlipRpt();
            loReportViewer = new ReportViewerUI();
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
                    
                    if (GlobalVariables.PreviewOR)
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

        private void computeTAX()
        {
            decimal _VATableAmount = 0;
            decimal _VATExemptSale = 0;

            foreach (DataGridViewRow _dr in loDataGrid.Rows)
            {
                _VATableAmount += decimal.Parse(_dr.Cells["DetailTotalPrice"].Value.ToString());
            }

            decimal _VATableSale = _VATableAmount / decimal.Parse("1.12");
            decimal _VATAmount = _VATableAmount - _VATableSale;

            lblVATSale.Text = string.Format("{0:n}", _VATableSale);
            lblVATExemptSale.Text = string.Format("{0:n}", _VATExemptSale);
            lblVATAmount.Text = string.Format("{0:n}", _VATAmount);

            txtAmountRendered.Text = string.Format("{0:n}", _VATableAmount + _VATExemptSale);
        }

        public void writeORSeqNo()
        {
            try
            {
                string _ORNo = "";
                try
                {
                    _ORNo = (int.Parse(txtORNo.Text) + 1).ToString();
                }
                catch
                {
                    _ORNo = "";
                }
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/ORSeqNo.txt");
                writeFile.WriteLine(_ORNo);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void readORSeqNo()
        {
            System.IO.TextReader readFile = new StreamReader(".../Main/text/ORSeqNo.txt");
            txtORNo.Text = readFile.ReadLine();
            readFile.Close();
            readFile = null;
        }

        private string insertInventory(string pTransactionNo, string pCustomer, decimal pTotalQtyOut)
        {
            string _InventoryId = "";
            try
            {
                loInventory.Id = "";
                loInventory.Date = DateTime.Now;
                loInventory.Type = "Stock Withdrawal - POS";
                loInventory.POId = "";
                loInventory.SOId = "";
                loInventory.STInId = "";
                loInventory.STOutId = "";
                loInventory.Reference = "Transaction No. "+pTransactionNo;
                loInventory.CustomerId = pCustomer;
                loInventory.SupplierId = "0";
                loInventory.TotalPOQty = 0;
                loInventory.TotalQtyIn = 0;
                loInventory.TotalSOQty = 0;
                loInventory.TotalQtyOut = pTotalQtyOut;
                loInventory.TotalVariance = 0;
                loInventory.FromLocationId = "";
                loInventory.ToLocationId = "";
                loInventory.Remarks = "";
                loInventory.UserId = GlobalVariables.UserId;

                try
                {
                    _InventoryId = loInventory.save(GlobalVariables.Operation.Add);
                    if (_InventoryId != "")
                    {
                        foreach (DataGridViewRow _dr in loDataGrid.Rows)
                        {
                            loInventoryDetail.DetailId = "";
                            loInventoryDetail.InventoryId = _InventoryId;
                            loInventoryDetail.PODetailId = "0";
                            loInventoryDetail.SODetailId = "0";
                            loInventoryDetail.StockId = _dr.Cells["DetailStockId"].Value.ToString();
                            loInventoryDetail.LocationId = GlobalVariables.CurrentLocationId;
                            loInventoryDetail.POQty = 0;
                            loInventoryDetail.QtyIn = 0;
                            loInventoryDetail.SOQty = 0;
                            loInventoryDetail.QtyOut = decimal.Parse(_dr.Cells["DetailQty"].Value.ToString());
                            loInventoryDetail.Variance = 0;
                            loInventoryDetail.Remarks = "";
                            loInventoryDetail.UserId = GlobalVariables.UserId;
                            loInventoryDetail.save(GlobalVariables.Operation.Add);
                        }
                    }
                }
                catch { }
            }
            catch { }

            return _InventoryId;
        }

        private void RenderUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

            readORSeqNo();

            lblTotalPrice.Text = string.Format("{0:n}", lTotalDue);
            lblTotalDue.Text = string.Format("{0:n}", lTotalDue);

            computeTAX();

            lFromClose = false;

            lModeOfPaymentId = GlobalVariables.DefaultModeOfPaymentId;
            lPaymentDetails = "";
            lblModeOfPayment.Text = GlobalVariables.DefaultModeOfPaymentDescription;

            lblCustomer.Text = lCustomerName;
            lDiscountId = "";
            lblDiscountDescription.Text = "";
            lStart = true;
        }

        private void txtAmountRendered_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblChange.Text = string.Format("{0:n}", decimal.Parse(txtAmountRendered.Text) - decimal.Parse(lblTotalDue.Text));
            }
            catch
            {
                lblChange.Text = "0.00";
            }
        }

        private void txtAmountRendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnProcess_Click(null, new EventArgs());
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            AddDiscountUI loAddDiscount = new AddDiscountUI();
            loAddDiscount.ShowDialog();
            if (!lFromClose)
            {
                lDiscountId = loAddDiscount.lDiscountId;
                lblDiscountDescription.Text = loAddDiscount.lDiscountDescription;
                decimal _totalPrice = decimal.Parse(lblTotalPrice.Text);
                decimal _discount = 0;
                decimal _lessVAT = 0;
                decimal _totalDue = 0;
                decimal _vatableSales = 0;
                decimal _vatExemptSales = decimal.Parse(lblVATExemptSale.Text);

                
                //decimal _VATableAmount = 0;
                //decimal _VATExemptSale = 0;
                
                //foreach (DataGridViewRow _dr in loDataGrid.Rows)
                //{
                    //if (_dr.Cells[8].Value.ToString() == "Y")
                    //{
                       // _VATableAmount += decimal.Parse(_dr.Cells[7].Value.ToString());
                    //}
                    //else
                    //{
                        //_VATExemptSale += decimal.Parse(_dr.Cells[7].Value.ToString());
                    //}
                //}
                

                if (loAddDiscount.lVATExempt)
                {
                    _vatableSales = (_totalPrice - _vatExemptSales) / decimal.Parse("1.12");
                    _lessVAT = (_totalPrice - _vatExemptSales) - _vatableSales;

                    if (loAddDiscount.lAmount > 0)
                    {
                        _discount = loAddDiscount.lAmount;
                    }
                    else if (loAddDiscount.lPercentage > 0)
                    {
                        _discount = (_vatableSales + _vatExemptSales) * (loAddDiscount.lPercentage / 100);
                    }
                    _totalDue = _totalPrice - (_discount + _lessVAT);

                    lblTotalDiscount.Text = string.Format("{0:n}", _discount);
                    lblLessVAT.Text = string.Format("{0:n}", _lessVAT);
                    lblTotalDue.Text = string.Format("{0:n}", _totalDue);

                    lblVATSale.Text = "0.00";
                    lblVATExemptSale.Text = string.Format("{0:n}", _vatableSales + _vatExemptSales);
                    lblVATAmount.Text = "0.00";
                }
                else
                {
                    if (loAddDiscount.lAmount > 0)
                    {
                        _discount = loAddDiscount.lAmount;

                    }
                    else if (loAddDiscount.lPercentage > 0)
                    {
                        _discount = _totalPrice * (loAddDiscount.lPercentage / 100);
                    }

                    _totalDue = _totalPrice - _discount;
                    _vatableSales = (_totalDue - _vatExemptSales) / decimal.Parse("1.12");

                    lblTotalDiscount.Text = string.Format("{0:n}", _discount);
                    lblLessVAT.Text = "0.00";
                    lblTotalDue.Text = string.Format("{0:n}", _totalDue);

                    lblVATSale.Text = string.Format("{0:n}", _vatableSales);
                    //do not touch vatexempt sale...
                    lblVATAmount.Text = string.Format("{0:n}", (_totalDue - _vatExemptSales) - _vatableSales);
                }

                if (loAddDiscount.lDetails != "")
                {
                    lblRemarks.Text = "Discount:" + loAddDiscount.lDiscountDescription + "; Details:" + loAddDiscount.lDetails;
                }
                else
                {
                    lblRemarks.Text = "Discount:" + loAddDiscount.lDiscountDescription;
                }

                txtAmountRendered.Text = string.Format("{0:n}", decimal.Parse(lblTotalDue.Text));
                txtAmountRendered.Focus();
            }
        }

        private void btnCustomerLookUp_Click(object sender, EventArgs e)
        {
            FindCustomerListUI loFindCustomer = new FindCustomerListUI();
            loFindCustomer.ShowDialog();
            if (loFindCustomer.lFromSelection)
            {
                lblCustomer.Text = loFindCustomer.lCustomerName;
                lCustomerId = loFindCustomer.lCustomerId;
            }
        }

        private void btnModeOfPaymentLookUp_Click(object sender, EventArgs e)
        {
            FindModeOfPaymentUI loFindModeOfPayment = new FindModeOfPaymentUI();
            loFindModeOfPayment.ShowDialog();
            if (loFindModeOfPayment.lFromOK)
            {
                lblModeOfPayment.Text = loFindModeOfPayment.lDescription;
                lModeOfPaymentId = loFindModeOfPayment.lId;
                lPaymentDetails = loFindModeOfPayment.lDetails;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(lblChange.Text) < 0)
            {
                MessageBoxUI mb = new MessageBoxUI("Amount rendered must be greater than Total Due!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                mb.showDialog();
                txtAmountRendered.Focus();
                return;
            }
            lFromClose = false;
            loPOSTransaction.Id = lTransactionId;
            loPOSTransaction.Date = DateTime.Now;
            loPOSTransaction.CashierPeriodId = GlobalVariables.CashierPeriodId;
            loPOSTransaction.CustomerId = lCustomerId;
            loPOSTransaction.OrderType = lOrderType;
            loPOSTransaction.TableId = lTableId;
            loPOSTransaction.ORNo = txtORNo.Text;
            loPOSTransaction.TotalPrice = decimal.Parse(lblTotalPrice.Text);
            loPOSTransaction.TotalDiscount = decimal.Parse(lblTotalDiscount.Text);
            loPOSTransaction.TotalLessVAT = decimal.Parse(lblLessVAT.Text);
            loPOSTransaction.TotalDue = decimal.Parse(lblTotalDue.Text);
            loPOSTransaction.VATSale = decimal.Parse(lblVATSale.Text);
            loPOSTransaction.VATExemptSale = decimal.Parse(lblVATExemptSale.Text);
            loPOSTransaction.VATAmount = decimal.Parse(lblVATAmount.Text);
            loPOSTransaction.AmountTendered = decimal.Parse(txtAmountRendered.Text);
            loPOSTransaction.Paid = "Y";
            loPOSTransaction.DiscountId = lDiscountId;
            loPOSTransaction.ModeOfPaymentId = lModeOfPaymentId;
            loPOSTransaction.PaymentDetails = lPaymentDetails;
            loPOSTransaction.OutletId = GlobalVariables.CurrentLocationId;
            loPOSTransaction.CashierId = GlobalVariables.CashierId;
            loPOSTransaction.Terminal = GlobalVariables.Terminal;
            loPOSTransaction.Remarks = lblRemarks.Text;
            loPOSTransaction.UserId = GlobalVariables.UserId;
            try
            {
                string _TransactionId;
                if (lOperation == "Add")
                {
                    _TransactionId = loPOSTransaction.save(GlobalVariables.Operation.Add);
                }
                else if (lOperation == "Edit")
                {
                    _TransactionId = loPOSTransaction.save(GlobalVariables.Operation.Edit);
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Operation not defined!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                if (_TransactionId != "")
                {
                    decimal _totalQty = 0;
                    foreach (DataGridViewRow _dr in loDataGrid.Rows)
                    {
                        loPOSTransactionDetail.TransactionId = _TransactionId;
                        loPOSTransactionDetail.StockId = _dr.Cells["DetailStockId"].Value.ToString();
                        loPOSTransactionDetail.LocationId = GlobalVariables.CurrentLocationId;
                        loPOSTransactionDetail.VATable = "Y";
                        _totalQty += decimal.Parse(_dr.Cells["DetailQty"].Value.ToString());
                        loPOSTransactionDetail.Qty = decimal.Parse(_dr.Cells["DetailQty"].Value.ToString());
                        loPOSTransactionDetail.UnitCost = decimal.Parse(_dr.Cells["DetailUnitCost"].Value.ToString());
                        loPOSTransactionDetail.BasePrice = decimal.Parse(_dr.Cells["DetailBasePrice"].Value.ToString());
                        loPOSTransactionDetail.UnitPrice = decimal.Parse(_dr.Cells["DetailUnitPrice"].Value.ToString());
                        loPOSTransactionDetail.DiscountId = "0";
                        loPOSTransactionDetail.DiscountAmount = 0;
                        loPOSTransactionDetail.TotalPrice = decimal.Parse(_dr.Cells["DetailTotalPrice"].Value.ToString());
                        loPOSTransactionDetail.UserId = GlobalVariables.UserId;

                        if (lOperation == "Add")
                        {
                            loPOSTransactionDetail.save(GlobalVariables.Operation.Add);
                        }
                        else if (lOperation == "Edit")
                        {
                            if (_dr.Cells[0].Value.ToString() != "")
                            {
                                loPOSTransactionDetail.DetailId = _dr.Cells["DetailId"].Value.ToString();
                                loPOSTransactionDetail.save(GlobalVariables.Operation.Edit);
                            }
                            else
                            {
                                loPOSTransactionDetail.save(GlobalVariables.Operation.Add);
                            }
                        }
                    }
                    //insert Inventory
                    loInventory.final(insertInventory(_TransactionId, lCustomerId, _totalQty));
                    
                    try
                    {
                        OpenCashDrawer loOpenCashDrawer = new OpenCashDrawer();
                        loOpenCashDrawer.openCashDrawer();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxUI mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        mb.showDialog();
                    }
                    writeORSeqNo();

                    if (lOperation == "Add")
                    {
                        printOrderSlip(_TransactionId, txtORNo.Text);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBoxUI _mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("9");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText("0");
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            if (lStart)
            {
                txtAmountRendered.Clear();
                lStart = false;
            }
            txtAmountRendered.AppendText(".");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmountRendered.Clear();
            txtAmountRendered.Focus();
        }
    }
}
