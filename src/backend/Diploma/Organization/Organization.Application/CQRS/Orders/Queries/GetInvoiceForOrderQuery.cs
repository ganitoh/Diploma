using Common.API;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.Infrastructure.PDF.Abstractions;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Команда для получения накладной для заказа 
/// </summary>
public record GetInvoiceForOrderQuery(int OrderId)  : IQuery<FileDto>;

/// <inheritdoc />
internal class GetInvoiceForOrderQueryHandler : IQueryHandler<GetInvoiceForOrderQuery, FileDto>
{
    private readonly OrganizationDbContext _context;
    private readonly IGenerateInvoiceForOrder _generator;

    public GetInvoiceForOrderQueryHandler(OrganizationDbContext context, IGenerateInvoiceForOrder generator)
    {
        _context = context;
        _generator = generator;
    }

    public async Task<FileDto> Handle(GetInvoiceForOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
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