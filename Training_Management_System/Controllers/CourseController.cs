using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_Management_System.Classes;
using Training_Management_System.Models;

namespace Training_Management_System.Controllers
{
    public class CourseController : Controller
    {
        public ActionResult GetListOfCourses()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView", "Login");
            }
            else
            {

                training_management_systemEntities entities = new training_management_systemEntities();
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
                    .Where(x => x.is_registration_active == "YES");
                return View("GetListOfCourses", courseregistermodel);
            }
        }
        
        public ActionResult Register(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView", "Login");
            }
            else
            {
                CourseRegistration obj = new CourseRegistration();
                int result = obj.courseregistration(id);

                training_management_systemEntities entities = new training_management_systemEntities();
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
                    .Where(x => x.is_registration_active == "YES");

                if (result == 0)
                {
                    ViewBag.Message = "You have  already registered for this course...";
                    return View("GetListOfCourses", courseregistermodel);
                }
                else if (result == 1)
                {
                    ViewBag.Message = "You have  registered for this course successfully...";
                    return View("GetListOfCourses", courseregistermodel);
                }
                else if (result == -2)
                {
                    ViewBag.Message = "Sorry no more seats are available for this course...";
                    return View("GetListOfCourses", courseregistermodel);
                }
                else
                {
                    return RedirectToAction("LoginPageView", "Login");
                }
            }
        }
        

        public ActionResult MyCourses() {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("LoginPageView", "Login");
            }
            else
            {
                int usrId = 0;
                int.TryParse(Session["user_id"].ToString(), out usrId);
                training_management_systemEntities entities = new training_management_systemEntities();
                CourseRegisterModel crvm;
                var model = from course_management in entities.course_management
                            join class_management in entities.class_management
                                on course_management.course_id equals class_management.course_id
                            join course_registration in entities.course_registration
                                on class_management.class_id equals course_registration.class_id
                            where usrId == course_registration.user_id
                            select new
                            {
                                course_id = course_management.course_id,
                                course_name = course_management.course_name,
                                course_type = course_management.course_type,
                                course_category = course_management.course_category,
                                course_duration = course_management.course_duration,
                                class_id = class_management.class_id,
                                class_name = class_management.class_name,
                                class_start_date = class_management.class_start_date.ToString(),
                                class_end_date = class_management.class_end_date.ToString(),
                                available_seats = class_management.available_seats,
                                is_registration_active = class_management.is_registration_active
                            };

                List<CourseRegisterModel> lst = new List<CourseRegisterModel>();

                foreach (var item in model)
                {
                    crvm = new CourseRegisterModel();
                    crvm.course_id = item.course_id;
                    crvm.course_name = item.course_name;
                    crvm.course_type = item.course_type;
                    crvm.course_category = item.course_category;
                    crvm.course_duration = item.course_duration;
                    crvm.class_id = item.class_id;
                    crvm.class_name = item.class_name;
                    crvm.class_start_date = item.class_start_date;
                    crvm.class_end_date = item.class_end_date;
                    crvm.available_seats = item.available_seats;
                    crvm.is_registration_active = item.is_registration_active;
                    lst.Add(crvm);
                }
                return View(lst);
            }
        }
    }
}