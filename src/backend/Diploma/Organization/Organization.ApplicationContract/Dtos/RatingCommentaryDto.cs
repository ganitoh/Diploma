namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Dto - оценка с комментом
/// </summary>
public class RatingCommentaryDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
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
    public string CreateDate { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; } = string.Empty;
}