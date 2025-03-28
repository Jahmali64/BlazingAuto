using BlazingAuto.UI.Client.ApiServices;

namespace BlazingAuto.UI.Client;

public static class DependencyInjection {
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) {
        string baseUrl = configuration.GetValue<string>("BaseApiUrl") ?? string.Empty;
        Action<HttpClient> httpClient = client => client.BaseAddress = new Uri(baseUrl);
        
        services.AddHttpClient<VideoGameApiService>(httpClient);
        
        return services;
    }
}
