using AppDbContext.IRepos;
using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class ApplicationUserRepo : BaseRepo<ApplicationUser>, IApplicationUserRepo
    {
        public ApplicationUserRepo(Ecommerce_DBContext db) : base(db) 
        {

        }

    }
}
