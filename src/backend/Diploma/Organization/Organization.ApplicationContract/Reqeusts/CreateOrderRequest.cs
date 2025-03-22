using Organization.ApplicationContract.Dtos;

namespace Organization.ApplicationContract.Reqeusts;

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
    public int[] ProductsIds { get; set; }
}