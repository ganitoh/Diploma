using Analytics.Application.Common.Persistance.Repositories;
using Analytics.ApplicationContract.Requests;
using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;

namespace Analytics.Application.CQRS.OrderItems.Commands;

public record UpdateOrderItemAnalyticsCommand(UpdateOrderItemAnalyticsRequest Request) : ICommand<Unit>;

public class UpdateOrderItemAnalyticsCommandHandler : ICommandHandler<UpdateOrderItemAnalyticsCommand, Unit>
{
    private readonly IOrderItemAnalyticsRepository _orderItemAnalyticsRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderItemAnalyticsCommandHandler(IOrderItemAnalyticsRepository orderItemAnalyticsRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderItemAnalyticsRepository = orderItemAnalyticsRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateOrderItemAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var orderItemAnalytics = await _orderItemAnalyticsRepository.GetByIdAsync(request.Request.Id);
        
        if (orderItemAnalytics is null)
            throw new NotFoundException("Order Item Analytics not found");
        
        _mapper.Map(request.Request, orderItemAnalytics);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}
