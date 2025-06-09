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
    public decimal RatingValue { get; set; }
    
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

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Идентификатор рейтинга
    /// </summary>
    public int RatingId { get; set; }
    
    /// <summary>
    /// Рейтинг
    /// </summary>
    public virtual Rating Rating { get; set; }
}