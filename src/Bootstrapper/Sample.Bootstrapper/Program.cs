var builder = WebApplication.CreateBuilder(args);

// builder.Host.LoadModuleSettings();

// var assemblies = ModuleExtensions.LoadAssemblies(builder.Configuration);
// var modules = ModuleExtensions.LoadModules(assemblies);

// foreach (var module in modules)
//     module.Register(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // swagger
}

// foreach (var module in modules)
//     module.Use(app);

// assemblies.Clear();
// modules.Clear();

app.Run();