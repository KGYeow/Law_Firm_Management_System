using System.Threading.Tasks;
using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        public TaskController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // GET: api/Task/Backlog/Partner
        [HttpGet]
        [Route("BacklogPartner")]
        public IActionResult GetBacklogTasksPartner()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.PartnerUser).
                Where(t => t.AssignedTime > t.DueTime && t.PartnerUserId == user.Id).ToList();

            return Ok(tasks);
        }

        // GET: api/Task/Backlog/Parelegal
        [HttpGet]
        [Route("BacklogParelegal")]
        public IActionResult GetBacklogTasksParelegal()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.ParelegalUser).
                Where(t => t.AssignedTime > t.DueTime && t.ParelegalUserId == user.Id).ToList();

            return Ok(tasks);
        }

        // GET: api/Task/ToDo/Partner
        [HttpGet]
        [Route("ToDoPartner")]
        public IActionResult GetToDoTasksPartner()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.PartnerUser).
                Where(t => t.InProgress == false && t.CompletedTime == null).ToList();
            return Ok(tasks);
        }

        // GET: api/Task/ToDo/Parelegal
        [HttpGet]
        [Route("ToDoParelegal")]
        public IActionResult GetToDoTasksParelegal()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.ParelegalUser).
                Where(t => t.InProgress == false && t.CompletedTime == null).ToList();
            return Ok(tasks);
        }

        // GET: api/Task/InProgress/Partner
        [HttpGet]
        [Route("InProgressPartner")]
        public IActionResult GetInProgressTasksPartner()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.PartnerUser).
                Where(t => t.InProgress == true).ToList();
            return Ok(tasks);
        }

        // GET: api/Task/InProgress/Parelegal
        [HttpGet]
        [Route("InProgressParelegal")]
        public IActionResult GetInProgressTasksParelegal()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.ParelegalUser).
                Where(t => t.InProgress == true).ToList();
            return Ok(tasks);
        }

        // GET: api/Task/Complete/Partner
        [HttpGet]
        [Route("CompletePartner")]
        public IActionResult GetCompleteTasksPartner()
        {
            var user = userService.GetUser(User);
            var tasks = context.Tasks.Include(t => t.PartnerUser).
                Where(t => t.CompletedTime != null).ToList();
            return Ok(tasks);
        }

        // GET: api/Task/Complete/Parelegal
        [HttpGet]
        [Route("CompleteParelegal")]
        public IActionResult GetCompleteTasksParelegal()
        {
            var tasks = context.Tasks.Include(t => t.ParelegalUser).
                Where(t => t.CompletedTime != null).ToList();
            return Ok(tasks);
        }

        //Get the related case for the specific task        
        [HttpGet]
        [Route("RelatedCase")]
        public IActionResult RelatedCase(int caseId)
        {
            // Find the case by its ID
            var relatedCase = context.Cases.Where(c => c.Id == caseId).FirstOrDefault();
            // Check if the case exists
            if (relatedCase == null)
            {
                throw new Exception("Related case not found");
            }
            // Return the related case
            return Ok(relatedCase);
        }

        //Get the related event for the specific task
        [HttpGet]
        [Route("RelatedEvent")]
        public IActionResult RelatedEvent(int eventId)
        {
            // Find the event by its ID
            var relatedEvent = context.Events.Where(c => c.Id == eventId).FirstOrDefault();
            // Check if the event exists
            if (relatedEvent == null)
            {
                throw new Exception("Related event not found");
            }
            // Return the related event
            return Ok(relatedEvent);
        }

        //Get the related document for the specific task
        [HttpGet]
        [Route("RelatedDocument")]
        public IActionResult RelatedDocument(int documentId)
        {
            // Find the document by its ID
            var relatedDocument = context.Documents.Where(c => c.Id == documentId).ToList();
            // Check if the event exists
            if (relatedDocument == null)
            {
                throw new Exception("Related document not found");
            }
            // Return the related event
            return Ok(relatedDocument);
        }

        //Create task by partner
        [HttpPost]
        [Route("")]
        public IActionResult CreateTask([FromBody] TaskPartnerCreateDto dto)
        {
            var user = userService.GetUser(User);
            var partner = context.Partners.Where(c => c.UserId == user.Id).FirstOrDefault();

            var task = new Models.Task
            {
                PartnerUserId = user.Id,
                ParelegalUserId = dto.IsAssignedParalegal ? partner.ParalegalUserId : null,
                CaseId = dto.CaseId,
                EventId = dto.EventId,
                DocumentId = dto.DocumentId,
                Title = dto.Title,
                Description = dto.Description,
                AssignedTime = DateTime.Now,
                DueTime = dto.DueTime,
                InProgress = false
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            return Ok(new Response { Message = "New task created successfully" });
        }

        //Update task by partner
        [HttpPut]
        [Route("")]
        public IActionResult UpdateTask([FromBody] TaskPartnerUpdateDto dto)
        {
            var user = userService.GetUser(User);
            var existingTask = context.Tasks.Where(t => t.Id == dto.TaskId).FirstOrDefault();

            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;

            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        //Update task completed time to now and task progress in progress - complete
        [HttpPut]
        [Route("Complete")]
        public IActionResult UpdateCompletedTime(int taskId)
        {
            var user = userService.GetUser(User);
            var existingTask = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            existingTask.CompletedTime = DateTime.Now;
            existingTask.InProgress = false;

            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        //Update task start InProgress   to do - in progress
        [HttpPut]
        [Route("Start")]
        public IActionResult UpdateStartedProgress(int taskId)
        {
            var user = userService.GetUser(User);
            var existingTask = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            existingTask.InProgress = true;

            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        //Delete task by partner
        [HttpDelete]
        [Route("{TaskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            var user = userService.GetUser(User);
            var deleteTask = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            context.Tasks.Remove(deleteTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Document deleted successfully" });
        }

        public class TaskPartnerCreateDto
        {
            public int CaseId { get; set; }
            public int EventId { get; set; }
            public int DocumentId { get; set; }
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
            public DateTime AssignedTime { get; set; }
            public DateTime DueTime { get; set; }
            public bool IsAssignedParalegal { get; set; }
        }

        public class TaskPartnerUpdateDto
        {
            public int TaskId { get; set; }
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }

        public class CompletedTimeDto
        {
            public int TaskId { get; set; }
            public DateTime? CompletedTime { get; set; }
        }
    }
}