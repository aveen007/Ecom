using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class ProductSpecificationProfile : Profile
    {
        public ProductSpecificationProfile()
        {
            CreateMap<ProductSpecification, ProductSpecificationViewModel>();
            CreateMap<ProductSpecification, ProductSpecificationViewModel>().ReverseMap();
        }
    }
}
