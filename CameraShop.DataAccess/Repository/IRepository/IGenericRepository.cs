using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.DataAccess.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        // Another way to get Element but this time not just by Id, you can pass expression also, ...
        Task<T> FirstOrDefault(
           Expression<Func<T, bool>> filter = null,
           string IncludeProperties = null
           );
        /// Adding Expression parameter is to allow thid Method to apply some sort of condition, ordering, filtering, ...
        /// <summary>
        /// Adding Expression parameter is to allow thid Method to apply some sort of condition, ordering, filtering, ...
        /// IQueryable to hold the result of order query
        /// IncludeProperies are used for Eager Loading 
        /// </summary>
        /// <param name="filter">Expression</param>
        /// <param name="OrderBy">IQueryable</param>
        /// <param name="IncludeProperties">string</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
            string IncludeProperties = null
            );
        void Add(T entity);
        void Remove(T entity);

        void Remove(int id);
        void RemoveRange(IEnumerable<T> entities);
    }
}
