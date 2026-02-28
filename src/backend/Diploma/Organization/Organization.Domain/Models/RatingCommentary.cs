using Common.Domain;
using Organization.Domain.ValueObjects;

namespace Organization.Domain.Models;

public class RatingCommentary : Entity<int>
{
    private const int MaxCommentaryLength = 250;
    
    public RatingValue RatingValue { get; protected set; }
    public string Commentary { get; private set; }
    public string? UserName { get; protected set; }
    public DateTime CreateAtDate { get; private set; }
    public Guid UserId { get; protected set; }
    public int RatingId { get; protected set; }
    public virtual Rating Rating { get; protected set; }

    protected RatingCommentary() { }

    public RatingCommentary(RatingValue ratingValue, string commentary, Guid userId,  DateTime createAtDate)
    {
        ValidateCommentary(commentary);
        
        CreateAtDate = createAtDate;
        RatingValue = ratingValue;
        Commentary = commentary;
        UserId = userId;
    }
    
    public void ChangeCommentary(string commentary)
    {
        ValidateCommentary(commentary);
        Commentary = commentary;
    }
    
    public void ChangeRating(RatingValue ratingValue) =>  RatingValue = ratingValue;

    #region Validate

    private static void ValidateCommentary(string commentary)
    {
        if (string.IsNullOrWhiteSpace(commentary))
            throw new DomainException("Commentary cannot be null or empty");

        if (commentary.Length > MaxCommentaryLength)
            throw new DomainException(
                $"Commentary cannot be longer than {MaxCommentaryLength}");
    }

    #endregion
}