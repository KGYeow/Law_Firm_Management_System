using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public int? ParelegalUserId { get; set; }
        public int PartnerUserId { get; set; }
        public int? CaseId { get; set; }
        public int? EventId { get; set; }
        public int? DocumentId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime AssignedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public DateTime DueTime { get; set; }
        public bool InProgress { get; set; }

        public virtual Case? Case { get; set; }
        public virtual Document? Document { get; set; }
        public virtual Event? Event { get; set; }
        public virtual User? ParelegalUser { get; set; }
        public virtual User PartnerUser { get; set; } = null!;
    }
}
