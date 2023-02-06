using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
