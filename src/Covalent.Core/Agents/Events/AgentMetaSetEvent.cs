using Covalent.Core.Cqrs;

namespace Covalent.Agents.Events;

[GenerateSerializer]
public sealed class AgentMetaSetEvent : BaseAgentEvent
{
    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Description { get; set; } = null!;
}
