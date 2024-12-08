using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", true);
var postgresPassword = builder.AddParameter("postgres-password", true);
var postgres = builder.AddPostgres("sample-postgres", postgresUsername, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmqUsername = builder.AddParameter("rabbitmq-username", true);
var rabbitmqPassword = builder.AddParameter("rabbitmq-password", true);
var rabbitmq = builder.AddRabbitMQ("sample-rabbitmq", rabbitmqUsername, rabbitmqPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin();

// var notificationsServiceDb = postgres.AddDatabase("sample-notifications-service-db");
// var notificationsService = builder.AddProject<Sample_Services_Notifications>("sample-notifications-service")
//     .WaitFor(notificationsServiceDb)
//     .WaitFor(rabbitmq)
//     .WithReference(notificationsServiceDb)
//     .WithEnvironment("Rabbit__Host", rabbitmq.Resource.ConnectionStringExpression);

var organizationsServiceDb = postgres.AddDatabase("sample-organizations-service-db");
var organizationsService = builder.AddProject<Sample_Services_Organizations>("sample-organizations-service")
    .WaitFor(organizationsServiceDb)
    .WaitFor(rabbitmq)
    .WithReference(organizationsServiceDb)
    .WithEnvironment("Rabbit__Host", rabbitmq.Resource.ConnectionStringExpression);

var usersServiceDb = postgres.AddDatabase("sample-users-service-db");
var usersService = builder.AddProject<Sample_Services_Users>("sample-users-service")
    .WaitFor(usersServiceDb)
    .WaitFor(rabbitmq)
    .WithReference(usersServiceDb)
    .WithEnvironment("Rabbit__Host", rabbitmq.Resource.ConnectionStringExpression);

// builder.AddProject<Sample_Gateway_Yarp>("sample-yarp-gateway")
//     .WaitFor(notificationsService)
//     .WaitFor(organizationsService)
//     .WaitFor(usersService)
//     .WithReference(notificationsService)
//     .WithReference(organizationsService)
//     .WithReference(usersService);

builder.Build().Run();