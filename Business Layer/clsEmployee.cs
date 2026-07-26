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
        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;

        public int  EmployeeID {  get; set; }
        public string FullName { get; set; }
        public string Phone {  get; set; }
        public string Role { get; set; }
        public decimal SalaryPerMonth { get; set; }
        public DateTime date {  get; set; }
        public bool IsActive { get; set; }

        public clsEmployee()
        {
            EmployeeID = 0;
            FullName = string.Empty;
            Phone = string.Empty;
            Role = string.Empty;
            SalaryPerMonth = 0;
            date = DateTime.Now;
            IsActive = false;
            Mode = enMode.AddNew;
        }
        public clsEmployee(int EmployeeID, string FullName, string Phone,
            string Role, decimal SalaryPerMonth, DateTime Hiredate, bool IsActive)
        {
            this.EmployeeID = EmployeeID;
            this.FullName = FullName;
            this.Phone = Phone;
            this.Role = Role;
            this.SalaryPerMonth = SalaryPerMonth;
            this.date = Hiredate;
            this.IsActive = IsActive;
            Mode = enMode.Update;

        }


        public static clsEmployee Find(int ID)
        {
            string FullName = "", Phone = "", Role = "";
            decimal SalaryPerMonth = 0;
            DateTime Hiredate = DateTime.Now;
            bool IsActive = false;
            bool IsFound = clsEmployeesData.GetEmployeeInfoByID
                (
                  ID, ref FullName, ref Phone,
                  ref Role, ref SalaryPerMonth, ref Hiredate,
                  ref IsActive
                  );
            if (IsFound)
                return new clsEmployee(ID, FullName, Phone, Role, SalaryPerMonth, Hiredate, IsActive);
            else return null;
        }

        public static DataTable GetAllEmployee()
        {
            //call DataAccess;

            return clsEmployeesData.GetAllEmployee();
        }
        public static DataTable GetEmployeeNames()
        {
            //call DataAccess;

            return clsEmployeesData.GetEmployeesNames();
        }
        public  bool _UpdateEmployee()
        {
            return clsEmployeesData.UpdateEmployee(this.EmployeeID, this.FullName, this.Phone, this.Role,
                this.SalaryPerMonth, this.date, this.IsActive);
        }

        public  bool _AddNewEmployee()
        {
            this.EmployeeID = clsEmployeesData.AddNewEmployee(this.FullName, this.Phone, this.Role,
                this.SalaryPerMonth, this.date, this.IsActive);
            return (this.EmployeeID > 0);
        }

        public static bool DeleteEmployee(int EmployeeID)
        {
            return clsEmployeesData.DeleteEmployee(EmployeeID) ;
        }

        public static int GetEmployeeCount()
        {
            return clsEmployeesData.TotalEmployee();
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEmployee())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }

                case enMode.Update:

                    return (_UpdateEmployee());
            }


            return false;
        }
    }
}
