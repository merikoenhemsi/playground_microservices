using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Behaviors;
using Ordering.Core.Interfaces;
using Ordering.Core.Orders.Services;
using System.Reflection;

namespace Ordering.Core;

public static class ServiceCollectionSetup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<ICustomerUpdateService, CustomerUpdateService>();
        return services;
    }
}