using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Task
    {
        public Task()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? AssignedTime { get; set; }
        public DateTime? CompletedTime { get; set; }

        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
