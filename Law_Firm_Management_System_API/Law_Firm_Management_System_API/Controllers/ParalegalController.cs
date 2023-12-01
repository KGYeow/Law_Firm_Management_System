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

        [HttpGet]
        [Route("")]
        public IActionResult GetParalegalList()
        {
            var l = context.Paralegals.Include(a => a.User).Include(a => a.Partners).OrderByDescending(a => a.User.FullName).ToList()
                .Select(x => new { fullName = x.User.FullName, assignedPartner = x.Partners, phoneNumber = x.PhoneNumber, address = x.Address, email = x.User.Email, isActive = x.IsActive });
            return Ok(l);
        }
    }
}