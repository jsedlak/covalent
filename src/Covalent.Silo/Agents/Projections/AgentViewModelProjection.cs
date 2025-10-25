using Covalent.Agents.Events;
using Strata;

namespace Covalent.Silo.Agents.Projections;

internal class AgentViewModelProjection : IOutboxRecipient<BaseAgentEvent>
{
    public Task Handle(int version, BaseAgentEvent @event)
    {
        return Task.CompletedTask;
    }
}