using Microsoft.Extensions.Hosting;

namespace Covalent;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddCovalent(this IHostApplicationBuilder builder)
    {
        var providerSection = builder.Configuration.GetSection("Covalent:Providers");

        // AzureFoundry
        foreach(var child in providerSection.GetChildren())
        {
            Console.WriteLine($"Found provider {child.Key}");

            // keyed service "namedAzureFoundry"
            foreach(var keyedService in child.GetChildren())
            {
                Console.WriteLine($"Found provider keyed service {keyedService.Key}");

                // TODO: Create a ProviderBuilder
                // dynamic providerBuilder;

                // TODO: Get config section "child.Key__keyedService" e.g. "AzureFoundry__namedAzureFoundry"
                // so that the provider builder can pull e.g. __Uri and __Token
                // providerBuilder.Register()
            }
        }

        return builder;
    }
}
