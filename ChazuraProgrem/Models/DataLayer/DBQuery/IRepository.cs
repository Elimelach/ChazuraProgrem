using System.Collections.Generic;
using System.Linq;

namespace ChazuraProgram.Models
{
    public interface IRepository<T> where T : class
    {
        int Count { get; }

        void Delete(T entity);
        T Get(int id);
        T Get(QueryOptions<T> options);
        T Get(string id);
        IQueryable<T> GetWithLinq(QueryOptions<T> options);
        void Insert(T entity);
        IEnumerable<T> List(QueryOptions<T> options);
        void Save();
        void Update(T entity);
    }
}