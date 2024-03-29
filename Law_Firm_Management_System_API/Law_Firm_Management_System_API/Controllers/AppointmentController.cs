﻿using Law_Firm_Management_System_API.Authentication;
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

        // Get the specific appointment's information.
        [HttpGet]
        [Route("Info/{AppointmentId}")]
        public IActionResult GetAppointmentInformation(int appointmentId)
        {
            var appointmentInfo = context.Appointments.Include(a => a.Client).Include(a => a.Category).Include(a => a.PartnerUser).Where(a => a.Id == appointmentId)
                .Select(x => new { id = x.Id, clientId = x.ClientId, clientFullName = x.Client.FullName, partnerUserId = x.PartnerUserId, partnerFullName = x.PartnerUser.FullName, category = x.Category.Name, appointmentTime = x.AppointmentTime, status = x.Status, description = x.Description })
                .FirstOrDefault();
            return Ok(appointmentInfo);
        }

        // Get the list of client appointments from partner's perspective.
        [HttpGet]
        [Route("PartnerPerspectiveList")]
        public IActionResult GetAppointmentsPartnerPerspective([FromQuery] AppointmentFilterDto dto)
        {
            var user = userService.GetUser(User);
            var l = context.Appointments.Include(a => a.Client).Include(a => a.Category).Where(a => a.PartnerUserId == user.Id).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, clientId = x.ClientId, fullName = x.Client?.FullName, category = x.Category.Name, appointmentTime = x.AppointmentTime, status = x.Status, description = x.Description });
            
            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
            if (dto.Category != null)
                l = l.Where(a => a.category == dto.Category);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);
            l.ToList();

            return Ok(l);
        }

        // Get the list of appointments from client's perspective.
        [HttpGet]
        [Route("ClientPerspectiveList")]
        public IActionResult GetAppointmentsClientPerspective([FromQuery] AppointmentFilterDto dto)
        {
            var user = userService.GetUser(User);
            var l = context.Appointments.Include(a => a.PartnerUser).Include(a => a.Category).Where(a => a.Client.UserId == user.Id).OrderByDescending(a => a.Id).ToList()
                .Select(x => new { id = x.Id, partnerUserId = x.PartnerUserId, fullName = x.PartnerUser?.FullName, category = x.Category.Name, appointmentTime = x.AppointmentTime, status = x.Status, description = x.Description });

            if (dto.PartnerUserId != null)
                l = l.Where(a => a.partnerUserId == dto.PartnerUserId);
            if (dto.Category != null)
                l = l.Where(a => a.category == dto.Category);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);
            l.ToList();

            return Ok(l);
        }

        // Create the appointment made by the client.
        [HttpPost]
        [Route("ClientCreate")]
        public IActionResult CreateAppointmentClientPerspective([FromBody] AppointmentClientCreateDto dto)
        {
            var user = userService.GetUser(User);
            var client = context.Clients.Where(a => a.UserId == user.Id).FirstOrDefault();

            var appointment = new Appointment
            {
                ClientId = client.Id,
                PartnerUserId = dto.PartnerUserId,
                CategoryId = dto.CategoryId,
                AppointmentTime = dto.AppointmentTime,
                Status = "Pending",
                Description = dto.Description
            };
            context.Appointments.Add(appointment);
            context.SaveChanges();

            var notification = new Notification
            {
                UserId = dto.PartnerUserId,
                Title = "New Pending Appointment",
                Description = "There is a new client appointment, " + client.FullName + ", waiting for your approval.",
                IsRead = false,
            };
            context.Notifications.Add(notification);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New appointment created successfully" });
        }

        // Create the appointment made by the partner.
        [HttpPost]
        [Route("PartnerCreate")]
        public IActionResult CreateAppointmentPartnerPerspective([FromBody] AppointmentPartnerCreateDto dto)
        {
            var user = userService.GetUser(User);

            var appointment = new Appointment
            {
                ClientId = dto.ClientId,
                PartnerUserId = user.Id,
                CategoryId = dto.CategoryId,
                AppointmentTime = dto.AppointmentTime,
                Status = "Approved",
                Description = dto.Description
            };
            context.Appointments.Add(appointment);
            context.SaveChanges();

            var client = context.Clients.Where(a => a.Id == dto.ClientId).FirstOrDefault();
            if (client.UserId != null)
            {
                var notification = new Notification
                {
                    UserId = (int)client.UserId,
                    Title = "New Created Appointment",
                    Description = "There is a new appointment created by the partner, " + user.FullName + ", for you.",
                    IsRead = false,
                };
                context.Notifications.Add(notification);
                context.SaveChanges();
            }

            return Ok(new Response { Status = "Success", Message = "New appointment created successfully" });
        }

        // Delete the appointment made by the client.
        [HttpDelete]
        [Route("")]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            var user = userService.GetUser(User);
            var appointment = context.Appointments.Where(a => a.Id == appointmentId).FirstOrDefault();

            context.Appointments.Remove(appointment);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Appointment deleted successfully" });
        }

        // Approve or reject the pending appointment.
        [HttpPut]
        [Route("PartnerApproval")]
        public IActionResult AppointmentApproval([FromBody] AppointmentApprovalDto dto)
        {
            var user = userService.GetUser(User);
            var appointment = context.Appointments.Where(a => a.Id == dto.AppointmentId).FirstOrDefault();
            appointment.Status = dto.Status;
            context.Appointments.Update(appointment);
            context.SaveChanges();

            var notifyMessage = "";
            var notificationTitle = "";
            var notificationDescription = "";
            if (dto.Status == "Approved")
            {
                notifyMessage = "Appointment approved successfully";
                notificationTitle = "Appointment Approved";
                notificationDescription = "Your appointment has been approved by partner, " + user.FullName + ".";
            }
            else if (dto.Status == "Rejected")
            {
                notifyMessage = "Appointment rejected successfully";
                notificationTitle = "Appointment Rejected";
                notificationDescription = "Your appointment has been rejected by partner, " + user.FullName + ".";
            }
            else
            {
                notifyMessage = "Appointment cancelled successfully";
                notificationTitle = "Appointment Cancelled";
                notificationDescription = "Your appointment has been cancelled by partner, " + user.FullName + ".";
            }

            var client = context.Clients.Where(a => a.Id == appointment.ClientId).FirstOrDefault();
            if (client.UserId != null)
            {
                var notification = new Notification
                {
                    UserId = (int)client.UserId,
                    Title = notificationTitle,
                    Description = notificationDescription,
                    IsRead = false,
                };
                context.Notifications.Add(notification);
                context.SaveChanges();
            }

            return Ok(new Response { Status = "Success", Message = notifyMessage });
        }

        public class AppointmentClientCreateDto
        {
            public int PartnerUserId { get; set; }
            public int CategoryId { get; set; }
            public DateTime AppointmentTime { get; set; }
            public string? Description { get; set; }
        }

        public class AppointmentPartnerCreateDto
        {
            public int ClientId { get; set; }
            public int CategoryId { get; set; }
            public DateTime AppointmentTime { get; set; }
            public string? Description { get; set; }
        }

        public class AppointmentFilterDto
        {
            public int? ClientId { get; set; }
            public int? PartnerUserId { get; set; }
            public string? Category { get; set; }
            public string? Status { get; set; }
        }

        public class AppointmentApprovalDto
        {
            public int AppointmentId { get; set; }
            public string Status { get; set; } = null!;
        }
    }
}