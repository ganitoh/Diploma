namespace Common.Domain;

public abstract class AuditableEntity<TEntityId> : Entity<TEntityId>,IAuditableEntity
{
    public Guid CreatedBy { get; }
    public string? CreatedByName { get; }
    public DateTime CreatedOn { get; }
    public Guid LastModifiedBy { get; set; }
    public string? LastModifiedByName { get; }
    public DateTime LastModifiedOn { get; set; }
    
    protected AuditableEntity()
    {
        CreatedBy = default;
        CreatedByName = default;
        CreatedOn = DateTime.UtcNow;
        LastModifiedBy = default;
        LastModifiedByName = default;
        LastModifiedOn = DateTime.UtcNow;
    }
}