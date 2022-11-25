using EmployeeManagement.ADO_commands;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagement.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(EmployeeDetails loginDetails)
        {
            EmployeeAdo logger = new EmployeeAdo();
            var validate=logger.ValidateUser(loginDetails);
            if (validate == true)
            {
                FormsAuthentication.SetAuthCookie(loginDetails.EmpName, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.send = 0;
                return View(loginDetails);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}