using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Данные для запроса на обновление статуса заказа
/// </summary>
/// <param name="OrderId">идентификатор заказа</param>
/// <param name="Status">Статус заказа</param>
public record ChangeOrderStatusRequest(int OrderId, OrderStatus Status);