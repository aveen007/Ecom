﻿using System;
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
    public class SpecificationsController : BaseController
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        public SpecificationsController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;

        }
        // GET: Specifications
        public IActionResult Index()
        {
            var specifications = _unitOfWork.SpecificationRepo.GetAll();


            return View(specifications.ToList());
        }

        // GET: Specifications/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = _unitOfWork.SpecificationRepo.Get(id.Value);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // GET: Specifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Specification1,ValueType")] Specification specification)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecificationRepo.Add(specification);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specification);
        }

        // GET: Specifications/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = _unitOfWork.SpecificationRepo.Get(id.Value);
            if (specification == null)
            {
                return NotFound();
            }
            return View(specification);
        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Specification1,ValueType")] Specification specification)
        {
            if (id != specification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.SpecificationRepo.Update(specification);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
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
            return View(specification);
        }

        // GET: Specifications/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = _unitOfWork.SpecificationRepo.Get(id.Value);

            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // POST: Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.SpecificationRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecificationExists(int id)
        {
            return _unitOfWork.SpecificationRepo.IsExist(id);
        }
    }
}
