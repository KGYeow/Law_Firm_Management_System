using Law_Firm_Management_System_API.Authentication;
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

        // Assign a paralegal to the partner.
        [HttpPut]
        [Route("AssignedParalegal/Add/{ParalegalUserId}")]
        public IActionResult AddAssignedParalegal(int paralegalUserId)
        {
            var assignedByOtherPartner = context.Partners.Where(a => a.ParalegalUserId == paralegalUserId).FirstOrDefault();
            if (assignedByOtherPartner != null)
                throw new Exception("This paralegal has been assigned to another partner");

            var user = userService.GetUser(User);
            var partner = context.Partners.Where(a => a.UserId == user.Id).FirstOrDefault();

            partner.ParalegalUserId = paralegalUserId;
            context.Partners.Update(partner);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your assigned paralegal has been added successfully" });
        }

        // Change the assigned paralegal.
        [HttpPut]
        [Route("AssignedParalegal/Change/{ParalegalUserId}")]
        public IActionResult ChangeAssignedParalegal(int paralegalUserId)
        {
            var assignedByOtherPartner = context.Partners.Where(a => a.ParalegalUserId == paralegalUserId).FirstOrDefault();
            if (assignedByOtherPartner != null)
                throw new Exception("This paralegal has been assigned to another partner");

            var user = userService.GetUser(User);
            var partner = context.Partners.Where(a => a.UserId == user.Id).FirstOrDefault();

            partner.ParalegalUserId = paralegalUserId;
            context.Partners.Update(partner);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your assigned paralegal has been changed successfully" });
        }

        // Delete the assigned paralegal.
        [HttpPut]
        [Route("AssignedParalegal/Delete")]
        public IActionResult DeleteAssignedParalegal()
        {
            var user = userService.GetUser(User);
            var partner = context.Partners.Where(a => a.UserId == user.Id).FirstOrDefault();

            partner.ParalegalUserId = null;
            context.Partners.Update(partner);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Your assigned paralegal has been deleted successfully" });
        }
    }
}