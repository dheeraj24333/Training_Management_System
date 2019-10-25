using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training_Management_System.Classes
{
    public class CourseRegistration
    {
        public int courseregistration(int id)
        {

             if (HttpContext.Current.Session["user_id"] != null)
            {
                int usrId = 0;
                int.TryParse(HttpContext.Current.Session["user_id"].ToString(), out usrId);
                training_management_systemEntities entities = new training_management_systemEntities();

                course_registration CourseRegistrationCheck = entities.course_registration.SingleOrDefault(e => e.class_id == id & e.user_id == usrId);

                if (CourseRegistrationCheck != null) {
                    return 0;
    }
                course_registration course_Registration = new course_registration();
                class_management class_Management = entities.class_management.SingleOrDefault(e => e.class_id == id);
                if (class_Management != null) {
                    class_Management.available_seats = class_Management.available_seats - 1;
                    course_Registration.user_id = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                    course_Registration.class_id = id;
                    //  course_Registration.manager_id = 1;
                    course_Registration.status = "Registered";
                    entities.course_registration.Add(course_Registration);
                    entities.SaveChanges();
                }
                return 1;
            }
            else
            {

                return -1;
            }


        }

    }
}