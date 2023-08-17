using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Common
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity> WhereIf<TEntity>(
            this IQueryable<TEntity> query,
            bool condition,
            Expression<Func<TEntity, bool>> predicate)
        {
            if (condition)
            {
                return query.Where(predicate);
            }
            return query;
        }

        public static IQueryable<TEntity> IncludeIf<TEntity, TProperty>(
            this IQueryable<TEntity> query,
            bool condition,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
        {
            if (condition)
            {
                return query.Include(navigationProperty);
            }
            return query;
        }
    }
}
