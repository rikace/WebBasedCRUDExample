namespace TechStudioTest.Utilities
{

    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExtensionMethods
    {
        public static IQueryable<TEntity> IncludeMulti<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class
            => (includes != null) ? includes.Aggregate(query, (current, include) => current.Include(include)) : query;

        private static TDbContext SetLazyLoad<TDbContext>(this TDbContext context, bool enableLazyLoad) where TDbContext : DbContext
        {
            context.Configuration.LazyLoadingEnabled = enableLazyLoad;
            return context;
        }

        public static TDbContext EnableLazyLoad<TDbContext>(this TDbContext context) where TDbContext : DbContext => SetLazyLoad(context, true);

        public static TDbContext DisableLazyLoad<TDbContext>(this TDbContext context) where TDbContext : DbContext => SetLazyLoad(context, false);
    }
}
