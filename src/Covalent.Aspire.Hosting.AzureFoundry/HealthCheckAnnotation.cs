using Covalent.Aspire.Hosting.HealthChecks;
using Covalent.Aspire.Hosting.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Aspire.Hosting;

/// <summary>
/// Annotation for registering health checks with Azure Foundry resources.
/// </summary>
public class HealthCheckAnnotation : IResourceAnnotation
{
    public void Configure(IServiceCollection services, IResource resource)
    {
        if (resource is AzureFoundryResource azureFoundryResource)
        {
            services.AddHealthChecks()
                .AddCheck<AzureFoundryHealthCheck>(
                    $"azure_foundry_{azureFoundryResource.Name}",
                    tags: new[] { "azure_foundry", "external" });
        }
    }
}