using BlazingAuto.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingAuto.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContextFactory<BlazingAutoDbContext>(builder => builder.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection")));
        
        return services;
    }
}
