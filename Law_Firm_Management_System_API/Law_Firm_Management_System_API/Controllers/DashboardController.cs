using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("Data/Partner")]
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

        // Get the dashboard data from paralegal's perspective.
        [HttpGet]
        [Route("Data/Paralegal")]
        public IActionResult GetParalegalDashboardData()
        {
            var user = userService.GetUser(User);

            var taskList = context.Tasks.Where(a => a.ParalegalUserId == user.Id);
            var numTasksToDo = taskList.Where(a => a.CompletedTime == null && a.InProgress == false).Count();
            var numTasksInProgress = taskList.Where(a => a.InProgress == true).Count();
            var numTasksCompleted = taskList.Where(a => a.CompletedTime != null).Count();

            return Ok(new { numTasksToDo, numTasksInProgress, numTasksCompleted });
        }

        // Get the dashboard data from client's perspective.
        [HttpGet]
        [Route("Data/Client")]
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

        // Get the upcoming events from partner's perspective.
        [HttpGet]
        [Route("UpcomingEvents/Partner")]
        public IActionResult GetPartnerUpcomingEvents()
        {
            var user = userService.GetUser(User);

            var upcomingAppointments = context.Appointments.Include(a => a.Category).Include(a => a.Client).Where(a => a.PartnerUserId == user.Id && a.Status == "Approved").ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.AppointmentTime,
                    Title = x.Category.Name,
                    Description = "There is an appointment between the client, " + x.Client.FullName + ", and you.",
                });
            var upcomingEvents = context.Events.Include(a => a.Client).Where(a => a.PartnerUserId == user.Id && a.IsCompleted == false).ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.EventTime,
                    Title = x.Name,
                    Description = "There is an event you need to attend with the client, " + x.Client.FullName + ".",
                });
            var upcomingTasks = context.Tasks.Where(a => a.PartnerUserId == user.Id && a.InProgress == true).ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.DueTime,
                    Title = x.Title,
                    Description = x.Description,
                });

            var upcomingEventsAll = new List<UpcomingEvent>();
            upcomingEventsAll.AddRange(upcomingAppointments);
            upcomingEventsAll.AddRange(upcomingEvents);
            upcomingEventsAll.AddRange(upcomingTasks);

            return Ok(upcomingEventsAll.OrderBy(a => a.Time).ToList());
        }

        // Get the upcoming events from paralegal's perspective.
        [HttpGet]
        [Route("UpcomingEvents/Paralegal")]
        public IActionResult GetParalegalUpcomingEvents()
        {
            var user = userService.GetUser(User);

            var upcomingTasks = context.Tasks.Where(a => a.ParalegalUserId == user.Id && a.InProgress == true).ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.DueTime,
                    Title = x.Title,
                    Description = x.Description,
                });

            var upcomingEventsAll = new List<UpcomingEvent>();
            upcomingEventsAll.AddRange(upcomingTasks);

            return Ok(upcomingEventsAll.OrderBy(a => a.Time).ToList());
        }

        // Get the upcoming events from client's perspective.
        [HttpGet]
        [Route("UpcomingEvents/Client")]
        public IActionResult GetClientUpcomingEvents()
        {
            var user = userService.GetUser(User);
            var client = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            var upcomingAppointments = context.Appointments.Include(a => a.Category).Include(a => a.PartnerUser).Where(a => a.ClientId == client.Id && a.Status == "Approved").ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.AppointmentTime,
                    Title = x.Category.Name,
                    Description = "There is an appointment between the partner, " + x.PartnerUser.FullName + ", and you.",
                });
            var upcomingEvents = context.Events.Include(a => a.PartnerUser).Where(a => a.ClientId == client.Id && a.IsCompleted == false).ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.EventTime,
                    Title = x.Name,
                    Description = "There is an event you need to attend with the partner, " + x.PartnerUser.FullName + ".",
                });

            var upcomingEventsAll = new List<UpcomingEvent>();
            upcomingEventsAll.AddRange(upcomingAppointments);
            upcomingEventsAll.AddRange(upcomingEvents);

            return Ok(upcomingEventsAll.OrderBy(a => a.Time).ToList());
        }

        public class AnnouncementDto
        {
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }

        public class UpcomingEvent
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public DateTime Time { get; set; }
        }
    }
}