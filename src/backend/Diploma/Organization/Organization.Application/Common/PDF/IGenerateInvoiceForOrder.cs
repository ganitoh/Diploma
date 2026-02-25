using Organization.Domain.Models;

namespace Organizaiton.Application.Common.PDF;

/// <summary>
/// Генерация накладной для заказа
/// </summary>
public interface IGenerateInvoiceForOrder
{
    void Generate(Order order, Stream stream);
}