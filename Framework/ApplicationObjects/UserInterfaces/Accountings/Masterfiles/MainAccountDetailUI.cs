﻿using System;
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
using JCSoftwares_V.ApplicationObjects.Classes.Accountings;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Accountings.Masterfiles
{
    public partial class MainAccountDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[4];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        MainAccount loMainAccount;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public MainAccountDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loMainAccount = new MainAccount();
        }
        public MainAccountDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loMainAccount = new MainAccount();
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

        private void MainAccountDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    txtDescription.Text = lRecords[2];
                    txtRemarks.Text = lRecords[3];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "MainAccountDetailUI_Load");
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
                loMainAccount.Id = lId;
                loMainAccount.Code = txtCode.Text;
                loMainAccount.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loMainAccount.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loMainAccount.UserId = GlobalVariables.UserId;

                string _Id = loMainAccount.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Main Account has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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