using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PartnerUserId { get; set; }
        public string? Category { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public string? Status { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual User PartnerUser { get; set; } = null!;
    }
}
