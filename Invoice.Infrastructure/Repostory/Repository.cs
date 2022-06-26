using Invoice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.Repostory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InvoiceContext _db;
        internal DbSet<T> dbSet;
        public Repository(InvoiceContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public T Find(int id)
        {
            return dbSet.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> Query = dbSet;
            if (filter != null)
                Query = Query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Query = Query.Include(includeProperty);
                }
            if (!isTracking)
            {
                Query = Query.AsNoTracking();
            }

            return Query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> Query = dbSet;
            if (filter != null)
                Query = Query.Where(filter);


            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Query = Query.Include(includeProperty);
                }
            if (orderBy != null)
                Query = orderBy(Query);
            if (!isTracking)
            {
                Query = Query.AsNoTracking();
            }

            return Query.ToList<T>();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

       
    }
}
