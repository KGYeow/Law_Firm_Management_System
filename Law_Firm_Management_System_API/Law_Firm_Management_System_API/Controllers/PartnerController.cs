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

        // Get the assigned paralegal of a specific partner.
        [HttpGet]
        [Route("AssignedParalegal")]
        public IActionResult GetAssignedParalegal()
        {
            var user = userService.GetUser(User);
            var assignedParalegal = context.Partners.Where(a => a.UserId == user.Id)
                .Select(x => new { userId = x.ParalegalUserId, fullName = x.ParalegalUser.User.FullName, email = x.ParalegalUser.User.Email, phoneNumber = x.ParalegalUser.PhoneNumber })
                .FirstOrDefault();
            return Ok(assignedParalegal);
        }
    }
}