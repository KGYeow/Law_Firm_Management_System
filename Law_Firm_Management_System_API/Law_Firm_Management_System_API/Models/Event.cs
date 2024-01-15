using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Event
    {
        public Event()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public int? CaseId { get; set; }
        public int? PartnerUserId { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public DateTime EventTime { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }

        public virtual Case? Case { get; set; }
        public virtual Client? Client { get; set; }
        public virtual User? PartnerUser { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
