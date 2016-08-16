using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using For_MSBC;
using For_MSBC.Models;
using AutoMapper;

namespace For_MSBC.Controllers
{
    public class DepartmentController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();

        public ActionResult Index()
        {
            ViewBag.DetpList = db.Depts;

            return View();
        }

        [HttpGet]
        public ActionResult GetEployeeList(int DeptId)
        {
            var EmployeeList = db.Employees.Where(i => i.DeptId == DeptId);

            List<EmployeeViewModel> EmployeeViewModelList = new List<EmployeeViewModel>();

             foreach (Employee emp in EmployeeList)
                {
                    EmployeeViewModelList.Add(new EmployeeViewModel
                    {
                        EmpID = emp.EmpID,
                        Name = emp.Name,
                        Address = emp.Address,
                        Phone = emp.Phone,
                        DeptName = emp.Dept.Name
                    });
                }


             return PartialView("_EployeeDetails", EmployeeViewModelList);
        }

        public ActionResult FillEmployee(int deptID)
        {
            var employees = db.Employees.Where(c => c.DeptId == deptID);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
	}
}