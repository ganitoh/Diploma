namespace Common.API.Paged;

public class SortPagedRequest : PagedRequest
{
    /// <summary>
    /// Название поля для сортировки
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public bool? OrderAsc { get; set; }
}