using AppDbContext.IRepos;
using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class NotificationRepo : BaseRepo<Notification>, INotificationRepo
    {
        public NotificationRepo(Ecommerce_DBContext db) : base(db) 
        {

        }

    }
}
