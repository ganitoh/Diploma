using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Рейтинг
/// </summary>
public class Rating : Entity<int>
{
    /// <summary>
    /// Общее значение
    /// </summary>
    public decimal Vale { get; set; }
    
    /// <summary>
    /// Всего оценок
    /// </summary>
    public int Total { get; set; }
    
    /// <summary>
    /// Коментарии о оценки каждого пользователя
    /// </summary>
    public ICollection<RatingCommentary> Commentaries { get; set; }
}