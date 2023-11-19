using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Case
    {
        public Case()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
