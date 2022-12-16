using AppDbContext.IRepos;
using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class ValueTypeRepo : BaseRepo<AppDbContext.Models.ValueType>, IValueTypeRepo
    {
        public ValueTypeRepo(Ecommerce_DBContext db) : base(db) 
        {

        }

    }
}
