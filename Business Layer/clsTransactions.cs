using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsTransactions
    {
        public static DataTable GetAllTransactions()
        {
            //call DataAccess;

            return clsTransactionsData.GetAllTransactions();
        }

        public static int TotalTransactions()
        {
            return clsTransactionsData.TotalTransactions();
        }

    }
}
