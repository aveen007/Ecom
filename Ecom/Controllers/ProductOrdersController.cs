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

    public class ProductOrdersController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private List<ProductOrder> productsCart;
        public ProductOrdersController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            this._mapper = mapper;
            _userManager = userManager;
        }
       

        public async Task<IActionResult> ProceedToCheckkOut(ICollection<ProductOrder> ProductOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var xy = ProductOrder.Count();
                    foreach (var item in ProductOrder)
                    {
                        var x = item.Quantity;
                        _unitOfWork.ProductOrderRepo.Update(item);

                        await _unitOfWork.SaveAsync();
                    }
                    Notify("Edit saved successfully!!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Notify("oops,Something went wrong", notificationType: NotificationTypeEnum.error);
                }
                //return RedirectToAction(nameof(Index), new { page = 1 });
            }
            //var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return RedirectToAction("ShoppingCart", "Orders");
        }

       
       

        
        private bool ProductOrderExists(int id)
        {
            return _unitOfWork.ProductOrderRepo.IsExist(id);
        }
    }
}
