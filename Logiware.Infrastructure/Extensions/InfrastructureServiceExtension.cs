using Logiware.Domain.Contracts;
using Logiware.Infrastructure.Data;
using Logiware.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logiware.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    { 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonnelRepository, PersonnelRepository>();
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<IShipmentItemRepository, ShipmentItemRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IOwnershipRepository, OwnershipRepository>();
        services.AddScoped<IShipmentReceiveRepository, ShipmentReceiveRepository>();
        //services.AddScoped<>();
        
       
        return services;
    }
}