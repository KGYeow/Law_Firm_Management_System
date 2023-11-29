using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Partner
    {
        public Partner()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ParalegalId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual Paralegal? Paralegal { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
