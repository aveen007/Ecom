using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDbContext.Models;

namespace Ecom.Controllers
{
    public class CategoryPromotionsController : Controller
    {
        private readonly Ecommerce_DBContext _context;

        public CategoryPromotionsController(Ecommerce_DBContext context)
        {
            _context = context;
        }

        // GET: CategoryPromotions
        public async Task<IActionResult> Index()
        {
            var ecommerce_DBContext = _context.CategoryPromotion.Include(c => c.Category).Include(c => c.Promotion);
            return View(await ecommerce_DBContext.ToListAsync());
        }

        // GET: CategoryPromotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPromotion = await _context.CategoryPromotion
                .Include(c => c.Category)
                .Include(c => c.Promotion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPromotion == null)
            {
                return NotFound();
            }

            return View(categoryPromotion);
        }

        // GET: CategoryPromotions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Description");
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name");
            return View();
        }

        // POST: CategoryPromotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,PromotionId")] CategoryPromotion categoryPromotion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryPromotion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Description", categoryPromotion.CategoryId);
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", categoryPromotion.PromotionId);
            return View(categoryPromotion);
        }

        // GET: CategoryPromotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPromotion = await _context.CategoryPromotion.FindAsync(id);
            if (categoryPromotion == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Description", categoryPromotion.CategoryId);
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", categoryPromotion.PromotionId);
            return View(categoryPromotion);
        }

        // POST: CategoryPromotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,PromotionId")] CategoryPromotion categoryPromotion)
        {
            if (id != categoryPromotion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryPromotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryPromotionExists(categoryPromotion.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Description", categoryPromotion.CategoryId);
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", categoryPromotion.PromotionId);
            return View(categoryPromotion);
        }

        // GET: CategoryPromotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPromotion = await _context.CategoryPromotion
                .Include(c => c.Category)
                .Include(c => c.Promotion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPromotion == null)
            {
                return NotFound();
            }

            return View(categoryPromotion);
        }

        // POST: CategoryPromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryPromotion = await _context.CategoryPromotion.FindAsync(id);
            _context.CategoryPromotion.Remove(categoryPromotion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryPromotionExists(int id)
        {
            return _context.CategoryPromotion.Any(e => e.Id == id);
        }
    }
}
