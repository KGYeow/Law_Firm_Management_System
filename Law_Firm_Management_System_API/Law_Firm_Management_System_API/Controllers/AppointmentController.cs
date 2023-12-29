using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Authorization;
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

        // Get the list of appointment category.
        [HttpGet]
        [Route("Category")]
        public IActionResult GetAppointmentCategory()
        {
            var l = context.AppointmentCategories.OrderBy(a => a.Name).ToList();
            return Ok(l);
        }

        // Get the list of client appointments from partner's perspective.
        [HttpGet]
        [Route("List/PartnerPerspective/{PartnerUserId}")]
        public IActionResult GetAppointmentsPartnerPerspective(int partnerUserId)
        {
            var l = context.Appointments.Include(a => a.Client).Include(a => a.Category).Where(a => a.PartnerUserId == partnerUserId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.Client?.FullName, category = x.Category.Name, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }

        // Get the list of appointments from client's perspective.
        [HttpGet]
        [Route("List/ClientPerspective/{ClientUserId}")]
        public IActionResult GetAppointmentsClientPerspective(int clientUserId)
        {
            var l = context.Appointments.Include(a => a.PartnerUser).Include(a => a.Category).Where(a => a.Client.UserId == clientUserId).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, fullName = x.PartnerUser?.FullName, category = x.Category.Name, appointmentTime = x.AppointmentTime, status = x.Status });
            return Ok(l);
        }

        // Create the appointment made by the client.
        [HttpPost]
        [Route("ClientCreate")]
        public IActionResult CreateAppointmentClientPerspective([FromBody] AppointmentDto dto)
        {
            var user = userService.GetUser(User);
            var client = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            var appointment = new Appointment
            {
                ClientId = client.Id,
                PartnerUserId = dto.PartnerUserId,
                CategoryId = dto.CategoryId,
                AppointmentTime = dto.AppointmentTime,
                Status = "Pending"
            };
            context.Appointments.Add(appointment);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New appointment created successfully!" });
        }

        // Approve or reject the pending appointment.
        [HttpPut]
        [Route("PartnerApproval/{AppointmentId}/{Status}")]
        public IActionResult AppointmentApproval(int appointmentId, string status)
        {
            var appointment = context.Appointments.Where(a => a.Id == appointmentId).FirstOrDefault();
            appointment.Status = status;
            context.Appointments.Update(appointment);
            context.SaveChanges();

            if (status == "Approved")
                return Ok(new Response { Status = "Success", Message = "Appointment approved successfully!" });
            else
                return Ok(new Response { Status = "Success", Message = "Appointment rejected successfully!" });
        }

        public class AppointmentDto
        {
            public int PartnerUserId { get; set; }
            public int CategoryId { get; set; }
            public DateTime AppointmentTime { get; set; }
        }

        public class AppointmentApprovalDto
        {
            public int AppointmentId { get; set; }
            public string? Status { get; set; }
        }
    }
}