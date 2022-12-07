using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDbContext.Models;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AppDbContext.UOW;
using Microsoft.Extensions.Configuration;

namespace Ecom.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly Ecommerce_DBContext _context;

        public ProductsController(Ecommerce_DBContext _context, IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._context = _context;
        }
        private async Task saveImage(Product product)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(product.Imagefile.FileName);
            string extension = Path.GetExtension(product.Imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yyyy-mm-ss-fff") + extension;
            string path = Path.Combine(wwwRootPath + "\\images\\products", filename);
            product.ImageLink = Path.Combine("/images/products/", filename);
            using var fileStream = new FileStream(path, FileMode.Create);
            await product.Imagefile.CopyToAsync(fileStream);
        }
        private void deleteImage(string imagePath)
        {
            if (imagePath == null)
                return;
            string fullpath = Path.Combine(_hostEnvironment.WebRootPath + "\\", imagePath.Substring(1));
            if (System.IO.File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            /*   var ecommerce_DBContext = _context.Product.Include(p => p.Category);
               return View(await ecommerce_DBContext.ToListAsync());*/
            var products = _unitOfWork.ProductRepo.GetAll();
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            var product = _unitOfWork.ProductRepo.Get(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Sku,CategoryId,Imagefile")] Product product)
        {
            if (ModelState.IsValid)
            {

                if (product.Imagefile != null)
                    await saveImage(product);
                _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
     

            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Sku,CategoryId,Imagefile")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Imagefile != null)
                        await saveImage(product);
                    _unitOfWork.ProductRepo.Update(product);
                    await _unitOfWork.SaveAsync();
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
