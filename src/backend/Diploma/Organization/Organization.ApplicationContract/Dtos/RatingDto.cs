namespace Organization.ApplicationContract.Dtos;

public class RatingDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
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