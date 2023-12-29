using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using static Law_Firm_Management_System_API.Controllers.AppointmentController;

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
            var l = context.Announcements.OrderByDescending(a => a.Id).ToList();
            return Ok(l);
        }

        [HttpPost]
        [Route("PostAnnouncement")]
        public IActionResult PostAnnouncement([FromBody] AnnouncementDto dto)
        {
            var user = userService.GetUser(User);
            var announcement = new Announcement
            {
                PartnerUserId = user.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreatedTime = DateTime.Now,
            };
            context.Announcements.Add(announcement);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New announcement created successfully!" });
        }

        public class AnnouncementDto
        {
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
    }
}