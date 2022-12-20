using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDbContext.Models;
using Microsoft.AspNetCore.Hosting;
using AppDbContext.UOW;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Controllers
{
    public class AddressesController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public AddressesController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;

        }

        // GET: Addresses
        public IActionResult Index()
        {
            var addresses = _unitOfWork.AddressRepo.GetAll();
            var addressesViewModels = _mapper.Map<List<AddressViewModel>>(addresses);

            return View(addressesViewModels);
        }

        // GET: Addresses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = _unitOfWork.AddressRepo.Get(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            var addressTypeViewModel = _mapper.Map<AddressViewModel>(address);

            return View(addressTypeViewModel);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Governorate,City,Region")] Address address)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AddressRepo.Add(address);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            var addressTypeViewModel = _mapper.Map<AddressViewModel>(address);

            return View(addressTypeViewModel);
        }

        // GET: Addresses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = _unitOfWork.AddressRepo.Get(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            var addressTypeViewModel = _mapper.Map<AddressViewModel>(address);

            return View(addressTypeViewModel);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Governorate,City,Region")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.AddressRepo.Update(address);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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

            var addressTypeViewModel = _mapper.Map<AddressViewModel>(address);

            return View(addressTypeViewModel);
        }

        // GET: Addresses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = _unitOfWork.AddressRepo.Get(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            var addressTypeViewModel = _mapper.Map<AddressViewModel>(address);

            return View(addressTypeViewModel);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.AddressRepo.Delete(id);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _unitOfWork.AddressRepo.IsExist(id);
        }
    }
}
