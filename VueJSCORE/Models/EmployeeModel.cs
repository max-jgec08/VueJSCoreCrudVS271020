using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VueJSCORE.Models
{
    public class EmployeeModel
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Designation { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
    }
}
