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
    public partial class SalesInventoryUI : Form
    {
        POSTransactionDetail loPOSTransactionDetail;
        SalesInventoryRpt loSalesInventoryRpt;
        SalesInventoryByCustomerRpt loSalesInventoryByCustomerRpt;
        SalesInventoryByCategoryRpt loSalesInventoryByCategoryRpt;
        
        public SalesInventoryUI()
        {
            InitializeComponent();
            loPOSTransactionDetail = new POSTransactionDetail();
            loSalesInventoryRpt = new SalesInventoryRpt();
            loSalesInventoryByCustomerRpt = new SalesInventoryByCustomerRpt();
            loSalesInventoryByCategoryRpt = new SalesInventoryByCategoryRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void SalesInventoryUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmSalesInventory", "Preview"))
                {
                    return;
                }

                try
                {
                    DataTable _dtByDate = loPOSTransactionDetail.getSalesInventory(dtpFromDate.Value, dtpToDate.Value);
                    if (_dtByDate.Rows.Count > 0)
                    {
                        loSalesInventoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loSalesInventoryRpt.Database.Tables[1].SetDataSource(_dtByDate);
                        loSalesInventoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loSalesInventoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loSalesInventoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loSalesInventoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loSalesInventoryRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loSalesInventoryRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loSalesInventoryRpt.SetParameterValue("Title", "Sales Inventory - By Date");
                        loSalesInventoryRpt.SetParameterValue("SubTitle", "Sales Inventory - By Date");
                        crvByDate.ReportSource = loSalesInventoryRpt;
                    }

                    DataTable _dtBy = loPOSTransactionDetail.getSalesInventoryBy(dtpFromDate.Value, dtpToDate.Value);

                    if (_dtBy.Rows.Count > 0)
                    {
                        loSalesInventoryByCustomerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loSalesInventoryByCustomerRpt.Database.Tables[1].SetDataSource(_dtBy);
                        loSalesInventoryByCustomerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loSalesInventoryByCustomerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loSalesInventoryByCustomerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loSalesInventoryByCustomerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loSalesInventoryByCustomerRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loSalesInventoryByCustomerRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loSalesInventoryByCustomerRpt.SetParameterValue("Title", "Sales Inventory - By Customer");
                        loSalesInventoryByCustomerRpt.SetParameterValue("SubTitle", "Sales Inventory - By Customer");
                        crvByCustomer.ReportSource = loSalesInventoryByCustomerRpt;
                    }

                    if (_dtBy.Rows.Count > 0)
                    {
                        loSalesInventoryByCategoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loSalesInventoryByCategoryRpt.Database.Tables[1].SetDataSource(_dtBy);
                        loSalesInventoryByCategoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loSalesInventoryByCategoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loSalesInventoryByCategoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loSalesInventoryByCategoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loSalesInventoryByCategoryRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loSalesInventoryByCategoryRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loSalesInventoryByCategoryRpt.SetParameterValue("Title", "Sales Inventory - By Category");
                        loSalesInventoryByCategoryRpt.SetParameterValue("SubTitle", "Sales Inventory - By Category");
                        crvByCategory.ReportSource = loSalesInventoryByCategoryRpt;
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
