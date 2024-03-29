﻿using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class User
    {
        public User()
        {
            Announcements = new HashSet<Announcement>();
            Appointments = new HashSet<Appointment>();
            Cases = new HashSet<Case>();
            Clients = new HashSet<Client>();
            Documents = new HashSet<Document>();
            Events = new HashSet<Event>();
            Notifications = new HashSet<Notification>();
            TaskParalegalUsers = new HashSet<Task>();
            TaskPartnerUsers = new HashSet<Task>();
        }

        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[]? ProfilePhoto { get; set; }
        public string Password { get; set; } = null!;

        public virtual UserRole UserRole { get; set; } = null!;
        public virtual Paralegal? Paralegal { get; set; }
        public virtual Partner? Partner { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Task> TaskParalegalUsers { get; set; }
        public virtual ICollection<Task> TaskPartnerUsers { get; set; }
    }
}
