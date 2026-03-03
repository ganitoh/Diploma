using Common.API;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.PDF;
using Organizaiton.Application.Common.Persistance;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Команда для получения накладной для заказа 
/// </summary>
public record GetInvoiceForOrderQuery(int OrderId)  : IQuery<FileDto>;

/// <inheritdoc />
internal class GetInvoiceForOrderQueryHandler : IQueryHandler<GetInvoiceForOrderQuery, FileDto>
{
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IGenerateInvoiceForOrder _generator;

    public GetInvoiceForOrderQueryHandler(IReadOnlyOrganizationDbContext context, IGenerateInvoiceForOrder generator)
    {
        _context = context;
        _generator = generator;
    }

    public async Task<FileDto> Handle(GetInvoiceForOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
                        .Include(x => x.Items)
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