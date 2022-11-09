using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class CategorySpecificationController : BaseController
    {
        public CategorySpecificationController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.CategorySpecificationRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Category Specification Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategorySpecificationViewModel csvm)
        {
            if (ModelState.IsValid)
            {
                _uow.CategorySpecificationRepo.Add(new AppDbContext.Models.CategorySpecification
                {
                    Specification = "Product Dimensions",
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(csvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
