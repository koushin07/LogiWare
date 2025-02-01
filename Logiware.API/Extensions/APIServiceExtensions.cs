using Logiware.API.Configuration;
using Logiware.Application.Interfaces;

namespace Logiware.API.Extensions;

public static class APIServiceExtensions
{
    public static IServiceCollection AddConfigurationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenSettings>(configuration);
        services.AddSingleton<ITokenSettings, TokenSettings>();
        return services;
    }
}