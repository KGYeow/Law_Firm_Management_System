using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class UserCaseInvolvement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseId { get; set; }

        public virtual Case Case { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
