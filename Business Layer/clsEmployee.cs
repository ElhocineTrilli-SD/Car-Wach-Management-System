using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsEmployee
    {
        public static DataTable GetAllEmployee()
        {
            //call DataAccess;

            return clsEmployeesData.GetAllEmployee();
        }
    }
}
