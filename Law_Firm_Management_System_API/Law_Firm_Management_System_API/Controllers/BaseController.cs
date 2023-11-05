using Law_Firm_Management_System_API.Models;
using Law_Firm_Management_System_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Law_Firm_Management_System_API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration configuration;
        protected readonly UserService userService;
        protected readonly Law_Firm_Management_System_DBContext context;

        public BaseController(IConfiguration configuration, UserService userService, Law_Firm_Management_System_DBContext context)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.context = context;
        }
    }
}