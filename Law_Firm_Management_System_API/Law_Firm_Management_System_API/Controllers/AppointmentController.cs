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

        [HttpGet]
        [Route("")]
        public IActionResult GetAppointment()
        {
            var l = context.Appointments.Include(a => a.User).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, userId = x.UserId, fullName = x.User?.FullName, category = x.Category, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }
    }
}