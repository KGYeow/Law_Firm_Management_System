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

        // Get the list of client appointments for a specific partner.
        [HttpGet]
        [Route("ClientAppointments/{PartnerId}")]
        public IActionResult GetClientAppointments(int partnerId)
        {
            var l = context.Appointments.Include(a => a.Client).Where(a => a.PartnerUserId == partnerId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.Client?.FullName, category = x.Category, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }

        // Get the list of appointments made by a specific client.
        [HttpGet]
        [Route("ClientAppointmentsToLawyer")]
        public IActionResult GetClientAppointmentsToLawyer(int clientUserId)
        {
            var l = context.Appointments.Include(a => a.Client).Where(a => a.ClientId == clientUserId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.Client?.FullName, category = x.Category, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }
    }
}