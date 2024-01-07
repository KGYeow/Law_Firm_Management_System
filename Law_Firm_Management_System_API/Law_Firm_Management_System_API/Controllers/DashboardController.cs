using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Get the list of announcements.
        [HttpGet]
        [Route("Announcement")]
        public IActionResult GetAnnouncement()
        {
            var l = context.Announcements.Include(a => a.PartnerUser).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, partnerUserId = x.PartnerUserId, partnerFullName = x.PartnerUser.FullName, title = x.Title, description = x.Description, createdTime = x.CreatedTime });
            return Ok(l);
        }

        // Post an announcement.
        [HttpPost]
        [Route("Announcement/Post")]
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

            return Ok(new Response { Status = "Success", Message = "New announcement created successfully" });
        }

        // Delete the announcement.
        [HttpDelete]
        [Route("Announcement/Delete")]
        public IActionResult DeleteAnnouncement(int announcementId)
        {
            var user = userService.GetUser(User);
            var announcement = context.Announcements.Where(a => a.Id == announcementId).FirstOrDefault();
            context.Announcements.Remove(announcement);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Announcement deleted successfully" });
        }

        // Get the dashboard data from partner's perspective.
        [HttpGet]
        [Route("Employee/DashboardData")]
        public IActionResult GetPartnerDashboardData()
        {
            var user = userService.GetUser(User);

            var numAppointments = context.Appointments.Where(a => a.PartnerUserId == user.Id)
                .ToList().Count;
            var numPendingAppointments = context.Appointments.Where(a => a.PartnerUserId == user.Id && a.Status == "Pending")
                .ToList().Count;

            var numCases = context.Cases.Where(a => a.PartnerUserId == user.Id)
                .ToList().Count;
            var numCompletedCases = context.Cases.Where(a => a.PartnerUserId == user.Id && a.ClosedTime != null)
                .ToList().Count;

            var numEvent = context.Events.Include(a => a.Case).Where(a => a.Case.PartnerUserId == user.Id)
                .ToList().Count;
            var numCompletedEvent = context.Events.Include(a => a.Case).Where(a => a.Case.PartnerUserId == user.Id && a.IsCompleted == true)
                .ToList().Count;

            var numCompletedTasks = context.Tasks.Where(a => a.Id == user.Id && a.CompletedTime != null)
                .ToList().Count;

            return Ok(new { numAppointments, numPendingAppointments, numCases, numCompletedCases, numEvent, numCompletedEvent, numCompletedTasks });
        }

        // Get the dashboard data from client's perspective.
        [HttpGet]
        [Route("Client/DashboardData")]
        public IActionResult GetClientDashboardData()
        {
            var user = userService.GetUser(User);

            var numAppointments = context.Appointments.Where(a => a.ClientId == user.Id)
                .ToList().Count;
            var numPendingAppointments = context.Appointments.Where(a => a.ClientId == user.Id && a.Status == "Pending")
                .ToList().Count;

            var numCases = context.Cases.Where(a => a.ClientId == user.Id)
                .ToList().Count;
            var numCompletedCases = context.Cases.Where(a => a.ClientId == user.Id && a.ClosedTime != null)
                .ToList().Count;

            var numEvent = context.Events.Include(a => a.Case).Where(a => a.Case.ClientId == user.Id)
                .ToList().Count;
            var numCompletedEvent = context.Events.Include(a => a.Case).Where(a => a.Case.ClientId == user.Id && a.IsCompleted == true)
                .ToList().Count;

            return Ok(new { numAppointments, numPendingAppointments, numCases, numCompletedCases, numEvent, numCompletedEvent });
        }

        public class AnnouncementDto
        {
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
    }
}