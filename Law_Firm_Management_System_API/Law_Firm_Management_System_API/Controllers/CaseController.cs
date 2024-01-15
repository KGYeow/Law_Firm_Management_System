using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Law_Firm_Management_System_API.Controllers.AppointmentController;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : BaseController
    {
        public CaseController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of cases.
        [HttpGet]
        [Route("")]
        public IActionResult GetCaseList()
        {
            var user = userService.GetUser(User);
            var l = context.Cases
                .Where(a => a.PartnerUserId == user.Id)
                .Select(x => new { id = x.Id, name = x.Name })
                .ToList();
            return Ok(l);
        }

        // Get the list of cases from partner's perspective.
        [HttpGet]
        [Route("PartnerPerspectiveCaseList")]
        public IActionResult GetCasePartnerPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Cases
                .Include(a => a.Status)
                .Include(a => a.Client)
                .Where(a => a.PartnerUserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    clientName = x.Client.FullName, 
                    clientPhone = x.Client.PhoneNumber,
                    clientEmail = x.Client.Email,
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName,
                    statusDescription = x.Status.StatusDescription});

            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        // Get the list of cases from paralegal's perspective.
        [HttpGet]
        [Route("ParalegalPerspectiveCaseList")]
        public IActionResult GetCaseParalegalPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            // Retrieve the partner ID assigned to the paralegal
            var partnerId = context.Partners
                .Where(p => p.ParalegalUserId == user.Id)
                .Select(p => p.UserId)
                .FirstOrDefault();

            var l = context.Cases
                .Include(a => a.Status)
                .Where(a => a.PartnerUserId == partnerId)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    clientName = x.Client.FullName, 
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName });

            if (dto.ClientId != null)
                l = l.Where(a => a.clientId == dto.ClientId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        // Get the list of cases from client's perspective.
        [HttpGet]
        [Route("ClientPerspectiveCaseList")]
        public IActionResult GetCaseClientPerspective([FromQuery] CaseFilterDto dto)
        {
            var user = userService.GetUser(User);

            var l = context.Cases
                .Include(a => a.Status)
                .Where(a => a.Client.UserId == user.Id)  // Filter cases by assigned partner
                .Select(x => new { 
                    id = x.Id, 
                    name = x.Name, 
                    clientId = x.ClientId, 
                    partnerUserId = x.PartnerUserId, 
                    clientName = x.Client.FullName, 
                    partnerName = x.PartnerUser.FullName, 
                    createdTime = x.CreatedTime, 
                    updatedTime = x.UpdatedTime, 
                    closedTime = x.ClosedTime, 
                    statusId = x.StatusId, 
                    status = x.Status.StatusName });

            if (dto.PartnerUserId != null)
                l = l.Where(a => a.partnerUserId == dto.PartnerUserId);
            if (dto.Status != null)
                l = l.Where(a => a.status == dto.Status);

            l.ToList();

            return Ok(l);
        }

        // Get the specific case's information.
        [HttpGet]
        [Route("Info/{CaseId}")]
        public IActionResult GetCaseInfo(int caseId)
        {
            var user = userService.GetUser(User);

            var caseInfo = context.Cases
                .Include(a => a.Status)
                .Where(a => a.Id == caseId)  // Filter cases by assigned partner
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    clientId = x.ClientId,
                    clientName = x.Client.FullName,
                    clientPhone = x.Client.PhoneNumber,
                    clientEmail = x.Client.Email,
                    createdTime = x.CreatedTime,
                    updatedTime = x.UpdatedTime,
                    closedTime = x.ClosedTime,
                    statusId = x.StatusId,
                    status = x.Status.StatusName,
                    statusDescription = x.Status.StatusDescription
                })
                .FirstOrDefault();
            return Ok(caseInfo);
        }

        [HttpPost]
        [Route("CaseCreate")]
        public IActionResult CreateCasePartnerPerspective([FromBody] CasePartnerCreateDto dto)
        {
            var user = userService.GetUser(User);

            var newCase = new Case
            {
                PartnerUserId = user.Id,
                ClientId = dto.ClientId,
                Name = dto.Name,
                CreatedTime = DateTime.Now,
                UpdatedTime = null,
                ClosedTime = null,
                StatusId = 1,
                // Add other necessary properties based on your Case model
            };

            context.Cases.Add(newCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New case created successfully" });
        }

        // Delete the event permanently.
        [HttpDelete]
        [Route("{CaseId}")]
        public IActionResult Delete(int caseId)
        {
            var user = userService.GetUser(User);
            var existingCase = context.Cases.Where(a => a.Id == caseId).FirstOrDefault();
            context.Cases.Remove(existingCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Case deleted successfully" });
        }

        // Rename the existing Case.
        [HttpPut]
        [Route("Rename")]
        public IActionResult Rename([FromBody] CaseRenameDto dto)
        {
            var user = userService.GetUser(User);

            var existingDoc = context.Cases
                .FirstOrDefault(c => c.Id == dto.CaseId);

            existingDoc.Name = dto.CaseName;
            existingDoc.UpdatedTime = DateTime.Now; // Update the updatedTime property
            context.Cases.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Case renamed successfully" });
        }

        //Edit Client of Case
        [HttpPut]
        [Route("EditClient")]
        public IActionResult EditClient([FromBody] EditClientDto dto)
        {
            var user = userService.GetUser(User);
            var existingCase = context.Cases
                .Include(c => c.Client)  // Make sure to include the Client navigation property
                .FirstOrDefault(c => c.Id == dto.CaseId);

            if (existingCase == null)
            {
                return NotFound(new Response { Status = "Error", Message = "Case not found" });
            }

            // Check if the client ID has changed
            if (existingCase.ClientId != dto.ClientId)
            {
                // Fetch the new client
                var newClient = context.Clients.FirstOrDefault(client => client.Id == dto.ClientId);

                if (newClient == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Client not found" });
                }

                // Update the case with the new client
                existingCase.Client = newClient;  // Set the Client navigation property
                existingCase.ClientId = newClient.Id;
                existingCase.UpdatedTime = DateTime.Now; // Update the updatedTime property
            }

            context.Cases.Update(existingCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Client updated successfully" });
        }

        [HttpPut]
        [Route("UpdateClosedTime/{caseId}")]
        public IActionResult UpdateClosedTime(int caseId)
        {
            var user = userService.GetUser(User);

            var existingCase = context.Cases
                .Include(c => c.Status)  // Include the Status navigation property
                .FirstOrDefault(c => c.Id == caseId && c.PartnerUserId == user.Id);

            /*
            if (existingCase == null)
            {
                return NotFound(new Response { Status = "Error", Message = "Case not found" });
            }

            // Check if the case status is already Settled
            if (existingCase.Status.StatusName != "Settled")
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid operation. Case status must be Settled." });
            }*/

            if (existingCase == null)
            {
                throw new Exception("The Case is not found in the system.");
            }

            // Update the ClosedTime
            existingCase.ClosedTime = DateTime.Now;
            context.Cases.Update(existingCase);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "ClosedTime updated successfully" });
        }

        // Change the case status
        [HttpPut]
        [Route("ChangeStatus/{caseId}")]
        public IActionResult ChangeCaseStatus(int caseId, [FromBody] ChangeStatusDto dto)
        {
            var user = userService.GetUser(User);

            var existingCase = context.Cases
                .Include(c => c.Status)  // Include the Status navigation property
                .FirstOrDefault(c => c.Id == caseId && c.PartnerUserId == user.Id);

            if (existingCase == null)
            {
                return NotFound(new Response { Status = "Error", Message = "Case not found" });
            }

            // Check if the new status is valid
            var newStatus = context.CaseStatuses.FirstOrDefault(s => s.StatusName == dto.NewStatus);

            if (newStatus == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid status" });
            }

            // Update the case with the new status
            existingCase.Status = newStatus;  // Set the Status navigation property
            existingCase.StatusId = newStatus.Id;
            existingCase.UpdatedTime = DateTime.Now; // Update the updatedTime property
            context.Cases.Update(existingCase);
            context.SaveChanges();

            if (dto.NewStatus == "Settled")
            {
                var updateClosedTimeResponse = UpdateClosedTime(caseId);
                // You can handle the response as needed
            }

            return Ok(new Response { Status = "Success", Message = "Case status updated successfully" });
        }

        public class CaseFilterDto
        {
            public int? ClientId { get; set; }
            public int? PartnerUserId { get; set; }
            public string? Status { get; set; }
        }

        public class CasePartnerCreateDto
        {
            public string? Name { get; set; }
            public int? ClientId { get; set; }
        }

        public class CaseRenameDto
        {
            public int CaseId { get; set; }
            public string CaseName { get; set; } = null!;
        }

        public class EditClientDto
        {
            public int CaseId { get; set; }
            public int ClientId { get; set; }
        }

        public class ChangeStatusDto
        {
            public string NewStatus { get; set; } = null!;
        }
    }
}