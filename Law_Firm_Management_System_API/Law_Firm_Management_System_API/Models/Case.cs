using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Case
    {
        public Case()
        {
            Documents = new HashSet<Document>();
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public int? PartnerUserId { get; set; }
        public int? ClientId { get; set; }
        //public int? StatusId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? ClosedTime { get; set; }
        public int? StatusID { get; set; }

        public virtual Client? Client { get; set; }
        public virtual User? User { get; set; }
        public virtual CaseStatus? CaseStatus { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        }
}
