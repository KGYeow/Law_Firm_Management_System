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
            var l = context.Cases.OrderBy(a => a.Name).ToList().Select(x => new { id = x.Id, name = x.Name });
            return Ok(l);
        }

        // Get the list of cases from partner's perspective.
        [HttpGet]
        [Route("PartnerPerspectiveCaseList")]
        public IActionResult GetCasePartnerPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);
            var l = context.Cases.Include(a => a.User).Include(a => a.Client).Include(a => a.CaseStatus).Where(a => a.PartnerUserId == user.Id).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, name = x.Name, clientId = x.ClientId, clientName = x.Client?.FullName, createdTime = x.CreatedTime, updatedTime = x.UpdatedTime, closedTime = x.ClosedTime, statusId = x.StatusID, status = x.CaseStatus?.StatusName });

            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
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
                StatusID = 1,
                // Add other necessary properties based on your Case model
            };

            context.Cases.Add(newCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New case created successfully" });
        }

        /*[HttpGet]
        [Route("DisplayCaseDetails/{caseId}")]

        public IActionResult DisplayCaseDetails(int caseId)
        {
            var user = userService.GetUser(User);
            var caseDetails = context.Cases.Include(a => a.User).Include(a => a.Client).Include(a => a.CaseStatus).Where(a => a.PartnerUserId == user.Id && a.Id == caseId).OrderByDescending(a => a.Id).ToList()
                            .Select(x => new { id = x.Id, name = x.Name, clientId = x.ClientId, clientName = x.Client?.FullName, createdTime = x.CreatedTime, updatedTime = x.UpdatedTime, closedTime = x.ClosedTime, statusId = x.StatusId, status = x.CaseStatus?.StatusName })
                            .FirstOrDefault();

            return Ok(caseDetails);
        }*/


        public class CaseFilterDto
        {
            public int? ClientId { get; set; }
            public string? Status { get; set; }
        }

        public class CasePartnerCreateDto
        {
            public string? Name { get; set; }
            public int? ClientId { get; set; }
        }
    }
}