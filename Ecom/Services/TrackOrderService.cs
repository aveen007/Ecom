using AppDbContext.Models;
using AppDbContext.UOW;

namespace Ecom.Services
{
    public class TrackOrderService : ITrackOrderService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public TrackOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ShippingState TrackOrder(int id)
        {
            var shipping = _unitOfWork.ShippingRepo.Get(id);
            var shippingState = _unitOfWork.ShippingStateRepo.Get(shipping.ShippingStateId);

            return shippingState;
        }
    }
}
