using AppDbContext.Models;

namespace Ecom.Services
{
    public interface ITrackOrderService
    {
        public ShippingState TrackOrder(int id);
    }
}
