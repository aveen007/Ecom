using System.Collections.Generic;

namespace AppDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        T Get(int id);

        // T Get(string id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        IEnumerable<T> GetAll();
    }
}
