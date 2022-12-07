using AppDbContext.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ecom.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IConfiguration Configuration;
        protected readonly IWebHostEnvironment hostEnvironment;

        public BaseController(IUnitOfWork unitOfWork, IConfiguration configuration
            , IWebHostEnvironment _hostEnvironment)
        {
            hostEnvironment = _hostEnvironment;
            Configuration = configuration;
            UnitOfWork = unitOfWork;
        }
    }
}
