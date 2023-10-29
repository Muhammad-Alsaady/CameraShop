using CameraShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.DataAccess.Repository
{
    public class Repository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.DbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
                query = query.Where(filter);
            /// Eager Loading
            if (string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var includeProp in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> Get(int id)
        { 
            return await DbSet.FindAsync(id);
        }

        public  IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
            string IncludeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if(filter != null) 
                query = query.Where(filter);
            /// Eager Loading
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach(var includeProp in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (OrderBy != null)
                return  OrderBy(query).ToList();

            return  query.ToList();
        }

        public void Remove(int id)
        {
            var element = DbSet.Find(id);
            DbSet.Remove(element);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        
    }
}
