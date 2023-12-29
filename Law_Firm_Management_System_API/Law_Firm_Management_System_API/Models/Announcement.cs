using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public int PartnerUserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual User PartnerUser { get; set; } = null!;
    }
}
