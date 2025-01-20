using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Aspire.ServiceDefaults.Extensions;
using Serilog;
using Serilog.Events;

namespace Sample.Aspire.ServiceDefaults;

public static class ServiceDefaultsConfiguration
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        builder.AddSerilogConfiguration();
        builder.AddOpenTelemetryConfiguration();
        builder.AddHealthChecksConfiguration();

        builder.Services.AddServiceDiscovery();
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        app.UseHealthChecksConfiguration();

        return app;
    }
}