using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        public DashboardController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        [HttpGet]
        [Route("Announcement")]
        public IActionResult GetAnnouncement()
        {
            var l = context.Announcements.ToList();
            return Ok(l);
        }
    }
}