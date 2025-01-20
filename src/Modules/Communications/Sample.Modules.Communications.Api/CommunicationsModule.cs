using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Shared.Infrastructure.Modules;

namespace Sample.Modules.Communications.Api;

public sealed class CommunicationsModule : IModule
{
    public string ModuleName => "Communications";

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void UseModule(WebApplication app)
    {
    }
}