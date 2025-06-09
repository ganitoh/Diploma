using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для обновлня данных товара
/// </summary>
public record UpdateProductCommand(UpdateProductRequest Data) : ICommand<int>;

/// <inheritdoc/>
internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly OrganizationDbContext _context;

    public UpdateProductCommandHandler(IMapper mapper, OrganizationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var oldDataProduct =
            await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Data.Id, cancellationToken) ??
            throw new NotFoundException("Товар не найден");
        
        _mapper.Map(request.Data, oldDataProduct);
        await _context.SaveChangesAsync(cancellationToken);
        return  oldDataProduct.Id;
    }
}