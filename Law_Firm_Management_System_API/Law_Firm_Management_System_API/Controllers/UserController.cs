using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Dto;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var l = context.Users.ToList();
            return Ok(l);
        }

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

        public class UserEditDto
        {
            public string? Username { get; set; }
            public string? FullName { get; set; }
            public string? Email { get; set; }
        }
    }
}