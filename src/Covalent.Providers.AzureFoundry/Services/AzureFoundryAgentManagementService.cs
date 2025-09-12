namespace Covalent.Providers.Services;

internal class AzureFoundryAgentManagementService : IAgentManagementService
{
    public Task Deploy(string name, string description, string systemPrompt, string model)
    {
        return Task.CompletedTask;
    }

    public string Name { get; set; } = null!;
}
