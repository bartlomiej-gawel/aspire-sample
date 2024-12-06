namespace Sample.Aspire.ServiceDefaults.OptionalExtensions.MassTransit;

public sealed class MassTransitRabbitOptions
{
    public static string SectionName => "Rabbit";
    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}