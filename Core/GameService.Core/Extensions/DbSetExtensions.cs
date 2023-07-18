using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameService.Core.Extensions
{
    public static class DbSetExtensions
    {
        public static DbSet<T> AddIncludes<T>(this DbSet<T> dbSet, IEnumerable<string> includes) where T : class
        {
            return includes != null
                ? includes.Select(t =>
                {
                    dbSet.Include(t);
                    return dbSet;
                }).First()
                : dbSet;
        }

        public static IQueryable<T> AddIncludes<T>(this IQueryable<T> query, IEnumerable<string> includes) where T : class
        {
            return includes == null ? query : includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}