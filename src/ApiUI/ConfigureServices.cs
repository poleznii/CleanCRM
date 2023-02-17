namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiUIServices(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
