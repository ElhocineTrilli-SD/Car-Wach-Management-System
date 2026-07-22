using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsService
    {
        public static DataTable GetAllServices()
        {
            //call DataAccess;

            return clsServiceData.GetAllServices();
        }

        public static int GetServicesCount()
        {
            return clsServiceData.TotalServices();
        }

        public static bool UpdateService(int ID, string ServiceName, string ServicePrice)
        {
            return clsServiceData.UpdateService(ID,ServiceName,ServicePrice) > 0;
        }

        public static bool AddNewService(string ServiceName, string ServicePrice)
        {

            return clsServiceData.AddNewService(ServiceName,ServicePrice) > 0;
        }

        public static bool DeleteService(int ServiceID)
        {
            return clsServiceData.DeleteService(ServiceID) > 0;
        }

    }
}
