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
        public ActionResult LoginPageView(int check=0)
        {
            if (check == 1)
            {
                ViewBag.Message = "Invalid Username or Password";
            }
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
            else {
                return RedirectToAction("LoginPageView",new { check=1});
            }
                
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView");
            }
            else {
                LoginProcess loginprocess = new LoginProcess();
                bool flag = loginprocess.checkfirsttimeuser(Convert.ToInt32(Session["user_id"].ToString()));

                if (flag) {

                    return RedirectToAction("Dashboard", "Dashboard");
                }
                else {
                    return View();
                }

                
            }
            
        }

        [HttpPost]
        public ActionResult ChangePassword(LoginPageModel LoginDataObject)
        {
            LoginProcess loginprocess = new LoginProcess();
            bool flag = loginprocess.ChangePassword(LoginDataObject);
            if (flag == true)
                return RedirectToAction("FirstTimeProfile", new {id=1 });
            else
                return RedirectToAction("error");
        }

        [HttpGet]
        public ActionResult FirstTimeProfile(int id = 0)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView");
            }
            else
            {
                LoginProcess loginprocess = new LoginProcess();
                bool flag = loginprocess.checkfirsttimeuser(Convert.ToInt32(Session["user_id"].ToString()));

                if (id==0 && flag)
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }
                else 
                {
                    if (id != 1 && flag) {
                       
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    else
                    {
                        LoginPageModel loginPageModel = new LoginPageModel();
                        loginPageModel.user_id = Convert.ToInt32(Session["user_id"].ToString());
                        return View(loginPageModel);
                    }
                }
                
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