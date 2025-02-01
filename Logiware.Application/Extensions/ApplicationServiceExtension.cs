using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Logiware.Application.Helpers;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using Logiware.Application.Services;
using Logiware.Application.Validators;
using LogiWare.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Logiware.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        /*
         * Dependency Injection
         */
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPersonnelService, PersonnelService>();
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IShipmentService, ShipmentService>();
        services.AddScoped<IShipmentItemService, ShipmentItemService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IOwnershipService, OwnershipService>();
        services.AddScoped<IShipmentReceiveService, ShipmentReceiveService>();



        var defaultConfig = new MapperConfiguration(cfg => cfg.AddProfile<DefaultProfile>());
        var defaultMapper = defaultConfig.CreateMapper();
        services.AddSingleton<IDefaultMapper>(sp => new DefaultMapper(defaultMapper));



        var shipmentConfig = new MapperConfiguration(cfg => cfg.AddProfile<ShipmentProfile>());
        var shipmentMapper = shipmentConfig.CreateMapper();
        services.AddSingleton<IShipmentMapper>(sp => new ShipmentMapper(shipmentMapper));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();




        return services;
    }
}
