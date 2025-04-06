using Microsoft.EntityFrameworkCore;

namespace Common.API.Paged;

public static class PagedExtension
{
    public static IQueryable<TEntity> PagedQueryable<TEntity>(this IQueryable<TEntity> queryable, int page, int pageSize) =>
        queryable.Skip((page - 1) * pageSize).Take(pageSize);
    
    public static IEnumerable<TEntity> PagedEnumerable<TEntity>(this IEnumerable<TEntity> queryable, int page, int pageSize) =>
        queryable.Skip((page - 1) * pageSize).Take(pageSize);

    public static async Task<PagedList<TEntity>> GetPagetListAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        PagedRequest request,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var resultList = await queryable.PagedQueryable(request.PageNumber, request.PageSize).ToListAsync(cancellationToken);
        var totalCount =  await queryable.CountAsync(cancellationToken);
        return new PagedList<TEntity>(resultList, totalCount);
    }
}