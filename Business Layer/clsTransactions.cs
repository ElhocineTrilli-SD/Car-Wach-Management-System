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
    public class clsTransactions
    {
        public  int ID{  get; set; }


        public static DataTable GetAllTransactions()
        {
            //call DataAccess;

            return clsTransactionsData.GetAllTransactions();
        }

        public static bool AddTransaction(int CustomerID, int EmployeeID,
            int ServiceID, DateTime TransactinsDate, Decimal AmountPaid, string PaymentMethod)
        {

           return   clsTransactionsData.AddPayment(CustomerID,EmployeeID,ServiceID,
                TransactinsDate,AmountPaid,PaymentMethod) != 0;
            
        }

        public static int TotalTransactions()
        {
            return clsTransactionsData.TotalTransactions();
        }

    }
}
