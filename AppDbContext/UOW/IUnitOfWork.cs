using AppDbContext.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.UOW
{
    public interface IUnitOfWork
    {
        public ICategoryRepo CategoryRepo { get; set; }
        public ICategorySpecificationRepo CategorySpecificationRepo { get; set; }
        public ICategorySpecificationValueRepo CategorySpecificationValueRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IProductSpecificationRepo ProductSpecificationRepo { get; set; }
        public IProductSpecificationValueRepo ProductSpecificationValueRepo { get; set; }

        public void SaveChanges ();

        public void RollBack();
    }
}
