using Organization.Domain.Models;

namespace Organization.Infrastructure.PDF.Abstractions;

/// <summary>
/// Генерация накладной для заказа
/// </summary>
public interface IGenerateInvoiceForOrder
{
    void Generate(Order order, Stream stream);
}