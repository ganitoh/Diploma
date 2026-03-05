using Analytics.Application.Common.Persistance.Repositories;
using Analytics.ApplicationContract.Requests;
using Analytics.Domain.Models;
using AutoMapper;
using Common.Application;
using Common.Application.Persistance;

namespace Analytics.Application.CQRS.Orders.Commands;

public record CreateOrderAnalyticsCommand(CreateOrderAnalyticsRequest Request) : ICommand<int>;

public class CreateOrderAnalyticsCommandHandler : ICommandHandler<CreateOrderAnalyticsCommand, int>
{
    private readonly IOrderAnalyticsRepository _orderAnalyticsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateOrderAnalyticsCommandHandler(IOrderAnalyticsRepository orderAnalyticsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orderAnalyticsRepository = orderAnalyticsRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var orderAnalytics = _mapper.Map<OrderAnalytics>(request.Request);
        _orderAnalyticsRepository.Create(orderAnalytics);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return orderAnalytics.Id;
    }
}