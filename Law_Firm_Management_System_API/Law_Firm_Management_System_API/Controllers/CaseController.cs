using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetCasePartnerPerspective()
        {
            var user = userService.GetUser(User);
            var l = context.Cases.Include(a => a.User).Include(a => a.Client).Include(a => a.CaseStatus).Where(a => a.PartnerUserId == user.Id).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, name = x.Name, partnerName = x.User?.FullName, clientName = x.Client?.FullName, createdTime = x.CreatedTime, updatedTime = x.UpdatedTime, closedTime = x.ClosedTime, status = x.CaseStatus?.StatusName });
            return Ok(l);
        }
    }
}