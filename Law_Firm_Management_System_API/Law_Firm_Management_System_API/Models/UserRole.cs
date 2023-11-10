using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            RoleAccessPages = new HashSet<RoleAccessPage>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<RoleAccessPage> RoleAccessPages { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
