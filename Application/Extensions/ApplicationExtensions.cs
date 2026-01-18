using Application.Services;
using Application.Services.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        return services;
    }
}