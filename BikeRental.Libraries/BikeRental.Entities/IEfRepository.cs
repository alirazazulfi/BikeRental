using BikeRental.Common;
using System.Linq.Expressions;

namespace BikeRental.Entities
{
    /// <summary>
    /// This is placed with database project becasue it is used in business layer and database Infrastructure layer. 
    /// if we keep it in Infrastructure it will create the circuler ref loop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEfRepository<T> where T : BaseEntity
    {
        #region ----- Aggrigate Data -----
        Task<long> GetMaxAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> max);
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        #endregion

        #region ----- Get Data -----        
        Task<T> GetByIdAsync(long id);
        Task<T> GetFirstAsync(QueryExpression<T> exp);
        Task<IEnumerable<T>> GetDataAsync(QueryExpression<T> exp);
        #endregion

        #region ----- Add Data -----
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entity);
        #endregion

        #region ----- Update Data -----
        Task UpdateAsync(T entity);
        Task UpdateAllAsync(IEnumerable<T> entity);
        #endregion

        #region ----- Remove Data -----
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
        #endregion
    }
}
