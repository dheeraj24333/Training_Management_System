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
    
    public partial class notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public notification()
        {
            this.notification_seen = new HashSet<notification_seen>();
        }
    
        public int not_id { get; set; }
        public string notice { get; set; }
        public Nullable<System.DateTime> tilldate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification_seen> notification_seen { get; set; }
    }
}
