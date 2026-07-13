using Guna.UI2.WinForms;
using Presentation_Layer.Customer;
using Presentation_Layer.Employee;
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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            ApplyBorderRadiusToPanels();
            ApplyMouseEnterButtons();
            MDIclientColor();
           

            frmDashboardContent content = new frmDashboardContent();
            content.MdiParent = this;
            OpenChildForm(content);

        }
        public void MDIclientColor()
        {
            var mdiClient = this.Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiClient != null)
            {
                mdiClient.BackColor = Color.FromArgb(248, 250, 252);
            }
        }

        private void ApplyBorderRadiusToPanels()
        {
            Guna.UI2.WinForms.Guna2Button[] button =
            {
       btnDashboard,
        btnEmployee,
        btnCustomer,
        btnServices,
        btnTransactions,
        btnLogout

            };

            foreach (var panel in button)
            {
                Guna.UI2.WinForms.Guna2Elipse elipse = new Guna.UI2.WinForms.Guna2Elipse();
                elipse.BorderRadius = 15;
                elipse.TargetControl = panel;
            }
        }
        private void ApplyMouseEnterButtons()
        {
            Guna.UI2.WinForms.Guna2Button[] button =
            {
       btnDashboard,
        btnEmployee,
        btnCustomer,
        btnServices,
        btnTransactions,
        btnLogout

            };

            foreach (var btn in button)
            {
                btn.MouseEnter += MenuButton_MouseEnter;
                btn.MouseLeave += MenuButton_MouseLeave;
                
            }
        }

        
        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;

            btn.FillColor = Color.White;
            btn.ForeColor = Color.DodgerBlue;
        }
        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;

            btn.FillColor = Color.Transparent;
            btn.ForeColor = Color.White;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void addUserControl(UserControl userControl)
        {
           // pnlDashbord.Controls.Clear();
            //pnlDashbord.Controls.Add(userControl);
        }
        private void OpenChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(childForm);
            childForm.Show();
        }
        private void btnEmployee_Click(object sender, EventArgs e)
        {
           frmEmployee frmEmployee = new frmEmployee();
           frmEmployee.MdiParent = this;
            OpenChildForm(frmEmployee);

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            frmDashboardContent content = new frmDashboardContent();
            content.MdiParent = this;
            OpenChildForm(content);

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.Show();
            this.Close();

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            frm.MdiParent = this;
            OpenChildForm(frm);
        }
    }
}
