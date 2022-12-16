using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAddressRepo AddressRepo { get; set; }
        public ICategoryPromotionRepo CategoryPromotionRepo { get; set; }
        public ICategoryRepo CategoryRepo { get; set; }
        public ICategorySpecificationRepo CategorySpecificationRepo { get; set; }
        public INotificationRepo NotificationRepo { get; set; }
        public INotificationTypeRepo NotificationTypeRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public IProductCategoryValueRepo ProductCategoryValueRepo { get; set; }
        public IProductOrderRepo ProductOrderRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IProductSpecificationRepo ProductSpecificationRepo { get; set; }
        public IProductSpecificationValueRepo ProductSpecificationValueRepo { get; set; }
        public IPromotionRepo PromotionRepo { get; set; }
        public IShippingRepo ShippingRepo { get; set; }
        public IShippingStateRepo ShippingStateRepo { get; set; }
        public ISpecificationRepo SpecificationRepo { get; set; }
        public IUserRepo UserRepo { get; set; }
        public IUserRatingRepo UserRatingRepo { get; set; }
        public IValueTypeRepo ValueTypeRepo { get; set; }

        protected readonly Ecommerce_DBContext _db;

        public UnitOfWork(Ecommerce_DBContext db)
        {
            _db = db;
            AddressRepo = new AddressRepo(db);
            CategoryPromotionRepo = new CategoryPromotionRepo(db);
            CategoryRepo = new CategoryRepo(db);
            CategorySpecificationRepo = new CategorySpecificationRepo(db);
            NotificationRepo = new NotificationRepo(db);
            NotificationTypeRepo = new NotificationTypeRepo(db);
            OrderRepo = new OrderRepo(db);
            ProductCategoryValueRepo = new ProductCategoryValueRepo(db);
            ProductOrderRepo = new ProductOrderRepo(db);
            ProductRepo = new ProductRepo(db);
            ProductSpecificationRepo = new ProductSpecificationRepo(db);
            ProductSpecificationValueRepo = new ProductSpecificationValueRepo(db);
            PromotionRepo = new PromotionRepo(db);
            ShippingRepo = new ShippingRepo(db);
            ShippingStateRepo = new ShippingStateRepo(db);
            SpecificationRepo = new SpecificationRepo(db);
            UserRepo = new UserRepo(db);
            UserRatingRepo = new UserRatingRepo(db);
        }

        public void RollBack()
        {
            _db.Dispose();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
