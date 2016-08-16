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
    public class EmployeeController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();
               
        public ActionResult Index()
        {
            EmployeeViewModel empModel = new EmployeeViewModel();

            List<Employee> employee = db.Employees.ToList();
                        
            List<EmployeeViewModel> EmployeeViewModelList = new List<EmployeeViewModel>();

            if (employee.Count != 0)
            {

                foreach (Employee emp in employee)
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
            }

            return View(EmployeeViewModelList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
                                   
            EmployeeViewModel empModel = new EmployeeViewModel();
           
            Mapper.CreateMap<Employee, EmployeeViewModel>();

            empModel = Mapper.Map<Employee, EmployeeViewModel>(employee); 

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(empModel);
        }

        public ActionResult Create()
        {
            var model = new For_MSBC.Models.EmployeeViewModel();
            ViewBag.DetpList = db.Depts;
            return View(model);
        }
                  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                Mapper.CreateMap<EmployeeViewModel, Employee>();

                employee = Mapper.Map<EmployeeViewModel, Employee>(viewmodel);

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            EmployeeViewModel empModel = new EmployeeViewModel();

            Mapper.CreateMap<Employee, EmployeeViewModel>();

            empModel = Mapper.Map<Employee, EmployeeViewModel>(employee);
            ViewBag.DetpList = db.Depts;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(empModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employeeView)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();

                Mapper.CreateMap<EmployeeViewModel, Employee>();

                employee = Mapper.Map<EmployeeViewModel, Employee>(employeeView);

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
