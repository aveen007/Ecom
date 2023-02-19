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
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Ecom.Models;
using PagedList;
using Microsoft.Data.SqlClient;

namespace Ecom.Controllers
{
    public class CategoriesController : BaseController
    {
  
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
         
        }
        public IActionResult About(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;


            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;
            ViewBag.CurrentFilter = searchString;

            var categories = _unitOfWork.CategoryRepo.GetAll().ToList();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categories);
            var categoryVMs = from s in categoriesViewModels
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                categoryVMs = categoryVMs.Where(s => s.Name.Contains(searchString)
                                      );
            }



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


            int pageSize = 12;
            int pageNumber = (page ?? 1);

           /* ViewData["Categories"] = categories;*/
            return View(categoryVMs.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Shop(int? id,string sortOrder, string currentFilter, string searchString,string ItemIds, int? page)
        {
            List<int> items = !string.IsNullOrEmpty(ItemIds) ? ItemIds.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            ViewBag.items = items;
          
            if (page == null)
            {
                page = 1;
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;


            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;
            ViewBag.CurrentFilter = searchString;

                var products = _unitOfWork.ProductRepo.GetAll().ToList();
            if (id != null)
            {              
                 products = _unitOfWork.ProductRepo.GetAll(filter: e => e.CategoryId == id).ToList();
               

            }
            var productviewmodels = _mapper.Map<List<ProductViewModel>>(products);
            var productVMs = from s in productviewmodels
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                productVMs = productVMs.Where(s => s.Name.Contains(searchString));
            }
           /* var tempProds = new List<ProductViewModel>(); */
            var tempIds= new List<int>();
            if (!items.Contains(0)&&items.Count!=0)
            {


                foreach (var item in items)

                {

                    var minPrice = (item-1) * 10;
                    var maxPrice = minPrice + 10;
                    var tempProds = productVMs.Where(s => s.Price >= minPrice && s.Price <= maxPrice);
                    foreach (var pro in tempProds)
                    {
                        tempIds.Add(pro.Id);
                    }
                }
                productVMs = productVMs.Where(s => tempIds.Contains(s.Id));
            }
            switch (sortOrder)
            {
                case "Name":
                    productVMs = productVMs.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    productVMs = productVMs.OrderBy(s => s.Id);
                    break;

                default:
                    productVMs = productVMs.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 12;
            int pageNumber = (page ?? 1);

            return View(productVMs.ToPagedList(pageNumber, pageSize));
           
         
            
        }
        // GET: Categories

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page )
        {
            if (page == null)
            {
                page = 1;
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
           

            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page= page;
            ViewBag.CurrentFilter = searchString;

            var categories = _unitOfWork.CategoryRepo.GetAll();

            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categories);
            var categoryVMs = from s in categoriesViewModels
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                categoryVMs = categoryVMs.Where(s => s.Name.Contains(searchString)
                                      );
            }

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


            int pageSize = 3;
            int pageNumber = (page ?? 1);
            
            return View(categoryVMs.ToPagedList(pageNumber, pageSize));
          

        }


        public IActionResult UserIndex()
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewBag.Page = page;
            var categories = _unitOfWork.CategoryRepo.GetAll();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categories);
            var categoryVMs = from s in categoriesViewModels
                              select s;
            /*switch (sortOrder)
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
            */

            //int pageSize = 3;
            //int pageNumber = (page ?? 1);

            return View(categoriesViewModels);


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
            ViewData["CategorySpecification"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "SpecificationName");
            ViewData["SelectedCategorySpecification"] = new SelectList(_unitOfWork.CategorySpecificationRepo.GetAll(filter: e => e.CategoryId == id).ToList(), "Id", "SpecificationId");

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategorySpecification"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "SpecificationName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category, [Bind("Specifications")]  String Specifications)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(category);

                await _unitOfWork.SaveAsync();
                if (Specifications != null)
                {
                    var tmp = Specifications.Substring(1, Specifications.Length - 2);
                    var tmps = tmp.Split(',');
                    var CatSpecs = new List<int>();
                    foreach (var catSpec in tmps)
                    {
                        var t = catSpec.Substring(1, catSpec.Length - 2);
                        CatSpecs.Add(Int32.Parse(t));
                    }

                    foreach (var t in CatSpecs)
                    {
                        CategorySpecification categorySpecification = new CategorySpecification();
                        categorySpecification.CategoryId = category.Id;
                        categorySpecification.SpecificationId = t;

                        _unitOfWork.CategorySpecificationRepo.Add(categorySpecification);
                    }

                    await _unitOfWork.SaveAsync();
                }
                Notify("Category created successfully!!");

                return RedirectToAction(nameof(Index), new
                {
                    page = 1,
                   
                });
             
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
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

            ViewData["CategorySpecification"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "SpecificationName");
            ViewData["SelectedCategorySpecification"] = new SelectList(_unitOfWork.CategorySpecificationRepo.GetAll(filter:e => e.CategoryId == id).ToList(), "Id", "SpecificationId");

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category, [Bind("Specifications")] String Specifications)
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

                    _unitOfWork.CategorySpecificationRepo.Delete(filter: e => e.CategoryId == category.Id);

                    await _unitOfWork.SaveAsync();

                    if (Specifications != null)
                    {
                        var tmp = Specifications.Substring(1, Specifications.Length - 2);
                        var tmps = tmp.Split(',');
                        var CatSpecs = new List<int>();
                        foreach (var catSpec in tmps)
                        {
                            var t = catSpec.Substring(1, catSpec.Length - 2);
                            CatSpecs.Add(Int32.Parse(t));
                        }

                        foreach (var t in CatSpecs)
                        {
                            CategorySpecification categorySpecification = new CategorySpecification();
                            categorySpecification.CategoryId = category.Id;
                            categorySpecification.SpecificationId = t;

                            _unitOfWork.CategorySpecificationRepo.Add(categorySpecification);
                        }
                        await _unitOfWork.SaveAsync();
                    }
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
                return RedirectToAction(nameof(Index), new { page = 1 });
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.CategorySpecificationRepo.Delete(filter: e => e.CategoryId == id);

                await _unitOfWork.SaveAsync();

                _unitOfWork.CategoryRepo.Delete(id);

                await _unitOfWork.SaveAsync();
                Notify("category deleted successfully!!");
            }
            catch(Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
            }
            return RedirectToAction(nameof(Index), new { page = 1 });
           

        }
        [Authorize(Roles = "Admin")]
        private bool CategoryExists(int id)
        {
            return _unitOfWork.CategoryRepo.IsExist(id);

        }
    }
}
