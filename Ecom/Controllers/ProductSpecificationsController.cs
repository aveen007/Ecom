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
        public IActionResult Index()
        {

            var productSpecifications = _unitOfWork.ProductSpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var productSpecificationsViewModels = _mapper.Map<List<ProductSpecificationViewModel>>(productSpecifications);

            return View(productSpecificationsViewModels);

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
                return RedirectToAction(nameof(Index));
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
            return View(productSpecification);
        }

        // POST: ProductSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Specification,ValueType")] ProductSpecification productSpecification)
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecificationExists(productSpecification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productSpecification);
        }

        // GET: ProductSpecifications/Delete/5
        public IActionResult Delete(int? id)
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

            return View(productSpecification);
        }

        // POST: ProductSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            _unitOfWork.ProductSpecificationRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSpecificationExists(int id)
        {
            return _unitOfWork.CategorySpecificationRepo.IsExist(id);
        }
    }
}
