using Covalent.Agents.Commands;
using Covalent.Agents.Events;
using Covalent.Agents.GrainModel;
using Covalent.Cqrs;
using Covalent.Silo.Agents.Model;
using Covalent.Silo.Agents.Projections;
using Strata;

namespace Covalent.Silo.Agents.Grains;

internal class AgentGrain : JournaledGrain<Agent, BaseAgentEvent>, IAgentGrain
{
    protected override void OnRegisterRecipients()
    {
        RegisterRecipient(
            nameof(AgentViewModelProjection),
            new AgentViewModelProjection()
        );
    }

    public async Task<CommandResponse> SetMeta(SetAgentMetaCommand command)
    {
        await RaiseEvent(new AgentMetaSetEvent
        {
            Name = command.Name,
            Description = command.Description
        });

        return CommandResponse.Good();
    }

    public async Task<CommandResponse> SetProviderAndModel(SetAgentProviderAndModelCommand command)
    {
        await RaiseEvent(new AgentProviderAndModelSetEvent
        {
            Provider = command.Provider,
            Model = command.Model
        });

        return CommandResponse.Good();
    }

    public async Task<CommandResponse> SetSystemPrompt(SetAgentSystemPromptCommand command)
    {
        await RaiseEvent(new AgentSystemPromptSetEvent
        {
            SystemPrompt = command.SystemPrompt
        });

        return CommandResponse.Good();
    }
}