using System.Reflection;
using Covalent.Runtime;
using Microsoft.Extensions.Hosting;

namespace Covalent;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddCovalent(this IHostApplicationBuilder builder)
    {
        BuildServicesFromConfiguration(builder, "Providers");

        return builder;
    }
    
    /// <summary>
    /// Builds services from the configuration section Covalent__{category}__{name}__{key}
    /// e.g. Covalent__Provider__AzureFoundry__namedAzureFoundry
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="category">The category of the services to build.</param>
    private static void BuildServicesFromConfiguration(IHostApplicationBuilder builder, string category) {

        var configurationSection = builder.Configuration.GetSection($"Covalent:{category}"); 

        // look at the base directory for the app and find all assemblies in the format *.{category}.dll and load them.
        var baseDirectory = AppContext.BaseDirectory;
        var assemblies = Directory.GetFiles(baseDirectory, $"*.{category}.*.dll")
            .Select(File.ReadAllBytes)
            .Select(Assembly.Load);

        foreach(var assembly in assemblies) 
        {
            Console.WriteLine($"Found plugin assembly {assembly.FullName}");
        }

        // AzureFoundry, OpenAI, etc.
        foreach(var child in configurationSection.GetChildren())
        {
            var name = child.Key;
            Console.WriteLine($"Found {category} service for {name}");

            // get all attributes in all assemblies that have the ServiceBuilderAttribute that match on category and name
            var serviceBuilderAttributes = assemblies
                .SelectMany(a => a.GetCustomAttributes<ServiceBuilderAttribute>())
                .FirstOrDefault(a => a.Category == category && a.Name == name && a.BuilderType.BaseType == typeof(ServiceBuilder));

            // Make sure we found a ServiceBuilderAttribute
            if (serviceBuilderAttributes == null)
            {
                throw new InvalidOperationException($"No ServiceBuilderAttribute found for {category} service {name}");
            }

            // create our service builder instance to help register each keyed service
            var serviceBuilder = Activator.CreateInstance(serviceBuilderAttributes.BuilderType) as ServiceBuilder;

            // keyed service "namedAzureFoundry"
            // for each keyed service, register the service
            foreach(var keyedService in child.GetChildren())
            {
                Console.WriteLine($"Registering {category} service {name} with key {keyedService.Key}");
                
                serviceBuilder?.Register(builder, name);
            }
        }
    }
}
