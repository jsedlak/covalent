using Covalent.Agents.Commands;
using Covalent.Cqrs;

namespace Covalent.Agents.GrainModel;

/// <summary>
/// Embodies the Agent aggregate as a runtime object and provides a mechanism for
/// configuring and interacting with the agent.
/// </summary>
public interface IAgentGrain : IGrainWithStringKey
{
    Task<CommandResponse> SetMeta(SetAgentMetaCommand command);

    Task<CommandResponse> SetProviderAndModel(SetAgentProviderAndModelCommand command);

    Task<CommandResponse> SetSystemPrompt(SetAgentSystemPromptCommand command);
}
