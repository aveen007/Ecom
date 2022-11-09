using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class ProductSpecificationValueController : BaseController
    {
        public ProductSpecificationValueController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.ProductSpecificationValueRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Product Specification Value Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductSpecificationValueViewModel psvvm)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductSpecificationValueRepo.Add(new AppDbContext.Models.ProductSpecificationValue
                {
                    ProductId = 1,
                    SpecificationId = 1,
                    Value = "Black",
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(psvvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
