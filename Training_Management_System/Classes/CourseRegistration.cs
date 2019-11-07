using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Net.Mail;

namespace Training_Management_System.Classes
{
    public class CourseRegistration
    {
        public int courseregistration(int classid)
        {
            if (HttpContext.Current.Session["user_id"] != null)
            {
                int usrId = 0;
                int.TryParse(HttpContext.Current.Session["user_id"].ToString(), out usrId);
                training_management_systemEntities entities = new training_management_systemEntities();
                course_registration CourseRegistrationCheck = entities.course_registration.SingleOrDefault(e => e.class_id == classid & e.user_id == usrId);

                if (CourseRegistrationCheck != null)
                {
                    return 0;
                }
                course_registration course_Registration = new course_registration();
                class_management class_Management = entities.class_management.SingleOrDefault(e => e.class_id == classid);

                if (class_Management.available_seats == 0)
                {
                    return -2;
                }

                if (class_Management != null)
                {
                    class_Management.available_seats = class_Management.available_seats - 1;
                    course_Registration.user_id = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                    course_Registration.class_id = classid;
                    course_Registration.status = "Registered";
                    entities.calender_events = calender_Events(classid, usrId, entities.calender_events);
                    entities.course_registration.Add(course_Registration);
                    entities.SaveChanges();
                    sentemail(classid, usrId);
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }



        public DbSet<calender_events> calender_Events(int id, int usrId, DbSet<calender_events> set)
        {


            calender_events calender_Events_Object;

            training_management_systemEntities entities = new training_management_systemEntities();
            class_management classmanagementobject = entities.class_management.SingleOrDefault(e => e.class_id == id);

            if (classmanagementobject != null)
            {
                var startdate = classmanagementobject.class_start_date;
                var enddate = classmanagementobject.class_end_date;
                while (!startdate.Equals(enddate))
                {
                    calender_Events_Object = new calender_events();
                    calender_Events_Object.user_id = usrId;
                    calender_Events_Object.class_id = id;
                    calender_Events_Object.subject = classmanagementobject.class_name;
                    calender_Events_Object.description = classmanagementobject.max_seats.ToString();
                    calender_Events_Object.class_start_date = startdate;
                    calender_Events_Object.class_end_date = startdate;
                    calender_Events_Object.themecolor = "blue";
                    calender_Events_Object.isfullday = false;
                    DateTime startdate1 = Convert.ToDateTime(startdate);
                    startdate = startdate1.AddDays(1);
                    set.Add(calender_Events_Object);
                }
            }
            return set;
        }


        public void sentemail(int classid, int usrId)
        {
            try
            {
                training_management_systemEntities entities = new training_management_systemEntities();

                var data = entities.company_employee
                    .Join(entities.course_registration, u => u.user_id, v => v.user_id, (u, v) => new { u, v })
                    .Join(entities.class_management, w => w.v.class_id, x => x.class_id, (w, x) => new { w, x })
                    .Join(entities.course_management, y => y.x.course_id, z => z.course_id, (y, z) => new { y, z })
                    .Select(x => new
                    {
                        user_id = x.y.w.u.user_id,
                        title = x.y.w.u.user_title,
                        department = x.y.w.u.user_department,
                        class_id = x.y.x.class_id,
                        username = x.y.w.u.username,
                        userfullname = x.y.w.u.user_full_name,
                        coursename = x.z.course_name,
                        classname = x.y.x.class_name,
                        courseduration = x.z.course_duration,
                        class_start_date = x.y.x.class_start_date.ToString() ,
                        start_time = (TimeSpan) x.y.x.start_time,
                        class_end_date = x.y.x.class_end_date.ToString() ,
                        end_time = (TimeSpan) x.y.x.end_time,
                        maxseats = x.y.x.max_seats,
                        availableseats = x.y.x.available_seats,
                    }).Where(x => x.user_id == usrId & x.class_id == classid)
                    .FirstOrDefault();
               
                MailMessage usermail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.outlook.com");
                usermail.To.Add(data.username);
                usermail.Subject = "New Registration";
                usermail.IsBodyHtml = true;
                string usermailbody , adminmailbody;
                usermailbody = "<p>Hi "+data.title+" " + data.userfullname + ",</p><p>You Have registered for " + data.coursename + " course successfully...</p>" +
                    "<p>All details are given below : </p>" +
                    "<p>Class Name : " + data.classname + "</P>" +
                    "<p>Class starts on : "+data.class_start_date +"</p>" +
                    "<p>Class ends on : "+data.class_end_date +"</p>" +
                    "<p>Daily class timing will be "+data.start_time+" to "+data.end_time+"</p>" +
                    "<p>Total course duration is  : "+data.courseduration + "</p>" +
                    "<p>Thanks you...! </p>";
                usermail.Body = usermailbody;
                
                MailMessage adminmail = new MailMessage();
                adminmail.To.Add("kiran.bhore@communitybrands.com");
                adminmail.Subject = "New Registration";
                adminmail.IsBodyHtml = true;
                adminmailbody = "<p>Hi Team,</p><p>"+data.title+" "+data.userfullname+" has registered for "+data.coursename+ " course successfully</p>" +
                   "<p>All other details are given below : </p>" +
                   "<p>Employee's department : " + data.department+ "</P>" +
                   "<p>Employee's username : " + data.username + "</P>" +
                    "<p>Registered for class : " + data.classname + "</P>" +
                    "<p>Batch starts on : " + data.class_start_date + "</p>" +
                    "<p>Class ends on : " + data.class_end_date + "</p>" +
                    "<p>Daily class timing will be " + data.start_time + " to " + data.end_time + "</p>" +
                    "<p>Now available seats are   : "+data.availableseats+"</p>" +
                    "<p>Thanks you...! </p>";
                adminmail.Body = adminmailbody;

                SmtpServer.Send(usermail);
                SmtpServer.Send(adminmail);
            }
            catch (Exception ex)
            {
               
            }


        }
    }
}