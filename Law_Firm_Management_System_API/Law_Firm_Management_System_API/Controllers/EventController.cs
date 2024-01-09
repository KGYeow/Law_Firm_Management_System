using Law_Firm_Management_System_API.Authentication;
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
                .OrderBy(a => a.Name)
                .Where(a => a.PartnerUserId == user.Id)  // Filter events by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    clientId = x.Case.ClientId,
                    clientName = x.Client.FullName,
                    paralegalName = x.PartnerUser.Partner.ParalegalUser.User.FullName,
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
                .OrderBy(a => a.Name)
                .Where(a => a.PartnerUserId == partnerId)  // Filter events by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    clientId = x.Case.ClientId,
                    clientName = x.Client.FullName,
                    partnerName = x.PartnerUser.FullName,
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

        // Get the list of events from client's perspective.
        [HttpGet]
        [Route("ClientPerspectiveEventList")]
        public IActionResult GetEventClientPerspective([FromQuery] EventFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Events
                .Include(a => a.Case)
                .OrderBy(a => a.Name)
                .Where(a => a.Client.UserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    partnerName = x.PartnerUser.FullName,
                    clientId = x.ClientId,
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

        //Add event from partner perspective
        [HttpPost]
        [Route("EventCreate")]
        public IActionResult CreateEventPartnerPerspective([FromBody] EventPartnerCreateDto dto)
        {
            var user = userService.GetUser(User);

            // Retrieve the Case entity based on CaseId
            var selectedCase = context.Cases.Find(dto.CaseId);

            if (selectedCase == null)
            {
                // Handle the case where the specified CaseId is not valid
                //return BadRequest(new Response { Status = "Error", Message = "Invalid CaseId" });
                throw new Exception("Invalid CaseId");
            }

            var newEvent = new Event
            {
                CaseId = dto.CaseId,
                PartnerUserId = user.Id,
                ClientId = selectedCase.ClientId,
                Name = dto.Name,
                CreatedTime = DateTime.Now,
                EventTime = dto.EventTime,
                IsCompleted = false,
            };

            context.Events.Add(newEvent);
            context.SaveChanges();

            var client = context.Clients.Where(a => a.Id == selectedCase.ClientId).FirstOrDefault();
            if (client.UserId != null)
            {
                var notification = new Notification
                {
                    UserId = (int)client.UserId,
                    Title = "New Created Event",
                    Description = "There is a new event created by the partner, " + user.FullName + ", for you.",
                    IsRead = false,
                };
                context.Notifications.Add(notification);
                context.SaveChanges();
            }

            return Ok(new Response { Status = "Success", Message = "New event created successfully" });
        }

        // Update the current event.
        [HttpPut]
        [Route("Update/{eventId}")]
        public IActionResult Archive(int eventId)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Events.Where(a => a.Id == eventId).FirstOrDefault();

            existingDoc.IsCompleted = !existingDoc.IsCompleted; ;
            context.Events.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Event status updated successfully" });
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
            public int? CaseId { get; set; }
            public DateTime EventTime { get; set; }

        }
    }
}


