using AutoMapper;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organizaiton.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для содания товара
/// </summary>
public record  CreateProductCommand(CreateProductRequest ProductData) : ICommand<int>;

/// <summary>
/// Хендлер команды для содания товара
/// </summary>
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IProductRepository  _repository;
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductData);
        _repository.Create(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        return product.Id;
    }
}