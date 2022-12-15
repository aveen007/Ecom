using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
