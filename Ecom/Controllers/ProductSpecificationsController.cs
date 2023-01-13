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
using PagedList;

namespace Ecom.Controllers
{
    public class ProductSpecificationsController : BaseController
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        public ProductSpecificationsController (IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
        }

        // GET: ProductSpecifications
        public IActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;

            var productSpecifications = _unitOfWork.ProductSpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var productSpecificationsViewModels = _mapper.Map<List<ProductSpecificationViewModel>>(productSpecifications);
            var ProductSpecs = from s in productSpecificationsViewModels
                               select s;
            switch (sortOrder)
            {
                case "Name":
                    ProductSpecs = ProductSpecs.OrderByDescending(s => s.SpecificationName);
                    break;
                case "Date":
                    ProductSpecs = ProductSpecs.OrderBy(s => s.Id);
                    break;

                default:
                    ProductSpecs = ProductSpecs.OrderBy(s => s.SpecificationName);
                    break;
            }

            


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(ProductSpecs.ToPagedList(pageNumber, pageSize));

        }

        // GET: ProductSpecifications/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecifications = _unitOfWork.ProductSpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var productSpecification = new ProductSpecification();
            for (int i = 0; i < productSpecifications.Count; i++)
            {
                var temp = productSpecifications[i];
                if (temp.Id == id)
                {
                    productSpecification = productSpecifications[i];
                }

            }
            if (productSpecification == null)
            {
                return NotFound();
            }
            var productSpecificationsViewModel = _mapper.Map<ProductSpecificationViewModel>(productSpecification);

            return View(productSpecificationsViewModel);
        }

        // GET: ProductSpecifications/Create
        public IActionResult Create()
        {
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName");
            return View();
        }

        // POST: ProductSpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SpecificationName,Description,ValueTypeId")] ProductSpecification productSpecification)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductSpecificationRepo.Add(productSpecification);
                await _unitOfWork.SaveAsync();
                Notify("Product specification created successfully!!");
                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
            }
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName", productSpecification.ValueTypeId);

            var productSpecificationsViewModel = _mapper.Map<ProductSpecificationViewModel>(productSpecification);

            return View(productSpecificationsViewModel);
        }

        // GET: ProductSpecifications/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var productSpecification = _unitOfWork.ProductSpecificationRepo.Get(id.Value);
            if (productSpecification == null)
            {
                return NotFound();
            }
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName", productSpecification.ValueTypeId);

            var productSpecificationsViewModel = _mapper.Map<ProductSpecificationViewModel>(productSpecification);

            return View(productSpecificationsViewModel);
        }

        // POST: ProductSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpecificationName,Description,ValueTypeId")] ProductSpecification productSpecification)
        {
            if (id != productSpecification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProductSpecificationRepo.Update(productSpecification);
                    await _unitOfWork.SaveAsync();
                    Notify("Edit saved successfully!!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecificationExists(productSpecification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
                    }
                }
                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
            }
            var productSpecificationsViewModel = _mapper.Map<ProductSpecificationViewModel>(productSpecification);

            return View(productSpecificationsViewModel);
        }

        // GET: ProductSpecifications/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecifications = _unitOfWork.ProductSpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var productSpecification = new ProductSpecification();
            for (int i = 0; i < productSpecifications.Count; i++)
            {
                var temp = productSpecifications[i];
                if (temp.Id == id)
                {
                    productSpecification = productSpecifications[i];
                }

            }
            if (productSpecification == null)
            {
                return NotFound();
            }
            var productSpecificationsViewModel = _mapper.Map<ProductSpecificationViewModel>(productSpecification);

            return View(productSpecificationsViewModel);
        }

        // POST: ProductSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.ProductSpecificationRepo.Delete(id);
                await _unitOfWork.SaveAsync();
                Notify("Product specification deleted successfully!!");
            }
           catch (Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
            }
            return RedirectToAction(nameof(Index), new
            {
                page = 1
            });
        }

        private bool ProductSpecificationExists(int id)
        {
            return _unitOfWork.ProductSpecificationRepo.IsExist(id);
        }
    }
}
