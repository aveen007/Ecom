using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepo CategoryRepo { get; set; }
        public ICategorySpecificationRepo CategorySpecificationRepo { get; set; }
        public ICategorySpecificationValueRepo CategorySpecificationValueRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IProductSpecificationRepo ProductSpecificationRepo { get; set; }
        public IProductSpecificationValueRepo ProductSpecificationValueRepo { get; set; }

        protected readonly Ecommerce_DBContext _db;

        public UnitOfWork(Ecommerce_DBContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepo(db);
            CategorySpecificationRepo = new CategorySpecificationRepo(db);
            CategorySpecificationValueRepo = new CategorySpecificationValueRepo(db);
            ProductRepo = new ProductRepo(db);
            ProductSpecificationRepo = new ProductSpecificationRepo(db);
            ProductSpecificationValueRepo = new ProductSpecificationValueRepo(db);
        }

        public void RollBack()
        {
            _db.Dispose();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
