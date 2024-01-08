using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of upcoming appointments from partner's perspective.
        [HttpGet]
        [Route("Partner/UpcomingAppointment")]
        public IActionResult GetPartnerUpcomingAppointment()
        {
            var user = userService.GetUser(User);
            var l = context.Appointments.Where(a => a.PartnerUserId == user.Id && a.Status == "Approved").ToList();
            return Ok(l);
        }

        // Get the list of upcoming appointments from client's perspective.
        [HttpGet]
        [Route("Client/UpcomingAppointment")]
        public IActionResult GetClientUpcomingAppointment()
        {
            var user = userService.GetUser(User);
            var l = context.Appointments.Where(a => a.ClientId == user.Id && a.Status == "Approved").ToList();
            return Ok(l);
        }

        // Get the list of upcoming events from partner's perspective.
        [HttpGet]
        [Route("Partner/UpcomingEvent")]
        public IActionResult GetPartnerUpcomingEvent()
        {
            var user = userService.GetUser(User);
            var l = context.Events.Include(a => a.Case).Where(a => a.Case.PartnerUserId == user.Id && a.IsCompleted == false).ToList();
            return Ok(l);
        }

        // Get the list of upcoming events from client's perspective.
        [HttpGet]
        [Route("Client/UpcomingEvent")]
        public IActionResult GetClientUpcomingEvent()
        {
            var user = userService.GetUser(User);
            var l = context.Events.Include(a => a.Case).Where(a => a.Case.ClientId == user.Id && a.IsCompleted == false).ToList();
            return Ok(l);
        }

        // Get the list of in-progress tasks from partner's perspective.
        [HttpGet]
        [Route("Partner/InProgress")]
        public IActionResult GetPartnerInProgressTask()
        {
            var user = userService.GetUser(User);
            var l = context.Tasks.Where(a => a.PartnerUserId == user.Id && a.InProgress == true).ToList();
            return Ok(l);
        }

        // Get the list of in-progress tasks from paralegal's perspective.
        [HttpGet]
        [Route("Paralegal/InProgress")]
        public IActionResult GetParalegalInProgressTask()
        {
            var user = userService.GetUser(User);
            var l = context.Tasks.Where(a => a.ParelegalUserId == user.Id && a.InProgress == true).ToList();
            return Ok(l);
        }
    }
}