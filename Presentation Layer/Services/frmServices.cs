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

namespace Presentation_Layer.Services
{
    public partial class frmServices : Form
    {
        private DataTable _dtServices;

        public clsService clsServiceInfo = new clsService();
        public int _ServiceID;
        public frmServices()
        {
            InitializeComponent();
        }
        public void RefrechEmployeeList()
        {
            _dtServices = clsService.GetAllServices();
            dgvServices.DataSource = _dtServices;
        }
        private void frmServices_Load(object sender, EventArgs e)
        {
            RefrechEmployeeList();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            if (txtSName.Text == "" || txtSPrice.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {

                //int CustomerID = (int)dgvServices.CurrentRow.Cells[0].Value;
                clsServiceInfo.ServiceName = txtSName.Text;
                clsServiceInfo.Price = Convert.ToDecimal( txtSPrice.Text);


                if (clsServiceInfo.Save())
                {
                    MessageBox.Show("The new Service has been added successfully.", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                     );
                    frmServices_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to add the new Service.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvServices.CurrentRow.Cells[0].Value;
            DialogResult result = MessageBox.Show(
                             "Are you sure you want to delete this service?",
                             "Confirm Delete",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning
                             );

            if (result == DialogResult.Yes)
            {
                // delete Code : 
                if (clsService.DeleteService(ID))
                {
                    MessageBox.Show(
                                    "The service with ID " + ID + " was deleted successfully.",
                                    "Delete Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmServices_Load(null, null);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _ServiceID = (int)dgvServices.CurrentRow.Cells[0].Value;
            clsServiceInfo = clsService.Find(_ServiceID);
            clsServiceInfo.ServiceName = txtSName.Text;
            clsServiceInfo.Price = Convert.ToDecimal( txtSPrice.Text);

            DialogResult result = MessageBox.Show(
                           "Are you sure you want to update service Info?",
                           "Confirm Update",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning
                           );

            if (result == DialogResult.Yes)
            {
                // Update Code : 
                if (clsServiceInfo.Save())
                { 
                    MessageBox.Show(
                                    "The service with ID " + _ServiceID + " was updated successfully.",
                                    "Update Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmServices_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to update the service info Please try again.",
                                    "Update Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }
            }
        }

        private void dgvServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvServices.Rows[e.RowIndex];

                txtSName.Text = row.Cells[1].Value.ToString();
                txtSPrice.Text = row.Cells[2].Value.ToString();
              
            }
        }
    }
}
