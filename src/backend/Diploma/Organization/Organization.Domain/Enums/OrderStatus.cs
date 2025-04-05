using System.ComponentModel;

namespace Organization.Domain.Enums;

public enum OrderStatus
{
    [Description("Создан")]
    Created = 1,
    [Description("Собирается")]
    Collected,
    [Description("На доставке")]
    InDelivery,
    [Description("Закрыт")]
    Close
}