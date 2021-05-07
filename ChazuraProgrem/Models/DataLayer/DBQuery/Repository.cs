using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChazuraProgram.Models
{
    public class Repository<T> : IRepository<T>where T : class
    {
        protected ChazuraContext Context { get; set; }
        private DbSet<T> Dbset { get; set; }
        public Repository(ChazuraContext ctx)
        {
            Context = ctx;
            Dbset = Context.Set<T>();
        }
       

        private int? count;
        public int Count => count ?? Dbset.Count();

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return  query.ToList();
        }

        public virtual T Get(int id) => Dbset.Find(id);
        public virtual IQueryable<T> GetWithLinq(QueryOptions<T> options)
        {
           IQueryable<T> query = BuildQuery(options);
            return query;
        }
        public virtual T Get(string id) => Dbset.Find(id);
        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }

        public virtual void Insert(T entity) => Dbset.Add(entity);
        public virtual void Update(T entity) => Dbset.Update(entity);
        public virtual void Delete(T entity) => Dbset.Remove(entity);
        public virtual void Save() => Context.SaveChanges();

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = Dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                foreach (var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }
                count = query.Count();
            }
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }
            if (options.HasPaging)
                query = query.PageBy(options.PageNumber, options.PageSize);

            return query;
        }
    }
}
