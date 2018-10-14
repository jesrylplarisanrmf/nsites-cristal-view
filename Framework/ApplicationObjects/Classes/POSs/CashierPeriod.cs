using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using JCSoftwares_V.Global;
using System.Net.Http;

namespace JCSoftwares_V.ApplicationObjects.Classes.POSs
{
    class CashierPeriod
    {
        #region "CONSTRUCTORS"
        public CashierPeriod()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public string PeriodStatus { get; set; }
        public string CashierId { get; set; }
        public decimal CashDeposit { get; set; }
        public decimal TotalSales { get; set; }
        public decimal ReturnedItemTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetSales { get; set; }
        public decimal NonCashSales { get; set; }
        public decimal CashSales { get; set; }
        public decimal NetCashSales { get; set; }
        public decimal CashCount { get; set; }
        public decimal Variance { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCashierPeriods?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCashierPeriodOpen()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCashierPeriodOpen").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCashierPeriodStockSold(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCashierPeriodStockSold?pCashierPeriodId="+pCashierPeriodId).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCashierPeriodReturnedItem(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCashierPeriodReturnedItem?pCashierPeriodId=" + pCashierPeriodId).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCashierPeriodByDate(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCashierPeriodByDate?pStartDate=" + string.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + string.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public string save(GlobalVariables.Operation pOperation)
        {
            string _Id = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertCashierPeriod/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateCashierPeriod/", this).Result;
                        _Id = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _Id.Replace("\"", "");
        }

        public bool openCashierPeriod(string pCashierId, decimal pCashDeposit, string pRemarks)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/openCashierPeriod?pCashierId=" + pCashierId + "&pCashDeposit=" + pCashDeposit + "&pRemarks=" + pRemarks + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool closeCashierPeriod(string pId, string pCashierId,decimal pTotalSales, decimal pReturnedItemTotal, 
            decimal pTotalDiscount, decimal pNetSales, decimal pNonCashSales,
            decimal pCashSales, decimal pNetCashSales, decimal pCashCount, decimal pVariance, string pRemarks)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/closeCashierPeriod?pId=" + pId +
                    "&pCashierId=" + pCashierId +
                    "&pTotalSales=" + pTotalSales +
                    "&pReturnedItemTotal=" + pReturnedItemTotal +
                    "&pTotalDiscount=" + pTotalDiscount +
                    "&pNetSales=" + pNetSales +
                    "&pNonCashSales=" + pNonCashSales +
                    "&pCashSales=" + pCashSales +
                    "&pNetCashSales=" + pNetCashSales +
                    "&pCashCount=" + pCashCount +
                    "&pVariance=" + pVariance +
                    "&pRemarks=" + pRemarks +
                    "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool remove(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeCashierPeriod?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
