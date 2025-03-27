using BlazingAuto.Application.Services.VideoGames;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingAuto.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IVideoGameService, VideoGameService>();
        
        return services;
    }
}
