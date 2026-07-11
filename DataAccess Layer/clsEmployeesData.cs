using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    public class clsEmployeesData
    {

        public static int AddNewEmployee(string Name, string Gender, string Phone, string Position, int Salary, string JDate)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = "insert into Employees values('{0}','{1}','{2}','{3}','{4}','{5}')";
                    Query = string.Format(Query, Name, Gender, Phone, Position, Salary, JDate);

                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected;
        }

        public static DataTable GetAllEmployee()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = "Select * From employees";

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }


                }


            }
            catch (Exception ex) { }
            ;
            return dt;
        }

        public static int DeleteEmployee(int ID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"Delete from Salaries where EmployeeID = @EmpID
                                     Delete from Employees where EmpID = @EmpID ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpID", ID);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected;

        }

        public static int UpdateEmployee(int ID, string FullName, string Phone,
            string Role, string salary, DateTime Hiredate, bool IsActive)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"UPDATE Employees
   SET [FullName] = @FullName,
      [Phone] = @Phone,
      [Role] = @Role,
      [SalaryPerMonth] = @SalaryPerMonth,
      [HireDate] = @HireDate,
      [IsActive] = @IsActive
       where EmployeeID = @EmployeeID ; ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", ID);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Role", Role);
                        command.Parameters.AddWithValue("@SalaryPerMonth", salary);
                        command.Parameters.AddWithValue("@HireDate", Hiredate);
                        command.Parameters.AddWithValue("@IsActive", IsActive);


                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected;

        }


    }
}
