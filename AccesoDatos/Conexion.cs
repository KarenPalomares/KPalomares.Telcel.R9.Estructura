using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AccesoDatos
{
    public class Conexion
    {
        public static string Get()
        {
            return ConfigurationManager.ConnectionStrings["KPalomaresEstructura"].ToString();
        }
    }
}
