﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using JCSoftwares_V.Global;
using System.Net.Http;

namespace JCSoftwares_V.ApplicationObjects.Classes.Lendings
{
    class LoanEndOfDay
    {
        #region "CONSTRUCTORS"
        public LoanEndOfDay()
        {
     
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BranchId { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalRunningBalance { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalVariance { get; set; }
        public decimal TotalLoanRelease { get; set; }
        public decimal TotalServiceFee { get; set; }
        public string EndedBy { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanEndOfDays?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanEndOfDayByBranch(string pBranchId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanEndOfDayByBranch?pBranchId=" + pBranchId + "").Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertLoanEndOfDay/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateLoanEndOfDay/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeLoanEndOfDay?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
