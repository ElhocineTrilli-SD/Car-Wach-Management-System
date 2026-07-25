using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    public class clsCustomerData
    {
        public static bool GetCustomerInfoByID(int CustomerID,ref string FullName,ref string Phone,
            ref string CarPlateNumber,ref string CarBrand,ref string CarModel,ref string CarColor)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
            {
                string Query = "select * from customers where CustomerID = @CustomerID";

                using (SqlCommand cmd = new SqlCommand(Query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            IsFound = true;
                            FullName = (string)reader["FullName"];
                            Phone = (string)reader["Phone"];
                            CarPlateNumber = (string)reader["CarPlateNumber"];
                            CarBrand = (string)reader["CarBrand"];
                            CarModel = (string)reader["CarModel"];
                            CarColor = (string)reader["CarColor"];
                        }
                        else
                        {
                            IsFound = false;
                        }

                    }
                    catch (Exception ex)
                    {
                         clsEventLog.LogException("GetCustomerInfoByID", ex);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }


        public static int TotalCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Customers";

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
        //
      

        public static DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = "Select * From Customers";

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

        public static DataTable GetCustomersNames()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = @"SELECT
    0 AS CustomerID,
    'None' AS FullName

UNION ALL

SELECT
    CustomerID,
    FullName
FROM Customers
ORDER BY CustomerID;";

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


        public static bool UpdateCustomer(int CustomerID, string FullName, string Phone,
             string CarPlateNumber, string CarBrand, string CarModel, string CarColor)
        {
    
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"UPDATE Customers
   SET [FullName] = @FullName
      ,[Phone] = @Phone
      ,[CarPlateNumber] = @CarPlateNumber
      ,[CarBrand] = @CarBrand
      ,[CarModel] = @CarModel
      ,[CarColor] = @CarColor
 WHERE CustomerID = @CustomerID ; ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@CarPlateNumber", CarPlateNumber);
                        command.Parameters.AddWithValue("@CarBrand", CarBrand);
                        command.Parameters.AddWithValue("@CarModel", CarModel);
                        command.Parameters.AddWithValue("@CarColor", CarColor);


                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected > 0;

        }


        public static int AddNewCustomer( string FullName, string Phone,
             string CarPlateNumber, string CarBrand, string CarModel, string CarColor)
        {
            int CustomerID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"INSERT INTO Customers
                    VALUES('{0}','{1}','{2}','{3}','{4}','{5}');
                    SELECT SCOPE_IDENTITY();";


                    Query = string.Format(Query,FullName,Phone,CarPlateNumber,CarBrand,CarModel,CarColor);

                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object Result = command.ExecuteScalar();
                        if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                        {
                            CustomerID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return CustomerID;
        }

        public static bool DeleteCustomer(int ID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"Delete from Customers where CustomerID = @ID
                                     ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected > 0;

        }


    }
}
