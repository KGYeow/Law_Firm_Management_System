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

        // Get the notification history.
        [HttpGet]
        [Route("Log")]
        public IActionResult GetUserNotificationLog()
        {
            var user = userService.GetUser(User);
            var l = context.Notifications.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }

        // Get the list of a specific user's unread notifications.
        [HttpGet]
        [Route("UnreadList")]
        public IActionResult GetUnreadUserNotifications()
        {
            var user = userService.GetUser(User);
            var l = context.Notifications.Where(a => a.UserId == user.Id && a.IsRead == false).OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }

        // Read the user notification.
        [HttpPut]
        [Route("Read")]
        public IActionResult ReadNotification(int notificationId)
        {
            var userNotification = context.Notifications.Where(a => a.Id == notificationId).FirstOrDefault();
            userNotification.IsRead = true;
            context.Notifications.Update(userNotification);
            context.SaveChanges();

            return Ok();
        }
    }
}