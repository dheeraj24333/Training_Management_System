using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Training_Management_System.Models
{
    public class UserProfileModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Select Manager")]
        public int ManagerId { get; set; }
        public Nullable<int> user_id { get; set; }
        public string manager_name { get; set; }

        
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [DisplayName("Date of joining")]
        [DataType(DataType.Date)]
        public DateTime Date_Of_Joining { get; set; }

    }
}