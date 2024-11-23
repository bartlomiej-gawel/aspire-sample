using Sample.ModularMonolith.Bootstrapper.Extensions;
using Sample.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

var assemblies = ModuleExtensions.LoadAssemblies();
var modules = ModuleExtensions.LoadModules(assemblies);

builder.Services.AddFastEndpointsConfiguration();
builder.Services.AddSwaggerDocumentation();

builder.AddServiceDefaults();

foreach (var module in modules)
    module.Register(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseFastEndpointsConfiguration();
app.UseSwaggerDocumentation();

app.MapDefaultEndpoints();

foreach (var module in modules)
    module.Use(app);

assemblies.Clear();
modules.Clear();

app.Run();