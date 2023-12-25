using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class AppointmentCategory
    {
        public AppointmentCategory()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
