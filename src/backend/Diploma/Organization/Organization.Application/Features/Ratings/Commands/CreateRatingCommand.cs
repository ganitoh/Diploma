using System.Security.Claims;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Domain.ValueObjects;

namespace Organizaiton.Application.Features.Ratings.Commands;

/// <summary>
/// Команда для создания отзыва
/// </summary>
public record CreateRatingCommand(CreateRatingRequest RequestData, ClaimsPrincipal User) : ICommand<int>;

/// <inheritdoc />
internal class CreateRatingCommandHandler : ICommandHandler<CreateRatingCommand, int>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRatingCommandHandler(IRatingRepository ratingRepository, IUnitOfWork unitOfWork)
    {
        _ratingRepository = ratingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var userId = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
            throw new ApplicationException("Идентификатор пользователя не нйден");

        var userName = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var rating = await _ratingRepository.GetByIdAsync(request.RequestData.ParentRatingId);
        
        if (rating is null)
            throw new NotFoundException("Рейтинг не найден");

        var ratingCommentary = new RatingCommentary(new RatingValue(request.RequestData.RatingValue),
            request.RequestData.Commentary, new Guid(userId), userName);
        
        rating.AddCommentary(ratingCommentary);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return ratingCommentary.Id;
    }
}