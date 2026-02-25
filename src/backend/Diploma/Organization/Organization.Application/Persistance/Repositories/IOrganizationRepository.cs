using Common.Application.Persistance;

namespace Organizaiton.Application.Persistance.Repositories;

public interface IOrganizationRepository : IRepository<Organization.Domain.Models.Organization> { }