using System;
using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using EmployeeManagement.ADO_commands;

namespace EmployeeManagement.Controllers
{
    public class TeamsController : Controller
    {
        public string cs = @"Data Source = IND-600\SQLEXPRESS ; Initial Catalog = EmployeeManagement ; Integrated Security= True";

        // GET: Teams
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult teams()
        {
            TeamAdo viewteam = new TeamAdo();
            var datatable = viewteam.ViewTeam();

            return View(datatable);
        }

        public ActionResult addteams()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addteams(TeamDetails team)
        {
            if (ModelState.IsValid == true) {
                TeamAdo addteam = new TeamAdo();
                addteam.AddTeam(team);
                return RedirectToAction("teams");
            }
            else
            {
                return View(team);
            }
            
        }

        public ActionResult viewmembers(int id)
        {
            TeamAdo ViewTeam = new TeamAdo();
            var teammembers = ViewTeam.ViewTeamMembers(id);
            return View(teammembers);
        }
    }
}