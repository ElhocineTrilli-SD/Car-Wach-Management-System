using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsCustomer
    {
        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;

        public int CustomerID {  get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string CarPlateNumber { get; set; }
        public string CarBrand {  get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }

        public clsCustomer()
        {
            Mode = enMode.AddNew;
            this.CustomerID = 0;
            this.FullName = "";
            this.Phone = "";
            this.CarPlateNumber = "";
            this.CarBrand = "";
            this.CarModel = "";
            this.CarColor = "";
        }
        public clsCustomer(int CustomerID, string FullName, string Phone,
             string CarPlateNumber, string CarBrand, string CarModel, string CarColor)
        {
            this.CustomerID=CustomerID;
            this.FullName=FullName;
            this.Phone = Phone;
            this.CarPlateNumber=CarPlateNumber;
            this.CarBrand=CarBrand;
            this.CarModel=CarModel;
            this.CarColor=CarColor;
            Mode = enMode.Update;
        }
        public static clsCustomer Find(int ID)
        {
            string FullName = "", Phone = "",
              CarPlateNumber = "", CarBrand = "", CarModel = "", CarColor = "";
           
            //
            bool IsFound = clsCustomerData.GetCustomerInfoByID
                (
                  ID, ref FullName , ref Phone ,
                  ref CarPlateNumber ,ref CarBrand ,ref CarModel ,
                  ref CarColor 
                  );

            if (IsFound)
                return new clsCustomer(ID, FullName, Phone, CarPlateNumber, CarBrand, CarModel, CarColor);

            else return null;

        }
        public static DataTable GetAllCustomers()
        {
            //call DataAccess;

            return clsCustomerData.GetAllCustomers();
        }
        public static DataTable GetCustomersNames()
        {
            //call DataAccess;

            return clsCustomerData.GetCustomersNames();
        }
        public static int GetCustomersCount()
        {
            return clsCustomerData.TotalCustomers();
        }
        public  bool _UpdateCustomer() 
        {
            return clsCustomerData.UpdateCustomer(this.CustomerID,this.FullName,this.Phone,this.CarPlateNumber
                ,this.CarBrand,this.CarModel,this.CarColor);
        }
        private  bool _AddNewCustomer()
        {
            this.CustomerID = clsCustomerData.AddNewCustomer(this.FullName,this.Phone,
                this.CarPlateNumber,this.CarBrand,this.CarModel,this.CarColor);

            return (this.CustomerID > 0);
        }
        public static bool DeleteCustomer(int ID)
        {
            return clsCustomerData.DeleteCustomer(ID) ;
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCustomer())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }

                case enMode.Update:

                    return _UpdateCustomer() ;
            }


            return false;
        }
    }
}
