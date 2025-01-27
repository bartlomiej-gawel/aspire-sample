using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Shared.Infrastructure.Modules;

namespace Sample.Modules.Organizations;

public sealed class OrganizationsModule : IModule
{
    public string ModuleName => "Organizations";

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void UseModule(WebApplication app)
    {
    }
}