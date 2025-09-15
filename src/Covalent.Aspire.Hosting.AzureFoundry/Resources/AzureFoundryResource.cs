using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Covalent.Aspire.Hosting.Resources;

/// <summary>
/// Represents an Azure Foundry resource in an Aspire distributed application.
/// </summary>
/// <param name="name">The name of the Azure Foundry resource.</param>
public class AzureFoundryResource(string name) :
    Resource(name)
{
    /// <summary>
    /// Gets or sets the URI for the Azure Foundry management endpoint.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Gets or sets the token for Azure Foundry authentication.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Gets or sets the URI parameter resource for the Azure Foundry management endpoint.
    /// </summary>
    public IResourceBuilder<ParameterResource>? UriParameter { get; set; }

    /// <summary>
    /// Gets or sets the token parameter resource for Azure Foundry authentication.
    /// </summary>
    public IResourceBuilder<ParameterResource>? TokenParameter { get; set; }

    /// <summary>
    /// Gets or sets the current health status of the Azure Foundry resource.
    /// </summary>
    public HealthStatus Status { get; set; } = HealthStatus.Unhealthy;

    /// <summary>
    /// Gets or sets the last status check timestamp.
    /// </summary>
    public DateTime LastStatusCheck { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets additional status information.
    /// </summary>
    public Dictionary<string, object> StatusData { get; set; } = new();

}