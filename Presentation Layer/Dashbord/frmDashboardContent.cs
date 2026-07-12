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

namespace Presentation_Layer.Dashbord
{
    public partial class frmDashboardContent : Form
    {
        public frmDashboardContent()
        {
            InitializeComponent();
            GetTotalEmployee();
        }

        public void GetTotalEmployee()
        {
            int Count = clsEmployee.GetEmployeeCount();
            lblCountEmployees.Text = "+ " + Count.ToString();
        }

    }
}
