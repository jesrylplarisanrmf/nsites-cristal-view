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
    class POSTransactionDetail
    {
        #region "CONSTRUCTORS"
        public POSTransactionDetail()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string TransactionId { get; set; }
        public string StockId { get; set; }
        public string LocationId { get; set; }
        public string VATable { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal BasePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string DiscountId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getPOSTransactionDetails(string pTransactionId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getPOSTransactionDetails?pTransactionId=" + pTransactionId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSalesInventory(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSalesInventory?pStartDate=" + string.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + string.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSalesInventoryBy(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSalesInventoryBy?pStartDate=" + string.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + string.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getReturnedItems(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getReturnedItems?pStartDate=" + string.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + string.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool save(GlobalVariables.Operation pOperation)
        {
            bool _result = false;
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertPOSTransactionDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updatePOSTransactionDetail/", this).Result;
                        _result = bool.Parse(responseEdit.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _result;
        }

        public bool remove(string pDetailId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removePOSTransactionDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
