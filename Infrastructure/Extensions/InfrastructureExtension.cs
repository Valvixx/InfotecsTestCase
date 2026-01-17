using System.Reflection;
using FluentMigrator.Runner;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddDapper(this IServiceCollection services)
    {
        services.AddScoped<IDapperSettings, DapperSettings>();
        services.AddScoped<IDapperContext, DapperContext>();
        return services;
    }

    public static IServiceCollection AddFluentMigrator(this IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
        return services;
    }
    public static IServiceProvider UpdateDatabase(this IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMigrationRunner>();
        
        runner.MigrateUp();
        return serviceProvider;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
    
}