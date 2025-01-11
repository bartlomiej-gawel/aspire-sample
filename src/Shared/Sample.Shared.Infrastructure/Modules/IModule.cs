using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Shared.Infrastructure.Modules;

public interface IModule
{
    string ModuleName { get; }
    void RegisterModule(IServiceCollection services, IConfiguration configuration);
    void UseModule(WebApplication app);
}