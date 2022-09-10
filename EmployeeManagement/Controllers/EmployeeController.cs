using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using EmployeeManagement.ADO_commands;
using PagedList;
using PagedList.Mvc;


namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            throw new Exception("Oops..");
            //return View();
           
        }

        public ActionResult search(int ? page)
        {
            DataTable dt = new DataTable();
            EmployeeAdo find = new EmployeeAdo();
            dt = find.EmployeeSearch("");
            List<EmployeeDetails> employeetable = new List<EmployeeDetails>();
            employeetable = find.ConvertDataTable<EmployeeDetails>(dt);
            return View(employeetable.ToPagedList(page ?? 1,5));    
        }

        public ActionResult searchby(string search, int page)
        {

            EmployeeAdo find = new EmployeeAdo();
            var dt = find.EmployeeSearch(search);
            List<EmployeeDetails> employeetable = new List<EmployeeDetails>();
            employeetable = find.ConvertDataTable<EmployeeDetails>(dt);
            ViewBag.pass = search;
            return View("search",employeetable.ToPagedList(page, 5));

        }

        [HttpPost]
        public ActionResult search(string search, int? page)
        {
            EmployeeAdo find = new EmployeeAdo();
            var dt = find.EmployeeSearch(search);
            List<EmployeeDetails> employeetable = new List<EmployeeDetails>();
            employeetable = find.ConvertDataTable<EmployeeDetails>(dt);
            ViewBag.pass = search;
            return View(employeetable.ToPagedList(page ?? 1, 5));
        }

        public ActionResult addemp()
        {
            TeamAdo team = new TeamAdo();
            EmployeeDetails employee = new EmployeeDetails();
            DataTable dataTable = team.GetTeam();
            ViewBag.TeamList = employee.ToSelectList(dataTable, "TeamId", "TeamName");
            return View();
            
            
        }
        [HttpPost]
        public JsonResult checkemail(string email)
        {
            EmployeeAdo check = new EmployeeAdo();
            var result = check.checkemailavailability(email);
            
                return Json(result,JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public ActionResult GetJobRole(int Team)
        {
            EmployeeAdo getjobrole = new EmployeeAdo();
            var teamjobs = getjobrole.GetJobRole(Team);
            var jsonteam=getjobrole.DataTableToJSONWithStringBuilder(teamjobs);
            return Json(jsonteam, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult addemp(EmployeeDetails addemployee)
        {
            if (ModelState.IsValid == true)
            {
                EmployeeAdo employee = new EmployeeAdo();
                employee.Addemployee(addemployee);
                return RedirectToAction("search");
            }
            else
            {
                TeamAdo team = new TeamAdo();
                EmployeeDetails employee = new EmployeeDetails();
                DataTable dataTable = team.GetTeam();
                ViewBag.TeamList = employee.ToSelectList(dataTable, "TeamId", "TeamName");

                return View(addemployee);
            }

           
        }

        public ActionResult EmpView(int id)
        {
           
           EmployeeAdo view = new EmployeeAdo();    
           var datatable = view.EmployeeView(id);
            return View(datatable);
        }

        public ActionResult Edit(int id)
        {
            
            EmployeeAdo view = new EmployeeAdo();
            var datatable = view.EmployeeView(id);
            if (datatable.Rows.Count == 1)
            {
                TeamAdo teamget = new TeamAdo();
                var team = teamget.GetTeam();
                EmployeeAdo editget = new EmployeeAdo();
                var empdetails= editget.Editemployee(datatable);
                ViewBag.TeamList = empdetails.ToSelectList(team, "TeamId", "TeamName");
               
          

                return View(empdetails);

            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public ActionResult Edit(EmployeeDetails emp)
        {
            if (ModelState.IsValid == true) {
                EmployeeAdo update = new EmployeeAdo();
                update.UpdateEmployee(emp);
                return RedirectToAction("EmpView", new { id= emp.EmpId});
            }
            else
            {
                TeamAdo team = new TeamAdo();
                EmployeeDetails employee = new EmployeeDetails();
                DataTable dataTable = team.GetTeam();
                ViewBag.TeamList = employee.ToSelectList(dataTable, "TeamId", "TeamName");

                return View(emp);
            }
      


           
        }

        public ActionResult Delete(int id)
        {
            EmployeeAdo delete = new EmployeeAdo();
            delete.DeleteEmployee(id);
            return RedirectToAction("Search");
        }

       
      

    }
}