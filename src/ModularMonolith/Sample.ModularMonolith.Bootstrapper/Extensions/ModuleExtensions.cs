using System.Reflection;
using Sample.ModularMonolith.Shared.Modules;

namespace Sample.ModularMonolith.Bootstrapper.Extensions;

internal static class ModuleExtensions
{
    public static IList<Assembly> LoadAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .ToList();

        var locations = assemblies
            .Where(x => !x.IsDynamic)
            .Select(x => x.Location)
            .ToArray();

        var files = Directory
            .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

    public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
    {
        return assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
    }

    public static IHostBuilder LoadModuleSettings(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configurationBuilder) =>
        {
            foreach (var settings in GetModuleSettingsFile("*", context))
                configurationBuilder.AddJsonFile(settings);

            foreach (var settings in GetModuleSettingsFile($"*.{context.HostingEnvironment.EnvironmentName}", context))
                configurationBuilder.AddJsonFile(settings);
        });

        return builder;
    }

    private static IEnumerable<string> GetModuleSettingsFile(string pattern, HostBuilderContext context)
    {
        var settingsFiles = Directory.EnumerateFiles(
            context.HostingEnvironment.ContentRootPath,
            $"module.{pattern}.json",
            SearchOption.AllDirectories);

        return settingsFiles;
    }
}