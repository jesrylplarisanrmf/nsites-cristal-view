using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using JCSoftwares_V.Global;
using JCSoftwares_V.ApplicationObjects.Classes;
using JCSoftwares_V.ApplicationObjects.Classes.Lendings;
using JCSoftwares_V.ApplicationObjects.Classes.Procurements;
using JCSoftwares_V.ApplicationObjects.Classes.Generics;

using JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Reports.TransactionsRpt;
using JCSoftwares_V.ApplicationObjects.UserInterfaces.Generics;

namespace JCSoftwares_V.ApplicationObjects.UserInterfaces.Lendings.Reports
{
    public partial class CashFlowSummaryUI : Form
    {
        public CashFlowSummaryUI()
        {
            InitializeComponent();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void CashFlowSummaryUI_Load(object sender, EventArgs e)
        {

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }
    }
}
