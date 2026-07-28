using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
  
    public class clsDashboardData
    {
        public static DataTable GetMostRequestedServices()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = @"SELECT TOP (4)
    S.ServiceName,
   
    COUNT(T.ServiceID) AS TotalOrders
FROM Transactions T
INNER JOIN Services S
    ON T.ServiceID = S.ServiceID
GROUP BY
    S.ServiceName
ORDER BY
    TotalOrders DESC;";

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


    }
}
