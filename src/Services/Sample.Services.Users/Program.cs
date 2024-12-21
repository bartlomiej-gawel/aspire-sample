using System.Reflection;
using FluentValidation;
using Sample.Aspire.ServiceDefaults;
using Sample.Aspire.ServiceDefaults.ExtensionsToRegister;
using Sample.Services.Users.Database;
using Sample.Services.Users.Features.ActivationTokens;
using Sample.Services.Users.Features.ActivationTokens.ActivateUser;
using Sample.Services.Users.Features.RefreshTokens.LoginWithRefreshToken;
using Sample.Services.Users.Features.RefreshTokens.RevokeRefreshTokens;
using Sample.Services.Users.Features.Users.LoginUser;
using Sample.Services.Users.Features.Users.RegisterUser;
using Sample.Services.Users.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<UsersServiceDbContext>("sample-users-service-db");

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<ActivationTokenLinkFactory>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMassTransitConfiguration<UsersServiceDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseServiceDefaults();

if (app.Environment.IsDevelopment()) 
    await app.RunDatabaseMigrationsAsync<UsersServiceDbContext>();

app.MapRegisterUserEndpoint();
app.MapLoginUserEndpoint();
app.MapRevokeRefreshTokensEndpoint();
app.MapLoginWithRefreshTokenEndpoint();
app.MapActivateUserEndpoint();

app.Run();