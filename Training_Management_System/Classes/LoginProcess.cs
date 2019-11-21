using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training_Management_System.Models;

namespace Training_Management_System.Classes
{
    public class LoginProcess
    {
        public int LoginCheck(LoginPageModel LoginDataObject)
        {
            int returnvalue;
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.username == LoginDataObject.UserName & e.password == LoginDataObject.Password);


            if (emp != null)
            {
                HttpContext.Current.Session["user_id"] = emp.user_id;
                HttpContext.Current.Session["username"] = emp.username;
                if (emp.password == "12345")
                {
                    returnvalue = 0;
                }
                else
                    returnvalue = 1;
            }
            else
            {
                returnvalue = 2;
            }
            return returnvalue;
        }



        public Boolean ChangePassword(LoginPageModel LoginDataObject)
        {

            bool flag = false;
            int Session_id = 0;
            if (HttpContext.Current.Session["user_id"] != null)
            {
                Session_id = Convert.ToInt32(HttpContext.Current.Session["user_id"].ToString());

            }
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == Session_id);

            emp.password = LoginDataObject.Password;
            entities.SaveChanges();
            if (emp.password != "12345")
                flag = true;

            return flag;
        }

        public Boolean SaveProfile(LoginPageModel LoginDataObject)
        {
            int Session_id = 0;
            if (HttpContext.Current.Session["user_id"] != null)
            {
                Session_id = Convert.ToInt32(HttpContext.Current.Session["user_id"].ToString());

            }
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == Session_id );

            if (emp != null)
            {
                emp.user_full_name = LoginDataObject.FullName;
                emp.user_title = LoginDataObject.Title;
                emp.user_department = LoginDataObject.Department;
                if (LoginDataObject.ManagerId == "None")
                    emp.manager_id = null;
                else
                    emp.manager_id = Convert.ToInt32(LoginDataObject.ManagerId);
                emp.date_of_joining = LoginDataObject.Date_Of_Joining;
                entities.SaveChanges();
                return true;
            }
            else
                return false;
        }



        public Boolean checkfirsttimeuser(int user_id)
        {
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == user_id);
            if (emp.password.Equals("12345"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        }
    }