using Business_Layer;
using Guna.UI2.WinForms;
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

        public clsTransactions _TransactionsInfo = new clsTransactions();
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
           
            cbService.DisplayMember = "ServiceName";
            cbService.ValueMember = "ServiceID";
            cbService.SelectedIndex = 0;
            cbService.DataSource = dt;
        }
        public void RefrechTransactionsList()
        {
            _dtTransactions = clsTransactions.GetAllTransactions();
            dgvTransactions.DataSource = _dtTransactions;
        }
        private void frmTransactions_Load(object sender, EventArgs e)
        {
            RefrechTransactionsList();

            defaultValue();

        }
        public void defaultValue()
        {
            txtAmount.Text = "";
            cbCustomer.SelectedIndex = 0;
            cbEmployee.SelectedIndex = 0;
            cbService.SelectedIndex = 0;
            cbPaymentMethod.SelectedIndex = 0;
            TDate.Value = DateTime.Now;
        }
        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbService.SelectedValue == null ||
         cbService.SelectedValue is DataRowView)
                return;
            int serviceID = Convert.ToInt32(cbService.SelectedValue);

            txtAmount.Text = clsService.GetServicePriceByID(serviceID).ToString();
            txtAmount.Enabled = false;
        }
        private void cbService_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateInputs()
        {
            bool IsValid = true;

            errorProvider1.Clear();

            if (cbCustomer.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbCustomer, "Select a customer.");
                IsValid = false;
            }

            if (cbEmployee.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbEmployee, "Select an employee.");
                IsValid = false;
            }

            if (cbService.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbService, "Select a service.");
                IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                errorProvider1.SetError(txtAmount, "Enter the amount.");
                IsValid = false;
            }

            if (cbPaymentMethod.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbService, "Select payment method.");
                IsValid = false;
            }

          
            return IsValid;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            DialogResult result = MessageBox.Show(
                            "Are you sure you want to Add this Billing?",
                            "Confirm payment",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                            );

            if (result == DialogResult.Yes)
            {
                _TransactionsInfo.CustomerID = Convert.ToInt32(cbCustomer.SelectedValue);
                _TransactionsInfo.EmployeeID = Convert.ToInt32(cbEmployee.SelectedValue);
                _TransactionsInfo.ServiceID = Convert.ToInt32(cbService.SelectedValue);
                _TransactionsInfo.TransactionsDate = TDate.Value;
                _TransactionsInfo.AmountPaid = Convert.ToDecimal(txtAmount.Text);
                _TransactionsInfo.PaymentMethod = cbPaymentMethod.Text;

                if (_TransactionsInfo.Save())
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
        private void txtPaymentMethod_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void guna2ContextMenuStrip2_Click(object sender, EventArgs e)
        {
      

        }

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void z(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
