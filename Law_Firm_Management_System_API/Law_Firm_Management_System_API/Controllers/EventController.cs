using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseController
    {
        public EventController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of event.
        [HttpGet]
        [Route("")]
        public IActionResult GetEventList()
        {
            var l = context.Events.Include(a => a.Case).OrderBy(a => a.Name).ToList()
                .Select(x => new { caseID = x.Case?.Id, name = x.Name, createdTime = x.CreatedTime, eventTime = x.EventTime, isCompleted = x.IsCompleted });
            return Ok(l);
        }
    }
}


