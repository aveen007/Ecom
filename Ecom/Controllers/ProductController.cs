using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.ProductRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Product Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductRepo.Add(new AppDbContext.Models.Product
                {
                    Name = "Keurig K-Mini Brewer",
                    Price = 49.99m,
                    Sku = "6358476",
                    CategoryId = 1,
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(pvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
