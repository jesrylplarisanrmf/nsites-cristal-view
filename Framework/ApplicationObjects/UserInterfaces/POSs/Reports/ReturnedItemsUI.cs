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

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Reports
{
    public partial class ReturnedItemsUI : Form
    {
        POSTransactionDetail loPOSTransactionDetail;
        ReturnedItemsRpt loReturnedItemsRpt;
        
        public ReturnedItemsUI()
        {
            InitializeComponent();
            loPOSTransactionDetail = new POSTransactionDetail();
            loReturnedItemsRpt = new ReturnedItemsRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void ReturnedItemsUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmReturnedItems", "Preview"))
                {
                    return;
                }

                try
                {
                    DataTable _dtReturnedItem = loPOSTransactionDetail.getReturnedItems(dtpFromDate.Value, dtpToDate.Value);
                    if (_dtReturnedItem.Rows.Count > 0)
                    {
                        loReturnedItemsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loReturnedItemsRpt.Database.Tables[1].SetDataSource(_dtReturnedItem);
                        loReturnedItemsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loReturnedItemsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loReturnedItemsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loReturnedItemsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loReturnedItemsRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loReturnedItemsRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loReturnedItemsRpt.SetParameterValue("Title", "Returned Items");
                        loReturnedItemsRpt.SetParameterValue("SubTitle", "Returned Items");
                        crvList.ReportSource = loReturnedItemsRpt;
                    }
                    else
                    {
                        MessageBoxUI _mb = new MessageBoxUI("No records found!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb.showDialog();
                        return;
                    }
                }
                catch (Exception ex)
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
    }
}
