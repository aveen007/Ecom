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
    public class ShippingsController : Controller
    {
        private readonly Ecommerce_DBContext _context;

        public ShippingsController(Ecommerce_DBContext context)
        {
            _context = context;
        }

        // GET: Shippings
        public async Task<IActionResult> Index()
        {
            var ecommerce_DBContext = _context.Shipping.Include(s => s.Order).Include(s => s.ShippingState);
            return View(await ecommerce_DBContext.ToListAsync());
        }

        // GET: Shippings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping
                .Include(s => s.Order)
                .Include(s => s.ShippingState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // GET: Shippings/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "UserId");
            ViewData["ShippingStateId"] = new SelectList(_context.ShippingState, "Id", "Description");
            return View();
        }

        // POST: Shippings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ShippingStateId,ShippingPrice")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "UserId", shipping.OrderId);
            ViewData["ShippingStateId"] = new SelectList(_context.ShippingState, "Id", "Description", shipping.ShippingStateId);
            return View(shipping);
        }

        // GET: Shippings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping.FindAsync(id);
            if (shipping == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "UserId", shipping.OrderId);
            ViewData["ShippingStateId"] = new SelectList(_context.ShippingState, "Id", "Description", shipping.ShippingStateId);
            return View(shipping);
        }

        // POST: Shippings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ShippingStateId,ShippingPrice")] Shipping shipping)
        {
            if (id != shipping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingExists(shipping.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "UserId", shipping.OrderId);
            ViewData["ShippingStateId"] = new SelectList(_context.ShippingState, "Id", "Description", shipping.ShippingStateId);
            return View(shipping);
        }

        // GET: Shippings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping
                .Include(s => s.Order)
                .Include(s => s.ShippingState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // POST: Shippings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipping = await _context.Shipping.FindAsync(id);
            _context.Shipping.Remove(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingExists(int id)
        {
            return _context.Shipping.Any(e => e.Id == id);
        }
    }
}
