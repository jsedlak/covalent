using Covalent.Agents.Commands;
using Covalent.Agents.Events;
using Covalent.Silo.Agents.GrainModel;
using Covalent.Silos.Agents.Model;
using Strata;

namespace Covalent.Silo.Agents.Grains;

internal class AgentGrain : EventSourcedGrain<Agent, BaseAgentEvent>, IAgentGrain
{
    public async Task SetMeta(SetAgentMetaCommand command)
    {
        await Raise(new AgentMetaSetEvent
        {
            Name = command.Name,
            Description = command.Description
        });
    }

    public async Task SetProviderAndModel(SetAgentProviderAndModelCommand command)
    {
        await Raise(new AgentProviderAndModelSetEvent
        {
            Provider = command.Provider,
            Model = command.Model
        });
    }

    public async Task SetSystemPrompt(SetAgentSystemPromptCommand command)
    {
        await Raise(new AgentSystemPromptSetEvent
        {
            SystemPrompt = command.SystemPrompt
        });
    }
}
