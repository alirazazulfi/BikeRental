using BikeRental.Common;
using BikeRental.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRental.Repository
{
    public class EfRepository<T> : IEfRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext Context;

        public EfRepository(ApplicationDbContext dbContext) => Context = dbContext;

        #region ----- Aggrigate Data -----

        public async Task<long> GetMaxAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> max) { try { return await Context.Set<T>().Where(predicate).AsNoTracking().MaxAsync(max); } catch { return 0; } }

        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().AsNoTracking().AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().AsNoTracking().CountAsync(predicate);

        #endregion

        #region ----- Get Data -----

        public async Task<T> GetByIdAsync(long id) => await Context.Set<T>().FindAsync(id);

        public async Task<T> GetFirstAsync(QueryExpression<T> exp)
        {
            var result = GetQueryData(exp);
            return exp.AsNoTracking ? await result.AsNoTracking().FirstOrDefaultAsync() : await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetDataAsync(QueryExpression<T> exp)
        {
            var result = GetQueryData(exp);
            return exp.AsNoTracking ? await result.AsNoTracking().ToListAsync() : await result.ToListAsync();
        }

        private IQueryable<T> GetQueryData(QueryExpression<T> exp)
        {
            var result = Context.Set<T>().Where(exp.Predicate);
            result = SetDataIncludes(result, exp);
            result = SetDataOrder(result, exp);
            return result;
        }

        private static IQueryable<T> SetDataIncludes(IQueryable<T> result, QueryExpression<T> exp)
        {
            if (exp.Includes != null) foreach (var includeExpression in exp.Includes) result = result.Include(includeExpression);
            if (exp.ThenIncludes != null) foreach (var includeExpression in exp.ThenIncludes) result = result.Include(includeExpression);
            return result;
        }

        private static IQueryable<T> SetDataOrder(IQueryable<T> result, QueryExpression<T> exp)
        {
            if (exp.OrderBy != null && exp.ThenOrderBy != null) result = result.OrderBy(exp.OrderBy).ThenBy(exp.ThenOrderBy);
            if (exp.OrderByDesc != null && exp.ThenOrderByDesc != null) result = result.OrderByDescending(exp.OrderByDesc).ThenByDescending(exp.ThenOrderByDesc);

            if (exp.OrderBy != null && exp.ThenOrderByDesc != null) result = result.OrderBy(exp.OrderBy).ThenByDescending(exp.ThenOrderByDesc);
            if (exp.OrderByDesc != null && exp.ThenOrderBy != null) result = result.OrderByDescending(exp.OrderByDesc).ThenBy(exp.ThenOrderBy);

            if (exp.OrderBy != null && exp.ThenOrderBy == null) result = result.OrderBy(exp.OrderBy);
            if (exp.OrderByDesc != null && exp.ThenOrderByDesc == null) result = result.OrderByDescending(exp.OrderByDesc);
            return result;
        }

        #endregion

        #region ----- Add Data -----

        public async Task<T> AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRange(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entity)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.ChangeTracker.LazyLoadingEnabled = false;
            Context.Set<T>().AddRange(entity);
            await Context.SaveChangesAsync();
            Context.ChangeTracker.AutoDetectChangesEnabled = true;
            Context.ChangeTracker.LazyLoadingEnabled = true;
            return entity;
        }

        #endregion

        #region ----- Update Data -----

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAllAsync(IEnumerable<T> entity)
        {
            Context.Set<T>().UpdateRange(entity);
            await Context.SaveChangesAsync();
        }

        #endregion

        #region ----- Remove Data -----

        public async Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            Context.Set<T>().RemoveRange(entity);
            await Context.SaveChangesAsync();
        }

        #endregion
    }
}
