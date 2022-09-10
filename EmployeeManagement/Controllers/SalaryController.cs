using EmployeeManagement.ADO_commands;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSalary()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddSalary(Salary salary)
        {
            SalaryAdo addsalarydetails = new SalaryAdo();
            addsalarydetails.AddSalary(salary);
            return RedirectToAction("Index");
        }

        public ActionResult hike()
        {
            return View();

        }
        public ActionResult hikebyemployee()
        {
            Salary salary = new Salary();
            return View(salary);
        }

        [HttpPost]
        public ActionResult hikebyemployee(int empid)
        {
            SalaryAdo getsalarydetails=new SalaryAdo();
            var empbyid=getsalarydetails.GetSalarydetailsbyemployee(empid);
            return View(empbyid);
        }

        [HttpPost]
        public ActionResult executehikeforemployee(int hike,Salary salary)
        {
            SalaryAdo addhike = new SalaryAdo();
            addhike.incrementsalary(hike,salary);
            return RedirectToAction("index");
        }

        public ActionResult hikebyteam()
        {
            TeamAdo team = new TeamAdo();
            EmployeeDetails employee = new EmployeeDetails();
            DataTable dataTable = team.GetTeam();
            ViewBag.TeamList = employee.ToSelectList(dataTable, "TeamId", "TeamName");
            return View();
        }

        [HttpPost]
        public ActionResult hikebyteamid(int hike,EmployeeDetails employee)
        {
            SalaryAdo empdetails = new SalaryAdo();
            var empdetail=empdetails.getsalarydetailsbyteam(employee.TeamId);
            for(int i=0;i<empdetail.Rows.Count;i++)
            {
                Salary salary= new Salary();
                salary.SalaryId = Convert.ToInt32(empdetail.Rows[i][0].ToString());
                salary.EmployeeId=Convert.ToInt32(empdetail.Rows [i][1].ToString());
                salary.SalaryAmount = Convert.ToInt64(empdetail.Rows[i][2].ToString());
                SalaryAdo salaryad = new SalaryAdo();
                salaryad.incrementsalary(hike,salary);


            }
            return View("index");

        }

        public ActionResult hiketocompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult hiketocompany(int hike)
        {
            SalaryAdo empdetails = new SalaryAdo();
            var empdetail = empdetails.getsalarydetailsforall();
            for (int i = 0; i < empdetail.Rows.Count; i++)
            {
                Salary salary = new Salary();
                salary.SalaryId = Convert.ToInt32(empdetail.Rows[i][0].ToString());
                salary.EmployeeId = Convert.ToInt32(empdetail.Rows[i][1].ToString());
                salary.SalaryAmount = Convert.ToInt64(empdetail.Rows[i][2].ToString());
                SalaryAdo salaryad = new SalaryAdo();
                salaryad.incrementsalary(hike, salary);


            }
            return View("index");

        }
        public ActionResult viewsalary()
        {
            return View();
        }

        public ActionResult viewsalarybyemployee()
        {
            DataTable tbl = new DataTable();
            return View(tbl);
        }

        [HttpPost]
        public ActionResult viewsalarybyemployee(int id)
        {
            SalaryAdo viewsalary = new SalaryAdo();
           var salarydetails= viewsalary.viewsalarydetailsbyid(id);
            return View(salarydetails);
        }

        public ActionResult viewsalarybyteam()
        {
            SalaryAdo viewsalary = new SalaryAdo();
            var teamssalary = viewsalary.viewsalarybyteam();
            return View(teamssalary);
        }

        public ActionResult payslip()
        {
            return View();
        }

        public ActionResult generatepayslip()
        {
            return View();
        }

        [HttpPost]
        public ActionResult generatepayslip(payslip payslip)
        {
            SalaryAdo paysalary = new SalaryAdo();
             var msg=paysalary.generatepayslip(payslip);
             ViewBag.status = msg;
                return View();

        }

        public ActionResult viewpayslip()
        {
            return View();
        }

    
        [HttpPost]
        public ActionResult viewpayslipresult(payslip view)
        {
            SalaryAdo viewpayslip = new SalaryAdo();
           // var viewresult = ;
            return View(viewpayslip.viewpayslip(view));

        }
      
    }
}