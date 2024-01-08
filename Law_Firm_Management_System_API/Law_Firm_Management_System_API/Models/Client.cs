using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Client
    {
        public Client()
        {
            Appointments = new HashSet<Appointment>();
            Cases = new HashSet<Case>();
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
