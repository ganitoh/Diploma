using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.API.Paged;

/// <summary>
/// Определяет запрос на получение страницы данных из источника.
/// </summary>
public class PagedRequest
{
    public PagedRequest() { }
    
    /// <summary>Номер запрашиваемой страницы.</summary>
    [Required]
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;

    /// <summary>Количество элементов на странице.</summary>
    [Required]
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}