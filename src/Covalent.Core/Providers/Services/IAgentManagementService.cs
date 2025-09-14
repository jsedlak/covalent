using Covalent.Runtime;

namespace Covalent.Providers.Services;

public interface IAgentManagementService : INamedService
{
    Task Deploy(string name, string description, string systemPrompt, string model);
}
