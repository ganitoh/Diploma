using Organization.Domain.Enums;

namespace Organization.ApplicationContract.MessageDtos;

/// <summary>
/// Модель для отпаврки в кафку о смене статуса заказа
/// </summary>
public class ChangeOrderStatusDto
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
    
    /// <summary>
    /// Дата и время
    /// </summary>
    public DateTime DateTime { get; set; }
}