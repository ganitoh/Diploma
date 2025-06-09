namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Данные для запроса на обновление товара
/// </summary>
public class UpdateProductRequest : CreateProductRequest
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int Id { get; set; }
}