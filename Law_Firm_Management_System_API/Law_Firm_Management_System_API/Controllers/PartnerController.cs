using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : BaseController
    {
        public PartnerController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of partner.
        [HttpGet]
        [Route("")]
        public IActionResult GetPartnerList()
        {
            var l = context.Partners.Include(a => a.User).Include(a => a.ParalegalUser.User).OrderBy(a => a.User.FullName).ToList()
                .Select(x => new { userId = x.UserId, fullName = x.User.FullName, assignedParalegal = x.ParalegalUser?.User.FullName, phoneNumber = x.PhoneNumber, address = x.Address, email = x.User.Email});
            return Ok(l);
        }

        // Get the assigned partner of a specific paralegal.
        [HttpGet]
        [Route("AssignedPartner/{ParalegalId}")]
        public IActionResult GetAssignedPartner(int paralegalId)
        {
            var assignedPartner = context.Partners.Where(a => a.ParalegalUserId == paralegalId)
                .Select(x => new { fullName = x.User.FullName, email = x.User.Email, phoneNumber = x.PhoneNumber })
                .FirstOrDefault();
            return Ok(assignedPartner);
        }
    }
}