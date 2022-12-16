using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class ValueTypeProfile : Profile
    {
        public ValueTypeProfile()
        {
            CreateMap<ValueType, ValueTypeViewModel>();
            CreateMap<ValueType, ValueTypeViewModel>().ReverseMap();
        }
    }
}
