using Business_Layer;
using Guna.Charts.WinForms;
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

namespace Presentation_Layer.Dashbord
{
    public partial class frmDashboardContent : Form
    {

        public DataTable MostRequeredSrvices;
        List<Label> lblOrders = new List<Label>();
        List<Label> lblServices = new List<Label>();
        List<Guna2ProgressBar> PrograssBars = new List<Guna2ProgressBar>();
        public DataTable Chart;
        public frmDashboardContent()
        {
            InitializeComponent();
            GetTotals();
            MostRequeredSrvices = clsDashboard.GetMostRequestedServices();
            Chart = clsDashboard.GetWeeklyRevenue();
            lblOrders = new List<Label>()   {
        lblOrders01,
        lblOredrs02,
        lblOredrs03,
        lblOredrs04
    };
            lblServices = new List<Label>() {
                lblService01,
                lblService2,
                lblService03,
                lblservice04 };
            PrograssBars = new List<Guna2ProgressBar>() {
                pb01,pb02,pb03,pb04
            };
            UpdateMostRequeredSrvices();
            PopulateGunaChartFromDataTable(Chart);
        }
        public void UpdateMostRequeredSrvices()
        {
           

            for(int i = 0; i < MostRequeredSrvices.Rows.Count; i++)
            {
                // name of service::
                lblServices[i].Text = MostRequeredSrvices.Rows[i]["ServiceName"].ToString();

                //TotalOredrs of service::
                lblOrders[i].Text = MostRequeredSrvices.Rows[i]["TotalOrders"].ToString();

                int Order = Convert.ToInt32(MostRequeredSrvices.Rows[i]["TotalOrders"]);

                int MaxOredrs = Convert.ToInt32(MostRequeredSrvices.Rows[0]["TotalOrders"]);

                // prograss bar::
                PrograssBars[i].Value = (Order * 100) / MaxOredrs;

            }

            


        }
        public void GetTotals()
        {
            
            int Count = clsEmployee.GetEmployeeCount();
            lblCountEmployees.Text =  Count.ToString();
            int CountCostomers = clsCustomer.GetCustomersCount();
            lblCustomersCount.Text = CountCostomers.ToString();
            int CountServices = clsService.GetServicesCount();
            lblServicesCount.Text = CountServices.ToString();
            decimal Revenue = clsTransactions.TotalRevenue();
           TotalRevenue.Text = Revenue.ToString();
        }
        private void frmDashboardContent_Load(object sender, EventArgs e)
        {

        }
       
        private void PopulateGunaChartFromDataTable(DataTable dt)
        {
            gunaChart1.Datasets.Clear();

            GunaBarDataset dataset = new GunaBarDataset
            {
                Label = "Daily Revenue"
            };
            dataset.FillColors.Add(Color.FromArgb(30, 144, 255));
         
            foreach (DataRow row in dt.Rows)
            {
                string dayName = row["DayName"].ToString();
                double totalRevenue = Convert.ToDouble(row["TotalRevenue"]);
                string shortDayName = dayName.Length >= 3 ? dayName.Substring(0, 3) : dayName;

                

                // إدخال اليوم والقيمة
                dataset.DataPoints.Add(shortDayName, totalRevenue);
            }
            
            dataset.CornerRadius = 8;
            gunaChart1.Datasets.Add(dataset);
            gunaChart1.Update();
        }
    }
}
