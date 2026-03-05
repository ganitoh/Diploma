using Analytics.Application.Common.Persistance.Repositories;
using Analytics.ApplicationContract.Requests;
using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;

namespace Analytics.Application.CQRS.Orders.Commands;

public record UpdateOrderAnalyticsCommand(UpdateOrderAnalyticsRequest Request) : ICommand<Unit>;

public class UpdateOrderAnalyticsCommandHandler : ICommandHandler<UpdateOrderAnalyticsCommand, Unit>
{
    private readonly IOrderAnalyticsRepository _orderAnalyticsRepository;
    private readonly IMapper  _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderAnalyticsCommandHandler(IOrderAnalyticsRepository orderAnalyticsRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderAnalyticsRepository = orderAnalyticsRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async  Task<Unit> Handle(UpdateOrderAnalyticsCommand request, CancellationToken cancellationToken)
    {
        var orderAnalytics = await _orderAnalyticsRepository.GetByIdAsync(request.Request.Id);
        
        if (orderAnalytics is null)
            throw new NotFoundException("Order Analytics not found");
        
        _mapper.Map(request, orderAnalytics);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}