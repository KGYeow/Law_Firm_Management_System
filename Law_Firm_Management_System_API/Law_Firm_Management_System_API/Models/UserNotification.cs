using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? NotificationId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }

        public virtual Notification? Notification { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
