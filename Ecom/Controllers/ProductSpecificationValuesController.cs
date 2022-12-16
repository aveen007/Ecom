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

namespace Ecom.Controllers
{
    public class ProductSpecificationValuesController : BaseController
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductSpecificationValuesController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;

        }

        // GET: ProductSpecificationValues
        public IActionResult Index()
        {

            var ProductSpecificationValues = _unitOfWork.ProductSpecificationValueRepo.GetAll(includeProperties: "Product,Specification").ToList();
            return View(ProductSpecificationValues);
        }

        // GET: ProductSpecificationValues/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecificationValues = _unitOfWork.ProductSpecificationValueRepo.GetAll(includeProperties: "Product,Specification").ToList();
            var productSpecificationValue = new ProductSpecificationValue();
            for (int i = 0; i < productSpecificationValues.Count; i++)
            {
                var temp = productSpecificationValues[i];
                if (temp.Id == id)
                {
                    productSpecificationValue = temp;
                }

            }
            if (productSpecificationValue == null)
            {
                return NotFound();
            }

            return View(productSpecificationValue);
        }

        // GET: ProductSpecificationValues/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_unitOfWork.ProductRepo.GetAll().ToList(), "Id", "Id");
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.ProductSpecificationRepo.GetAll().ToList(), "Id", "Specification");
            return View();
        }

        // POST: ProductSpecificationValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,SpecificationId,Value")] ProductSpecificationValue productSpecificationValue)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductSpecificationValueRepo.Add(productSpecificationValue);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_unitOfWork.ProductRepo.GetAll().ToList(), productSpecificationValue.ProductId);
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.ProductSpecificationRepo.GetAll().ToList(), productSpecificationValue.SpecificationId);
            return View(productSpecificationValue);
        }

        // GET: ProductSpecificationValues/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecificationValue = _unitOfWork.ProductSpecificationValueRepo.Get(id.Value);
            if (productSpecificationValue == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_unitOfWork.ProductRepo.GetAll().ToList(), "Id", "ImageLink", productSpecificationValue.ProductId);
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.ProductSpecificationRepo.GetAll().ToList(), "Id", "Specification", productSpecificationValue.SpecificationId);
            return View(productSpecificationValue);
        }

        // POST: ProductSpecificationValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,SpecificationId,Value")] ProductSpecificationValue productSpecificationValue)
        {
            if (id != productSpecificationValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProductSpecificationValueRepo.Update(productSpecificationValue);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecificationValueExists(productSpecificationValue.Id))
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
            ViewData["ProductId"] = new SelectList(_unitOfWork.ProductRepo.GetAll().ToList(), "Id", "ImageLink", productSpecificationValue.ProductId);
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.ProductSpecificationRepo.GetAll().ToList(), "Id", "Specification", productSpecificationValue.SpecificationId);
            return View(productSpecificationValue);
        }

        // GET: ProductSpecificationValues/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecificationValues = _unitOfWork.ProductSpecificationValueRepo.GetAll(includeProperties: "Product,Specification").ToList();
            var productSpecificationValue = new ProductSpecificationValue();
            for (int i = 0; i < productSpecificationValues.Count; i++)
            {
                var temp = productSpecificationValues[i];
                if (temp.Id == id)
                {
                    productSpecificationValue = temp;
                }

            }
            if (productSpecificationValue == null)
            {
                return NotFound();
            }

            return View(productSpecificationValue);
        }

        // POST: ProductSpecificationValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.ProductSpecificationValueRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSpecificationValueExists(int id)
        {
            return _unitOfWork.ProductSpecificationValueRepo.IsExist(id);
        }
    }
}
