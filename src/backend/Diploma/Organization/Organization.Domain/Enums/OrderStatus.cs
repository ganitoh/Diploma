using System.ComponentModel;

namespace Organization.Domain.Enums;

/// <summary>
/// Статусы заказа
/// </summary>
public enum OrderStatus
{
    [Description("Создан")]
    Created = 1,
    [Description("Формируется")]
    Collected,
    [Description("В доставке")]
    InDelivery,
    [Description("Закрыт")]
    Close
}