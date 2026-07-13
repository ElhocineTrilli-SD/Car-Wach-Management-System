using Guna.UI2.WinForms;
using Presentation_Layer.Dashbord;
using Presentation_Layer.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Control Temp = ((Control)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";
            if(GlobalClass.GetStoredCredential(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
            else
            {
                cbRememberMe.Checked = false;
            }
              
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }


            if (txtUserName.Text == "" && txtPassword.Text == "")
            {
               
                return; }

            txtUserName.Validating += ValidateEmptyTextBox;
            txtPassword.Validating += ValidateEmptyTextBox;


            if (txtPassword.Text == "admin" && txtUserName.Text == "admin")
            {
                if(cbRememberMe.Checked)
                {
                    GlobalClass.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    GlobalClass.RememberUsernameAndPassword("","");

                }

                frmDashboard frmDashboard = new frmDashboard();
                frmDashboard.ShowDialog();
            }
            else
            {
                MessageBox.Show("Incorrect password. Please try again.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;
        }
    }
}
