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
using JCSoftwares_V.ApplicationObjects.Classes.Payrolls;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Payrolls.Masterfiles
{
    public partial class IncomeTaxTableDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[7];
        GlobalVariables.Operation lOperation;
        IncomeTaxTable loIncomeTaxTable;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public IncomeTaxTableDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loIncomeTaxTable = new IncomeTaxTable();
        }
        public IncomeTaxTableDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loIncomeTaxTable = new IncomeTaxTable();
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
            txtLowerLimit.Text = "0.00";
            txtUpperLimit.Text = "0.00";
            txtBaseAmount.Text = "0.00";
            txtBaseTax.Text = "0.00";
            txtPercentOver.Text = "0.00";
            txtRemarks.Clear();
            txtLowerLimit.Focus();
        }
        #endregion "END OF METHODS"

        private void IncomeTaxTableDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtLowerLimit.Text = string.Format("{0:n}",decimal.Parse(lRecords[1].ToString()));
                    txtUpperLimit.Text = string.Format("{0:n}", decimal.Parse(lRecords[2].ToString()));
                    txtBaseAmount.Text = string.Format("{0:n}", decimal.Parse(lRecords[3].ToString()));
                    txtBaseTax.Text = string.Format("{0:n}", decimal.Parse(lRecords[4].ToString()));
                    txtPercentOver.Text = string.Format("{0:n}", decimal.Parse(lRecords[5].ToString()));
                    txtRemarks.Text = lRecords[6];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "IncomeTaxTableDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loIncomeTaxTable.Id = lId;
                loIncomeTaxTable.LowerLimit = decimal.Parse(txtLowerLimit.Text);
                loIncomeTaxTable.UpperLimit = decimal.Parse(txtUpperLimit.Text);
                loIncomeTaxTable.BaseAmount = decimal.Parse(txtBaseAmount.Text);
                loIncomeTaxTable.BaseTax = decimal.Parse(txtBaseTax.Text);
                loIncomeTaxTable.PercentOver = decimal.Parse(txtPercentOver.Text);
                loIncomeTaxTable.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loIncomeTaxTable.UserId = GlobalVariables.UserId;

                string _Id = loIncomeTaxTable.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Income Tax Table has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = decimal.Parse(txtLowerLimit.Text).ToString();
                    lRecords[2] = decimal.Parse(txtUpperLimit.Text).ToString();
                    lRecords[3] = decimal.Parse(txtBaseAmount.Text).ToString();
                    lRecords[4] = decimal.Parse(txtBaseTax.Text).ToString();
                    lRecords[5] = decimal.Parse(txtPercentOver.Text).ToString();
                    lRecords[6] = txtRemarks.Text;
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

        private void txtLowerLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtLowerLimit.Text = string.Format("{0:n}",decimal.Parse(txtLowerLimit.Text));
            }
            catch
            {
                txtLowerLimit.Text = "0.00";
            }
        }

        private void txtUpperLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUpperLimit.Text = string.Format("{0:n}", decimal.Parse(txtUpperLimit.Text));
            }
            catch
            {
                txtUpperLimit.Text = "0.00";
            }
        }

        private void txtBaseAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBaseAmount.Text = string.Format("{0:n}", decimal.Parse(txtBaseAmount.Text));
            }
            catch
            {
                txtBaseAmount.Text = "0.00";
            }
        }

        private void txtBaseTax_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBaseTax.Text = string.Format("{0:n}", decimal.Parse(txtBaseTax.Text));
            }
            catch
            {
                txtBaseTax.Text = "0.00";
            }
        }

        private void txtPercentOver_Leave(object sender, EventArgs e)
        {
            try
            {
                txtPercentOver.Text = string.Format("{0:n}", decimal.Parse(txtPercentOver.Text));
            }
            catch
            {
                txtPercentOver.Text = "0.00";
            }
        }
    }
}
