namespace Covalent.Providers.Services;

public interface IAgentManagementService
{
    Task Deploy(string name, string description, string systemPrompt, string model);

    string Name { get; }
}
