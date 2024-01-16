using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
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
            var l = context.Documents.Include(a => a.Category).Include(a => a.Case).Include(a => a.User).Where(a => a.IsArchived == false).ToList()
                .Select(x => new { id = x.Id, categoryId = x.CategoryId, categoryName = x.Category.Name, caseId = x.CaseId, caseName = x.Case?.Name, name = x.Name, modifiedTime = x.ModifiedTime, userId = x.UserId, modifiedBy = x.User.FullName, type = x.Type, isArchived = x.IsArchived });

            if (dto.DocId != null)
                l = l.Where(a => a.id == dto.DocId);
            if (dto.CategoryId != null)
                l = l.Where(a => a.categoryId == dto.CategoryId);
            if (dto.CaseId != null)
                l = l.Where(a => a.caseId == dto.CaseId);
            l.ToList();

            return Ok(l);
        }

        // Get the document information.
        [HttpGet]
        [Route("Info/{DocId}")]
        public IActionResult GetDocumentInfo(int docId)
        {
            var documentInfo = context.Documents.Include(a => a.Category).Include(a => a.Case).Include(a => a.User).Where(d => d.Id == docId)
                .Select(x => new { id = x.Id, categoryId = x.CategoryId, categoryName = x.Category.Name, caseId = x.CaseId, caseName = x.Case.Name, name = x.Name, createdTime = x.CreatedTime, modifiedTime = x.ModifiedTime, userId = x.UserId, modifiedBy = x.User.FullName, type = x.Type, isArchived = x.IsArchived })
                .FirstOrDefault();
            return Ok(documentInfo);
        }

        // Get the document attachment.
        [HttpGet]
        [Route("GetAttachment/{DocId}")]
        public IActionResult GetDocumentAttachment(int docId)
        {
            var document = context.Documents.Where(d => d.Id == docId).FirstOrDefault();
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
                UserId = user.Id,
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                Attachment = dto.Attachment,
                Type = dto.Type,
                IsArchived = false 
            };
            context.Documents.Add(document);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "New document created successfully" });
        }

        // Update the existing document information.
        [HttpPut]
        [Route("Edit/Info")]
        public IActionResult UpdateInfo([FromBody] DocEditInfoDto dto)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Id == dto.DocId).FirstOrDefault();

            existingDoc.Name = dto.Name;
            existingDoc.CategoryId = dto.CategoryId;
            existingDoc.CaseId = dto.CaseId;
            existingDoc.UserId = user.Id;
            existingDoc.ModifiedTime = DateTime.Now;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document information updated successfully" });
        }

        // Update the existing document attachment.
        [HttpPut]
        [Route("Edit/Attachment")]
        public IActionResult UpdateAttachment([FromBody] DocEditAttachmentDto dto)
        {
            var user = userService.GetUser(User);
            var existingDoc = context.Documents.Where(a => a.Id == dto.DocId).FirstOrDefault();

            existingDoc.Name = Path.ChangeExtension(existingDoc.Name, "." + dto.Extension);
            existingDoc.Attachment = dto.Attachment;
            existingDoc.Type = dto.Type;
            existingDoc.UserId = user.Id;
            existingDoc.ModifiedTime = DateTime.Now;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document file updated successfully" });
        }

        // Archive the existing document.
        [HttpPut]
        [Route("Archive/{DocId}")]
        public IActionResult Archive(int docId)
        {
            var user = userService.GetUser(User);

            context.Tasks.Where(a => a.DocumentId == docId).ToList().ForEach(a => a.DocumentId = null);
            context.SaveChanges();

            var existingDoc = context.Documents.Where(a => a.Id == docId).FirstOrDefault();
            existingDoc.IsArchived = true;
            context.Documents.Update(existingDoc);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Document archived successfully" });
        }

        // Get the list of document names related to a specific case.
        [HttpGet]
        [Route("GetDocumentNamesByCase/{CaseId}")]
        public IActionResult GetDocumentNamesByCase(int caseId)
        {
            var documentNames = context.Documents
                .Where(d => d.CaseId == caseId && d.IsArchived == false)
                .Select(x => new { id = x.Id, documentName = x.Name })
                .ToList();

            return Ok(documentNames);
        }

        public class DocFilterDto
        {
            public int? DocId { get; set; }
            public int? CategoryId { get; set; }
            public int? CaseId { get; set; }
        }

        public class DocDto
        {
            public string Name { get; set; } = null!;
            public int CategoryId { get; set; }
            public int? CaseId { get; set; }
            public byte[] Attachment { get; set; } = null!;
            public string Type { get; set; } = null!;
        }

        public class DocEditInfoDto
        {
            public int DocId { get; set; }
            public string Name { get; set; } = null!;
            public int CategoryId { get; set; }
            public int? CaseId { get; set; }
        }

        public class DocEditAttachmentDto
        {
            public int DocId { get; set; }
            public byte[] Attachment { get; set; } = null!;
            public string Type { get; set; } = null!;
            public string Extension { get; set; } = null!;
        }
    }
}