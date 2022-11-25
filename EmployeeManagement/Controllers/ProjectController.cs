using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using EmployeeManagement.Models;
using EmployeeManagement.ADO_commands;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
      
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addprojects()
        {
                EmployeeDetails employee = new EmployeeDetails();
                ProjectDetails projectDetails = new ProjectDetails();
                TeamAdo GetTeam = new TeamAdo();
                var datatable = GetTeam.GetTeam();
                ViewBag.TeamId = employee.ToSelectList(datatable, "TeamId", "TeamName");

                return View();
            }
         

        [HttpPost]
        public ActionResult addprojects(ProjectDetails project)
        {
            if (ModelState.IsValid == true) {
                ProjectAdo addproject = new ProjectAdo();
                addproject.addproject(project);

                return RedirectToAction("index", "Home");
            }
            else
            {
                EmployeeDetails employee = new EmployeeDetails();
                TeamAdo GetTeam = new TeamAdo();
                var datatable = GetTeam.GetTeam();
                ViewBag.TeamId = employee.ToSelectList(datatable, "TeamId", "TeamName");
                return View(project);
            }
            

        }

        public ActionResult viewproject()
        {
            ProjectAdo project = new ProjectAdo();
            var datatable = project.viewallproject();
            return View(datatable);

            
        }

        public ActionResult projecttable()
        {
            ProjectAdo project = new ProjectAdo();
            var datatable = project.viewallproject();
            return PartialView(datatable);

        }
       

        public ActionResult updateproject(int id)
        {

            
            ProjectAdo GetProject = new ProjectAdo();
            var project = GetProject.getprojectdetails(id);

             DataTable team = new DataTable();
             TeamAdo GetTeam = new TeamAdo();
             EmployeeDetails employee = new EmployeeDetails();
             var datatable = GetTeam.GetTeam();
             ViewBag.TeamList = employee.ToSelectList(datatable, "TeamId", "TeamName");

            return View(project);

        }
        [HttpPost]
        public ActionResult updateproject(ProjectDetails project)
        {
            if (ModelState.IsValid == true) {
                ProjectAdo upproject = new ProjectAdo();
                upproject.Updateproject(project);
                return RedirectToAction("viewproject");
            }
            else
            {
                EmployeeDetails employee = new EmployeeDetails();
                TeamAdo GetTeam = new TeamAdo();
                var datatable = GetTeam.GetTeam();
                ViewBag.TeamId = employee.ToSelectList(datatable, "TeamId", "TeamName");
                return View(project);

            }
           
        }
        public ActionResult viewprojectbystatus(int id)
        {
            ProjectAdo filter = new ProjectAdo();
             var table=filter.Filterprojectbystatus(id);
            return View("viewproject",table);
        }

        public ActionResult viewprojectbyteam(int id)
        {
            ProjectAdo projectbyteam= new ProjectAdo(); 
            var table=projectbyteam.viewprojectbyteam(id);
                return View(table);
        }

        public ActionResult Delete(int id)
        {
            ProjectAdo deleteproject = new ProjectAdo();
            deleteproject.DeleteProject(id);
            return RedirectToAction("projecttable");
        }

       
    }
}