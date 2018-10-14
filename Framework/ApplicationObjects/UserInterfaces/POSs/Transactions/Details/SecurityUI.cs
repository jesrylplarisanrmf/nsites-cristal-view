using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.POSs.Transactions.Details
{
    public partial class SecurityUI : Form
    {
        public bool lOKToOverride;

        public SecurityUI()
        {
            InitializeComponent();
            lOKToOverride = false;
        }

        private void SecurityUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            lOKToOverride = false;
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnValidate_Click(null, new EventArgs());
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == GlobalVariables.OverridePassword)
            {
                lOKToOverride = true;
            }
            else
            {
                MessageBoxUI _mb = new MessageBoxUI("Incorrect Password!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                txtPassword.Focus();
                return;
            }
            this.Close();
        }
    }
}
