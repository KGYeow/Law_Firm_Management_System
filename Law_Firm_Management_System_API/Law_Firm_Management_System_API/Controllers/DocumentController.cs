using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var l = context.Documents.Include(a => a.Category).Include(a => a.Case).Include(a => a.PartnerUser).ToList()
                .Select(x => new { id = x.Id, categoryId = x.CategoryId, categoryName = x.Category.Name, caseId = x.CaseId, caseName = x.Case?.Name, name = x.Name, modifiedDate = x.ModifiedDate, modifiedBy = x.PartnerUser.FullName, type = x.Type, isArchived = x.IsArchived });
            return Ok(l);
        }
    }
}