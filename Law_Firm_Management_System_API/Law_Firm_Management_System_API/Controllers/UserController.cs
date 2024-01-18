using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Dto;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of user accounts.
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var l = context.Users.Include(a => a.UserRole).ToList()
                .Select(x => new { id = x.Id, username = x.Username, fullName = x.FullName, email = x.Email, role = x.UserRole?.Name });
            return Ok(l);
        }

        // Get the self user information.
        [HttpGet]
        [Route("Me")]
        public IActionResult Me()
        {
            var user = userService.GetUser(User);
            return Ok(user);
        }

        // Update the current logged-in user account information.
        [HttpPut]
        [Route("Me")]
        public IActionResult MeUpdate([FromBody] UserMeEditDto dto)
        {
            var user = userService.GetUser(User);
            user.Username = dto.Username;
            user.FullName = dto.FullName;
            user.Email = dto.Email;

            context.Users.Update(user);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your user profile has been edited successfully" });
        }

        // Change the password of the current logged-in user.
        [HttpPut]
        [Route("Me/Password/{NewPassword}")]
        public IActionResult MeUpdatePassword(string newPassword)
        {
            var user = userService.GetUser(User);

            if (!string.IsNullOrEmpty(newPassword))
            {
                var encryptedPassword = AppStatic.Encrypt(newPassword);

                if (user.Password != encryptedPassword)
                {
                    user.Password = encryptedPassword;
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                else
                    throw new Exception("The new password is the same as the old password");
            }

            return Ok(new Response { Status = "Success", Message = "Your password updated successfully" });
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

        // Get the current logged-in user role information.
        [HttpGet]
        [Route("Me/Info/Role")]
        public IActionResult GetMeRoleInfo()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();

            if (role.Name == "Partner")
            {
                var roleInfo = context.Partners.Include(a => a.User).Include(a => a.ParalegalUser).Where(a => a.UserId == user.Id)
                    .Select(x => new { assignedParalegal = x.ParalegalUser.User.FullName, phoneNum = x.PhoneNumber, address = x.Address })
                    .FirstOrDefault();

                return Ok(roleInfo);
            }
            else if (role.Name == "Paralegal")
            {
                var assignedPartner = context.Partners.Include(a => a.User).Where(a => a.ParalegalUserId == user.Id)
                    .Select(x => new { fullName = x.User.FullName }).FirstOrDefault();

                var roleInfo = context.Paralegals.Include(a => a.User).Where(a => a.UserId == user.Id)
                    .Select(x => new { assignedPartner = assignedPartner.fullName, phoneNum = x.PhoneNumber, address = x.Address })
                    .FirstOrDefault();

                return Ok(roleInfo);
            }
            else
            {
                var roleInfo = context.Clients.Include(a => a.User).Where(a => a.UserId == user.Id)
                    .Select(x => new { clientId = x.Id, fullName = x.FullName, phoneNum = x.PhoneNumber, email = x.Email, address = x.Address })
                    .FirstOrDefault();
                return Ok(roleInfo);
            }
        }

        // Update the current logged-in user partner information.
        [HttpPut]
        [Route("Me/Info/Role/Partner")]
        public IActionResult MeUpdatePartnerInfo([FromBody] UserMePartnerParalegalEditDto dto)
        {
            var user = userService.GetUser(User);
            var roleInfo = context.Partners.Where(a => a.UserId == user.Id).FirstOrDefault();

            roleInfo.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNum) ? null : dto.PhoneNum;
            roleInfo.Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address;
            context.Partners.Update(roleInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your partner information has been edited successfully" });
        }

        // Update the current logged-in user paralegal information.
        [HttpPut]
        [Route("Me/Info/Role/Paralegal")]
        public IActionResult MeUpdateParalegalInfo([FromBody] UserMePartnerParalegalEditDto dto)
        {
            var user = userService.GetUser(User);
            var roleInfo = context.Paralegals.Where(a => a.UserId == user.Id).FirstOrDefault();

            roleInfo.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNum) ? null : dto.PhoneNum;
            roleInfo.Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address;
            context.Paralegals.Update(roleInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your paralegal information has been edited successfully" });
        }

        // Update the current logged-in user client information.
        [HttpPut]
        [Route("Me/Info/Role/Client")]
        public IActionResult MeUpdateClientInfo([FromBody] UserMeClientEditDto dto)
        {
            var user = userService.GetUser(User);
            var roleInfo = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            roleInfo.FullName = dto.FullName;
            roleInfo.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNum) ? null : dto.PhoneNum;
            roleInfo.Email = string.IsNullOrEmpty(dto.Email) ? null : dto.Email;
            roleInfo.Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address;
            context.Clients.Update(roleInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your client information has been edited successfully" });
        }

        public class UserMeEditDto
        {
            public string Username { get; set; } = null!;
            public string FullName { get; set; } = null!;
            public string Email { get; set; } = null!;
        }
        
        public class UserMePartnerParalegalEditDto
        {
            public string? PhoneNum { get; set; }
            public string? Address { get; set; }
        }

        public class UserMeClientEditDto
        {
            public string FullName { get; set; } = null!;
            public string? PhoneNum { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
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