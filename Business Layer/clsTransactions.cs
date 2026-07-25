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
        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;


        //default values:
        clsTransactions()
        {
            Mode = enMode.AddNew;
            this.TransactionsID = 0;
            this.CustomerID = 0;
            this.EmployeeID = 0;
            this.ServiceID = 0;
            this.AmountPaid = 0;
            this.PaymentMethod = "";
            this.TransactionsDate = DateTime.Now;
        }

        clsTransactions(int TransactionID , int CustomerID,
            int EmployeeID,int serviceID,DateTime TransactionsDate,decimal AmountPaid
            , string PaymentMethod)
        {
            this.TransactionsID = TransactionID;
            this.CustomerID = CustomerID;
            CustomerInfo = clsCustomer.Find(CustomerID);
            this.EmployeeID = EmployeeID;
            this.ServiceID= serviceID;
            this.TransactionsDate = TransactionsDate;
            this.AmountPaid =AmountPaid;
            this.PaymentMethod= PaymentMethod;

            Mode = enMode.Update;
        }

        public  int TransactionsID {  get; set; }
        public clsCustomer CustomerInfo;
        public int CustomerID { get; set; }

        public clsEmployee EmployeeInfo;
        public int EmployeeID { get; set; }

        public clsService ServiceInfo;
        public int ServiceID { get; set; }

        public DateTime TransactionsDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }


        public static DataTable GetAllTransactions()
        {
            //call DataAccess;

            return clsTransactionsData.GetAllTransactions();
        }

        public static decimal TotalRevenue()
        {
            return clsTransactionsData.GetTotalRevenue();
        }


        public static  bool AddTransaction(int CustomerID, int EmployeeID,
            int ServiceID, DateTime TransactinsDate, Decimal AmountPaid, string PaymentMethod)
        {

            return clsTransactionsData.AddPayment(CustomerID, EmployeeID, ServiceID,
                 TransactinsDate, AmountPaid, PaymentMethod) != 0;
            //this.ID = clsTransactionsData.AddPayment(CustomerID, EmployeeID, ServiceID,
            //    TransactinsDate, AmountPaid, PaymentMethod);

            //return this.ID != 0;
        }

        public static int TotalTransactions()
        {
            return clsTransactionsData.TotalTransactions();
        }

    }
}
