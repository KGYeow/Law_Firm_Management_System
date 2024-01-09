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
        [Route("Announcement/Delete/{AnnouncementId}")]
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
        [Route("Partner/DashboardData")]
        public IActionResult GetPartnerDashboardData()
        {
            var user = userService.GetUser(User);

            var appointmentList = context.Appointments.Where(a => a.PartnerUserId == user.Id);
            var numAppointmentsTotal = appointmentList.Count();
            var numAppointmentsPending = appointmentList.Where(a => a.Status == "Pending").Count();
            var numAppointmentsApproved = appointmentList.Where(a => a.Status == "Approved").Count();
            var numAppointmentsRejected = appointmentList.Where(a => a.Status == "Rejected").Count();
            var numAppointmentsCancelled = appointmentList.Where(a => a.Status == "Cancelled").Count();

            var caseList = context.Cases.Where(a => a.PartnerUserId == user.Id);
            var numCasesTotal = caseList.Count();
            var numCasesCompleted = caseList.Where(a => a.ClosedTime != null).Count();
            var numCasesIncompleted = numCasesTotal - numCasesCompleted;

            var eventList = context.Events.Where(a => a.PartnerUserId == user.Id);
            var numEventTotal = eventList.Count();
            var numEventCompleted = eventList.Where(a => a.IsCompleted == true).Count();
            var numEventIncompleted = numEventTotal - numEventCompleted;

            var taskList = context.Tasks.Where(a => a.PartnerUserId == user.Id);
            var numTasksToDo = taskList.Where(a => a.CompletedTime == null && a.InProgress == false).Count();
            var numTasksInProgress = taskList.Where(a => a.InProgress == true).Count();
            var numTasksCompleted = taskList.Where(a => a.CompletedTime != null).Count();

            return Ok(new
            {
                numAppointmentsTotal,
                numAppointmentsPending,
                numAppointmentsApproved,
                numAppointmentsRejected,
                numAppointmentsCancelled,
                numCasesTotal,
                numCasesCompleted,
                numCasesIncompleted,
                numEventTotal,
                numEventCompleted,
                numEventIncompleted,
                numTasksToDo,
                numTasksInProgress,
                numTasksCompleted
            });
        }

        // Get the dashboard data from partner's perspective.
        [HttpGet]
        [Route("Paralegal/DashboardData")]
        public IActionResult GetParalegalDashboardData()
        {
            var user = userService.GetUser(User);

            var taskList = context.Tasks.Where(a => a.ParelegalUserId == user.Id);
            var numTasksToDo = taskList.Where(a => a.CompletedTime == null && a.InProgress == false).Count();
            var numTasksInProgress = taskList.Where(a => a.InProgress == true).Count();
            var numTasksCompleted = taskList.Where(a => a.CompletedTime != null).Count();

            return Ok(new { numTasksToDo, numTasksInProgress, numTasksCompleted });
        }

        // Get the dashboard data from client's perspective.
        [HttpGet]
        [Route("Client/DashboardData")]
        public IActionResult GetClientDashboardData()
        {
            var user = userService.GetUser(User);
            var client = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            var appointmentList = context.Appointments.Where(a => a.ClientId == client.Id);
            var numAppointmentsTotal = appointmentList.Count();
            var numAppointmentsPending = appointmentList.Where(a => a.Status == "Pending").Count();
            var numAppointmentsApproved = appointmentList.Where(a => a.Status == "Approved").Count();
            var numAppointmentsRejected = appointmentList.Where(a => a.Status == "Rejected").Count();
            var numAppointmentsCancelled = appointmentList.Where(a => a.Status == "Cancelled").Count();

            var caseList = context.Cases.Where(a => a.ClientId == client.Id);
            var numCasesTotal = caseList.Count();
            var numCasesCompleted = caseList.Where(a => a.ClosedTime != null).Count();
            var numCasesIncompleted = numCasesTotal - numCasesCompleted;

            var eventList = context.Events.Where(a => a.ClientId == client.Id);
            var numEventTotal = eventList.Count();
            var numEventCompleted = eventList.Where(a => a.IsCompleted == true).Count();
            var numEventIncompleted = numEventTotal - numEventCompleted;

            return Ok(new
            {
                numAppointmentsTotal,
                numAppointmentsPending,
                numAppointmentsApproved,
                numAppointmentsRejected,
                numAppointmentsCancelled,
                numCasesTotal,
                numCasesCompleted,
                numCasesIncompleted,
                numEventTotal,
                numEventCompleted,
                numEventIncompleted,
            });
        }

        public class AnnouncementDto
        {
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
    }
}