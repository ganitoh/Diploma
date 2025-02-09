namespace Common.Domain;

public abstract class Entity<TEntityId> : IEntity<TEntityId>
{
    public TEntityId Id { get; set; }
}