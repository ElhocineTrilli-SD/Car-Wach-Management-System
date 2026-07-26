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
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;

namespace Presentation_Layer.Employee
{
    public partial class frmEmployee : Form
    {
        DataTable _dtEmployees;

        public clsEmployee _EmployeeInfo;
       

        public frmEmployee()
        {
            InitializeComponent();
         _EmployeeInfo = new clsEmployee();
        }

        public void RefrechEmployeeList()
        {
            _dtEmployees = clsEmployee.GetAllEmployee();
            dgvEmployees.DataSource = _dtEmployees;
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // JustNumbers
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployees.Rows[e.RowIndex];

                txtEmployeeFullName.Text = row.Cells[1].Value.ToString();
                txtPhone.Text = row.Cells[2].Value.ToString();
                txtRole.Text = row.Cells[3].Value.ToString();
                txtSalary.Text = row.Cells[4].Value.ToString();
                dtpHireDate.Text = row.Cells[5].Value.ToString();
                cbIsActive.SelectedIndex = Convert.ToBoolean(row.Cells[6].Value) ? 0 : 1;

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvEmployees.CurrentRow.Cells[0].Value;
            _EmployeeInfo = clsEmployee.Find(ID);
            _EmployeeInfo.FullName = txtEmployeeFullName.Text;
            _EmployeeInfo.Phone = txtPhone.Text;
            _EmployeeInfo.Role = txtRole.Text;
            _EmployeeInfo.SalaryPerMonth = Convert.ToDecimal(txtSalary.Text);
            _EmployeeInfo.date = dtpHireDate.Value;
            _EmployeeInfo.IsActive = cbIsActive.SelectedIndex == 0;


            DialogResult result = MessageBox.Show(
                           "Are you sure you want to update employee Info?",
                           "Confirm Update",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning
                           );

            if (result == DialogResult.Yes)
            {
                // Update Code : 
                if (_EmployeeInfo.Save())
                {
                    MessageBox.Show(
                                    "The employee with ID " + ID + " was updated successfully.",
                                    "Update Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmEmployee_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to update the employee info Please try again.",
                                    "Update Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }
            }
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            RefrechEmployeeList();
        }

        private void addemployee_Click(object sender, EventArgs e)
        {
            if (txtEmployeeFullName.Text == "" || txtPhone.Text == "" || txtRole.Text == ""
                || txtSalary.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                _EmployeeInfo.FullName = txtEmployeeFullName.Text;
                _EmployeeInfo.Phone = txtPhone.Text;
                _EmployeeInfo.Role = txtRole.Text;
                _EmployeeInfo.SalaryPerMonth =Convert.ToDecimal( txtSalary.Text);
                _EmployeeInfo.date = dtpHireDate.Value;
                _EmployeeInfo.IsActive = cbIsActive.SelectedIndex == 0 ;

                if (_EmployeeInfo.Save())
                {
                    MessageBox.Show("The new employee has been added successfully.", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                     );
                    frmEmployee_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to add the new employee.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int EmpID = (int)dgvEmployees.CurrentRow.Cells[0].Value;

            DialogResult result = MessageBox.Show(
                              "Are you sure you want to delete this employee?",
                              "Confirm Delete",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Warning
                              );

            if (result == DialogResult.Yes)
            {
                // delete Code : 
                if (clsEmployee.DeleteEmployee(EmpID))
                {
                    MessageBox.Show(
                                    "The employee with ID " + EmpID + " was deleted successfully.",
                                    "Delete Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                   );
                    frmEmployee_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to delete the employee with ID " + EmpID + ". Please try again.",
                                    "Delete Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                }
            }
        }
    }
    }
