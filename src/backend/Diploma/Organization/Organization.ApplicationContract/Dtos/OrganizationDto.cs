namespace Organization.ApplicationContract.Dtos;

public class OrganizationDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Инн
    /// </summary>
    public string INN { get; set; } = string.Empty;

    /// <summary>
    /// Электронаая почта 
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Юредический адрес
    /// </summary>
    public string LegalAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Продукция для продажи
    /// </summary>
    public ICollection<ProductDto> Products { get; set; }
    
    /// <summary>
    /// Заказы на продажу
    /// </summary>
    public ICollection<OrderDto> SellOrders { get; set; }
    
    /// <summary>
    /// Заказы на покупку
    /// </summary>
    public ICollection<OrderDto> BuyOrders { get; set; }
}