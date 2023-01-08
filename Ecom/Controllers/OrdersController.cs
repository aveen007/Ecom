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
    public class OrdersController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;

        }

        // GET: Orders
        public IActionResult Index()
        {
            var orders = _unitOfWork.OrderRepo.GetAll(includeProperties: "User").ToList();
            var ordersViewModels = _mapper.Map<List<OrderViewModel>>(orders);

            return View(ordersViewModels);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = _unitOfWork.OrderRepo.GetAll(includeProperties: "User").ToList();
            var order = new Order();
            for (int i = 0; i < orders.Count; i++)
            {
                var temp = orders[i];
                if (temp.Id == id)
                {
                    order = orders[i];
                }

            }

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_unitOfWork.ApplicationUserRepo.GetAll().ToList(), "Id", "FirstName", "LastName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ShippingId,OrderDate,TotalPrice,IsOrdered")] Order order)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.OrderRepo.Add(order);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.ApplicationUserRepo.GetAll().ToList(), "Id", "FirstName", "LastName", order.UserId);
            
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            
            return View(orderViewModel);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _unitOfWork.OrderRepo.Get(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.ApplicationUserRepo.GetAll().ToList(), "Id", "FirstName", "LastName", order.UserId);

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ShippingId,OrderDate,TotalPrice,IsOrdered")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.OrderRepo.Update(order);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_unitOfWork.ApplicationUserRepo.GetAll().ToList(), "Id", "FirstName", "LastName", order.UserId);

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = _unitOfWork.OrderRepo.GetAll(includeProperties: "User").ToList();
            var order = new Order();
            for (int i = 0; i < orders.Count; i++)
            {
                var temp = orders[i];
                if (temp.Id == id)
                {
                    order = orders[i];
                }

            }

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.OrderRepo.Delete(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _unitOfWork.OrderRepo.IsExist(id);
        }
    }
}
