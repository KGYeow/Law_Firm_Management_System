using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParalegalController : BaseController
    {
        public ParalegalController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of paralegal.
        [HttpGet]
        [Route("")]
        public IActionResult GetParalegalList()
        {
            var l = context.Paralegals.Include(a => a.User).OrderBy(a => a.User.FullName).ToList()
                .Select(x => new
                {
                    fullName = x.User.FullName,
                    assignedPartner = context.Partners.Where(p => p.ParalegalUserId == x.UserId && x.IsActive == true).Select(p => p.User.FullName).FirstOrDefault(),
                    phoneNumber = x.PhoneNumber,
                    address = x.Address,
                    email = x.User.Email,
                    isActive = x.IsActive
                });
            return Ok(l);
        }

        // Get the specific paralegal's information.
        [HttpGet]
        [Route("Info/{ParalegalUserId}")]
        public IActionResult GetClientInfo(int paralegalUserId)
        {
            var paralegalInfo = context.Paralegals.Include(a => a.User).Where(a => a.UserId == paralegalUserId)
                .Select(x => new { userId = x.UserId, fullName = x.User.FullName, email = x.User.Email, phoneNumber = x.PhoneNumber, address = x.Address, isActive = x.IsActive, profilePhoto = x.User.ProfilePhoto })
                .FirstOrDefault();
            return Ok(paralegalInfo);
        }

        // Get the assigned partner of a specific paralegal.
        [HttpGet]
        [Route("AssignedPartner")]
        public IActionResult GetAssignedPartner()
        {
            var user = userService.GetUser(User);
            var assignedPartner = context.Partners.Where(a => a.ParalegalUserId == user.Id)
                .Select(x => new { fullName = x.User.FullName, email = x.User.Email, phoneNumber = x.PhoneNumber })
                .FirstOrDefault();
            return Ok(assignedPartner);
        }
    }
}