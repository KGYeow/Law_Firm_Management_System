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
        public string Name { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public DateTime EventTime { get; set; }
        public bool IsCompleted { get; set; }

        public virtual Case? Case { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
