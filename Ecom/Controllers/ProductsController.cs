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
using AutoMapper;
using Ecom.Models;
using PagedList;

namespace Ecom.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public ProductsController( IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;

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
        public IActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;

            var products = _unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);
            var productVMs = from s in productsViewModels
                             select s;
            switch (sortOrder)
            {
                case "Name":
                    productVMs = productVMs.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    productVMs = productVMs.OrderBy(s => s.Id);
                    break;

                default:
                    productVMs = productVMs.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(productVMs.ToPagedList(pageNumber, pageSize));
          
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var products = _unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
            var product = new Product();
            for (int i = 0; i < products.Count; i++)
            {
                var temp = products[i];
                if (temp.Id == id)
                {
                    product = products[i];
                }

            }

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Sku,CategoryId,Imagefile")] Product product)
        {
            if (ModelState.IsValid)
            {

                if (product.Imagefile != null)
                    await saveImage(product);
                _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveAsync();
                Notify("Product created successfully!!");

                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
     

            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name", product.CategoryId);

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var product = _unitOfWork.ProductRepo.Get(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name", product.CategoryId);

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Sku,CategoryId,Imagefile")] Product product)
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
                    Notify("Edit saved successfully!!");


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Notify("oops,Something went wrong",notificationType: NotificationTypeEnum.error);

                    }
                }
                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name", product.CategoryId);
            
            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
            var product = new Product();
            for (int i = 0; i < products.Count; i++)
            {
                var temp = products[i];
                if (temp.Id == id)
                {
                    product = products[i];
                }

            }

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try { 
            _unitOfWork.ProductRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            Notify("product deleted successfully!!");
        }
            catch(Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
             
    }
            return RedirectToAction(nameof(Index), new
            {
                page = 1
            });

        }

        private bool ProductExists(int id)
        {

            return _unitOfWork.ProductRepo.IsExist(id);
        }
    }
}
