using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace VueJSCORE.DataAccessLayer
{
    public class ConnectionString
    {
        public static string GetConnStr(IConfiguration configuration)
        {
            return configuration.GetSection("Data").GetSection("ConnectionString").Value;
        }
    }
}
