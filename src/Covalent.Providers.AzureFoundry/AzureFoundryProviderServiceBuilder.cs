using Covalent.Runtime;
using Microsoft.Extensions.Hosting;

[assembly: ServiceBuilder("Providers", "AzureFoundry", typeof(Covalent.Providers.AzureFoundryProviderServiceBuilder))]

namespace Covalent.Providers;

public class AzureFoundryProviderServiceBuilder : ServiceBuilder
{
    public override void Register(IHostApplicationBuilder builder, string name)
    {
        builder.AddKeyedAzureFoundry(name);
    }
}
