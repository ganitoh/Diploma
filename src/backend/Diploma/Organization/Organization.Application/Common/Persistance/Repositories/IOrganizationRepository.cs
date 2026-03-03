using Common.Application.Persistance;

namespace Organizaiton.Application.Common.Persistance;

public interface IOrganizationRepository : IRepository<Organization.Domain.Models.Organization> { }