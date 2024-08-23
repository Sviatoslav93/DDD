using System.Reflection;
using Mapster;
using MapsterMapper;

namespace ToDoList.Api.Mappings;

public static class ConfigureMapping
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
