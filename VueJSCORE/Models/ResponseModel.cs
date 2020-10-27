using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSCORE.Models
{
    public class ResponseModel
    {
        public int resID { get; set; }
        public bool status { get; set; }
        public string successMsg { get; set; }
        public string errorMsg { get; set; }
    }
}
