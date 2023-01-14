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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Ecom.Models;
using PagedList;

namespace Ecom.Controllers
{

    public class OrdersController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private List<ProductOrder> productsCart;
        public OrdersController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
            _userManager = userManager;
        }
        public static List< Tuple<Product, int> > cart = new List<Tuple<Product,int> >();
        public IActionResult AddToCart(int? id, int quantity)
        {
            
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var orders = _unitOfWork.OrderRepo.GetAll(filter: e => e.UserId == userId && e.IsOrdered == false).ToList();

            Product product = _unitOfWork.ProductRepo.Get(id.Value);
            Order order;
            if (!orders.Any())
            {

                order = new Order
                {
                    UserId = userId,
                    TotalPrice = 0,
                    OrderDate = DateTime.Today,
                    IsOrdered = false
                };
                _unitOfWork.OrderRepo.Add(order);
                _unitOfWork.SaveAsync();
            }
            else
            {
                order = orders.First();
            }
            order.TotalPrice += quantity * product.Price;
            var producorders = _unitOfWork.ProductOrderRepo.GetAll(filter: e => e.OrderId == order.Id && e.ProductId == product.Id).ToList();

            ProductOrder productOrder;
            if (!producorders.Any())
            {
                productOrder = new ProductOrder
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    SinglePrice = product.Price
                };
                _unitOfWork.ProductOrderRepo.Add(productOrder);
                _unitOfWork.SaveAsync();
            }
            else
            {
                productOrder = producorders.First();
                productOrder.Quantity += quantity;
                _unitOfWork.ProductOrderRepo.Update(productOrder);
                _unitOfWork.SaveAsync();
            }

            _unitOfWork.OrderRepo.Update(order);
            _unitOfWork.SaveAsync();
            var categoyId = product.CategoryId;
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Shop", "Categories", new {id = categoyId });
        }
        public IActionResult ShoppingCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var orders = _unitOfWork.OrderRepo.GetAll(filter: e => e.UserId == userId && e.IsOrdered == false).ToList();
            Order order;
            if (!orders.Any())
            {
                Notify("There are no products in your cart dude!!");
                //return RedirectToAction("Shop", "Categories", new { id = categoyId });
            }
            else
            {
                order = orders.First();
                ViewData["ProductsOrder"] = _unitOfWork.ProductOrderRepo.GetAll(filter: e => e.OrderId == order.Id, includeProperties: "Product").ToList();
            }

            return View();

        }
        // GET: Orders
        public IActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Page = page;
            var orders = _unitOfWork.OrderRepo.GetAll();

            var ordersViewModels = _mapper.Map<List<OrderViewModel>>(orders);
            var orderVMs = from s in ordersViewModels
                              select s;
            switch (sortOrder)
            {
                case "Name":
                    orderVMs = orderVMs.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    orderVMs = orderVMs.OrderBy(s => s.Id);
                    break;
                default:
                    orderVMs = orderVMs.OrderBy(s => s.Id);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(orderVMs.ToPagedList(pageNumber, pageSize));
            
        }

        
        // GET: Orders/Details/5
        public IActionResult Details(int? id)
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

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        // GET: Orders/Create
        /*public IActionResult Create()
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
        */
        private bool OrderExists(int id)
        {
            return _unitOfWork.OrderRepo.IsExist(id);
        }
    }
}
