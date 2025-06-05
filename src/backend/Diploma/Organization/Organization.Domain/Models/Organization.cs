using System.Security.AccessControl;
using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Организация
/// </summary>
public class Organization : Entity<int>
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public required string Inn { get; set; }

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
    /// Флаг верефикации
    /// </summary>
    public bool IsApproval { get; set; }

    /// <summary>
    /// Продукты
    /// </summary>
    public virtual ICollection<Product> Products { get; set; }  

    /// <summary>
    /// Заказы на продажу
    /// </summary>
    public virtual ICollection<Order> SellOrders { get; set; }
    
    /// <summary>
    /// Заказы на покупку
    /// </summary>
    public virtual ICollection<Order> BuyOrders { get; set; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public ICollection<OrganizationUser> OrganizationUsers { get; set; }
}