namespace Sample.ModularMonolith.Shared.Options;

public sealed class PostgresOptions
{
    public static string SectionName => "Postgres";
    public string ConnectionString { get; init; } = null!;
}