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
            FillComboboxWithCustomersNames();
            FillComboboxWithEmployeesNames();
            FillComboboxWithServicesNames();
        }
        public void FillComboboxWithCustomersNames()
        {
            DataTable dt = clsCustomer.GetCustomersNames();
            cbCustomer.DataSource = dt;
            cbCustomer.DisplayMember = "FullName";
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.SelectedIndex = 0;

        }
        public void FillComboboxWithEmployeesNames()
        {
            DataTable dt = clsEmployee.GetEmployeeNames();
            cbEmployee.DataSource = dt;
            cbEmployee.DisplayMember = "FullName";
            cbEmployee.ValueMember = "EmployeeID";
            cbEmployee.SelectedIndex = 0;

        }

        public void FillComboboxWithServicesNames()
        {
            DataTable dt = clsService.GetServicesNames();
            cbService.DataSource = dt;
            cbService.DisplayMember = "ServiceName";
            cbService.ValueMember = "ServiceID";
            cbService.SelectedIndex = 0;

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

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbService.SelectedValue == null ||
         cbService.SelectedValue is DataRowView)
                return;
            int serviceID = Convert.ToInt32(cbService.SelectedValue);

            txtAmount.Text = clsService.GetServicePriceByID(serviceID).ToString();
        }


        private void cbService_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                            "Are you sure you want to Add this Billing?",
                            "Confirm payment",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                            );

            if (result == DialogResult.Yes)
            {
                int CustomerID = Convert.ToInt32(cbCustomer.SelectedValue);
                int EmployeeID = Convert.ToInt32(cbEmployee.SelectedValue);
                int ServiceID = Convert.ToInt32(cbService.SelectedValue);
                DateTime TransactinsDate = TDate.Value;
                Decimal AmountPaid = Convert.ToDecimal(txtAmount.Text);
                string PaymentMethod = txtPaymentMethod.Text;

                if (clsTransactions.AddTransaction(CustomerID, EmployeeID, ServiceID,
                    TransactinsDate, AmountPaid, PaymentMethod))
                {
                    MessageBox.Show("The new payment has been added successfully.", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                     );
                    frmTransactions_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to add the new payment.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvTransactions.CurrentRow.Cells[0].Value;
            DialogResult result = MessageBox.Show(
                             "Are you sure you want to delete this service?",
                             "Confirm Delete",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning
                             );

            if (result == DialogResult.Yes)
            {
                // delete Code : 
                if (clsTransactions.DeleteTransaction(ID))
                {
                    MessageBox.Show(
                                    "The service with ID " + ID + " was deleted successfully.",
                                    "Delete Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmTransactions_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to delete the service with ID " + ID + ". Please try again.",
                                    "Delete Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }


            }
        }
    }
}
