var builder = DistributedApplication.CreateBuilder(args);

// Postgres database
var postgresUserNane = builder.AddParameter("postgres-username", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var postgres = builder
    .AddPostgres("sample-postgres", postgresUserNane, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

// Module databases
var notificationsModuleDb = postgres.AddDatabase("sample-notifications-module-db");
var organizationsModuleDb = postgres.AddDatabase("sample-organizations-module-db");
var subscriptionsModuleDb = postgres.AddDatabase("sample-subscriptions-module-db");
var usersModuleDb = postgres.AddDatabase("sample-users-module-db");

// Bootstrapper project
builder.AddProject<Projects.Sample_ModularMonolith_Bootstrapper>("sample-bootstrapper")
    .WaitFor(postgres);

builder.Build().Run();