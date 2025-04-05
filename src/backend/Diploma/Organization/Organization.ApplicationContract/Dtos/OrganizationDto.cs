namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Организация
/// </summary>
public class OrganizationDto
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ИНН
    /// </summary>
    public string Inn { get; set; } = string.Empty;

    /// <summary>
    /// Юридический адрес
    /// </summary>
    public required string LegalAddress { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Продукты
    /// </summary>
    public virtual ICollection<ProductDto> Products { get; set; }  

    /// <summary>
    /// Заказы на продажу
    /// </summary>
    public virtual ICollection<OrderDto> SellOrders { get; set; }
    
    /// <summary>
    /// Заказы на покупку
    /// </summary>
    public virtual ICollection<OrderDto> BuyOrders { get; set; }
}