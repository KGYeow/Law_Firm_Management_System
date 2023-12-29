﻿using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int? CaseId { get; set; }
        public string Name { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte[] Attachment { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool IsArchived { get; set; }

        public virtual Case? Case { get; set; }
        public virtual DocumentCategory Category { get; set; } = null!;
    }
}
