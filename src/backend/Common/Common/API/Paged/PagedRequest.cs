using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.API.Paged;

/// <summary>
/// Определяет запрос на получение страницы данных из источника.
/// </summary>
public class PagedRequest
{
    public PagedRequest() { }
    
    /// <summary>Номер запрашиваемой страницы.</summary>
    [BindRequired]
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;

    /// <summary>Количество элементов на странице.</summary>
    [BindRequired]
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}