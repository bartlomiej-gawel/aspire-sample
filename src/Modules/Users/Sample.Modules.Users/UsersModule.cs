using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Modules.Users.Features.ActivationTokens;
using Sample.Modules.Users.Infrastructure.Jwt;
using Sample.Shared.Infrastructure.Modules;

namespace Sample.Modules.Users;

public sealed class UsersModule : IModule
{
    public string ModuleName => "Users";

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<JwtProvider>();
        services.AddScoped<ActivationTokenLinkFactory>();
    }

    public void UseModule(WebApplication app)
    {
    }
}