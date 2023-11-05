using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iRMS_API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AppController : ControllerBase
    {
        IWebHostEnvironment hostEnvironment;
        public AppController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context == null) return Problem();

            if (hostEnvironment.IsDevelopment())
            {
                return Ok(new
                {
                    Error = true,
                    Message = context.Error.Message,
                    Details = Problem(detail: context.Error.StackTrace, title: context.Error.Message).Value
                });
            }

            return Ok(new
            {
                Error = true,
                Message = context.Error.Message,
            });
        }
    }
}
