﻿using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Ratings.Commands;

/// <summary>
/// Команда для создания отзыва
/// </summary>
public record CreateRatingCommand(CreateRatingRequest RequestData) : ICommand<int>;

/// <inheritdoc />
internal class CreateRatingCommandHandler : ICommandHandler<CreateRatingCommand, int>
{
    private readonly OrganizationDbContext _context;

    public CreateRatingCommandHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var ratingCommentary = new RatingCommentary
        {
            Commentary = request.RequestData.Commentary,
            UserId = request.RequestData.UserId,
            RatingValue = request.RequestData.RatingValue,
            CreateDate = DateTime.UtcNow
        };

        if (request.RequestData.IsProduct)
        {
            var product = await _context.Products
                .Include(x => x.Rating)
                .ThenInclude(x => x.Commentaries)
                .FirstOrDefaultAsync(x => x.Id == request.RequestData.EntityId, cancellationToken) 
                          ?? throw new NotFoundException("Товар не найден");
            
            product.Rating!.Commentaries.Add(ratingCommentary);
            product.Rating!.CalculateRatingValue();
        }
        else
        {
            var organization = await _context.Organizations
                .Include(x => x.Rating)
                .ThenInclude(x => x.Commentaries)
                .FirstOrDefaultAsync(x => x.Id == request.RequestData.EntityId, cancellationToken)
                               ?? throw new NotFoundException("Организация не найден");
            
            organization.Rating!.Commentaries.Add(ratingCommentary);
            organization.Rating!.CalculateRatingValue();
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        return ratingCommentary.Id;
    }
}