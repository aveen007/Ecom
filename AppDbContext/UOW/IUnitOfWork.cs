using AppDbContext.IRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext.UOW
{
    public interface IUnitOfWork
    {
        public ICategoryPromotionRepo CategoryPromotionRepo { get; set; }
        public ICategoryRepo CategoryRepo { get; set; }
        public ICategorySpecificationRepo CategorySpecificationRepo { get; set; }
        public INotificationRepo NotificationRepo { get; set; }
        public INotificationTypeRepo NotificationTypeRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public IProductCategoryValueRepo ProductCategoryValueRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IProductOrderRepo ProductOrderRepo { get; set; }
        public IProductSpecificationRepo ProductSpecificationRepo { get; set; }
        public IProductSpecificationValueRepo ProductSpecificationValueRepo { get; set; }
        public IPromotionRepo PromotionRepo { get; set; }
        public IShippingRepo ShippingRepo { get; set; }
        public IShippingStateRepo ShippingStateRepo { get; set; }
        public ISpecificationRepo SpecificationRepo { get; set; }
        public IUserRatingRepo UserRatingRepo { get; set; }
        public IValueTypeRepo ValueTypeRepo { get; set; }
        public IApplicationUserRepo ApplicationUserRepo { get; set; }

        public void SaveChanges ();
        public Task<int> SaveAsync();
        public void RollBack();
    }
}
