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
    public partial class HolidayDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[5];
        GlobalVariables.Operation lOperation;
        Holiday loHoliday;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public HolidayDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loHoliday = new Holiday();
        }
        public HolidayDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loHoliday = new Holiday();
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
            cboType.SelectedIndex = -1;
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void HolidayDetailUI_Load(object sender, EventArgs e)
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
                    cboType.Text = lRecords[3];
                    txtRemarks.Text = lRecords[4];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "HolidayDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loHoliday.Id = lId;
                loHoliday.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loHoliday.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loHoliday.Type = GlobalFunctions.replaceChar(cboType.Text);
                loHoliday.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loHoliday.UserId = GlobalVariables.UserId;

                string _Id = loHoliday.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Holiday has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = cboType.Text;
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
