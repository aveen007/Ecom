using AppDbContext.Models;
using AutoMapper;
using Ecom.Models;

namespace Ecom.Profiles
{
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            CreateMap<Promotion, PromotionViewModel>();
            CreateMap<Promotion, PromotionViewModel>().ReverseMap();
        }
    }
}
