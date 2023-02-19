using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AppDbContext.UOW;
using Ecom.Controllers;
using AppDbContext.Models;
using System.Linq;

namespace Ecom.Models
{
    public class ViewBagActionFilter : IActionFilter
    {

       /**//* protected readonly IUnitOfWork _unitOfWork;
*//*
        public ViewBagActionFilter()
        {
         *//*   IUnitOfWork _unitOfWork = unitOfWork;*//*
        }*/

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();

            var controller = context.Controller as BaseController;
            if (!(controllerName == "Categories" && actionName == "Edit"))
            {
                var categories = controller.unitOfWork.CategoryRepo.GetAll();

                controller.ViewData.Add("Cats", categories);
            }
            if (!(controllerName == "Orders" && actionName == "ProceedToCheckOut") && !(controllerName == "TrackOrderService"))
            {
                var userId = controller.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

                var orders = controller.unitOfWork.OrderRepo.GetAll(filter: e => e.UserId == userId && e.IsOrdered == false).ToList();

                Order order;
                var productCount = 0;
                if (!orders.Any())
                {
                    productCount = 0;
                }
                else
                {
                    order = orders.First();
                    productCount = controller.unitOfWork.ProductOrderRepo.GetAll(filter: e => e.OrderId == order.Id).ToList().Count();
                }

                controller.ViewData.Add("ProductCount", productCount);
            }

        }

      /*  public override void OnResultExecuting(ResultExecutingContext context)
        {
            // for razor pages
            if (context.Controller is PageModel)
            {
                var controller = context.Controller as PageModel;
                var categories = _unitOfWork.CategoryRepo.GetAll();

                controller.ViewData.Add("Categories", categories);
                // or
            *//*    controller.ViewBag.Avatar = $"~/avatar/empty.png";*//*

                //also you have access to the httpcontext & route in controller.HttpContext & controller.RouteData
            }

            // for Razor Views
            if (context.Controller is Controller)
            {
                var controller = context.Controller as Controller;
                var categories = _unitOfWork.CategoryRepo.GetAll();

                controller.ViewData.Add("Categories", categories);
               *//* // or
                controller.ViewBag.Avatar = categories;
*//*
                //also you have access to the httpcontext & route in controller.HttpContext & controller.RouteData
            }

            base.OnResultExecuting(context);
        }*/
    }
}
