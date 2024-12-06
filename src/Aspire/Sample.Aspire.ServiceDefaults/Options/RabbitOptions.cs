namespace Sample.Aspire.ServiceDefaults.Options;

public sealed class RabbitOptions
{
    public const string SectionName = "Rabbit";
    
    public string Host { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
    public string Password { get; init; }  = string.Empty;
}