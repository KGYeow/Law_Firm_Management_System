using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; }
    }
}
