using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;
        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
