using System.Linq;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            if (typeof(T).GetProperty(name) != null)
            {
                return query.OrderBy(e => EF.Property<object>(e, name));
            }

            return query;
        }

        public static IQueryable<T> OrderByDesc<T>(this IQueryable<T> query, string name)
        {
            if (typeof(T).GetProperty(name) != null)
            {
                return query.OrderByDescending(e => EF.Property<object>(e, name));
            }

            return query;
        }
        public static IQueryable<User> WithRoles(this IQueryable<User> query) =>
            query.Include(u => u.RolesEntities).ThenInclude(re => re.Role);
    }
}