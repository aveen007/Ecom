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

namespace Ecom.Controllers
{
    public class PromotionsController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public PromotionsController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;

        }

        // GET: Promotions
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var promotions = _unitOfWork.PromotionRepo.GetAll();

            var promotionsViewModels = _mapper.Map<List<PromotionViewModel>>(promotions);
            var promotionVMs = from s in promotionsViewModels select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                promotionVMs = promotionVMs.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name":
                    promotionVMs = promotionVMs.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    promotionVMs = promotionVMs.OrderBy(s => s.Id);
                    break;

                default:
                    promotionVMs = promotionVMs.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(promotionVMs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = _unitOfWork.PromotionRepo.Get(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }

            ViewData["CategoryPromotion"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SelectedCategoryPromotion"] = new SelectList(_unitOfWork.CategoryPromotionRepo.GetAll(filter: e => e.PromotionId == id).ToList(), "Id", "CategoryId");

            var promotionViewModel = _mapper.Map<PromotionViewModel>(promotion);

            return View(promotionViewModel);
        }

        // GET: Promotions/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryPromotion"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DiscountRate,StartDate,EndDate")] Promotion promotion, [Bind("Categories")] String Categories)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PromotionRepo.Add(promotion);

                await _unitOfWork.SaveAsync();
                if (Categories != null)
                {
                    var tmp = Categories.Substring(1, Categories.Length - 2);
                    var tmps = tmp.Split(',');
                    var categories = new List<int>();
                    foreach (var catSpec in tmps)
                    {
                        var t = catSpec.Substring(1, catSpec.Length - 2);
                        categories.Add(Int32.Parse(t));
                    }

                    foreach (var t in categories)
                    {
                        CategoryPromotion categoryPromotion = new CategoryPromotion();
                        categoryPromotion.PromotionId = promotion.Id;
                        categoryPromotion.CategoryId = t;

                        _unitOfWork.CategoryPromotionRepo.Add(categoryPromotion);
                    }

                    await _unitOfWork.SaveAsync();
                }
                Notify("Promotion created successfully!!");

                return RedirectToAction(nameof(Index), new
                {
                    page = 1,

                });

            }

            var promotionViewModel = _mapper.Map<PromotionViewModel>(promotion);

            return View(promotionViewModel);
        }

        // GET: Promotions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var promotion = _unitOfWork.CategoryRepo.Get(id.Value);

            if (promotion == null)
            {
                return NotFound();
            }

            ViewData["CategoryPromotion"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SelectedCategoryPromotion"] = new SelectList(_unitOfWork.CategoryPromotionRepo.GetAll(filter: e => e.PromotionId == id).ToList(), "Id", "CategoryId");

            var promotionViewModel = _mapper.Map<CategoryViewModel>(promotion);

            return View(promotionViewModel);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DiscountRate,StartDate,EndDate")] Promotion promotion, [Bind("Categories")] String Categories)
        {
            if (id != promotion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.PromotionRepo.Update(promotion);

                    await _unitOfWork.SaveAsync();

                    _unitOfWork.CategoryPromotionRepo.Delete(filter: e => e.PromotionId == promotion.Id);

                    await _unitOfWork.SaveAsync();

                    if (Categories != null)
                    {
                        var tmp = Categories.Substring(1, Categories.Length - 2);
                        var tmps = tmp.Split(',');
                        var categories = new List<int>();
                        foreach (var catSpec in tmps)
                        {
                            var t = catSpec.Substring(1, catSpec.Length - 2);
                            categories.Add(Int32.Parse(t));
                        }

                        foreach (var t in categories)
                        {
                            CategoryPromotion categoryPromotion = new CategoryPromotion();
                            categoryPromotion.PromotionId = promotion.Id;
                            categoryPromotion.CategoryId = t;

                            _unitOfWork.CategoryPromotionRepo.Add(categoryPromotion);
                        }
                        await _unitOfWork.SaveAsync();
                    }
                    Notify("Edit saved successfully!!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.Id))
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

            var promotionViewModel = _mapper.Map<PromotionViewModel>(promotion);

            return View(promotionViewModel);
        }

        // GET: Promotions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var promotion = _unitOfWork.PromotionRepo.Get(id.Value);

            if (promotion == null)
            {
                return NotFound();
            }

            var promotionViewModel = _mapper.Map<PromotionViewModel>(promotion);

            return View(promotionViewModel);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.CategoryPromotionRepo.Delete(filter: e => e.PromotionId == id);

                await _unitOfWork.SaveAsync();

                _unitOfWork.PromotionRepo.Delete(id);

                await _unitOfWork.SaveAsync();
                Notify("promotion deleted successfully!!");
            }
            catch (Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
            }
            return RedirectToAction(nameof(Index), new { page = 1 });
        }

        [Authorize(Roles = "Admin")]
        private bool PromotionExists(int id)
        {
            return _unitOfWork.PromotionRepo.IsExist(id);
        }
    }
}
