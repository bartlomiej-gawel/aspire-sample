using FastEndpoints;

namespace Sample.Services.Users.Extensions.FastEndpoints;

public static class FastEndpointsExtensions
{
    public static IServiceCollection AddFastEndpointsExtensions(this IServiceCollection services)
    {
        services.AddFastEndpoints();

        return services;
    }

    public static WebApplication UseFastEndpointsExtensions(this WebApplication app)
    {
        app.UseFastEndpoints();

        return app;
    }
}