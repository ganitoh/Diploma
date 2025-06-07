namespace Organization.Domain.Models;

/// <summary>
/// Dto - оценка с комментом
/// </summary>
public class RatingCommentaryDto
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
}