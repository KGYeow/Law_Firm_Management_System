using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Clients = new HashSet<Client>();
            Paralegals = new HashSet<Paralegal>();
            TaskAssignments = new HashSet<TaskAssignment>();
            UserCaseInvolvements = new HashSet<UserCaseInvolvement>();
            UserNotifications = new HashSet<UserNotification>();
        }

        public int Id { get; set; }
        public int? UserRoleId { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }

        public virtual UserRole? UserRole { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Paralegal> Paralegals { get; set; }
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
        public virtual ICollection<UserCaseInvolvement> UserCaseInvolvements { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
