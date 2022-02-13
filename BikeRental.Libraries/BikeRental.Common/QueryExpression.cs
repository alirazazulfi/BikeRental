using System.Linq.Expressions;

namespace BikeRental.Common
{
    public class QueryExpression<T>
    {
        public QueryExpression()
        {
            Includes = new List<Expression<Func<T, object>>>();
            ThenIncludes = new List<string>();
        }

        public Expression<Func<T, bool>> Predicate { get; set; }
        public bool AsNoTracking { get; set; } = false;
        public Expression<Func<T, object>>? OrderBy { get; set; } = null;
        public Expression<Func<T, object>>? ThenOrderBy { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDesc { get; set; } = null;
        public Expression<Func<T, object>>? ThenOrderByDesc { get; set; } = null;
        public List<string>? ThenIncludes { get; set; } = null;
        public List<Expression<Func<T, object>>>? Includes { get; set; } = null;
    }
}