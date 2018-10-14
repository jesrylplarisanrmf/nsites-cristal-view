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

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Masterfiles
{
    public partial class ModeOfPaymentDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[6];
        GlobalVariables.Operation lOperation;
        ModeOfPayment loModeOfPayment;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ModeOfPaymentDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loModeOfPayment = new ModeOfPayment();
        }
        public ModeOfPaymentDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loModeOfPayment = new ModeOfPayment();
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
            chkDefault.Enabled = false;
            chkCashPayment.Enabled = false;
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void ModeOfPaymentDetailUI_Load(object sender, EventArgs e)
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
                    chkDefault.Checked = lRecords[3] == "Y" ? true:false;
                    chkCashPayment.Checked = lRecords[4] == "Y" ? true : false;
                    txtRemarks.Text = lRecords[5];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ModeOfPaymentDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loModeOfPayment.Id = lId;
                loModeOfPayment.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loModeOfPayment.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loModeOfPayment.Default = chkDefault.Checked ? "Y" : "N";
                loModeOfPayment.CashPayment = chkCashPayment.Checked ? "Y" : "N";
                loModeOfPayment.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loModeOfPayment.UserId = GlobalVariables.UserId;

                string _Id = loModeOfPayment.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("ModeOfPayment has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = chkDefault.Checked ? "Y" : "N";
                    lRecords[4] = chkCashPayment.Checked ? "Y" : "N";
                    lRecords[5] = txtRemarks.Text;
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
