using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Category { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? Status { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
