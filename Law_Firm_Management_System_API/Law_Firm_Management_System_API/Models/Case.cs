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
            UserCaseInvolvements = new HashSet<UserCaseInvolvement>();
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? ClosedTime { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<UserCaseInvolvement> UserCaseInvolvements { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual User PartnerUser { get; set; } = null!;
    }
}
