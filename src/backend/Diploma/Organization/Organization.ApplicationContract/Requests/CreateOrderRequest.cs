namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Запрос на создание заказа
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// Идентификатор продающей организации
    /// </summary>
    public int SellerOrganizationId { get; set; }


    /// <summary>
    /// Идентификатор покупающей организации
    /// </summary>
    public int BuyerOrganizationId { get; set; }

    /// <summary>
    /// Товары
    /// </summary>
    public virtual int[] ProductsIds { get; set; }
}