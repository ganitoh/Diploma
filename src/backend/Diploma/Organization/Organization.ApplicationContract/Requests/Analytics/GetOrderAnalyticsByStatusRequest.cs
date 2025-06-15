using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Requests.Analytics;

/// <summary>
/// Данные запроса на получение аналитики по заказам
/// </summary>
public class GetOrderAnalyticsByStatusRequest : GetAnalyticsRequest
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus[] Statuses { get; set; }
}