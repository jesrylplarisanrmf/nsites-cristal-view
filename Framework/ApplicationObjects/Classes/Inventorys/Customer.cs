﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using JCSoftwares_V.Global;
using System.Net.Http;

namespace JCSoftwares_V.ApplicationObjects.Classes.Inventorys
{
    class Customer
    {
        #region "CONSTRUCTORS"
        public Customer()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public string Name { get; set; }
        public string Default { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public decimal CreditLimit { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCustomers?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCustomerDefault()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCustomerDefault").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCustomerCreditLimit(string pCustomerId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCustomerCreditLimit?pCustomerId=" + pCustomerId + "").Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertCustomer/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateCustomer/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeCustomer?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}