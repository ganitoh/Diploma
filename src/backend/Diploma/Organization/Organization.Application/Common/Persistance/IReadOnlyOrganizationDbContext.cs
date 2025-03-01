namespace Organization.Application.Common.Persistance;

public interface IReadOnlyOrganizationDbContext
{
    IQueryable<Domain.Models.Organization> Organizations { get; }
}