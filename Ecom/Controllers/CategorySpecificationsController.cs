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
    public class CategorySpecificationsController : BaseController
    {


    
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategorySpecificationsController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;

        }
        // GET: CategorySpecifications
        public IActionResult Index()
        {

            var categoryspecifications = _unitOfWork.CategorySpecificationRepo.GetAll(includeProperties: "Category,Specification").ToList();
            return View(categoryspecifications);


        }

        // GET: CategorySpecifications/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var categoryspecs = _unitOfWork.CategorySpecificationRepo.GetAll(includeProperties: "Category,Specification").ToList();
            var categoryspec = new CategorySpecification();
            for (int i = 0; i < categoryspecs.Count; i++)
            {
                var temp = categoryspecs[i];
                if (temp.Id == id)
                {
                    categoryspec = temp;
                }

            }
            if (categoryspec == null)
            {
                return NotFound();
            }

            return View(categoryspec);
        }

        // GET: CategorySpecifications/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "Specification1");
            return View();
        }

        // POST: CategorySpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,SpecificationId")] CategorySpecification categorySpecification)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategorySpecificationRepo.Add(categorySpecification);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "Specification1");
            return View(categorySpecification);
        }

        // GET: CategorySpecifications/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorySpecification = _unitOfWork.CategorySpecificationRepo.Get(id.Value);
            if (categorySpecification == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "Specification1");
            return View(categorySpecification);
        }

        // POST: CategorySpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,SpecificationId")] CategorySpecification categorySpecification)
        {
            if (id != categorySpecification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CategorySpecificationRepo.Update(categorySpecification);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorySpecificationExists(categorySpecification.Id))
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
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepo.GetAll().ToList(), "Id", "Name");
            ViewData["SpecificationId"] = new SelectList(_unitOfWork.SpecificationRepo.GetAll().ToList(), "Id", "Specification1");
            return View(categorySpecification);
        }

        // GET: CategorySpecifications/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryspecs = _unitOfWork.CategorySpecificationRepo.GetAll(includeProperties: "Category,Specification").ToList();
            var categoryspec = new CategorySpecification();
            for (int i = 0; i < categoryspecs.Count; i++)
            {
                var temp = categoryspecs[i];
                if (temp.Id == id)
                {
                    categoryspec = temp;
                }

            }

            if (categoryspec == null)
            {
                return NotFound();
            }

            return View(categoryspec);
        }

        // POST: CategorySpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
            _unitOfWork.CategorySpecificationRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorySpecificationExists(int id)
        {
             return _unitOfWork.CategorySpecificationRepo.IsExist(id);
        }
    }
}
