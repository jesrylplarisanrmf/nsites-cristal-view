using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes;
using JCSoftwares_V.ApplicationObjects.Classes.HRISs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.HRISs.Masterfiles
{
    public partial class EmployeeDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[29];
        GlobalVariables.Operation lOperation;
        Employee loEmployee;
        EmploymentType loEmploymentType;
        Designation loDesignation;
        Department loDepartment;
        
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public EmployeeDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loEmployee = new Employee();
            loEmploymentType = new EmploymentType();
            loDesignation = new Designation();
            loDepartment = new Department();
        }
        public EmployeeDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loEmployee = new Employee();
            loEmploymentType = new EmploymentType();
            loDesignation = new Designation();
            loDepartment = new Department();
            lRecords = pRecords;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void clear()
        {
            lId = "";
            txtEmployeeNo.Clear();
            txtLastname.Clear();
            txtFirstname.Clear();
            txtMiddlename.Clear();
            chkActive.Checked = true;
            txtBiometricsIdNo.Clear();
            cboEmploymentType.SelectedIndex = -1;
            cboDesignation.SelectedIndex = -1;
            cboDepartment.SelectedIndex = -1;
            chkNoWorkSchedule.Checked = false;
            cboWorkSchedule.SelectedIndex = -1;
            dtpBirthday.Value = DateTime.Now;
            txtEmailAddress.Clear();
            txtTIN.Clear();
            chkTINDeducted.Checked = false;
            txtPhilHealthId.Clear();
            chkPhilHealthDeducted.Checked = false;
            txtSSSId.Clear();
            chkSSSDeducted.Checked = false;
            txtPagibigId.Clear();
            chkPagibigDeducted.Checked = false;
            txtPagibigEmployeeShare.Text = "0.00";
            txtPagibigEmployerShare.Text = "0.00";


            txtEmailAddress.Clear();
            cboImmediateSupervisor.SelectedIndex = -1;
            txtRemarks.Clear();
            txtEmployeeNo.Focus();
        }
        #endregion "END OF METHODS"

        private void EmployeeDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                try
                {
                    cboEmploymentType.DataSource = loEmploymentType.getAllData("ViewAll","","");
                    cboEmploymentType.DisplayMember = "Description";
                    cboEmploymentType.ValueMember = "Id";
                    cboEmploymentType.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboDesignation.DataSource = loDesignation.getAllData("ViewAll", "", "");
                    cboDesignation.DisplayMember = "Description";
                    cboDesignation.ValueMember = "Id";
                    cboDesignation.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboDepartment.DataSource = loDepartment.getAllData("ViewAll", "", "");
                    cboDepartment.DisplayMember = "Description";
                    cboDepartment.ValueMember = "Id";
                    cboDepartment.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboImmediateSupervisor.DataSource = loEmployee.getEmployeeNames();
                    cboImmediateSupervisor.DisplayMember = "Employee Name";
                    cboImmediateSupervisor.ValueMember = "Id";
                    cboImmediateSupervisor.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loEmployee.getAllData("", lRecords[0].ToString(),"").Rows)
                    {
                        lId = _dr["Id"].ToString();
                        txtEmployeeNo.Text = _dr["Employee No."].ToString();
                        txtLastname.Text = _dr["Lastname"].ToString();
                        txtFirstname.Text = _dr["Firstname"].ToString();
                        txtMiddlename.Text = _dr["Middlename"].ToString();
                        chkActive.Checked = _dr["Active"].ToString() == "Y" ? true : false;
                        txtBiometricsIdNo.Text = _dr["Biometrics Id No."].ToString();
                        cboEmploymentType.Text = _dr["Employment Type"].ToString();
                        cboDesignation.Text = _dr["Designation"].ToString();
                        cboDepartment.Text = _dr["Department"].ToString();
                        chkNoWorkSchedule.Checked = _dr["No Work Schedule"].ToString() == "Y" ? true : false;
                        cboWorkSchedule.Text = _dr["Work Schedule"].ToString();
                        dtpBirthday.Value = DateTime.Parse(_dr["Birthday"].ToString());
                        txtEmailAddress.Text = _dr["Email Address"].ToString();
                        txtTIN.Text = _dr["TIN"].ToString();
                        chkTINDeducted.Checked = _dr["TIN Deducted"].ToString() == "Y" ? true : false;
                        txtPhilHealthId.Text = _dr["PhilHealth Id"].ToString();
                        chkPhilHealthDeducted.Checked = _dr["PhilHealth Deducted"].ToString() == "Y" ? true : false;
                        txtSSSId.Text = _dr["SSS Id"].ToString();
                        chkSSSDeducted.Checked = _dr["SSS Deducted"].ToString() == "Y" ? true : false;
                        txtPagibigId.Text = _dr["Pagibig Id"].ToString();
                        chkPagibigDeducted.Checked = _dr["Pagibig Deducted"].ToString() == "Y" ? true : false;
                        txtPagibigEmployeeShare.Text = string.Format("{0:n}", decimal.Parse(_dr["Pagibig(Employee Share)"].ToString()));
                        txtPagibigEmployerShare.Text = string.Format("{0:n}", decimal.Parse(_dr["Pagibig(Employer Share)"].ToString()));
                        txtNoOfDependent.Text = _dr["No. of Dependent"].ToString();
                        cboRateType.Text = _dr["Rate Type"].ToString();
                        txtBasicPay.Text = string.Format("{0:n}", decimal.Parse(_dr["Basic Pay"].ToString()));
                        cboImmediateSupervisor.Text = _dr["Immediate Supervisor"].ToString();
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "EmployeeDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loEmployee.Id = lId;
                loEmployee.EmployeeNo = GlobalFunctions.replaceChar(txtEmployeeNo.Text);
                loEmployee.Lastname = GlobalFunctions.replaceChar(txtLastname.Text);
                loEmployee.Firstname = GlobalFunctions.replaceChar(txtFirstname.Text);
                loEmployee.Middlename = GlobalFunctions.replaceChar(txtMiddlename.Text);
                loEmployee.Active = chkActive.Checked ? "Y" : "N";
                loEmployee.BiometricsIdNo = GlobalFunctions.replaceChar(txtBiometricsIdNo.Text);
                try
                {
                    loEmployee.EmploymentTypeId = cboEmploymentType.SelectedValue.ToString();
                }
                catch
                {
                    loEmployee.EmploymentTypeId = "0";
                }
                try
                {
                    loEmployee.DesignationId = cboDesignation.SelectedValue.ToString();
                }
                catch
                {
                    loEmployee.DesignationId = "0";
                }
                try
                {
                    loEmployee.DepartmentId = cboDepartment.SelectedValue.ToString();
                }
                catch
                {
                    loEmployee.DepartmentId = "0";
                }
                loEmployee.NoWorkSchedule = chkActive.Checked ? "Y" : "N";
                try
                {
                    loEmployee.WorkScheduleId = cboWorkSchedule.SelectedValue.ToString();
                }
                catch
                {
                    loEmployee.WorkScheduleId = "0";
                }
                loEmployee.Birthday = dtpBirthday.Value;
                loEmployee.EmailAddress = GlobalFunctions.replaceChar(txtEmailAddress.Text);
                loEmployee.TIN = GlobalFunctions.replaceChar(txtTIN.Text);
                loEmployee.TINDeducted = chkTINDeducted.Checked ? "Y" : "N";
                loEmployee.PhilHealthId = GlobalFunctions.replaceChar(txtPhilHealthId.Text);
                loEmployee.PhilHealthDeducted = chkPhilHealthDeducted.Checked ? "Y" : "N";
                loEmployee.SSSId = GlobalFunctions.replaceChar(txtSSSId.Text);
                loEmployee.SSSDeducted = chkSSSDeducted.Checked ? "Y" : "N";
                loEmployee.PagibigId = GlobalFunctions.replaceChar(txtPagibigId.Text);
                loEmployee.PagibigDeducted = chkPagibigDeducted.Checked ? "Y" : "N";
                loEmployee.PagibigEmployeeShare = decimal.Parse(txtPagibigEmployeeShare.Text);
                loEmployee.PagibigEmployerShare = decimal.Parse(txtPagibigEmployerShare.Text);
                loEmployee.NoOfDependent = int.Parse(txtNoOfDependent.Text);
                loEmployee.RateType = cboRateType.Text;
                loEmployee.BasicPay = decimal.Parse(txtBasicPay.Text);
                try
                {
                    loEmployee.ImmediateSupervisor = cboImmediateSupervisor.SelectedValue.ToString();
                }
                catch
                {
                    loEmployee.ImmediateSupervisor = "0";
                }
                loEmployee.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loEmployee.UserId = GlobalVariables.UserId;

                string _Id = loEmployee.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Employee has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtEmployeeNo.Text;
                    lRecords[2] = txtLastname.Text;
                    lRecords[3] = txtFirstname.Text;
                    lRecords[4] = txtMiddlename.Text;
                    lRecords[5] = chkActive.Checked ? "Y" : "N";
                    lRecords[6] = txtBiometricsIdNo.Text;
                    lRecords[7] = cboEmploymentType.Text;
                    lRecords[8] = cboDesignation.Text;
                    lRecords[9] = cboDepartment.Text;
                    lRecords[10] = chkNoWorkSchedule.Checked ? "Y" : "N";
                    lRecords[11] = cboWorkSchedule.Text;
                    lRecords[12] = string.Format("{0:MM-dd-yyyy}", dtpBirthday.Value);
                    lRecords[13] = txtEmailAddress.Text;
                    lRecords[14] = txtTIN.Text;
                    lRecords[15] = chkTINDeducted.Checked ? "Y" : "N";
                    lRecords[16] = txtPhilHealthId.Text;
                    lRecords[17] = chkPhilHealthDeducted.Checked ? "Y" : "N";
                    lRecords[18] = txtSSSId.Text;
                    lRecords[19] = chkSSSDeducted.Checked ? "Y" : "N";
                    lRecords[20] = txtPagibigId.Text;
                    lRecords[21] = chkPagibigDeducted.Checked ? "Y" : "N";
                    lRecords[22] = decimal.Parse(txtPagibigEmployeeShare.Text).ToString();
                    lRecords[23] = decimal.Parse(txtPagibigEmployerShare.Text).ToString();
                    lRecords[24] = int.Parse(txtNoOfDependent.Text).ToString();
                    lRecords[25] = cboRateType.Text;
                    lRecords[26] = decimal.Parse(txtBasicPay.Text).ToString();
                    lRecords[27] = cboImmediateSupervisor.Text;
                    lRecords[28] = txtRemarks.Text;
                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                    }
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
