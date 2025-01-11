using Microsoft.AspNetCore.Routing;

namespace Sample.Shared.Infrastructure.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}