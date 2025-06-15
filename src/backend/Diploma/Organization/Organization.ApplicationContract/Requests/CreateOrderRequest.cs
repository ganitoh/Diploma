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
    public int BuyOrganizationId { get; set; }

    /// <summary>
    /// Мдентификатор товара
    /// </summary>
    public virtual int ProductId { get; set; }

    /// <summary>
    /// Колличетво товара
    /// </summary>
    public int Quantity { get; set; }
}