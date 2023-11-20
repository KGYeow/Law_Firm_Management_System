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
        [Route("NotificationLog/{Id}")]
        public IActionResult GetNotificationLog(int id)
        {
            var l = context.UserNotifications.Where(a => a.UserId == id).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("UserNotifications/{Id}")]
        public IActionResult GetUserNotifications(int id)
        {
            var l = context.UserNotifications.Where(a => a.UserId == id && a.IsRead == false).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }
    }
}