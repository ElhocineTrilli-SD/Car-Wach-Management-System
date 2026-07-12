using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess_Layer
{
    public class clsEmployeesData
    {

        public static int TotalEmployee()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Employees";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch
            {
                return 0;
            }

        }

        public static int AddNewEmployee( string FullName, string Phone,
            string Role, string salary, DateTime Hiredate, bool IsActive)
        {
            int EmpID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"INSERT INTO Employees
                 VALUES('{0}','{1}','{2}','{3}','{4}','{5}');
                 SELECT SCOPE_IDENTITY();";


                    Query = string.Format(Query,FullName,Phone,Role,salary,Hiredate,IsActive);

                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object Result = command.ExecuteScalar();
                        if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                        {
                            EmpID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return EmpID;
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
                    string Query = @"Delete from Employees where EmployeeID = @EmpID
                                     ";


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
