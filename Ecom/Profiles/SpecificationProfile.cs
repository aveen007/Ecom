using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class SpecificationProfile : Profile
    {
        public SpecificationProfile()
        {
            CreateMap<Specification, SpecificationViewModel>();
            CreateMap<Specification, SpecificationViewModel>().ReverseMap();
        }
    }
}
