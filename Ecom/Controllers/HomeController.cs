using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Ecom.Models;
using Ecom.Services;

namespace Ecom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISingletonRnd singletonService;
        private ITransientRnd transientService;
        private IScopedRnd scopedService;

        public HomeController(ILogger<HomeController> logger,
              ISingletonRnd _SingletonService,
            ITransientRnd _transientService,
            IScopedRnd _scopedService)
        {
            _logger = logger;
            singletonService = _SingletonService;
            transientService = _transientService;
            scopedService = _scopedService;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Singleton()
        {
            ViewBag.ServiceType = "Singleton";
            return View("ServicesView", singletonService.GetRandom());
        }

        public IActionResult Transient()
        {
            ViewBag.ServiceType = "Transient";
            return View("ServicesView", transientService.GetRandom());
        }

        public IActionResult Scoped()
        {
            ViewBag.ServiceType = "Scoped";
            return View("ServicesView", scopedService.GetRandom());
        }

    }
}
