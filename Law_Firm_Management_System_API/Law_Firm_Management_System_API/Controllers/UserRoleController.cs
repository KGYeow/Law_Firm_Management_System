using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : BaseController
    {
        public UserRoleController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of user role.
        [HttpGet]
        [Route("")]
        public IActionResult GetUserRoleList()
        {
            var l = context.UserRoles.ToList();
            return Ok(l);
        }

        // Get the user's role.
        [HttpGet]
        [Route("RoleName")]
        public IActionResult GetUserRole()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();
            return Ok(role.Name);
        }
    }
}