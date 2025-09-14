namespace Covalent.Providers.Services;

public class AzureFoundryAgentManagementService : IAgentManagementService
{
    private readonly string _uri;
    private readonly string _token;

    public AzureFoundryAgentManagementService(string name, string uri, string token)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        _token = token ?? throw new ArgumentNullException(nameof(token));
    }

    public Task Deploy(string name, string description, string systemPrompt, string model)
    {
        return Task.CompletedTask;
    }

    public string Name { get; private set; }

    public string Category => "AzureFoundry";
}
