﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class training_management_systemEntities : DbContext
    {
        public training_management_systemEntities()
            : base("name=training_management_systemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<calender_events> calender_events { get; set; }
        public virtual DbSet<class_management> class_management { get; set; }
        public virtual DbSet<company_employee> company_employee { get; set; }
        public virtual DbSet<course_management> course_management { get; set; }
        public virtual DbSet<course_registration> course_registration { get; set; }
        public virtual DbSet<instructor> instructors { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<notification_seen> notification_seen { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
