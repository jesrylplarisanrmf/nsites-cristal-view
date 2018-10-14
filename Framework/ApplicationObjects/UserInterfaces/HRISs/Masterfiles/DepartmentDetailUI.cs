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
    public partial class DepartmentDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[4];
        GlobalVariables.Operation lOperation;
        Department loDepartment;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public DepartmentDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loDepartment = new Department();
        }
        public DepartmentDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
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
            txtCode.Clear();
            txtDescription.Clear();
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void DepartmentDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    //txtCode.ReadOnly = true;
                    //txtCode.BackColor = SystemColors.Control;
                    //txtCode.TabStop = false;
                    txtDescription.Text = lRecords[2];
                    txtRemarks.Text = lRecords[3];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "DepartmentDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loDepartment.Id = lId;
                loDepartment.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loDepartment.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loDepartment.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loDepartment.UserId = GlobalVariables.UserId;

                string _Id = loDepartment.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Department has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
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
