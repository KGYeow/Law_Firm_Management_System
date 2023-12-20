using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Page
    {
        public Page()
        {
            RoleAccessPages = new HashSet<RoleAccessPage>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AccessName { get; set; }

        public virtual ICollection<RoleAccessPage> RoleAccessPages { get; set; }
    }
}
