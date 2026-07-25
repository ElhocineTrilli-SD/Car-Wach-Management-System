using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    public class clsTransactionsData
    {

        public static int TotalTransactions()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Transactions";

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

        public static int AddPayment(int CustomerID, int EmployeeID,
            int ServiceID, DateTime TransactinsDate, Decimal AmountPaid, string PaymentMethod)
        {                               
            int ID = 0;              
            try                          
            {                            
                using (SqlConnection connstring = new SqlConnection(clsConnection.DBConnectionString))
                {
                    connstring.Open();
                    string Query = @"INSERT INTO Transactions
                 VALUES('{0}','{1}','{2}','{3}','{4}','{5}');
                 SELECT SCOPE_IDENTITY();";


                    Query = string.Format(Query, CustomerID, EmployeeID, ServiceID,
                        TransactinsDate, AmountPaid, PaymentMethod);

                    using (SqlCommand command = new SqlCommand(Query, connstring))
                    {
                        object Result = command.ExecuteScalar();
                        if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                        {
                            ID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return ID;
        }


        public static DataTable GetAllTransactions()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnection.DBConnectionString))
                {
                    string Query = "Select * From vw_Transactions";

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
