using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Partner
    {
        public Partner()
        {
            Cases = new HashSet<Case>();
        }

        public int UserId { get; set; }
        public int? ParalegalUserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual Paralegal? ParalegalUser { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Case> Cases { get; set; }
    }
}
