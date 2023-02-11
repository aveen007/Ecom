using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Services
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackOrderService : ITrackOrderService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public TrackOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ShippingState TrackOrder(int id)
        {
            var shipping = _unitOfWork.ShippingRepo.Get(id);
            var shippingState = _unitOfWork.ShippingStateRepo.Get(shipping.ShippingStateId);

            return shippingState;
        }
    }
}
