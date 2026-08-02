using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DataAccess_Layer
{
    public class clsConnection
    {
        public static string DBConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString  ;
    }
}
