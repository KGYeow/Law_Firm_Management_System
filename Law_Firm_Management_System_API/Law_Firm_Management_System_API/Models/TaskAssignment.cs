using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class TaskAssignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }

        public virtual Task Task { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
