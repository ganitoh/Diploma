using Analytics.Application.Common.Persistance.Repositories;
using Analytics.ApplicationContract.Requests;
using Analytics.Domain.Models;
using AutoMapper;
using Common.Application;
using Common.Application.Persistance;

namespace Analytics.Application.CQRS.OrderItems.Commands;

public record CreateOrderItemAnalyticsCommand(CreateOrderItemAnalyticsRequest Request) : ICommand<int>;

public class CreateOrderItemAnalyticsCommandHandler : ICommandHandler<CreateOrderItemAnalyticsCommand, int>
{
    private readonly IOrderItemAnalyticsRepository _orderItemAnalyticsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderItemAnalyticsCommandHandler(IOrderItemAnalyticsRepository orderItemAnalyticsRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orderItemAnalyticsRepository = orderItemAnalyticsRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderItemAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var orderItemAnalytics = _mapper.Map<OrderItemAnalytics>(request.Request);
        _orderItemAnalyticsRepository.Create(orderItemAnalytics);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return orderItemAnalytics.Id;
    }
}
