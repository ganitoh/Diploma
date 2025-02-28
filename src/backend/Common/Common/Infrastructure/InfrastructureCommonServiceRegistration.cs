﻿using Common.Infrastructure.UnitOfWork;
using Common.Infrastructure.UserProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class InfrastructureCommonServiceRegistration
{
    public static IServiceCollection AddInfrastructureCommonService<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        services.AddScoped<IUserProvider, UserProvider.UserProvider>();

        services.AddHttpContextAccessor();
        
        return services;
    }
}