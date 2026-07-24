using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer.Transactions
{
    public partial class frmTransactions : Form
    {
        private DataTable _dtTransactions;
        public frmTransactions()
        {
            InitializeComponent();
        }
        public void RefrechTransactionsList()
        {
            _dtTransactions = clsTransactions.GetAllTransactions();
            dgvTransactions.DataSource = _dtTransactions;
        }
        private void frmTransactions_Load(object sender, EventArgs e)
        {
            RefrechTransactionsList();
        }
    }
}
