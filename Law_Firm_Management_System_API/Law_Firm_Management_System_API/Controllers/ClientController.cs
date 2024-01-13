using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        public ClientController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of client.
        [HttpGet]
        [Route("")]
        public IActionResult GetClientList()
        {
            var l = context.Clients.OrderBy(a => a.FullName).ToList();
            return Ok(l);
        }

        // Get the specific client's information.
        [HttpGet]
        [Route("Info/{ClientId}")]
        public IActionResult GetClientInfo(int clientId)
        {
            var clientInfo = context.Clients.Include(a => a.User).Where(a => a.Id == clientId)
                .Select(x => new { clientId = x.Id, userId = (int?)x.User.Id, fullName = x.FullName, phoneNum = x.PhoneNumber, email = x.Email, address = x.Address })
                .FirstOrDefault();
            return Ok(clientInfo);
        }

        // Update the client information.
        [HttpPut]
        [Route("Info/{ClientId}/Edit")]
        public IActionResult ClientInfoUpdate(int clientId, [FromBody] ClientEditDto dto)
        {
            var clientInfo = context.Clients.Where(a => a.Id == clientId).FirstOrDefault();

            clientInfo.FullName = dto.FullName;
            clientInfo.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNum) ? null : dto.PhoneNum;
            clientInfo.Email = string.IsNullOrEmpty(dto.Email) ? null : dto.Email;
            clientInfo.Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address;

            context.Clients.Update(clientInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "The client information has been updated successfully" });
        }

        // Delete the client.
        [HttpDelete]
        [Route("Delete/{ClientId}")]
        public IActionResult ClientDelete(int clientId)
        {
            var clientInfo = context.Clients.Where(a => a.Id == clientId).FirstOrDefault();
            context.Clients.Remove(clientInfo);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Client deleted successfully" });
        }

        public class ClientEditDto
        {
            public string FullName { get; set; } = null!;
            public string? PhoneNum { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
        }
    }
}