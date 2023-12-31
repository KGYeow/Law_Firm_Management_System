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

        // Get the list of partner.
/*        [HttpGet]
        [Route("")]
        public IActionResult GetCaseList()
        {
            var l = context.Cases.Include(a => a.PartnerUser).Include(a => a.Client).OrderBy(a => a.Name).ToList()
                .Select(x => new { name = x.Name, partnerName = x.PartnerUser?.FullName, clientName = x.Client?.FullName, createdTime = x.CreatedTime, updatedTime = x.UpdatedTime, closedTime = x.ClosedTime });
            return Ok(l);
        }*/

        // Get the list of case from partner's perspective.
/*        [HttpGet]
        [Route("PartnerPerspectiveCaseList")]
        public IActionResult GetCasePartnerPerspective()
        {
            var user = userService.GetUser(User);
            var l = context.Cases.Include(a => a.PartnerUser).Include(a => a.Client).Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { name = x.Name, partnerName = x.PartnerUser?.FullName, clientName = x.Client?.FullName, createdTime = x.CreatedTime, updatedTime = x.UpdatedTime, closedTime = x.ClosedTime });
            return Ok(l);
        }*/
    }
}


