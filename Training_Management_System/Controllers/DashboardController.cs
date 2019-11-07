using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Training_Management_System.Classes;
using Training_Management_System.Models;

namespace Training_Management_System.Controllers
{
    public class DashboardController : Controller
    {

        public ActionResult Dashboard()
        {
            int user_id = 0;
            user_id = Convert.ToInt32(Session["user_id"].ToString());
            training_management_systemEntities entities = new training_management_systemEntities();
            
            var course_Registrations = entities.course_registration.ToList().Where(x => x.user_id == user_id);
            int[] classidarray = new int[course_Registrations.Count()];

            int i = 0;
            foreach (var item in course_Registrations) {
                classidarray[i] = item.class_id.Value;
                i++;
            }
            IEnumerable<CourseRegisterModel> courseregistermodel = entities.course_management
                .Join(entities.class_management, u => u.course_id, uir => uir.course_id, (u, uir) => new { u, uir })
                .Select(m => new CourseRegisterModel
                {
                    course_id = m.u.course_id,
                    course_name = m.u.course_name,
                    course_type = m.u.course_type,
                    course_category = m.u.course_category,
                    course_duration = m.u.course_duration,
                    class_id = m.uir.class_id,
                    class_name = m.uir.class_name,
                    class_start_date = m.uir.class_start_date.ToString(),
                    class_end_date = m.uir.class_end_date.ToString(),
                    available_seats = m.uir.available_seats,
                    is_registration_active = m.uir.is_registration_active
                })
                .Where(x => x.is_registration_active == "YES" & !classidarray.Contains(x.class_id)).Take(5);
            //  return View("GetListOfCourses", courseregistermodel);

            dynamic mymodal = new ExpandoObject();




            var notification_seen_id = entities.notification_seen.ToList().Where(x => x.user_id == user_id);
            int[] notificationidarray = new int[notification_seen_id.Count()];

            int j = 0;
            foreach (var item in notification_seen_id)
            {
                notificationidarray[j] = item.not_id.Value;
                j++;
            }

            IEnumerable<NotificationModal> notificationModal = entities.notifications.Select(n => new NotificationModal
            {
                not_id = n.not_id,
                notice = n.notice,
                tilldate = n.tilldate                
            })
            .Where(x => !notificationidarray.Contains(x.not_id));


            mymodal.CourseRegisterModel = courseregistermodel;
            mymodal.NotificationModal = notificationModal;


            return View(mymodal);
        }
        [HttpGet]
        public ActionResult isseen(int not_id)
        {
            training_management_systemEntities entities = new training_management_systemEntities();
            notification_seen notification_Seen = new notification_seen();
            int user_id = Convert.ToInt32(Session["user_id"].ToString());
            notification_Seen.not_id = not_id;
            notification_Seen.user_id = user_id;
            entities.notification_seen.Add(notification_Seen);
            entities.SaveChanges();
            return RedirectToAction("Dashboard", "Dashboard");
        }
        [HttpGet]
        public ActionResult ViewProfile()
        {
            UserProfileDataRetrival UserProfileDataRetrivalobject = new UserProfileDataRetrival();
            UserProfileModel userprofilemodelobject = new UserProfileModel();
            userprofilemodelobject = UserProfileDataRetrivalobject.UserDataRetrivalFunction();
            return PartialView(userprofilemodelobject);
        }

        [HttpGet]
        public string GetEvents()
        {
            training_management_systemEntities entities = new training_management_systemEntities();
            int user_id = 0;
            user_id = Convert.ToInt32(Session["user_id"].ToString());
            var events = entities.calender_events
                .Join(entities.class_management, u => u.class_id, uir => uir.class_id, (u, uir) => new { u, uir })
                .Join(entities.course_management, m => m.uir.course_id, k => k.course_id, (m, k) => new { m, k })
                .ToList().Select(x => new
                {
                    user_id = x.m.u.user_id,
                    classname = x.m.uir.class_name,
                    coursename = x.k.course_name,
                    courseduration = x.k.course_duration,
                    class_start_date = x.m.u.class_start_date + x.m.uir.start_time,
                    class_end_date = x.m.u.class_end_date + x.m.uir.end_time,
                    maxseats = x.m.uir.max_seats,
                    availableseats = x.m.uir.available_seats,
                    themecolor = x.m.u.themecolor,
                    isfullday = x.m.u.isfullday,
                }).Where(e => e.user_id == user_id);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var serializedResult = serializer.Serialize(events);


            return serializedResult;
        }
        
    }
}