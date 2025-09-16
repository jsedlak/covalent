using Covalent.Agents.Commands;

namespace Covalent.Silo.Agents.GrainModel;

internal interface IAgentGrain : IGrainWithStringKey
{
    Task SetMeta(SetAgentMetaCommand command);

    Task SetProviderAndModel(SetAgentProviderAndModelCommand command);

    Task SetSystemPrompt(SetAgentSystemPromptCommand command);
}
