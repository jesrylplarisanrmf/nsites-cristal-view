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
using JCSoftwares_V.ApplicationObjects.Classes.Lendings;
using JCSoftwares_V.ApplicationObjects.Classes.HRISs;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Masterfiles
{
    public partial class CollectorDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[4];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Collector loCollector;
        Branch loBranch;
        Employee loEmployee;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CollectorDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loCollector = new Collector();
            loBranch = new Branch();
            loEmployee = new Employee();
        }
        public CollectorDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loCollector = new Collector();
            loBranch = new Branch();
            loEmployee = new Employee();
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
            cboEmployee.Text = "";
            cboBranch.Text = "";
            txtRemarks.Clear();
            cboEmployee.Focus();
        }
        #endregion "END OF METHODS"

        private void CollectorDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboEmployee.DataSource = loEmployee.getEmployeeNames();
                    cboEmployee.DisplayMember = "Employee Name";
                    cboEmployee.ValueMember = "Id";
                    cboEmployee.SelectedIndex = -1;
                }
                catch { }

                try
                {
                    cboBranch.DataSource = loBranch.getAllData("ViewAll","","");
                    cboBranch.DisplayMember = "Description";
                    cboBranch.ValueMember = "Id";
                    cboBranch.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    cboEmployee.Text = lRecords[1];
                    cboBranch.Text = lRecords[2];
                    txtRemarks.Text = lRecords[3];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CollectorDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loCollector.Id = lId;
                try
                {
                    loCollector.EmployeeId = cboEmployee.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select an Employee!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboEmployee.Focus();
                    return;
                }
                try
                {
                    loCollector.BranchId = cboBranch.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Branch!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboBranch.Focus();
                    return;
                }
                
                loCollector.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loCollector.UserId = GlobalVariables.UserId;

                string _Id = loCollector.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Collector has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = cboEmployee.Text;
                    lRecords[2] = cboBranch.Text;
                    lRecords[3] = txtRemarks.Text;
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
