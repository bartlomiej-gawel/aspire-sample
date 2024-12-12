using FastEndpoints;

namespace Sample.Services.Users.Features.ActivationTokens;

public sealed class ActivationTokenEndpointsGroup : Group
{
    public ActivationTokenEndpointsGroup()
    {
        Configure("api/users-service/activation-tokens", endpointDefinition =>
        {
            endpointDefinition.Description(builder => builder
                .WithTags("activation-tokens"));
        });
    }
}