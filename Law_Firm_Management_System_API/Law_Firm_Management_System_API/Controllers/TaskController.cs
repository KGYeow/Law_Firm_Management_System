using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        public TaskController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the backlog tasks.
        [HttpGet]
        [Route("Backlog")]
        public IActionResult GetBacklogTasks()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();
            var backlogTasks = context.Tasks.Where(t => t.DueTime < DateTime.Now && t.CompletedTime == null);

            if (role.Name == "Partner")
                backlogTasks = backlogTasks.Where(t => t.PartnerUserId == user.Id);
            else
                backlogTasks = backlogTasks.Where(t => t.ParalegalUserId == user.Id);

            return Ok(backlogTasks.ToList());
        }

        // Get the to do tasks.
        [HttpGet]
        [Route("ToDo")]
        public IActionResult GetToDoTasks()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();
            var toDoTasks = context.Tasks.Where(t => t.InProgress == false && t.CompletedTime == null && t.DueTime > DateTime.Now);

            if (role.Name == "Partner")
                toDoTasks = toDoTasks.Where(t => t.PartnerUserId == user.Id);
            else
                toDoTasks = toDoTasks.Where(t => t.ParalegalUserId == user.Id);

            return Ok(toDoTasks.ToList());
        }

        // Get the in-progress tasks.
        [HttpGet]
        [Route("InProgress")]
        public IActionResult GetInProgressTasks()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();
            var inProgressTasks = context.Tasks.Where(t => t.InProgress == true && t.DueTime > DateTime.Now);

            if (role.Name == "Partner")
                inProgressTasks = inProgressTasks.Where(t => t.PartnerUserId == user.Id);
            else
                inProgressTasks = inProgressTasks.Where(t => t.ParalegalUserId == user.Id);

            return Ok(inProgressTasks.ToList());
        }

        // Get the complete tasks.
        [HttpGet]
        [Route("Complete")]
        public IActionResult GetCompleteTasks()
        {
            var user = userService.GetUser(User);
            var role = context.UserRoles.Where(a => a.Id == user.UserRoleId).FirstOrDefault();
            var completeTasks = context.Tasks.Where(t => t.CompletedTime != null);

            if (role.Name == "Partner")
                completeTasks = completeTasks.Where(t => t.PartnerUserId == user.Id);
            else
                completeTasks = completeTasks.Where(t => t.ParalegalUserId == user.Id);

            return Ok(completeTasks.ToList());
        }

        // Get the specific task's information.
        [HttpGet]
        [Route("Info/{TaskId}")]
        public IActionResult GetTaskInformation(int taskId)
        {
            var task = context.Tasks.Include(a => a.ParalegalUser).Include(a => a.Document).Include(a => a.Case).Include(a => a.Event).Include(a => a.Document).Where(a => a.Id == taskId)
                .Select(x => new { id = x.Id, paralegalUserId = x.ParalegalUserId, paralegalFullName = x.ParalegalUser.FullName, caseId = x.CaseId, caseName = x.Case.Name, eventId = x.EventId, eventName = x.Event.Name, docId = x.DocumentId, docName = x.Document.Name, title = x.Title, description = x.Description, assignedTime = x.AssignedTime, completedTime = x.CompletedTime, dueTime = x.DueTime, inProgress = x.InProgress })
                .FirstOrDefault();

            return Ok(task);
        }

        // Create task by partner
        [HttpPost]
        [Route("")]
        public IActionResult CreateTask([FromBody] TaskCreateDto dto)
        {
            var user = userService.GetUser(User);
            var partner = context.Partners.Where(c => c.UserId == user.Id).FirstOrDefault();

            var task = new Models.Task
            {
                PartnerUserId = user.Id,
                ParalegalUserId = dto.IsAssignedParalegal ? partner.ParalegalUserId : null,
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

        // Update the task by partner.
        [HttpPut]
        [Route("")]
        public IActionResult UpdateTask([FromBody] TaskUpdateDto dto)
        {
            var user = userService.GetUser(User);
            var partner = context.Partners.Where(c => c.UserId == user.Id).FirstOrDefault();
            var existingTask = context.Tasks.Where(t => t.Id == dto.TaskId).FirstOrDefault();

            existingTask.ParalegalUserId = dto.IsAssignedParalegal ? partner.ParalegalUserId : null;
            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.CaseId = dto.CaseId;
            existingTask.EventId = dto.EventId;
            existingTask.DocumentId = dto.DocumentId;
            existingTask.DueTime = dto.DueTime;

            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        // Update the task completed time.
        [HttpPut]
        [Route("CompleteTask/{TaskId}")]
        public IActionResult UpdateCompletedTime(int taskId)
        {
            var existingTask = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            existingTask.CompletedTime = DateTime.Now;
            existingTask.InProgress = false;
            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        // Accept and update the task to start the progress.
        [HttpPut]
        [Route("StartProgress/{TaskId}")]
        public IActionResult UpdateToInProgress(int taskId)
        {
            var existingTask = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            existingTask.InProgress = true;
            context.Tasks.Update(existingTask);
            context.SaveChanges();

            return Ok(new Response { Message = "Task updated successfully" });
        }

        // Delete task by partner
        [HttpDelete]
        [Route("Delete/{TaskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            var user = userService.GetUser(User);
            var task = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            context.Tasks.Remove(task);
            context.SaveChanges();

            return Ok(new Response { Message = "Task deleted successfully" });
        }

        public class TaskCreateDto
        {
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
            public DateTime DueTime { get; set; }
            public int? CaseId { get; set; }
            public int? EventId { get; set; }
            public int? DocumentId { get; set; }
            public bool IsAssignedParalegal { get; set; }
        }

        public class TaskUpdateDto
        {
            public int TaskId { get; set; }
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
            public DateTime DueTime { get; set; }
            public int? CaseId { get; set; }
            public int? EventId { get; set; }
            public int? DocumentId { get; set; }
            public bool IsAssignedParalegal { get; set; }
        }

        public class CompletedTimeDto
        {
            public int TaskId { get; set; }
            public DateTime? CompletedTime { get; set; }
        }
    }
}