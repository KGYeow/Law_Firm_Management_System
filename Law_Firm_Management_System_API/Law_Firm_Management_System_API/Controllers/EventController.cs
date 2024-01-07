using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Law_Firm_Management_System_API.Controllers.CaseController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseController
    {
        public EventController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }
        // Get the list of events.
        [HttpGet]
        [Route("")]
        public IActionResult GetEventList()
        {
            var l = context.Events.OrderBy(a => a.Name).ToList().Select(x => new { id = x.Id, name = x.Name });
            return Ok(l);
        }

        // Get the list of event-ori.
        /*
        [HttpGet]
        [Route("")]
        public IActionResult GetEventList()
        {
            var l = context.Events.Include(a => a.Case).OrderBy(a => a.Name).ToList()
                .Select(x => new { caseID = x.Case?.Id, name = x.Name, createdTime = x.CreatedTime, eventTime = x.EventTime, isCompleted = x.IsCompleted });
            return Ok(l);
        }
        */

        // Get the list of events from partner's perspective.
        [HttpGet]
        [Route("PartnerPerspectiveEventList")]
        public IActionResult GetEventPartnerPerspective([FromQuery] EventFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Events
                .Include(a => a.Case)
                .Where(a => a.PartnerUserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    clientId = x.Case.ClientId,
                    clientName = x.Client.FullName,
                    createdTime = x.CreatedTime,
                    eventTime = x.EventTime,
                    isCompleted = x.IsCompleted
                });

            if (dto.CaseId != null)
                l = l.Where(a => a.caseID == dto.CaseId);
            if (dto.IsCompleted != null)
                l = l.Where(a => a.isCompleted == dto.IsCompleted);


            l.ToList();

            return Ok(l);
        }

        // Get the list of cases from paralegal's perspective.
        [HttpGet]
        [Route("ParalegalPerspectiveEventList")]
        public IActionResult GetEventParalegalPerspective([FromQuery] EventFilterDto dto)
        {
            var user = userService.GetUser(User);

            // Retrieve the partner ID assigned to the paralegal
            var partnerId = context.Partners
                .Where(p => p.ParalegalUserId == user.Id)
                .Select(p => p.UserId)
                .FirstOrDefault();

            var l = context.Events
                .Include(a => a.Case)
                .Where(a => a.PartnerUserId == partnerId)  // Filter events by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    clientId = x.Case.ClientId,
                    clientName = x.Client.FullName,
                    createdTime = x.CreatedTime,
                    eventTime = x.EventTime,
                    isCompleted = x.IsCompleted
                });

            if (dto.CaseId != null)
                l = l.Where(a => a.caseID == dto.CaseId);
            if (dto.IsCompleted != null)
                l = l.Where(a => a.isCompleted == dto.IsCompleted);

            l.ToList();

            return Ok(l);
        }

        public class EventFilterDto
        {
            public int? CaseId { get; set; }
            public int? PartnerUserId { get; set; }
            public bool? IsCompleted { get; set; } = null;
        }

        public class EventPartnerCreateDto
        {
            public string? Name { get; set; }
            public int? ClientId { get; set; }
        }
    }
}


