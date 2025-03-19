using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.UnitOfWork;
using Organization.Application.Common.Persistance.Repositories;
using Organization.ApplicationContract.Reqeusts;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для создания товара
/// </summary>
public class CreateProductCommand : CreateProductRequest, ICommand<int> { }

/// <summary>
/// Хендлер команды для создания товара
/// </summary>
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(
        IOrganizationRepository organizationRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetById(request.OrganizationId, cancellationToken) 
                           ?? throw new NotFoundException("Организация не найдена");
        
        var product = _mapper.Map<CreateProductRequest, Product>(request);
        organization.Products.Add(product);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return product.Id;
    }
}