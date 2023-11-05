using System;
using System.Collections.Generic;

namespace Law_Firm_Management_System_API.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; } = null!;
    }
}
