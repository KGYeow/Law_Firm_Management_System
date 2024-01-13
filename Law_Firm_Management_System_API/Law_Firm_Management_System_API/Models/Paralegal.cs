using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Paralegal
    {
        public Paralegal()
        {
            Partners = new HashSet<Partner>();
        }

        public int UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Partner> Partners { get; set; }
    }
}
