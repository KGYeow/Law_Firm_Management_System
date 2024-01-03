using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Law_Firm_Management_System_API.Controllers.DocumentController;

namespace Law_Firm_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : BaseController
    {
        public ArchiveController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of archived documents.
        [HttpGet]
        [Route("")]
        public IActionResult GetArchiveList()
        {
            var l = context.Documents.Where(a => a.IsArchived == true).ToList()
                .Select(x => new { id = x.Id, name = x.Name });
            return Ok(l);
        }

        // Get the list of filtered archived documents.
        [HttpGet]
        [Route("Filter")]
        public IActionResult GetArchiveFilteredList([FromQuery] DocFilterDto dto)
        {
            var l = context.Documents.Include(a => a.Category).Include(a => a.Case).Include(a => a.PartnerUser).Where(a => a.IsArchived == true).ToList()
                .Select(x => new { id = x.Id, categoryId = x.CategoryId, categoryName = x.Category.Name, caseId = x.CaseId, caseName = x.Case?.Name, name = x.Name, modifiedDate = x.ModifiedDate, userId = x.PartnerUserId, modifiedBy = x.PartnerUser.FullName, type = x.Type, isArchived = x.IsArchived });

            if (dto.DocId != null)
                l = l.Where(a => a.id == dto.DocId);
            if (dto.CategoryId != null)
                l = l.Where(a => a.categoryId == dto.CategoryId);
            if (dto.CaseId != null)
                l = l.Where(a => a.caseId == dto.CaseId);
            if (dto.UserId != null)
                l = l.Where(a => a.userId == dto.UserId);
            l.ToList();

            return Ok(l);
        }

        // Restore the archived document.
        [HttpPut]
        [Route("Restore/{DocId}")]
        public IActionResult Restore(int docId)
        {
            var user = userService.GetUser(User);
            var archivedDoc = context.Documents.Where(a => a.Id == docId).FirstOrDefault();

            archivedDoc.IsArchived = false;
            context.Documents.Update(archivedDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document restored successfully" });
        }

        // Delete the archived document permanently.
        [HttpPut]
        [Route("{DocId}")]
        public IActionResult Delete(int docId)
        {
            var user = userService.GetUser(User);
            var archivedDoc = context.Documents.Where(a => a.Id == docId).FirstOrDefault();
            context.Documents.Remove(archivedDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document deleted successfully" });
        }
    }
}