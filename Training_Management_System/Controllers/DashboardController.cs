using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_Management_System.Classes;
using Training_Management_System.Models;

namespace Training_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewProfile()
        {
            UserProfileDataRetrival UserProfileDataRetrivalobject = new UserProfileDataRetrival();
            UserProfileModel userprofilemodelobject = new UserProfileModel();
            userprofilemodelobject = UserProfileDataRetrivalobject.UserDataRetrivalFunction();
            return PartialView(userprofilemodelobject);
        }
    }
}