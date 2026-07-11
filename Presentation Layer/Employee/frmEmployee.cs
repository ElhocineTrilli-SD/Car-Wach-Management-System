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

namespace Presentation_Layer.Employee
{
    public partial class frmEmployee : Form
    {
        DataTable _dtEmployees;

        public frmEmployee()
        {
            InitializeComponent();
            RefrechEmployeeList();
        }

        public void RefrechEmployeeList()
        {
            _dtEmployees =  clsEmployee.GetAllEmployee();
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
                HireDate.Text = row.Cells[5].Value.ToString();
                cbIsActive.SelectedIndex = Convert.ToBoolean(row.Cells[6].Value) ? 0 : 1;

            }
        }
    }
}
