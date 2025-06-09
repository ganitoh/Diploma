using System.ComponentModel.DataAnnotations;

namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Данные запроса на создание оценки рейтинга
/// </summary>
public class CreateRatingRequest
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
    /// Идентификато сущности
    /// </summary>
    public int EntityId { get; set; }

    /// <summary>
    /// Флаг - для чего пришла оценка (продукт/организация)
    /// </summary>
    public bool IsProduct { get; set; }
}