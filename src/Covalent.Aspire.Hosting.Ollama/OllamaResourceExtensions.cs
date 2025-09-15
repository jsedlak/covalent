using Covalent.Aspire.Hosting.Resources;

namespace Aspire.Hosting;

public static class OllamaResourceExtensions
{
    /// <summary>
    /// Adds a local Ollama resource to the distributed application.
    /// </summary>
    public static IResourceBuilder<OllamaResource> AddOllama(
        this IDistributedApplicationBuilder builder,
        string name = "ollama",
        int port = 11434)
    {
        var resource = new OllamaResource(name);

        return builder.AddResource(resource)
                      .WithHttpEndpoint(port: port, targetPort: 11434, name: "http")
          .WithHttpHealthCheck("/");
    }
}
