using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Рейтинг
/// </summary>
public class Rating : Entity<int>
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
    public ICollection<RatingCommentary> Commentaries { get; set; }

    public void CalculateRatingValue()
    {
        Total = Commentaries.Count;
        Vale = Commentaries.Select(x => x.RatingValue).Sum() / Total;
    }
}