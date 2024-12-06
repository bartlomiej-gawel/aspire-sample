using MassTransit;
using Sample.Shared.Messages.UsersService;

namespace Sample.Services.Organizations.Features.Organizations.AddOrganizationFromRegistration;

public sealed class AddOrganizationFromRegistrationConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<AddOrganizationFromRegistrationConsumer> _logger;

    public AddOrganizationFromRegistrationConsumer(ILogger<AddOrganizationFromRegistrationConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserRegistered> context)
    {
        _logger.LogWarning("Received Text: {Text}", context.Message);
        return Task.CompletedTask;
    }
}