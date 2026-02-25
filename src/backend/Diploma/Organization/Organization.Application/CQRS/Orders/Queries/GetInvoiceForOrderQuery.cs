using Common.API;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.PDF;
using Organizaiton.Application.Persistance.Repositories;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Команда для получения накладной для заказа 
/// </summary>
public record GetInvoiceForOrderQuery(int OrderId)  : IQuery<FileDto>;

/// <inheritdoc />
internal class GetInvoiceForOrderQueryHandler : IQueryHandler<GetInvoiceForOrderQuery, FileDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IGenerateInvoiceForOrder _generator;

    public GetInvoiceForOrderQueryHandler(IOrderRepository orderRepository, IGenerateInvoiceForOrder generator)
    {
        _orderRepository = orderRepository;
        _generator = generator;
    }

    public async Task<FileDto> Handle(GetInvoiceForOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetQuery()
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .Include(x => x.SellerOrganization)
                        .Include(x => x.BuyerOrganization)
                        .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken) ??
                    throw new NotFoundException("Заказ не найден");

        using var stream = new MemoryStream();
        _generator.Generate(order, stream);
        stream.Position = 0;

        return new FileDto
        {
            Content = stream.ToArray(),
            FileName = $"Invoice_{request.OrderId}.pdf"
        };
    }
}