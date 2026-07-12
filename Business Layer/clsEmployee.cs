using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsEmployee
    {

        private int EmployeeID;

        public static DataTable GetAllEmployee()
        {
            //call DataAccess;

            return clsEmployeesData.GetAllEmployee();
        }

        public static bool UpdateEmployee(int ID, string FullName, string Phone,
            string Role, string salary, DateTime Hiredate, bool IsActive)
        {
            return clsEmployeesData.UpdateEmployee(ID, FullName, Phone, Role, salary, Hiredate, IsActive) > 0;
        }

        public static bool AddNewEmployee(string FullName, string Phone,
           string Role, string salary, DateTime Hiredate, bool IsActive)
        {
           
            return clsEmployeesData.AddNewEmployee(FullName, Phone, Role, salary, Hiredate, IsActive) > 0;
        }

        public static bool DeleteEmployee(int EmployeeID)
        {
            return clsEmployeesData.DeleteEmployee(EmployeeID) > 0;
        }

        public static int GetEmployeeCount()
        {
            return clsEmployeesData.TotalEmployee();
        }
    }
}
