using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_Management_System.Classes;
using Training_Management_System.Models;

namespace Training_Management_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult LoginPageView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPageView(LoginPageModel LoginDataObject)
        {
            LoginProcess loginprocess = new LoginProcess();
            int result = loginprocess.LoginCheck(LoginDataObject);

            if (result == 0)
                return RedirectToAction("ChangePassword");
            else if (result == 1)
                return RedirectToAction("Dashboard", "Dashboard");
            else
                return RedirectToAction("error");
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(LoginPageModel LoginDataObject)
        {
            LoginProcess loginprocess = new LoginProcess();
            bool flag = loginprocess.ChangePassword(LoginDataObject);
            if (flag == true)
                return RedirectToAction("FirstTimeProfile");
            else
                return RedirectToAction("error");
        }

        [HttpGet]
        public ActionResult FirstTimeProfile()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView");
            }
            else
            {
                return View();
            }
                
        }

        [HttpPost]
        public ActionResult FirstTimeProfile(LoginPageModel LoginDataObject)
        {
            LoginProcess loginprocess = new LoginProcess();
            Boolean flag = loginprocess.SaveProfile(LoginDataObject);
            if (flag == true)
                return RedirectToAction("LoginPageView");
            else
                return RedirectToAction("error");
        }

        [HttpGet]
        public ActionResult Logout() {
                Session.Abandon();
                Session.Clear();
                return RedirectToAction("LoginPageView");
        }

        

    }
}