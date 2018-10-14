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
using JCSoftwares_V.ApplicationObjects.Classes.POSs;
using JCSoftwares_V.ApplicationObjects.Classes.Systems;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Masterfiles
{
    public partial class CashierDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[6];
        GlobalVariables.Operation lOperation;
        Cashier loCashier;
        User loUser;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CashierDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loCashier = new Cashier();
            loUser = new User();
        }
        public CashierDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loCashier = new Cashier();
            loUser = new User();
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
            txtName.Clear();
            cboUsername.SelectedIndex = -1;
            txtRemarks.Clear();
            txtName.Focus();
        }
        #endregion "END OF METHODS"

        private void CashierDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboUsername.DataSource = loUser.getAllData("ViewAll","","");
                cboUsername.DisplayMember = "Username";
                cboUsername.ValueMember = "Id";
                cboUsername.SelectedIndex = -1;
                
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    txtName.Text = lRecords[2];
                    cboUsername.Text = lRecords[3];
                    txtRemarks.Text = lRecords[4];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CashierDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loCashier.Id = lId;
                loCashier.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loCashier.Name = GlobalFunctions.replaceChar(txtName.Text);
                try
                {
                    loCashier.UserId = cboUsername.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a User!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboUsername.Focus();
                    return;
                }
                loCashier.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loCashier.UserId2 = GlobalVariables.UserId;

                string _Id = loCashier.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtName.Text;
                    lRecords[3] = cboUsername.Text;
                    lRecords[4] = txtRemarks.Text;

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
