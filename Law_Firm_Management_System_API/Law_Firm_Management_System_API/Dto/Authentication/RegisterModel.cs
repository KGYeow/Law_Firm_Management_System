using System.ComponentModel.DataAnnotations;

namespace Law_Firm_Management_System_API.Dto.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "User Role is required")]
        public string Role { get; set; } = null!;
    }
}