using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Aspire.ServiceDefaults.Extensions;

namespace Sample.Aspire.ServiceDefaults;

public static class ServiceConfiguration
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
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