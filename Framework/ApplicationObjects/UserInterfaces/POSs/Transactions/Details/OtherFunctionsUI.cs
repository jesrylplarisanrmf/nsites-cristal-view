using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes;
using JCSoftwares_V.ApplicationObjects.Classes.POSs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class OtherFunctionsUI : Form
    {
        CashierPeriod loCashierPeriod;
        ReturnedItemDetailUI loReturnItemdDetail;
        SecurityUI loSecurity;
        
        public OtherFunctionsUI()
        {
            InitializeComponent();
            loCashierPeriod = new CashierPeriod();
            loReturnItemdDetail = new ReturnedItemDetailUI();
            loSecurity = new SecurityUI();
        }

        private void btnReturnedItem_Click(object sender, EventArgs e)
        {
            DataTable _dt = loCashierPeriod.getCashierPeriodOpen();
            if(_dt.Rows.Count > 0)
            {
                loReturnItemdDetail.ShowDialog();
                if (loReturnItemdDetail.lFromSave)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Item has been returned successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
            }
            else
            {
                MessageBoxUI _mb = new MessageBoxUI("Cashier Period must be Open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
        }

        private void OtherFunctionsUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
        }
    }
}
