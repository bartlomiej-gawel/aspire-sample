var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", true);
var postgresPassword = builder.AddParameter("postgres-password", true);
var postgres = builder.AddPostgres("sample-postgres", postgresUsername, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

builder.Build().Run();