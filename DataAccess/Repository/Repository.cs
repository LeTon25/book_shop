using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query =query.Where(filter);
            }
            if (includeProperties !=  null)
            {
                foreach (string property in includeProperties.Split(',',StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter,string? includeProperties = null,bool tracked = false)
        {
            if(tracked)
            {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);
                    }
                }
                return query.FirstOrDefault();
            }   
       
        }
    }
}
