using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.ObjectStorage;

public static class ObjectStorageServiceRegistration
{
    public static IServiceCollection AddObjectStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioConfig>(configuration.GetSection(nameof(MinioConfig)));
        services.AddSingleton<IObjectStorage, MinioObjectStorage>();
        return services;
    }
}