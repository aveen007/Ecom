using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressViewModel>();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
