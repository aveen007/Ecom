using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow) : base(uow)
        {
            
        }

        public IActionResult Index()
        {
            var a = _uow.CategoryRepo.GetAll();
            ViewBag.Msg = "Hello from Index";

            TempData["Message"] = "Hello from Category Index (TempData)";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                _uow.CategoryRepo.Add(new AppDbContext.Models.Category
                {
                    Name = "Kitchen Equipment",
                });
                _uow.SaveChanges();
            }
            else
            {
                return View(cvm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
