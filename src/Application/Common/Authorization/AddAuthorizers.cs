using CleanCRM.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanCRM.Application.Common.Authorization;

public static class AddAuthorizers
{
    /// <summary>
    /// https://medium.com/@austin.davies0101/creating-a-basic-authorization-pipeline-with-mediatr-and-asp-net-core-c257fe3cc76b
    /// </summary>
    public static void AddAuthorizersFromAssembly(
            this IServiceCollection services,
            Assembly assembly,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var authorizerType = typeof(IAuthorizer<>);
        assembly.GetTypesAssignableTo(authorizerType).ForEach((type) =>
        {
            foreach (var implementedInterface in type.ImplementedInterfaces)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(implementedInterface, type);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(implementedInterface, type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(implementedInterface, type);
                        break;
                }
            }
        });
    }

    public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
    {
        var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                            && !x.IsAbstract
                            && x != compareType
                            && x.GetInterfaces()
                                    .Any(i => i.IsGenericType
                                            && i.GetGenericTypeDefinition() == compareType))?.ToList();

        return typeInfoList;
    }
}
