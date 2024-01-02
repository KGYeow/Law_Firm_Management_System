using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class CaseStatus
    {
        public CaseStatus()
        {
            Cases = new HashSet<Case>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; } = null!;
        public string StatusDescription { get; set; } = null!;

        public virtual ICollection<Case> Cases { get; set; }
    }
}
