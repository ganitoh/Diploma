namespace Common.API.Paged;

public static class PagedExtension
{
    public static IQueryable<TEntity> PagedQueryable<TEntity>(this IQueryable<TEntity> queryable, int page, int pageSize) =>
        queryable.Skip((page - 1) * pageSize).Take(pageSize);
    
    public static IEnumerable<TEntity> PagedEnumerable<TEntity>(this IEnumerable<TEntity> queryable, int page, int pageSize) =>
        queryable.Skip((page - 1) * pageSize).Take(pageSize);
}