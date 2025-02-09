namespace Common.Domain;

public interface IAuditableEntity
{
    public Guid CreatedBy { get; }
    public string? CreatedByName { get; }
    public DateTime CreatedOn { get; }
    public Guid LastModifiedBy { get; set; }
    public string? LastModifiedByName { get; }
    public DateTime LastModifiedOn { get; set; }
}