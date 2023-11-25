using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Paralegal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
