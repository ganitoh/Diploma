using AutoMapper;
using Common.Application;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для содания товара
/// </summary>
public record  CreateProductCommand(CreateProductRequest ProductData) : ICommand<int>;

/// <summary>
/// Хендлер команды для содания товара
/// </summary>
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper  _mapper;

    public CreateProductCommandHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductData);
        product.IsStock = product.AvailableCount > 0;
        product.Rating = new Rating();
        
        _context.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
}