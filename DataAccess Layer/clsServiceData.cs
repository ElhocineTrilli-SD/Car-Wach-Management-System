using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    public class clsServiceData
    {
        public static int TotalServices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM services";

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


        public static DataTable GetAllServices()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = "Select * From services";

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

        public static int UpdateService(int ServiceID, string ServiceName, string ServicePrice)
        {

            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"UPDATE services
   SET [ServiceName] = @ServiceName
      
      ,[Price] = @Price
 WHERE ServiceID = @ServiceID ; ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID",ServiceID);
                        command.Parameters.AddWithValue("@ServiceName", ServiceName);
                        command.Parameters.AddWithValue("@Price", ServicePrice);
                      


                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected;

        }


        public static int AddNewService(string ServiceName,string ServicePrice)
        {
            int ServiceID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"INSERT INTO services
                    VALUES('{0}','{1}');
                    SELECT SCOPE_IDENTITY();";


                    Query = string.Format(Query,ServiceName,ServicePrice );

                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object Result = command.ExecuteScalar();
                        if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                        {
                            ServiceID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return ServiceID;
        }

        public static int DeleteService(int ID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string Query = @"Delete from services where ServiceID = @ID
                                     ";


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { }
            return RowAffected;

        }


    }
}
