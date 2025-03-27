using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazingAuto.UI.Client;

public static class Program {
    public static async Task Main(string[] args) {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        await builder.Build().RunAsync();
    }
}
