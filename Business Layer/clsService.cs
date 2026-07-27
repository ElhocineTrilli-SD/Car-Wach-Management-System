using DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode;

        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public clsService()
        {
            Mode = enMode.AddNew;
            this.ServiceID = 0;
            this.ServiceName = "";
            this.Price = 0;
        }

        public clsService(int ServiceID, string ServiceName, decimal Price)
        {
            Mode = enMode.Update;
            this.ServiceID = ServiceID;
            this.ServiceName = ServiceName;
            this.Price = Price;
        }

        public static clsService Find(int ID)
        {

            string ServiceName = "";
            decimal Price = 0;

            bool IsFound = clsServiceData.GetServiceInfoByID
                (
                  ID, ref ServiceName, ref Price 
                );

            if (IsFound)
                return new clsService(ID, ServiceName, Price);

            else return null;

        }

        public static DataTable GetAllServices()
        {
            //call DataAccess;

            return clsServiceData.GetAllServices();
        }
        public static DataTable GetServicesNames()
        {
            //call DataAccess;

            return clsServiceData.GetServicesNames();
        }
        public static decimal GetServicePriceByID(int ServiceID)
        {
            // ترجع سعر الخدمة من قاعدة البيانات
            return clsServiceData.GetServicePrice(ServiceID);
        }
        public static int GetServicesCount()
        {
            return clsServiceData.TotalServices();
        }

        public bool _UpdateService()
        {
            return clsServiceData.UpdateService(this.ServiceID, this.ServiceName, this.Price);
        }

        public bool _AddNewService()
        {
            this.ServiceID = clsServiceData.AddNewService(this.ServiceName, this.Price);

            return (this.ServiceID > 0);
        }

        public static bool DeleteService(int ServiceID)
        {
            return clsServiceData.DeleteService(ServiceID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewService())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                case enMode.Update:
                    return (_UpdateService());
            }
            return false;
        }
    }
}