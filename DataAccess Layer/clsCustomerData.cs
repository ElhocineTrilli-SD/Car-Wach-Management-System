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


        public static int UpdateCustomer(int CustomerID, string FullName, string Phone,
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
            return RowAffected;

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


    }
}
