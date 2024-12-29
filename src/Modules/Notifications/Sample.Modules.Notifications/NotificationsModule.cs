using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Shared.Infrastructure.Modules;

namespace Sample.Modules.Notifications;

public sealed class NotificationsModule : IModule
{
    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void UseModule(WebApplication app)
    {
    }
}