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
using PagedList;

namespace Ecom.Controllers
{
    public class SpecificationsController : BaseController
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        public SpecificationsController (IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
        }
        // GET: Specifications
        public IActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;

            var specifications = _unitOfWork.SpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var specificationsViewModels = _mapper.Map<List<SpecificationViewModel>>(specifications);
            var SpecificationsVMs = from s in specificationsViewModels
                                    select s;
            switch (sortOrder)
            {
                case "Name":
                    SpecificationsVMs = SpecificationsVMs.OrderByDescending(s => s.SpecificationName);
                    break;
                case "Date":
                    SpecificationsVMs = SpecificationsVMs.OrderBy(s => s.Id);
                    break;

                default:
                    SpecificationsVMs = SpecificationsVMs.OrderBy(s => s.SpecificationName);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(SpecificationsVMs.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Specifications/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specifications = _unitOfWork.SpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var specification = new Specification();
            for (int i = 0; i < specifications.Count; i++)
            {
                var temp = specifications[i];
                if (temp.Id == id)
                {
                    specification = specifications[i];
                }

            }
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
        public async Task<IActionResult> Create([Bind("Id,SpecificationName,Description,ValueTypeId")] Specification specification)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SpecificationRepo.Add(specification);
                await _unitOfWork.SaveAsync();
                Notify("Specification created successfully!!");
                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
            }
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName", specification.ValueTypeId);

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
            ViewData["ValueTypeId"] = new SelectList(_unitOfWork.ValueTypeRepo.GetAll().ToList(), "Id", "ValueName", specification.ValueTypeId);

            var specificationViewModel = _mapper.Map<SpecificationViewModel>(specification);

            return View(specificationViewModel);
        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpecificationName,Description,ValueTypeId")] Specification specification)
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
                    Notify("Edit saved successfully!!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
                    }
                }
                return RedirectToAction(nameof(Index), new
                {
                    page = 1
                });
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

            var specifications = _unitOfWork.SpecificationRepo.GetAll(includeProperties: "ValueType").ToList();
            var specification = new Specification();
            for (int i = 0; i < specifications.Count; i++)
            {
                var temp = specifications[i];
                if (temp.Id == id)
                {
                    specification = specifications[i];
                }

            }

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
            
            try
            {
                _unitOfWork.SpecificationRepo.Delete(id);
                await _unitOfWork.SaveAsync();
                Notify("specification deleted successfully!!");
            }
            catch (Exception)
            {
                Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
            }
            return RedirectToAction(nameof(Index), new
            {
                page = 1
            });
        }

        private bool SpecificationExists(int id)
        {
            return _unitOfWork.SpecificationRepo.IsExist(id);
        }
    }
}
