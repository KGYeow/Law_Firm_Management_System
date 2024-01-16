using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Document
    {
        public Document()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public int? CaseId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string Type { get; set; } = null!;
        public bool IsArchived { get; set; }
        public byte[] Attachment { get; set; } = null!;

        public virtual Case? Case { get; set; }
        public virtual DocumentCategory Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
