using Common.API.Paged;
using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Requests;

public class GetOrderByUserRequest : PagedRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// Флаг указывающий какие заказы возвращать (на покупку/продажу)
    /// </summary>
    public bool IsSellOrders { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus? Status { get; set; }
}