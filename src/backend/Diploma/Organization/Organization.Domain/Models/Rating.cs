using Common.Domain;
using Organization.Domain.ValueObjects;

namespace Organization.Domain.Models;

/// <summary>
/// Рейтинг
/// </summary>
public class Rating : Entity<int>
{
    public RatingValue Value { get; private set; }
    public int Total { get; private set; }

    private List<RatingCommentary> _commentaries = [];
    public IReadOnlyCollection<RatingCommentary> Commentaries => _commentaries;

    public Rating() { }

    public Rating(RatingValue value, int total)
    {
        Value = value;
        Total = total;
    }

    public void CalculateRatingValue()
    {
        Total = Commentaries.Count;
        Value = new RatingValue(Commentaries.Select(x => x.RatingValue).Sum() / Total);
    }

    public void AddCommentary(RatingCommentary commentary)
    {
        
        _commentaries.Add(commentary);
    }
}