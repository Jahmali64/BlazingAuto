using BlazingAuto.Application;
using BlazingAuto.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

namespace BlazingAuto.Api;

public static class Program {
    public static void Main(string[] args) {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try {
            Log.Information("Starting BlazingAuto.Api");
            
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(CancellationToken), serviceProvider => {
                IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
            });

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        } catch (Exception ex) {
            Log.Fatal(ex, "Unhandled exception");
        } finally {
            Log.CloseAndFlush();
        }
    }
}
