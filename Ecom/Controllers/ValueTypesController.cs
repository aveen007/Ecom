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
    public class ValueTypesController : BaseController
    {
  
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public ValueTypesController( IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
         
        }
        // GET: ValueTypes
        public IActionResult Index()
        {

            var valueTypes = _unitOfWork.ValueTypeRepo.GetAll();
            var valueTypesViewModels = _mapper.Map<List<ValueTypeViewModel>>(valueTypes);
            return View(valueTypesViewModels);

        }

        // GET: ValueTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var valueType = _unitOfWork.ValueTypeRepo.Get(id.Value);
            if (valueType == null)
            {
                return NotFound();
            }

            var valueTypeViewModel = _mapper.Map<ValueTypeViewModel>(valueType);

            return View(valueTypeViewModel);
        }

        // GET: ValueTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] AppDbContext.Models.ValueType valueType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ValueTypeRepo.Add(valueType);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
             
            }

            var valueTypeViewModel = _mapper.Map<ValueTypeViewModel>(valueType);

            return View(valueTypeViewModel);
        }

        // GET: ValueTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var valueType = _unitOfWork.ValueTypeRepo.Get(id.Value);

            if (valueType == null)
            {
                return NotFound();
            }

            var valueTypeViewModel = _mapper.Map<ValueTypeViewModel>(valueType);

            return View(valueTypeViewModel);
        }

        // POST: ValueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] AppDbContext.Models.ValueType valueType)
        {
            if (id != valueType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ValueTypeRepo.Update(valueType);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueTypeExists(valueType.Id))
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

            var valueTypeViewModel = _mapper.Map<ValueTypeViewModel>(valueType);

            return View(valueTypeViewModel);
        }

        // GET: ValueTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var valueType = _unitOfWork.ValueTypeRepo.Get(id.Value);

            if (valueType == null)
            {
                return NotFound();
            }

            var valueTypeViewModel = _mapper.Map<ValueTypeViewModel>(valueType);

            return View(valueTypeViewModel);
        }

        // POST: ValueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
 
            
            _unitOfWork.ValueTypeRepo.Delete(id);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ValueTypeExists(int id)
        {
            return _unitOfWork.ValueTypeRepo.IsExist(id);

        }
    }
}
