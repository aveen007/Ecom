using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class CategorySpecificationValueController : BaseController
    {
        public CategorySpecificationValueController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.CategorySpecificationValueRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Category Specification Value Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategorySpecificationValueViewModel csvvm)
        {
            if (ModelState.IsValid)
            {
                _uow.CategorySpecificationValueRepo.Add(new AppDbContext.Models.CategorySpecificationValue
                {
                    CategoryId = 1,
                    SpecificationId = 1,
                    Value = "4.5\"D x 11.3\"W x 12.1\"H",
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(csvvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
