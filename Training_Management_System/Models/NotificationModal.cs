using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training_Management_System.Models
{
    public class NotificationModal
    {
        public int not_id { get; set; }
        public string notice { get; set; }
        public Nullable<System.DateTime> tilldate { get; set; }

    }
}