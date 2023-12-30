using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        public NotificationController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        [HttpGet]
        [Route("NotificationLog")]
        public IActionResult GetNotificationLog()
        {
            var user = userService.GetUser(User);
            var l = context.UserNotifications.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("UserNotifications")]
        public IActionResult GetUserNotifications()
        {
            var user = userService.GetUser(User);
            var l = context.UserNotifications.Where(a => a.UserId == user.Id && a.IsRead == false).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }
    }
}