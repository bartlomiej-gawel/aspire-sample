using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.ModularMonolith.Shared.Modules;

public interface IModule
{
    public string Name { get; }
    void Register(IServiceCollection services, IConfiguration configuration);
    void Use(WebApplication app);
}