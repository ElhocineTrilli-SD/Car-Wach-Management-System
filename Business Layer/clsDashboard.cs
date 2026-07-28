using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDashboard
    {
        public static DataTable GetMostRequestedServices()
        {
            //call DataAccess;

            return clsDashboardData.GetMostRequestedServices();
        }

    }
}
