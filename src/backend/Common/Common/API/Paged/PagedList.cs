namespace Common.API.Paged;

public class PagedList<TEntity> where TEntity : class
{
    public PagedList(ICollection<TEntity> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    public ICollection<TEntity> Entities { get; set; }
    public int TotalCount { get; set; }
}