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
using JCSoftwares_V.ApplicationObjects.Classes.POSs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class ReturnedItemDetailUI : Form
    {
        Stock loStock;
        ReturnedItem loReturnedItem;
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        SecurityUI loSecurity;
        string lStockId;
        public bool lFromSave;
        
        public ReturnedItemDetailUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loReturnedItem = new ReturnedItem();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loSecurity = new SecurityUI();
            lStockId = "";
            lFromSave = false;
        }

        private string insertInventory(string pStockId, decimal pTotalQtyIn)
        {
            string _InventoryId = "";
            try
            {
                loInventory.Id = "";
                loInventory.Date = DateTime.Now;
                loInventory.Type = "Stock Receiving - POS";
                loInventory.POId = "";
                loInventory.SOId = "";
                loInventory.STInId = "";
                loInventory.STOutId = "";
                loInventory.Reference = "Returned Item ";
                loInventory.CustomerId = "0";
                loInventory.SupplierId = "0";
                loInventory.TotalPOQty = 0;
                loInventory.TotalQtyIn = pTotalQtyIn;
                loInventory.TotalSOQty = 0;
                loInventory.TotalQtyOut = 0;
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
                        loInventoryDetail.DetailId = "";
                        loInventoryDetail.InventoryId = _InventoryId;
                        loInventoryDetail.PODetailId = "0";
                        loInventoryDetail.SODetailId = "0";
                        loInventoryDetail.StockId = pStockId;
                        loInventoryDetail.LocationId = GlobalVariables.CurrentLocationId;
                        loInventoryDetail.POQty = 0;
                        loInventoryDetail.QtyIn = pTotalQtyIn;
                        loInventoryDetail.SOQty = 0;
                        loInventoryDetail.QtyOut = 0;
                        loInventoryDetail.Variance = 0;
                        loInventoryDetail.Remarks = "";
                        loInventoryDetail.UserId = GlobalVariables.UserId;
                        loInventoryDetail.save(GlobalVariables.Operation.Add);
                    }
                }
                catch { }
            }
            catch { }

            return _InventoryId;
        }

        private void ReturnedItemDetailUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            cboStock.DataSource = loStock.getAllData("ViewAll", "","");
            cboStock.ValueMember = "Id";
            cboStock.DisplayMember = "Description";
            cboStock.SelectedIndex = -1;

            lStockId = "";
            lFromSave = false;

            txtStockCode.Clear();
            txtUnit.Clear();
            txtQtyReturned.Text = "1";
            txtUnitPrice.Text = "0.00";
            txtTotalPrice.Text = "0.00";
            txtReason.Clear();
            txtStockCode.Focus();
        }

        private void txtStockCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                foreach (DataRow _dr in loStock.getAllData("",txtStockCode.Text,"").Rows)
                {
                    lStockId = _dr["Id"].ToString();
                    cboStock.Text = _dr["Description"].ToString();
                    txtUnit.Text = _dr["Unit"].ToString();
                    txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()) * decimal.Parse(txtQtyReturned.Text));
                }
            }
        }

        private void txtQtyReturned_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtQtyReturned.Text) * decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            }
        }

        private void cboStock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loStock.getAllData("",cboStock.SelectedValue.ToString(),"").Rows)
                {
                    lStockId = _dr["Id"].ToString();
                    txtStockCode.Text = _dr["Code"].ToString();
                    txtUnit.Text = _dr["Unit"].ToString();
                    txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()) * decimal.Parse(txtQtyReturned.Text));
                }
            }
            catch { }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (txtReason.Text == "")
            {
                MessageBoxUI _mb = new MessageBoxUI("Returned item must have a reason!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtReason.Focus();
                return;
            }

            if (lStockId == "")
            {
                MessageBoxUI _mb = new MessageBoxUI("You must select an Item!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtStockCode.Focus();
                return;
            }

            loSecurity.ShowDialog();
            if (loSecurity.lOKToOverride)
            {
                loReturnedItem.CashierPeriodId = GlobalVariables.CashierPeriodId;
                loReturnedItem.StockId = lStockId;
                loReturnedItem.LocationId = GlobalVariables.CurrentLocationId;
                loReturnedItem.Qty = decimal.Parse(txtQtyReturned.Text);
                loReturnedItem.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                loReturnedItem.TotalPrice = decimal.Parse(txtTotalPrice.Text);
                loReturnedItem.Reason = GlobalFunctions.replaceChar(txtReason.Text);
                loReturnedItem.UserId = GlobalVariables.UserId;
                try
                {
                    if (loReturnedItem.save(GlobalVariables.Operation.Add) != "")
                    {
                        //insert Inventory
                        loInventory.final(insertInventory(lStockId, decimal.Parse(txtQtyReturned.Text)));
                        
                        lFromSave = true;
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
        }

        private void txtQtyReturned_Leave(object sender, EventArgs e)
        {
            try
            {
                txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtQtyReturned.Text) * decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtQtyReturned.Text = "1";
                txtTotalPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            }
        }
    }
}
