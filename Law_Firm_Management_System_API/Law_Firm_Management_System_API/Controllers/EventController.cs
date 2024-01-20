using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Law_Firm_Management_System_API.Controllers.CaseController;
using static Law_Firm_Management_System_API.Controllers.DashboardController;
using static Law_Firm_Management_System_API.Controllers.DocumentController;
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

        // Get the list of upcoming events from client's perspective.
        [HttpGet]
        [Route("ClientPerspectiveUpcomingEventList")]
        public IActionResult GetUpcomingEventClientPerspective([FromQuery] EventFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Events
                .Include(a => a.Case)
                .OrderByDescending(a => a.CreatedTime)
                .Where(a => a.IsCompleted == false)
                .Where(a => a.Client.UserId == user.Id)// Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    partnerName = x.PartnerUser.FullName,
                    clientId = x.ClientId,
                    createdTime = x.CreatedTime,
                    eventTime = x.EventTime,
                    description = x.Description,
                    isCompleted = x.IsCompleted
                });
            if (dto.CaseId != null)
                l = l.Where(a => a.caseID == dto.CaseId);
            if (dto.IsCompleted != null)
                l = l.Where(a => a.isCompleted == dto.IsCompleted);

            l.ToList();

            return Ok(l);
        }

        // Get the list of past events from client's perspective.
        [HttpGet]
        [Route("ClientPerspectivePastEventList")]
        public IActionResult GetPastEventClientPerspective([FromQuery] EventFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Events
                .Include(a => a.Case)
                .OrderByDescending(a => a.CreatedTime)
                .Where(a => a.IsCompleted == true)
                .Where(a => a.Client.UserId == user.Id)// Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    partnerName = x.PartnerUser.FullName,
                    clientId = x.ClientId,
                    createdTime = x.CreatedTime,
                    eventTime = x.EventTime,
                    description = x.Description,
                    isCompleted = x.IsCompleted
                });
            if (dto.CaseId != null)
                l = l.Where(a => a.caseID == dto.CaseId);
            if (dto.IsCompleted != null)
                l = l.Where(a => a.isCompleted == dto.IsCompleted);

            l.ToList();

            return Ok(l);
        }

        // Get the upcoming events from client's perspective for calendar.
        [HttpGet]
        [Route("UpcomingEvents/Client")]
        public IActionResult GetClientUpcomingEvents()
        {
            var user = userService.GetUser(User);
            var client = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            var upcomingEvents = context.Events.Include(a => a.PartnerUser).Where(a => a.ClientId == client.Id && a.IsCompleted == false).ToList()
                .Select(x => new UpcomingEvent
                {
                    Time = x.EventTime,
                    Title = x.Name,
                    Description = "There is an event you need to attend with the partner, " + x.PartnerUser.FullName + ". Remarks: "+x.Description,
                });

            return Ok(upcomingEvents.OrderBy(a => a.Time).ToList());
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
                Description = dto.Description,
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
                    Description = "There is a event '"+ dto.Name +"' created by the partner, " + user.FullName + ", for you.",
                    IsRead = false,
                };
                context.Notifications.Add(notification);
                context.SaveChanges();
            }

            return Ok(new Response { Status = "Success", Message = "New event created successfully" });
        }

        // Update the status of current event.
        [HttpPut]
        [Route("Update/{eventId}")]
        public IActionResult Update(int eventId)
        {
            var user = userService.GetUser(User);
            var existingEvt = context.Events.Where(a => a.Id == eventId).FirstOrDefault();

            existingEvt.IsCompleted = !existingEvt.IsCompleted; ;
            context.Events.Update(existingEvt);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Event status updated successfully" });
        }

        // Rename the existing event.
        [HttpPut]
        [Route("Rename")]
        public IActionResult Rename([FromBody] EventRenameDto dto)
        {
            var user = userService.GetUser(User);
            var existingEvt = context.Events.Where(a => a.Id == dto.EventId).FirstOrDefault();

            existingEvt.Name = dto.Name;
            context.Events.Update(existingEvt);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Event renamed successfully" });
        }

        //Edit Case of Event
        [HttpPut]
        [Route("EditCase")]
        public IActionResult EditCase([FromBody] EditCaseDto dto)
        {
            var user = userService.GetUser(User);
            var existingEvt = context.Events
                .Include(c => c.Case)  // Make sure to include the Case navigation property
                .FirstOrDefault(c => c.Id == dto.EventId);

            if (existingEvt == null)
            {
                return NotFound(new Response { Status = "Error", Message = "Event not found" });
            }

            // Check if the case ID has changed
            if (existingEvt.CaseId != dto.CaseId)
            {
                // Fetch the new case
                var newCase = context.Cases.FirstOrDefault(cases => cases.Id == dto.CaseId);

                if (newCase == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Case not found" });
                }

                // Update the event with the new case
                existingEvt.Case = newCase;  // Set the Case navigation property
                existingEvt.CaseId = newCase.Id;
            }

            context.Events.Update(existingEvt);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Case updated successfully" });
        }

        // Reschedule the existing event.
        [HttpPut]
        [Route("EditTime")]
        public IActionResult EditTime([FromBody] EventTimeDto dto)
        {
            var user = userService.GetUser(User);
            var existingEvt = context.Events.Where(a => a.Id == dto.EventId).FirstOrDefault();

            existingEvt.EventTime = dto.EventTime;
            context.Events.Update(existingEvt);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Event rescheduled successfully" });
        }

        // Delete the event permanently.
        [HttpDelete]
        [Route("{EventId}")]
        public IActionResult Delete(int eventId)
        {
            var user = userService.GetUser(User);
            var existingEvt = context.Events.Where(a => a.Id == eventId).FirstOrDefault();
            context.Events.Remove(existingEvt);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Event deleted successfully" });
        }

        // Get the specific event's information.
        [HttpGet]
        [Route("Info/{EventId}")]
        public IActionResult GetEventInfo(int eventId)
        {
            var user = userService.GetUser(User);

            var eventInfo = context.Events
                .Where(a => a.Id == eventId)  // Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    caseID = x.Case.Id,
                    caseName = x.Case.Name,
                    partnerName = x.PartnerUser.FullName,
                    clientId = x.ClientId,
                    clientName = x.Client.FullName,
                    clientPhone = x.Client.PhoneNumber,
                    clientEmail = x.Client.Email,
                    createdTime = x.CreatedTime,
                    eventTime = x.EventTime,
                    description = x.Description,
                    isCompleted = x.IsCompleted
                })
                .FirstOrDefault();
            return Ok(eventInfo);
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
            public string Description { get; set; } = null!;
            public DateTime EventTime { get; set; }

        }
        public class EventRenameDto
        {
            public int EventId { get; set; }
            public string Name { get; set; } = null!;
        }
        public class EditCaseDto
        {
            public int EventId { get; set; }
            public int CaseId { get; set; }
        }
        public class EventTimeDto
        {
            public int EventId { get; set; }
            public DateTime EventTime { get; set; }
        }
    }
}


