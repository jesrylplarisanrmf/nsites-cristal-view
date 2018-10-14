using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using JCSoftwares_V.Global;
using System.Net.Http;

namespace JCSoftwares_V.ApplicationObjects.Classes.HRISs
{
    class Employee
    {
        #region "CONSTRUCTORS"
        public Employee()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Active { get; set; }
        public string BiometricsIdNo { get; set; }
        public string EmploymentTypeId { get; set; }
        public string DesignationId { get; set; }
        public string DepartmentId { get; set; }
        public string NoWorkSchedule { get; set; }
        public string WorkScheduleId { get; set; }
        public DateTime Birthday { get; set; }
        public string EmailAddress { get; set; }
        public string TIN { get; set; }
        public string TINDeducted { get; set; }
        public string PhilHealthId { get; set; }
        public string PhilHealthDeducted { get; set; }
        public string SSSId { get; set; }
        public string SSSDeducted { get; set; }
        public string PagibigId { get; set; }
        public string PagibigDeducted { get; set; }
        public decimal PagibigEmployeeShare { get; set; }
        public decimal PagibigEmployerShare { get; set; }
        public int NoOfDependent { get; set; }
        public string RateType { get; set; }
        public decimal BasicPay { get; set; }
        public string ImmediateSupervisor { get; set; }
        public string Remarks { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEmployees?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getEmployeeNames()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEmployeeNames").Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertEmployee/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateEmployee/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeEmployee?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
