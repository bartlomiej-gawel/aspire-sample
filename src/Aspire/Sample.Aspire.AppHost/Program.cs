using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", true);
var postgresPassword = builder.AddParameter("postgres-password", true);

var postgres = builder
    .AddPostgres("sample-postgres", postgresUsername, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

// var notificationsServiceDb = postgres.AddDatabase("sample-notifications-service-db");
// var notificationsService = builder.AddProject<Sample_Services_Notifications>("sample-notifications-service")
//     .WaitFor(notificationsServiceDb)
//     .WithReference(notificationsServiceDb);

var organizationsServiceDb = postgres.AddDatabase("sample-organizations-service-db");
var organizationsService = builder.AddProject<Sample_Services_Organizations>("sample-organizations-service")
    .WaitFor(organizationsServiceDb)
    .WithReference(organizationsServiceDb);

var usersServiceDb = postgres.AddDatabase("sample-users-service-db");
var usersService = builder.AddProject<Sample_Services_Users>("sample-users-service")
    .WaitFor(usersServiceDb)
    .WithReference(usersServiceDb);
//
// builder.AddProject<Sample_Gateway_Yarp>("sample-yarp-gateway")
//     .WaitFor(notificationsService)
//     .WaitFor(organizationsService)
//     .WaitFor(usersService)
//     .WithReference(notificationsService)
//     .WithReference(organizationsService)
//     .WithReference(usersService);

builder.Build().Run();