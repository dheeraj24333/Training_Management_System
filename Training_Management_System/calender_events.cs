//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Training_Management_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class calender_events
    {
        public int event_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> class_id { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> class_start_date { get; set; }
        public Nullable<System.DateTime> class_end_date { get; set; }
        public string themecolor { get; set; }
        public Nullable<bool> isfullday { get; set; }
    
        public virtual company_employee company_employee { get; set; }
    }
}
