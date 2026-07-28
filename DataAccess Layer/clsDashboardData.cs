using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        
        public static DataTable GetWeeklyRevenue()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = @"WITH AllDays AS(
    SELECT 'Monday' AS DayName, 1 AS DayNum UNION ALL
    SELECT 'Tuesday', 2 UNION ALL
    SELECT 'Wednesday', 3 UNION ALL
    SELECT 'Thursday', 4 UNION ALL
    SELECT 'Friday', 5 UNION ALL
    SELECT 'Saturday', 6 UNION ALL
    SELECT 'Sunday', 7
)
SELECT
    d.DayName,
    ISNULL(SUM(t.AmountPaid), 0) AS TotalRevenue
FROM AllDays d
LEFT JOIN Transactions t
    ON d.DayName = DATENAME(WEEKDAY, t.TransactionDate)
GROUP BY
    d.DayName,
    d.DayNum;";

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
            catch (Exception ex)
            {
                clsEventLog.LogException("GetWeeklyRevenue", ex);
            }
       ;
            return dt;
        }

    }
}
