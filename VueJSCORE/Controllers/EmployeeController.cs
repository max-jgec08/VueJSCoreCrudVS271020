using System;
using System.Linq;
using VueJSCORE.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VueJSCORE.DataAccessLayer;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace VueJSCORE.Controllers
{
    public class EmployeeController : Controller
    {
        ResponseModel response = new ResponseModel();
        EmployeeLayer dalObj = new EmployeeLayer();
        IConfiguration configuration;
        public EmployeeController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeeList()
        {
            List<EmployeeModel> empList = dalObj.GetAllEmployees(configuration).ToList();
            return Json(empList);
        }

        [HttpGet]
        public IActionResult Detail(int EmpID)
        {
            EmployeeModel model = dalObj.GetEmployeeData(EmpID, configuration);
            return View(model);
        }
    }
}