using Organization.Domain.Models;

namespace Organization.ApplicationContract.Dtos;

public class RatingDto
{
    /// <summary>
    /// Среднее значение
    /// </summary>
    public decimal Vale { get; set; }
    
    /// <summary>
    /// Всего оценок
    /// </summary>
    public int Total { get; set; }
    
    /// <summary>
    /// Коментарии и оценки каждого пользователя
    /// </summary>
    public ICollection<RatingCommentaryDto> Commentaries { get; set; }
}