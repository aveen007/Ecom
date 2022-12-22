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
using AutoMapper;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class SpecificationsController : BaseController
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        public SpecificationsController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
        }
        // GET: Specifications
        public IActionResult Index()
        {
            var specifications = _unitOfWork.SpecificationRepo.GetAll();
            var specificationsViewModels = _mapper.Map<List<SpecificationViewModel>>(specifications);

            return View(specificationsViewModels);
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
            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
        }

        // GET: Specifications/Create
        public IActionResult Create()
        {
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName");
            return View();
        }

        // POST: Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD

        public async Task<IActionResult> Create([Bind("Id,SpecificationName,Description,ValueTypeId")] Specification specification)

=======
        public async Task<IActionResult> Create([Bind("Id,SpecificationName,Description,ValueTypeId")] Specification specification)
>>>>>>> d2e446475989b1cbb2221f0b44ce05e06bdee07a
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecificationRepo.Add(specification);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
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
            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
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
            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
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

            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
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
