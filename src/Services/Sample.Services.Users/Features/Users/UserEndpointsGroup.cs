using FastEndpoints;

namespace Sample.Services.Users.Features.Users;

public sealed class UserEndpointsGroup : Group
{
    public UserEndpointsGroup()
    {
        Configure("api/users-service/users", endpointDefinition =>
        {
            endpointDefinition.Description(builder => builder
                .WithTags("users"));
        });
    }
}