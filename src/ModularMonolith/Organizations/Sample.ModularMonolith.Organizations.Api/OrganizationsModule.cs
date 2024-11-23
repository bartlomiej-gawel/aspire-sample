using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.ModularMonolith.Organizations.Api.Database;
using Sample.ModularMonolith.Shared.Modules;
using Sample.ModularMonolith.Shared.Options;

namespace Sample.ModularMonolith.Organizations.Api;

public sealed class OrganizationsModule : IModule
{
    public string Name => OrganizationsModuleConstants.ModuleName;
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var test = services.Configure<PostgresOptions>(configuration.GetSection("Organizations:Postgres"));
        
        // services.AddDbContext<OrganizationsModuleDbContext>(builder =>
        // {
        //     builder.UseNpgsql(configuration.GetConnectionString("Organizations__Postgres__ConnectionString"),
        //         configuration =>
        //         {
        //             
        //         });
        //     
        //     builder.UseNpgsql(options.ConnectionString);
        //     builder.EnableSensitiveDataLogging();
        // });
    }

    public void Use(WebApplication app)
    {
    }
}