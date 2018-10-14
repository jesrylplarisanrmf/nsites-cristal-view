
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
    class POSTransaction
    {
        #region "CONSTRUCTORS"
        public POSTransaction()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string CashierPeriodId { get; set; }
        public string CustomerId { get; set; }
        public string OrderType { get; set; }
        public string TableId { get; set; }
        public string ORNo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalLessVAT { get; set; }
        public decimal TotalDue { get; set; }
        public decimal VATSale { get; set; }
        public decimal VATExemptSale { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AmountTendered { get; set; }
        public string Paid { get; set; }
        public string OutletId { get; set; }
        public string DiscountId { get; set; }
        public string ModeOfPaymentId { get; set; }
        public string PaymentDetails { get; set; }
        public string CashierId { get; set; }
        public string Terminal { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pType, string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getPOSTransactions?pType=" + pType + "&pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getPOSTransaction(string pId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getPOSTransaction?pId=" + pId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getPOSTransactionsByDate(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getPOSTransactionsByDate?pStartDate=" + string.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + string.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getPOSTransactionLists(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getPOSTransactionLists?pCashierPeriodId=" + pCashierPeriodId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTotalSalesByCashierPeriod(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTotalSalesByCashierPeriod?pCashierPeriodId=" + pCashierPeriodId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTotalReturnedByCashierPeriod(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTotalReturnedByCashierPeriod?pCashierPeriodId=" + pCashierPeriodId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTotalDiscountByCashierPeriod(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTotalDiscountByCashierPeriod?pCashierPeriodId=" + pCashierPeriodId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSalesByCashierPeriod(string pCashierPeriodId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSalesByCashierPeriod?pCashierPeriodId=" + pCashierPeriodId + "").Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertPOSTransaction/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updatePOSTransaction/", this).Result;
                        _Id = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _Id.Replace("\"", "");
        }

        public bool remove(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removePOSTransaction?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
