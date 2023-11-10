using Modernize_API.Authentication;
using Modernize_API.Dto;
using Modernize_API.Models;
using Modernize_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Modernize_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : BaseController
    {
        public PageController(IConfiguration configuration, UserService userService, ModernizeDBContext context) : base(configuration, userService, context)
        {
        }

        [HttpGet]
        [Route("PageList")]
        public IActionResult GetPageList()
        {
            var l = context.Pages.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("AccessPageList/{Id}")]
        public IActionResult GetAccessPageList(int id)
        {
            var user = userService.GetUser(id);
            var userRoleID = user.UserRoleId;
            var accessList = new List<string>();

            if (userRoleID != null)
            {
                var access = context.RoleAccessPages.Include(a => a.Page).Where(a => a.UserRoleId == userRoleID).ToList();
                if (access != null)
                {
                    foreach (var item in access)
                    {
                        accessList.Add(item.Page.Name);
                    }
                }
                Console.WriteLine(accessList);
                return Ok(accessList);
            }
            throw new Exception("Access pages not found.");
        }

/*        [HttpPost]
        [Route("")]
        public IActionResult Add(ManagePageDto dto)
        {
            foreach (var item in dto.List)
            {
                var exist = context.UserRoles.Where(a => a.RoleId == dto.Id && a.PageListId == item.Id).Any();
                if (exist == false)
                {
                    var roleAccess = new RoleAccessPage();
                    roleAccess.RoleId = dto.Id;
                    roleAccess.PageListId = item.Id;
                    context.RoleAccessPages.Add(roleAccess);
                    context.SaveChanges();
                }

            }
            return Ok(dto);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var user = userService.GetUser(User);
            var access = context.RoleAccessPages.Where(d => d.Id == id).FirstOrDefault();

            context.RoleAccessPages.Remove(access);
            context.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Access Page removed successfully!" });
        }*/

        public class ManagePageDto
        {
            public int? Id { get; set; }
            public List<Page>? List { get; set; }
        }
    }
}