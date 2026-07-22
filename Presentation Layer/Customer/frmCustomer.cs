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

namespace Presentation_Layer.Customer
{
    public partial class frmCustomer : Form
    {
        public DataTable _dtCustomers;
        public frmCustomer()
        {
            InitializeComponent();
        }
        public void RefrechEmployeeList()
        {
            _dtCustomers = clsCustomer.GetAllCustomers();
            dgvCustomer.DataSource = _dtCustomers;
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            RefrechEmployeeList();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];

                txtFullName.Text = row.Cells[1].Value.ToString();
                txtPhone.Text = row.Cells[2].Value.ToString();
                txtCarPlateNumber.Text = row.Cells[3].Value.ToString();
                txtCarBrand.Text = row.Cells[4].Value.ToString();
                txtCarModel.Text = row.Cells[5].Value.ToString();
                txtCarColor.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int CustomerID = (int)dgvCustomer.CurrentRow.Cells[0].Value;
            string FullName = txtFullName.Text;
            string Phone = txtPhone.Text;
            string CarPlateNumber = txtCarPlateNumber.Text;
            string CarBrand = txtCarBrand.Text;
            string CarModel = txtCarModel.Text;
            string CarColor = txtCarColor.Text;


            DialogResult result = MessageBox.Show(
                           "Are you sure you want to update Customer Info?",
                           "Confirm Update",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning
                           );

            if (result == DialogResult.Yes)
            {
                // Update Code : 
                if (clsCustomer.UpdateEmployee(CustomerID, FullName, Phone, CarPlateNumber, CarBrand, CarModel, CarColor))
                {
                    MessageBox.Show(
                                    "The customer with ID " + CustomerID + " was updated successfully.",
                                    "Update Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmCustomer_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to update the customer info Please try again.",
                                    "Update Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text == "" || txtPhone.Text == "" || txtCarPlateNumber.Text == ""
                || txtCarModel.Text == "" || txtCarColor.Text == "" || txtCarBrand.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {

                int CustomerID = (int)dgvCustomer.CurrentRow.Cells[0].Value;
                string FullName = txtFullName.Text;
                string Phone = txtPhone.Text;
                string CarPlateNumber = txtCarPlateNumber.Text;
                string CarBrand = txtCarBrand.Text;
                string CarModel = txtCarModel.Text;
                string CarColor = txtCarColor.Text;

                if (clsCustomer.AddNewCustomer(FullName, Phone, CarPlateNumber, CarBrand, CarModel, CarColor))
                {
                    MessageBox.Show("The new Customer has been added successfully.", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                     );
                    frmCustomer_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to add the new Customer.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvCustomer.CurrentRow.Cells[0].Value;
            DialogResult result = MessageBox.Show(
                             "Are you sure you want to delete this customer?",
                             "Confirm Delete",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning
                             );

            if (result == DialogResult.Yes)
            {
                // delete Code : 
                if (clsCustomer.DeleteCustomer(ID))
                {
                    MessageBox.Show(
                                    "The customer with ID " + ID + " was deleted successfully.",
                                    "Delete Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmCustomer_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to delete the customer with ID " + ID + ". Please try again.",
                                    "Delete Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }
            }
        }
    }
}

