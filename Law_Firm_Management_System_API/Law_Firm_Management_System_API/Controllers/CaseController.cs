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
    public class CaseController : BaseController
    {
        public CaseController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of cases.
        [HttpGet]
        [Route("")]
        public IActionResult GetCaseList()
        {
            var user = userService.GetUser(User);
            var l = context.Cases
                .Where(a => a.PartnerUserId == user.Id)
                .Select(x => new { id = x.Id, name = x.Name })
                .ToList();
            return Ok(l);
        }

        // Get the list of cases from partner's perspective.
        [HttpGet]
        [Route("PartnerPerspectiveCaseList")]
        public IActionResult GetCasePartnerPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Cases
                .Include(a => a.Status)
                .Include(a => a.Client)
                .Where(a => a.PartnerUserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    clientName = x.Client.FullName, 
                    clientPhone = x.Client.PhoneNumber,
                    clientEmail = x.Client.Email,
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName,
                    statusDescription = x.Status.StatusDescription});

            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        // Get the list of cases from paralegal's perspective.
        [HttpGet]
        [Route("ParalegalPerspectiveCaseList")]
        public IActionResult GetCaseParalegalPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            // Retrieve the partner ID assigned to the paralegal
            var partnerId = context.Partners
                .Where(p => p.ParalegalUserId == user.Id)
                .Select(p => p.UserId)
                .FirstOrDefault();

            var l = context.Cases
                .Include(a => a.Status)
                .Where(a => a.PartnerUserId == partnerId)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    clientName = x.Client.FullName, 
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName });

            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        // Get the list of cases from client's perspective.
        [HttpGet]
        [Route("ClientPerspectiveCaseList")]
        public IActionResult GetCaseClientPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Cases
                .Include(a => a.Status)
                .Where(a => a.Client.UserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    partnerUserId = x.PartnerUserId, 
                    clientName = x.Client.FullName, 
                    partnerName = x.PartnerUser.FullName, 
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName });

            if (dto.PartnerUserId != null)
                l = l.Where(a => a.partnerUserId == dto.PartnerUserId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        [HttpPost]
        [Route("CaseCreate")]
        public IActionResult CreateCasePartnerPerspective([FromBody] CasePartnerCreateDto dto)
        {
            var user = userService.GetUser(User);

            var newCase = new Case
            {
                PartnerUserId = user.Id,
                ClientId = dto.ClientId,
                Name = dto.Name,
                CreatedTime = DateTime.Now,
                UpdatedTime = null,
                ClosedTime = null,
                StatusId = 1,
                // Add other necessary properties based on your Case model
            };

            context.Cases.Add(newCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New case created successfully" });
        }

        public class CaseFilterDto
        {
            public int? ClientId { get; set; }
            public int? PartnerUserId { get; set; }
            public string? Status { get; set; }
        }

        public class CasePartnerCreateDto
        {
            public string? Name { get; set; }
            public int? ClientId { get; set; }
        }
    }
}