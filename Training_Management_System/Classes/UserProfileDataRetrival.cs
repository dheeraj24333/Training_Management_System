using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training_Management_System.Models;

namespace Training_Management_System.Classes
{
    public class UserProfileDataRetrival
    {



        public UserProfileModel UserDataRetrivalFunction() {

            UserProfileModel userprofilemodelobject = new UserProfileModel();
            training_management_systemEntities entities = new training_management_systemEntities();
            int Session_id=0;
            if (HttpContext.Current.Session["user_id"] != null)
            {
                Session_id = Convert.ToInt32(HttpContext.Current.Session["user_id"].ToString());

            }

            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == Session_id);
            if (emp != null) {
                var model = entities.managers.SingleOrDefault(e => e.manager_id == emp.manager_id);
                userprofilemodelobject.FullName = emp.user_full_name;
                userprofilemodelobject.Title = emp.user_title ;
                userprofilemodelobject.Department = emp.user_department;
                userprofilemodelobject.manager_name = model.manager_name;
                userprofilemodelobject.Date_Of_Joining = Convert.ToDateTime( emp.date_of_joining);
            }
            return userprofilemodelobject;
        }


    }
}