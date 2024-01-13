using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Dto;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Law_Firm_Management_System_API.Controllers.ClientController;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var l = context.Users.Include(a => a.UserRole).ToList()
                .Select(x => new { id = x.Id, username = x.Username, fullName = x.FullName, email = x.Email, role = x.UserRole?.Name });
            return Ok(l);
        }

        // Update the current logged-in user account information.
        [HttpPut]
        [Route("Me/{Id}")]
        public IActionResult MeUpdate(int id, [FromBody] UserEditDto dto)
        {
            var userCurrent = context.Users.Where(a => a.Id == id).FirstOrDefault();
            userCurrent.Username = dto.Username;
            userCurrent.FullName = dto.FullName;
            userCurrent.Email = dto.Email;

            context.Users.Update(userCurrent);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "The user profile has been edited successfully!" });
        }

        // Change the password of the current logged-in user.
        [HttpPut]
        [Route("MePassword/{Id}")]
        public IActionResult MeUpdatePassword(int id, [FromBody] string password)
        {
            var userCurrent = context.Users.Where(a => a.Id == id).FirstOrDefault();

            if (password != null && password != "")
                userCurrent.Password = AppStatic.Encrypt(password);

            context.Users.Update(userCurrent);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "User update password successfully" });
        }

        // Get the specific user's account information.
        [HttpGet]
        [Route("Info/{UserId}")]
        public IActionResult GetUserInfo(int userId)
        {
            var clientId = 0;
            var client = context.Clients.Where(a => a.UserId == userId).FirstOrDefault();
            if (client != null)
                clientId = client.Id;

            var userInfo = context.Users.Include(a => a.UserRole).Where(a => a.Id == userId)
                .Select(x => new { id = x.Id, roleId = x.UserRoleId, role = x.UserRole.Name, clientId, fullName = x.FullName, username = x.Username, email = x.Email, profilePhoto = x.ProfilePhoto })
                .FirstOrDefault();
            return Ok(userInfo);
        }

        // Update the specific user's account information.
        [HttpPut]
        [Route("Info/{UserId}/Edit")]
        public IActionResult UserInfoUpdate(int userId, [FromBody] UserEditDto dto)
        {
            var role = context.UserRoles.Where(a => a.Name == dto.Role).FirstOrDefault();
            var userInfo = context.Users.Where(a => a.Id == userId).FirstOrDefault();

            userInfo.FullName = dto.FullName;
            userInfo.Username = dto.Username;
            userInfo.Email = dto.Email;
            userInfo.UserRoleId = role.Id;

            context.Users.Update(userInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "The user account information has been updated successfully" });
        }

        // Delete the user account.
        [HttpDelete]
        [Route("Delete/{UserId}")]
        public IActionResult UserDelete(int userId)
        {
            var userInfo = context.Users.Where(a => a.Id == userId).FirstOrDefault();
            context.Users.Remove(userInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "User account deleted successfully" });
        }

        public class UserEditDto
        {
            public string Username { get; set; } = null!;
            public string FullName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Role { get; set; } = null!;
        }
    }
}