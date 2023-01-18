using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class ProductOrderProfile : Profile
    {
        public ProductOrderProfile()
        {
            CreateMap<ProductOrder, ProductOrderViewModel>();
            CreateMap<ProductOrder, ProductOrderViewModel>().ReverseMap();
        }
    }
}
