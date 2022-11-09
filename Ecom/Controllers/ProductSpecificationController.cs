using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class ProductSpecificationController : BaseController
    {
        public ProductSpecificationController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.ProductSpecificationRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Product Specification Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductSpecificationViewModel psvm)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductSpecificationRepo.Add(new AppDbContext.Models.ProductSpecification
                {
                    Specification = "Color",
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(psvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
