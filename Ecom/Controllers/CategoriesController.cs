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
    public class CategoriesController : BaseController
    {
  
        private readonly IWebHostEnvironment _hostEnvironment;
       

        public CategoriesController( IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
         
        }
        // GET: Categories
        public async Task<IActionResult> Index()
        {

            var categories = _unitOfWork.CategoryRepo.GetAll();
            return View( categories.ToList());
            /*  return View(await _context.Category.ToListAsync());*/
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category =  _unitOfWork.CategoryRepo.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(category);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
             
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.CategoryRepo.Get(id.Value);
            /*var category = await _context.Category.FindAsync(id);*/
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.CategoryRepo.Get(id.Value);
            /*   var category = await _context.Category
                   .FirstOrDefaultAsync(m => m.Id == id);*/
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var category = await _context.Category.FindAsync(id);*/
            
            _unitOfWork.CategoryRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            /*_context.Category.Remove(category);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _unitOfWork.CategoryRepo.IsExist(id);
            /*return _context.Category.Any(e => e.Id == id);*/
        }
    }
}