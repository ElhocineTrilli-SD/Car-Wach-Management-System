using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsCustomer
    {
        public static DataTable GetAllCustomers()
        {
            //call DataAccess;

            return clsCustomerData.GetAllCustomers();
        }

        public static int GetCustomersCount()
        {
            return clsCustomerData.TotalCustomers();
        }


        public static bool UpdateEmployee(int CustomerID, string FullName, string Phone,
             string CarPlateNumber, string CarBrand, string CarModel, string CarColor)
          
        {
            return clsCustomerData.UpdateCustomer(CustomerID,FullName,Phone,CarPlateNumber,CarBrand,CarModel,CarColor) > 0;
        }
        public static bool AddNewCustomer(string FullName, string Phone,
             string CarPlateNumber, string CarBrand, string CarModel, string CarColor)
        {

            return clsCustomerData.AddNewCustomer(FullName,Phone,CarPlateNumber,CarBrand,CarModel,CarColor) > 0;
        }
    }
}
