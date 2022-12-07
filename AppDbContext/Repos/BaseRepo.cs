using AppDbContext.IRepos;
using AppDbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected Ecommerce_DBContext _db;

        private DbSet<T> _dbSet;
        public BaseRepo(Ecommerce_DBContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
        public bool IsExist(int id)
        {
            var dbItem = Get(id);
            if (dbItem != null)
            {
                _db.Entry<T>(dbItem).State = EntityState.Detached;
                return true;
            }
            return false;
        }
    }
}
