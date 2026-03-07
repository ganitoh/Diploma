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
    private readonly IOrderItemAnalyticsRepository _orderItemAnalyticsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper  _mapper;

    public CreateOrderAnalyticsCommandHandler(IOrderAnalyticsRepository orderAnalyticsRepository, IOrderItemAnalyticsRepository orderItemAnalyticsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orderAnalyticsRepository = orderAnalyticsRepository;
        _orderItemAnalyticsRepository = orderItemAnalyticsRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var orderAnalytics = _mapper.Map<OrderAnalytics>(request.Request);
        var orderItems = _mapper.Map<IEnumerable<OrderItemAnalytics>>(request.Request.Items);
        _orderAnalyticsRepository.Create(orderAnalytics);
        _orderItemAnalyticsRepository.AddRange(orderItems);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return orderAnalytics.Id;
    }
}