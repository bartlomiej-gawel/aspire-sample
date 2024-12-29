// using System.Reflection;
// using FluentValidation;
// using Sample.Aspire.ServiceDefaultsssss;
// using Sample.Aspire.ServiceDefaultsssss.ExtensionsToRegister;
// using Sample.Services.Organizations.Database;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.AddServiceDefaults();
// builder.AddNpgsqlDbContext<OrganizationsServiceDbContext>("sample-organizations-service-db");
//
// builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// builder.Services.AddMassTransitConfiguration<OrganizationsServiceDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());
//
// var app = builder.Build();
//
// app.UseServiceDefaults();
//
// if (app.Environment.IsDevelopment()) 
//     await app.RunDatabaseMigrationsAsync<OrganizationsServiceDbContext>();
//
// app.Run();