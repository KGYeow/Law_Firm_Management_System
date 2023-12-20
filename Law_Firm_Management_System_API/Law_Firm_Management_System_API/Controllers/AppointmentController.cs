using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : BaseController
    {
        public AppointmentController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of client appointments from partner's perspective.
        [HttpGet]
        [Route("ClientAppointments/{PartnerUserId}")]
        public IActionResult GetClientAppointments(int partnerUserId)
        {
            var l = context.Appointments.Include(a => a.Client).Where(a => a.PartnerUserId == partnerUserId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.Client?.FullName, category = x.Category, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }

        // Get the list of client appointments from client's perspective.
        [HttpGet]
        [Route("ClientAppointmentsToLawyer/{ClientUserId}")]
        public IActionResult GetClientAppointmentsToLawyer(int clientUserId)
        {
            var l = context.Appointments.Include(a => a.PartnerUser).Where(a => a.Client.UserId == clientUserId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.PartnerUser?.FullName, category = x.Category, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }
    }
}