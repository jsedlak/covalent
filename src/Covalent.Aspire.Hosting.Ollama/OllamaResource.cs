namespace Covalent.Aspire.Hosting.Resources;

/// <summary>
/// Represents an Ollama resource in an Aspire distributed application.
/// </summary>
/// <param name="name">The name of the Ollama resource.</param>
public sealed class OllamaResource : ContainerResource,
    IResourceWithConnectionString, IResourceWithEndpoints
{
    private readonly EndpointReference _httpEndpoint;

    public OllamaResource(string name) 
        : base(name)
    {
        // The endpoint will be attached in the extension
        _httpEndpoint = new EndpointReference(this, "http");
    }

    //// Env var that Aspire will populate for consumers
    //public string? ConnectionStringEnvironmentVariable { get; init; } = "OLLAMA_ENDPOINT";

    // Expression that other resources can reference at manifest time
    public ReferenceExpression ConnectionStringExpression =>
         ReferenceExpression.Create(
        $"Endpoint={_httpEndpoint.Property(EndpointProperty.Scheme)}://{_httpEndpoint.Property(EndpointProperty.Host)}:{_httpEndpoint.Property(EndpointProperty.Port)}"
      );


}