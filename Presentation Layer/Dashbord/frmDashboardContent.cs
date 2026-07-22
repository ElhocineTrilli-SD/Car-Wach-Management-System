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
            GetTotals();
        }

        public void GetTotals()
        {
            int Count = clsEmployee.GetEmployeeCount();
            lblCountEmployees.Text =  Count.ToString();
            int CountCostomers = clsCustomer.GetCustomersCount();
            lblCustomersCount.Text = CountCostomers.ToString();
            int CountServices = clsService.GetServicesCount();
            lblServicesCount.Text = CountServices.ToString();   
        }

        private void frmDashboardContent_Load(object sender, EventArgs e)
        {

        }
    }
}
