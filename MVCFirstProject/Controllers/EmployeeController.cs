using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFirstProject.DAL;
using MVCFirstProject.Models;

namespace MVCFirstProject.Controllers
{
    public class EmployeeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: Employee
        public ActionResult EmployeeInfo()
        {
            var empl = new Employee
            {
                EmployeeId = 1,
                EmployeeLocation = "Russia",
                EmployeeName = "Ivan"
            };

            return View(empl);
        }
    }
}