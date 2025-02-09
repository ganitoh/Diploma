namespace Common.Domain;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; set; }
}