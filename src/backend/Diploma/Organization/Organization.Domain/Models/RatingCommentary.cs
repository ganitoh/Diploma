using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Коментарий к рейнтигу
/// </summary>
public class RatingCommentary : Entity<int>
{
    /// <summary>
    /// Оцена пользователя
    /// </summary>
    public int RatingValue { get; set; }
    
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string? Commentary { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateDate { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}