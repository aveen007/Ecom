using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.Extensions.Configuration;
using Ecom.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Services
{
    [Route("api/TrackOrderService")]
    [ApiController]
    public class TrackOrderServiceController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public TrackOrderServiceController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }

        [HttpGet("{id}")]
        public ShippingState TrackOrder(int id)
        {
            var order = _unitOfWork.OrderRepo.Get(id);
            if (order == null)
            {
                return new ShippingState
                {
                    Name = "Not Found",
                    Description = "No order Found"
                };
            }
            else
            {
                var shipping = _unitOfWork.ShippingRepo.Get(order.ShippingId);
                var shippingState = _unitOfWork.ShippingStateRepo.Get(shipping.ShippingStateId);

                return new ShippingState
                {
                    Name = shippingState.Name,
                    Description = shippingState.Description
                };
            }
            
        }
    }
}
