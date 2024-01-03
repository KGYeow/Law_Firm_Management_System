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
    public class DocumentController : BaseController
    {
        public DocumentController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context) : base(configuration, userService, context)
        {
        }

        // Get the list of document category.
        [HttpGet]
        [Route("Category")]
        public IActionResult GetDocumentCategory()
        {
            var l = context.DocumentCategories.OrderBy(a => a.Name).ToList();
            return Ok(l);
        }

        // Get the list of documents.
        [HttpGet]
        [Route("")]
        public IActionResult GetDocumentList()
        {
            var l = context.Documents.Where(a => a.IsArchived == false).ToList()
                .Select(x => new { id = x.Id, name = x.Name });
            return Ok(l);
        }

        // Get the list of filtered documents.
        [HttpGet]
        [Route("Filter")]
        public IActionResult GetDocumentFilteredList([FromQuery] DocFilterDto dto)
        {
            var l = context.Documents.Include(a => a.Category).Include(a => a.Case).Include(a => a.PartnerUser).Where(a => a.IsArchived == false).ToList()
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

        // Get the document.
        [HttpGet]
        [Route("GetDocument/{DocId}")]
        public IActionResult GetDocument(int docId)
        {
            var document = context.Documents.Where(d => d.Id == docId).FirstOrDefault();
            if (document.Attachment == null)
                throw new Exception("The document is not found in the system");

            return Ok(document.Attachment);
        }

        // Create a new document.
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] DocDto dto)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Name == dto.Name).Any();

            if (existingDoc)
                throw new Exception("Document already exist");

            var document = new Document
            {
               Name = dto.Name,
                CategoryId = dto.CategoryId,
                CaseId = dto.CaseId,
                PartnerUserId = user.Id,
                ModifiedDate = DateTime.Now,
                Attachment = dto.Attachment,
                Type = dto.Type,
                IsArchived = false 
            };
            context.Documents.Add(document);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New document created successfully" });
        }

        // Update the existing document.
        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] DocEditDto dto)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Id == dto.DocId).FirstOrDefault();

            existingDoc.Name = dto.Name;
            existingDoc.CategoryId = dto.CategoryId;
            existingDoc.CaseId = dto.CaseId;
            existingDoc.PartnerUserId = user.Id;
            existingDoc.ModifiedDate = DateTime.Now;
            existingDoc.Attachment = dto.Attachment;
            existingDoc.Type = dto.Type;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document updated successfully" });
        }

        // Rename the existing document.
        [HttpPut]
        [Route("Rename")]
        public IActionResult Rename([FromBody] DocRenameDto dto)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Id == dto.DocId).FirstOrDefault();

            existingDoc.Name = dto.Name;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document renamed successfully" });
        }

        // Archive the existing document.
        [HttpPut]
        [Route("Archive/{DocId}")]
        public IActionResult Archive(int docId)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Id == docId).FirstOrDefault();

            existingDoc.IsArchived = true;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document archived successfully" });
        }

        public class DocFilterDto
        {
            public int? DocId { get; set; }
            public int? CategoryId { get; set; }
            public int? CaseId { get; set; }
            public int? UserId { get; set; }
        }

        public class DocDto
        {
            public string Name { get; set; } = null!;
            public int CategoryId { get; set; }
            public int? CaseId { get; set; }
            public byte[] Attachment { get; set; } = null!;
            public string Type { get; set; } = null!;
        }

        public class DocEditDto
        {
            public int DocId { get; set; }
            public string Name { get; set; } = null!;
            public int CategoryId { get; set; }
            public int? CaseId { get; set; }
            public byte[] Attachment { get; set; } = null!;
            public string Type { get; set; } = null!;
        }

        public class DocRenameDto
        {
            public int DocId { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}