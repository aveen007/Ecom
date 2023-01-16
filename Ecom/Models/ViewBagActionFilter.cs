using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AppDbContext.UOW;
using Ecom.Controllers;

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
            var controller = context.Controller as BaseController;
            var categories = controller.unitOfWork.CategoryRepo.GetAll();
            
            controller.ViewData.Add("Cats", categories);
          //  throw new System.NotImplementedException();
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
