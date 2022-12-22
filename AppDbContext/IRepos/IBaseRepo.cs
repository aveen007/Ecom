using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace AppDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        T Get(int id);

        // T Get(string id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
        void Delete(Expression<Func<T, bool>> filter = null);

        // public IEnumerable<T> GetAll();

        public bool IsExist(int id);
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
       
    }
}
