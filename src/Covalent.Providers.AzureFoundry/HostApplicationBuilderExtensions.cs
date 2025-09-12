using Covalent.Providers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Covalent.Providers;

public static class HostApplicationBuilderExtensions
{
    public static void AddKeyedAzureFoundry(this IHostApplicationBuilder builder, string name)
    {
        builder.Services.AddKeyedScoped<IAgentManagementService, AzureFoundryAgentManagementService>(name, (serviceProvider, _) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection($"AzureFoundry:{name}");

            var uri = section.GetValue<string>("Uri");
            if (uri is null)
            {
                throw new InvalidOperationException($"Connection string '{name}' not found.");
            }

            var token = section.GetValue<string>("Token");
            if (token is null)
            {
                throw new InvalidOperationException($"Token for '{name}' not found.");
            }

            return new AzureFoundryAgentManagementService(name, uri, token);
        });
    }
}
