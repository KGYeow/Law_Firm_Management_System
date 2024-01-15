using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Document
    {
        public Document()
        {
            Tasks = new HashSet<Task>();
            Cases = new HashSet<Case>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public int? CaseId { get; set; }
        public int PartnerUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] Attachment { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool IsArchived { get; set; }

        public virtual Case? Case { get; set; }
        public virtual DocumentCategory Category { get; set; } = null!;
        public virtual User PartnerUser { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
