using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class CategoriesController : BaseController
    {
  
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public CategoriesController( IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
         
        }
        // GET: Categories
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var categories = _unitOfWork.CategoryRepo.GetAll();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categories);
            var categoryVMs = from s in categoriesViewModels
                             select s;
            switch (sortOrder)
            {
                case "Name":
                    categoryVMs = categoryVMs.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    categoryVMs = categoryVMs.OrderBy(s => s.Id);
                    break;
                
                default:
                    categoryVMs = categoryVMs.OrderBy(s => s.Name);
                    break;
            }
            return View(categoryVMs);

        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.CategoryRepo.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["CategorySpecification"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "SpecificationName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(category);
                await _unitOfWork.SaveAsync();
                Notify("category created successfully!!");

                return RedirectToAction(nameof(Index));
             
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.CategoryRepo.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CategoryRepo.Update(category);
                    await _unitOfWork.SaveAsync();
                    Notify("Edit saved successfully!!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);

                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.CategoryRepo.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.CategoryRepo.Delete(id);
                await _unitOfWork.SaveAsync();
                Notify("category deleted successfully!!");
            }
            catch(Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
            }
                return RedirectToAction(nameof(Index));
           

        }

        private bool CategoryExists(int id)
        {
            return _unitOfWork.CategoryRepo.IsExist(id);

        }
    }
}
