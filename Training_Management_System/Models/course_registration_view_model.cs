using System;
using System.Collections.Generic;

namespace Training_Management_System.Models
{
    public class course_registration_view_model
    {

        public int course_id { get; set; }
        public string course_name { get; set; }
        public string course_type { get; set; }
        public string course_category { get; set; }
        public string course_description { get; set; }
        public Nullable<int> course_duration { get; set; }
        public int class_id { get; set; }
        public Nullable<int> class_instructor_id { get; set; }
        public string class_name { get; set; }
        public Nullable<System.DateTime> class_start_date { get; set; }
        public Nullable<System.DateTime> class_end_date { get; set; }
        public Nullable<int> max_seats { get; set; }
        public Nullable<int> available_seats { get; set; }
        public string is_registration_active { get; set; }

        public IEnumerable<course_table> course_table1 { get; set; }
        public IEnumerable<class_table> class_table1 { get; set; }


        public class course_table
        {
            public int course_id { get; set; }
            public string course_name { get; set; }
            public string course_type { get; set; }
            public string course_category { get; set; }
            public string course_description { get; set; }
            public Nullable<int> course_duration { get; set; }
        }
        public class class_table
        {
            public int class_id { get; set; }
            public Nullable<int> coursr_id { get; set; }
            public Nullable<int> class_instructor_id { get; set; }
            public string class_name { get; set; }
            public Nullable<System.DateTime> class_start_date { get; set; }
            public Nullable<System.DateTime> class_end_date { get; set; }
            public Nullable<int> max_seats { get; set; }
            public Nullable<int> available_seats { get; set; }
            public string is_registration_active { get; set; }
        }




    }





}