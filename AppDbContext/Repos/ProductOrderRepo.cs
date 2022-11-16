using AppDbContext.IRepos;
using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class ProductOrderRepo : BaseRepo<ProductOrder>, IProductOrderRepo
    {
        public ProductOrderRepo(Ecommerce_DBContext db) : base(db) 
        {

        }

    }
}
