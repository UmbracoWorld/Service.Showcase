namespace Service.Showcase.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Application.Showcase;
using Databases.Showcases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDateTimeProvider;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddEntityFrameworkInMemoryDatabase();
        _ = services.AddDbContext<ShowcaseDbContext>();
            // options => options.UseInMemoryDatabase($"Movies-{Guid.NewGuid()}"), ServiceLifetime.Singleton);

        _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        _ = services.AddScoped<EntityFrameworkShowcaseRepository>();
        _ = services.AddScoped<IShowcaseRepository>(p => p.GetRequiredService<EntityFrameworkShowcaseRepository>());

        _ = services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        
        return services;
    }
}
