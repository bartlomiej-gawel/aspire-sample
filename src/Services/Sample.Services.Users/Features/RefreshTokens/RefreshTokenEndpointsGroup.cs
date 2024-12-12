using FastEndpoints;

namespace Sample.Services.Users.Features.RefreshTokens;

public sealed class RefreshTokenEndpointsGroup : Group
{
    public RefreshTokenEndpointsGroup()
    {
        Configure("api/users-service/refresh-tokens", endpointDefinition =>
        {
            endpointDefinition.Description(builder => builder
                .WithTags("refresh-tokens"));
        });
    }
}